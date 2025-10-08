using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Manages AI service switching between different providers (Gemini, OpenAI, etc.)
/// </summary>
public class AIServiceManager : MonoBehaviour
{
    [SerializeField] private string configAddress = "Config/AIServiceConfig";
    private AIServiceConfig serviceConfig;
    private IAIClient currentClient;
    private GeminiClient geminiClient;
    private OpenAIClient openAIClient;
    
    // Events
    public event Action<AIModelType> OnModelChanged;
    public event Action<string> OnError;
    
    // Properties
    public IAIClient CurrentClient => currentClient;
    public AIModelType CurrentModel => serviceConfig?.CurrentModel ?? AIModelType.Gemini;
    public bool IsConfigured => currentClient?.IsConfigured ?? false;
    public string CurrentProviderName => currentClient?.ProviderName ?? "None";
    
    /// <summary>
    /// Initialize all AI clients
    /// </summary>
    public void InitializeClients()
    {
        if (serviceConfig == null)
        {
            Debug.LogError("AIServiceConfig is not assigned!");
            return;
        }
        
        // Initialize Gemini client
        if (serviceConfig.GeminiConfig != null)
        {
            geminiClient = new GeminiClient(serviceConfig.GeminiConfig);
            Debug.Log($"Gemini client initialized: {geminiClient.IsConfigured}");
        }
        
        // Initialize OpenAI client
        if (serviceConfig.OpenAIConfig != null)
        {
            openAIClient = new OpenAIClient(serviceConfig.OpenAIConfig);
            Debug.Log($"OpenAI client initialized: {openAIClient.IsConfigured}");
        }
        
        // Set initial client
        SwitchToModel(serviceConfig.CurrentModel);
    }
    
    /// <summary>
    /// Switch to a different AI model
    /// </summary>
    public bool SwitchToModel(AIModelType modelType)
    {
        if (serviceConfig == null)
        {
            OnError?.Invoke("Service configuration is not available");
            return false;
        }
        
        if (!serviceConfig.AllowModelSwitching)
        {
            OnError?.Invoke("Model switching is disabled");
            return false;
        }
        
        IAIClient newClient = modelType switch
        {
            AIModelType.Gemini => geminiClient,
            AIModelType.OpenAI => openAIClient,
            _ => null
        };
        
        if (newClient == null)
        {
            OnError?.Invoke($"Client for {modelType} is not available");
            return false;
        }
        
        if (!newClient.IsConfigured)
        {
            OnError?.Invoke($"{modelType} client is not properly configured");
            return false;
        }
        
        currentClient = newClient;
        serviceConfig.SetModel(modelType);
        
        Debug.Log($"Switched to {modelType} provider: {newClient.ProviderName}");
        OnModelChanged?.Invoke(modelType);
        
        return true;
    }
    
    /// <summary>
    /// Get available model types
    /// </summary>
    public AIModelType[] GetAvailableModels()
    {
        if (serviceConfig == null) return new AIModelType[0];
        return serviceConfig.GetAvailableModels();
    }
    
    /// <summary>
    /// Check if a specific model is available
    /// </summary>
    public bool IsModelAvailable(AIModelType modelType)
    {
        var available = GetAvailableModels();
        return System.Array.IndexOf(available, modelType) >= 0;
    }
    
    /// <summary>
    /// Get the current client for direct access (for backward compatibility)
    /// </summary>
    public T GetClient<T>() where T : class, IAIClient
    {
        return currentClient as T;
    }
    
    /// <summary>
    /// Get Gemini client specifically
    /// </summary>
    public GeminiClient GetGeminiClient()
    {
        return geminiClient;
    }
    
    /// <summary>
    /// Get OpenAI client specifically
    /// </summary>
    public OpenAIClient GetOpenAIClient()
    {
        return openAIClient;
    }
    
    /// <summary>
    /// Send a text prompt using the current AI model
    /// </summary>
    public System.Threading.Tasks.Task<string> AskAsync(string question, int? timeoutMs = null)
    {
        if (currentClient == null)
        {
            OnError?.Invoke("No AI client is currently active");
            return System.Threading.Tasks.Task.FromResult<string>(null);
        }
        
        return currentClient.AskAsync(question, timeoutMs);
    }
    
    /// <summary>
    /// Generate images using the current AI model
    /// </summary>
    public System.Threading.Tasks.Task<Texture2D[]> GeneratePic(string prompt, int? count = null, string aspectRatio = null)
    {
        if (currentClient == null)
        {
            OnError?.Invoke("No AI client is currently active");
            return System.Threading.Tasks.Task.FromResult<Texture2D[]>(null);
        }
        
        return currentClient.GeneratePic(prompt, count, aspectRatio);
    }
    
    public async UniTask<StoryInfo> LoadAIStory()
    {
        // 从Addressable加载AIServiceConfig
        Debug.Log("[AIServiceManager] Loading AIServiceConfig from Addressables...");
        
        var handle = Addressables.LoadAssetAsync<AIServiceConfig>(configAddress);
        await handle.Task;
        
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            serviceConfig = handle.Result;
            Debug.Log("[AIServiceManager] AIServiceConfig loaded successfully");
            
            // 初始化AI客户端
            if (serviceConfig != null)
            {
                InitializeClients();
                Debug.Log($"[AIServiceManager] Clients initialized, Current Model: {CurrentModel}, IsConfigured: {IsConfigured}");
            }
        }
        else
        {
            Debug.LogError($"[AIServiceManager] Failed to load AIServiceConfig from Addressables. Status: {handle.Status}");
            if (handle.IsValid())
                Addressables.Release(handle);
            return null;
        }
        
        var story = await GenerateAIStoryAsync(null);
        return story;
    }
    
    /// <summary>
    /// 使用AIStory系统生成StoryInfo对象
    /// 
    /// 此方法集成了AIStory系统的文本生成和图片生成功能，创建一个完整的StoryInfo对象。
    /// 生成的StoryInfo对象可以用于任何需要故事内容的场景，UI显示由调用方自行处理。
    /// 
    /// 使用示例：
    /// <code>
    /// // 使用默认配置生成
    /// var storyInfo = await aiService.GenerateAIStoryAsync(null);
    /// 
    /// // 使用自定义提示词
    /// var storyInfo = await aiService.GenerateAIStoryAsync("请生成一个关于...的故事");
    /// </code>
    /// </summary>
    /// <param name="storyPrompt">可选的自定义故事提示词，如果为null则使用配置中的主题自动生成</param>
    /// <returns>生成的StoryInfo对象，如果生成失败则返回null</returns>
    public async UniTask<StoryInfo> GenerateAIStoryAsync(string storyPrompt)
    {
        try
        {
            // 检查AI服务是否可用
            var aiService = FightScene.FightScene.target.AIServiceManager;
            if (aiService == null || !aiService)
            {
                Debug.LogWarning("AI Service is not available or not configured");
                return null;
            }

            // 构建故事生成提示词
            string prompt = BuildStoryPrompt(storyPrompt);
            
            // 使用AI生成故事文本
            string storyText = await aiService.AskAsync(prompt);
            
            if (string.IsNullOrEmpty(storyText))
            {
                Debug.LogError("Failed to generate story text from AI");
                return null;
            }
            
            // 输出AI返回的原始文本用于调试
            Debug.Log($"[AI Story] Raw response from AI:\n{storyText}");

            // 获取期望的页数
            int expectedPageCount = serviceConfig?.PageCount ?? 6;
            
            // 解析AI生成的故事文本，提取场景和对话，并确保返回正确数量
            var storyScenes = ParseStoryText(storyText, expectedPageCount);
            
            // 验证解析结果
            if (storyScenes == null || storyScenes.Count == 0)
            {
                Debug.LogError("[AI Story] Failed to parse story scenes from AI response");
                return null;
            }
            
            Debug.Log($"[AI Story] Successfully parsed {storyScenes.Count} scenes (expected: {expectedPageCount})");
            
            // 为每个场景生成对应的图片
            await GenerateStoryImagesAsync(storyScenes);
            
            // 创建StoryInfo对象
            var storyInfo = ScriptableObject.CreateInstance<StoryInfo>();
            storyInfo.StoryScenes = storyScenes;
            
            Debug.Log($"Successfully generated AI story with {storyScenes.Count} scenes");
            return storyInfo;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error generating AI story: {ex.Message}");
            return null;
        }
    }
        
    /// <summary>
    /// 构建故事生成的提示词
    /// </summary>
    private string BuildStoryPrompt(string customPrompt)
    {
        // 如果提供了自定义提示词，直接使用
        if (!string.IsNullOrEmpty(customPrompt))
        {
            return customPrompt;
        }
        
        // 从配置获取参数
        if (serviceConfig == null)
        {
            Debug.LogError("[BuildStoryPrompt] serviceConfig is null");
            return "";
        }
        
        int pageCount = serviceConfig.PageCount;
        
        // 根据配置生成故事主题
        string storyTheme = GetStoryThemeFromConfig();
        
        var prompt = $"请生成一个{storyTheme}连环画故事。\n\n";
        
        prompt += $"【连环画要求】\n";
        prompt += $"- 总页数：必须正好生成 {pageCount} 个场景（每个场景对应一页/一张图）\n";
        prompt += $"- 每页文字：每个场景包含 1-3 行文字（对话、独白或叙述）\n";
        prompt += $"- 故事连贯性：{pageCount}个场景必须构成一个完整、连续的故事，有起承转合\n";
        prompt += $"- 场景编号：按照故事发展顺序，从场景1到场景{pageCount}\n\n";
        
        prompt += $"【重要】请严格按照以下JSON格式返回，必须包含正好{pageCount}个场景：\n\n";
        prompt += $"{{\n";
        prompt += $"  \"scenes\": [\n";
        prompt += $"    {{\n";
        prompt += $"      \"description\": \"场景1的描述，说明这一页画面的内容\",\n";
        prompt += $"      \"lines\": [\n";
        prompt += $"        \"这一页的第1行文字（对话、独白或叙述）\",\n";
        prompt += $"        \"这一页的第2行文字\",\n";
        prompt += $"        \"这一页的第3行文字（如果需要）\"\n";
        prompt += $"      ]\n";
        prompt += $"    }},\n";
        prompt += $"    {{\n";
        prompt += $"      \"description\": \"场景2的描述\",\n";
        prompt += $"      \"lines\": [\"第2页的文字...\"]\n";
        prompt += $"    }}\n";
        prompt += $"    // ... 继续到场景{pageCount}\n";
        prompt += $"  ]\n";
        prompt += $"}}\n\n";
        
        prompt += $"注意：\n";
        prompt += $"1. 必须返回{pageCount}个场景，不能多也不能少\n";
        prompt += $"2. 每个场景的lines数组包含1到3个元素\n";
        prompt += $"3. 场景之间要有连贯的故事发展关系\n";
        prompt += $"4. description描述这一页的画面内容，lines是这一页配的文字\n";
        
        return prompt;
    }
    
    /// <summary>
    /// 解析AI生成的故事文本，并确保返回指定数量的场景
    /// </summary>
    /// <param name="storyText">AI返回的故事文本</param>
    /// <param name="expectedSceneCount">期望的场景数量（图片数量）</param>
    /// <returns>保证返回expectedSceneCount个场景的列表</returns>
    private List<StoryInfo.StoryScene> ParseStoryText(string storyText, int expectedSceneCount)
    {
        var scenes = new List<StoryInfo.StoryScene>();
        
        if (string.IsNullOrEmpty(storyText))
        {
            Debug.LogError("[AI Story Parse] Story text is null or empty");
            return scenes;
        }
        
        // 清理AI返回的文本，移除可能的markdown代码块标记
        string cleanedText = CleanJsonFromMarkdown(storyText);
        Debug.Log($"[AI Story Parse] Cleaned JSON:\n{cleanedText}");
        
        try
        {
            // 尝试解析JSON格式的故事内容
            var storyData = JsonUtility.FromJson<AIStoryData>(cleanedText);
            
            if (storyData?.scenes != null && storyData.scenes.Length > 0)
            {
                Debug.Log($"[AI Story Parse] JSON parsed successfully, found {storyData.scenes.Length} scenes");
                
                for (int i = 0; i < storyData.scenes.Length; i++)
                {
                    var sceneData = storyData.scenes[i];
                    
                    if (sceneData == null)
                    {
                        Debug.LogWarning($"[AI Story Parse] Scene {i} is null, skipping");
                        continue;
                    }
                    
                    var scene = new StoryInfo.StoryScene
                    {
                        Lines = new List<string>()
                    };
                    
                    // 添加场景描述（如果有）
                    if (!string.IsNullOrEmpty(sceneData.description))
                    {
                        scene.Lines.Add(sceneData.description);
                        Debug.Log($"[AI Story Parse] Scene {i} description: {sceneData.description}");
                    }
                    
                    // 添加对话行
                    if (sceneData.lines != null && sceneData.lines.Length > 0)
                    {
                        foreach (var line in sceneData.lines)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                scene.Lines.Add(line);
                                Debug.Log($"[AI Story Parse] Scene {i} line: {line}");
                            }
                        }
                    }
                    
                    if (scene.Lines.Count > 0)
                    {
                        scenes.Add(scene);
                        Debug.Log($"[AI Story Parse] Scene {i} added with {scene.Lines.Count} lines");
                    }
                    else
                    {
                        Debug.LogWarning($"[AI Story Parse] Scene {i} has no content, skipping");
                    }
                }
            }
            else
            {
                Debug.LogWarning("[AI Story Parse] JSON parsed but no scenes found");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AI Story Parse] JSON parsing failed: {ex.Message}\n{ex.StackTrace}");
            Debug.LogError($"[AI Story Parse] Attempting fallback parsing...");
            
            // 如果JSON解析失败，使用简单的文本分割作为后备方案
            scenes = FallbackParseStoryText(cleanedText);
        }
        
        Debug.Log($"[AI Story Parse] Parsed {scenes.Count} scenes, expected {expectedSceneCount}");
        
        // 确保场景数量等于expectedSceneCount
        if (scenes.Count > expectedSceneCount)
        {
            Debug.LogWarning($"[AI Story Parse] AI returned {scenes.Count} scenes, trimming to {expectedSceneCount}");
            scenes = scenes.GetRange(0, expectedSceneCount);
        }
        else if (scenes.Count < expectedSceneCount)
        {
            Debug.LogWarning($"[AI Story Parse] AI returned {scenes.Count} scenes, adding {expectedSceneCount - scenes.Count} placeholder scenes");
            while (scenes.Count < expectedSceneCount)
            {
                scenes.Add(new StoryInfo.StoryScene
                {
                    Lines = new List<string> { $"[场景 {scenes.Count + 1}]" }
                });
            }
        }
        
        Debug.Log($"[AI Story Parse] Final result: {scenes.Count} scenes (guaranteed = {expectedSceneCount})");
        return scenes;
    }
    
    /// <summary>
    /// 清理JSON文本，移除markdown代码块标记和其他干扰字符
    /// </summary>
    private string CleanJsonFromMarkdown(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
            
        // 移除markdown JSON代码块标记
        text = text.Trim();
        
        // 移除 ```json 和 ``` 标记
        if (text.StartsWith("```json"))
        {
            text = text.Substring(7);
        }
        else if (text.StartsWith("```"))
        {
            text = text.Substring(3);
        }
        
        if (text.EndsWith("```"))
        {
            text = text.Substring(0, text.Length - 3);
        }
        
        text = text.Trim();
        
        // 尝试找到实际的JSON内容（从第一个 { 到最后一个 }）
        int firstBrace = text.IndexOf('{');
        int lastBrace = text.LastIndexOf('}');
        
        if (firstBrace >= 0 && lastBrace > firstBrace)
        {
            text = text.Substring(firstBrace, lastBrace - firstBrace + 1);
        }
        
        return text;
    }
    
    /// <summary>
    /// 后备解析方法：当JSON解析失败时使用简单的文本分割
    /// </summary>
    private List<StoryInfo.StoryScene> FallbackParseStoryText(string storyText)
    {
        var scenes = new List<StoryInfo.StoryScene>();
        
        Debug.Log("[AI Story Parse] Using fallback text parsing");
        
        var lines = storyText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var currentScene = new StoryInfo.StoryScene
        {
            Lines = new List<string>()
        };
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // 跳过JSON标记和空行
            if (string.IsNullOrEmpty(trimmedLine) || 
                trimmedLine.StartsWith("{") || 
                trimmedLine.StartsWith("}") ||
                trimmedLine.StartsWith("[") ||
                trimmedLine.StartsWith("]") ||
                trimmedLine.Contains("\"scenes\"") ||
                trimmedLine.Contains("\"description\"") ||
                trimmedLine.Contains("\"lines\""))
            {
                continue;
            }
            
            // 移除引号和逗号
            trimmedLine = trimmedLine.Trim('"', ',', ' ');
            
            if (!string.IsNullOrEmpty(trimmedLine))
            {
                currentScene.Lines.Add(trimmedLine);
                Debug.Log($"[AI Story Parse Fallback] Added line: {trimmedLine}");
            }
        }
        
        if (currentScene.Lines.Count > 0)
        {
            scenes.Add(currentScene);
            Debug.Log($"[AI Story Parse Fallback] Created scene with {currentScene.Lines.Count} lines");
        }
        
        return scenes;
    }
    
    /// <summary>
    /// 为故事场景生成图片
    /// </summary>
    private async Task GenerateStoryImagesAsync(List<StoryInfo.StoryScene> scenes)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            try
            {
                var scene = scenes[i];
                var imagePrompt = BuildImagePrompt(scene, i, scenes.Count);
                
                // 使用AI生成图片
                var textures = await FightScene.FightScene.target.AIServiceManager.GeneratePic(imagePrompt, 1, "16:9");
                
                if (textures != null && textures.Length > 0 && textures[0] != null)
                {
                    // 将Texture2D转换为Sprite
                    var texture = textures[0];
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    scene.Pic = sprite;
                }
                else
                {
                    Debug.LogWarning($"Failed to generate image for scene {i}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error generating image for scene {i}: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 构建场景的描述内容，用于图片生成
    /// </summary>
    private string BuildStoryOverview(StoryInfo.StoryScene scene)
    {
        if (scene?.Lines == null || scene.Lines.Count == 0)
            return "";
        
        // 将场景的所有文字行合并为图片描述
        return string.Join(" ", scene.Lines);
    }
    
    /// <summary>
    /// 构建图片生成的提示词
    /// </summary>
    private string BuildImagePrompt(StoryInfo.StoryScene scene, int sceneIndex, int totalScenes)
    {
        // 从当前场景提取内容作为图片描述
        string sceneContent = BuildStoryOverview(scene);
        
        // 获取配置的图片风格
        string imageStyle = GetImageStyleFromConfig();
        
        // 基于当前场景的文字内容构建图片提示词
        var prompt = $"Create an image for this scene (page {sceneIndex + 1} of {totalScenes}):\n\n";
        prompt += $"\"{sceneContent}\"\n\n";
        prompt += $"Style: {imageStyle}\n";
        prompt += "Requirements: 8k resolution, highly detailed, professional quality, ";
        prompt += "accurately visualize the scene description above";
        
        Debug.Log($"[AIServiceManager] Image prompt for scene {sceneIndex + 1}/{totalScenes}:\n{prompt}");
        
        return prompt;
    }
    
    /// <summary>
    /// AI故事数据解析类
    /// </summary>
    [System.Serializable]
    private class AIStoryData
    {
        public AISceneData[] scenes;
    }
    
    [System.Serializable]
    private class AISceneData
    {
        public string description;
        public string[] lines;
    }
    
    /// <summary>
    /// 根据配置获取故事主题描述（从主题列表中随机选择）
    /// </summary>
    private string GetStoryThemeFromConfig()
    {
        if (serviceConfig == null || serviceConfig.StoryThemes == null || serviceConfig.StoryThemes.Length == 0)
        {
            return "关于亚洲小伙子光着膀子劳动";
        }
        
        // 从主题列表中随机选择一个
        var random = new System.Random();
        string selectedTheme = serviceConfig.StoryThemes[random.Next(serviceConfig.StoryThemes.Length)];
        
        Debug.Log($"[AIServiceManager] Selected story theme: {selectedTheme}");
        return selectedTheme;
    }
    
    /// <summary>
    /// 根据配置获取图片风格描述
    /// </summary>
    private string GetImageStyleFromConfig()
    {
        if (serviceConfig == null)
        {
            return "photorealistic style, high quality, natural colors";
        }
        
        string baseStyle = serviceConfig.ImageStyle switch
        {
            ImageStyle.Photorealistic => "photorealistic style, high quality, natural colors",
            ImageStyle.Anime => "anime style, cel-shaded, vibrant colors",
            ImageStyle.Watercolor => "watercolor painting style, soft brushstrokes, artistic",
            ImageStyle.OilPainting => "oil painting style, rich textures, classical art",
            ImageStyle.PencilSketch => "pencil sketch style, detailed linework, monochrome",
            ImageStyle.DigitalArt => "digital art style, clean lines, modern illustration",
            ImageStyle.Cinematic => "cinematic style, dramatic lighting, movie quality",
            _ => "photorealistic style, high quality, natural colors"
        };
        
        // 添加额外要求
        if (!string.IsNullOrWhiteSpace(serviceConfig.AdditionalImageRequirements))
        {
            baseStyle += $", {serviceConfig.AdditionalImageRequirements}";
            Debug.Log($"[AIServiceManager] Image style with additional requirements: {baseStyle}");
        }
        
        return baseStyle;
    }
}
