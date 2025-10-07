using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Manages AI service switching between different providers (Gemini, OpenAI, etc.)
/// </summary>
public class AIServiceManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private AIServiceConfig serviceConfig;
    
    public AIServiceConfig ServiceConfig 
    {
        get => serviceConfig; 
        set 
        { 
            serviceConfig = value;
            if (serviceConfig != null)
            {
                InitializeClients();
            }
        } 
    }
    
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
        // 示例：生成劳动场景的故事
        var context = new BattleContext {
            BattleId = "labor_scene_001",
            SceneType = "Labor",
            SceneDescription = "亚洲小伙子们在户外辛勤劳动的场景",
            SceneCount = 1,  // 生成4页连环画
            MinLinesPerScene = 2,  // 每页至少2行文字
            MaxLinesPerScene = 2   // 每页最多4行文字
        };
        var story = await GenerateAIStoryAsync(context);
        return story;
    }
    
    /// <summary>
    /// 使用AIStory系统生成StoryInfo对象
    /// 
    /// 此方法集成了AIStory系统的文本生成和图片生成功能，创建一个完整的StoryInfo对象。
    /// 生成的StoryInfo对象可以用于任何需要故事内容的场景，UI显示由调用方自行处理。
    /// 
    /// 使用示例（劳动场景）：
    /// <code>
    /// var context = new BattleContext {
    ///     BattleId = "labor_scene_001",
    ///     SceneType = "Labor",
    ///     SceneDescription = "工地劳动场景"
    /// };
    /// var storyInfo = await GenerateAIStoryAsync(context);
    /// // 之后可以自行处理storyInfo的显示逻辑
    /// </code>
    /// 
    /// 也支持战斗场景（向后兼容）：
    /// <code>
    /// var context = new BattleContext {
    ///     EventType = FightEventType.Arena,
    ///     IsPlayerWin = true,
    ///     BattleId = "battle_001",
    ///     SceneType = "Battle"
    /// };
    /// </code>
    /// </summary>
    /// <param name="battleContext">场景上下文信息，用于生成相关的故事内容（支持劳动、战斗等多种场景类型）</param>
    /// <param name="storyPrompt">可选的自定义故事提示词，如果提供则使用自定义提示词而非默认提示词</param>
    /// <returns>生成的StoryInfo对象，如果生成失败则返回null</returns>
    public async UniTask<StoryInfo> GenerateAIStoryAsync(BattleContext battleContext, string storyPrompt = null)
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
            string prompt = BuildStoryPrompt(battleContext, storyPrompt);
            
            // 使用AI生成故事文本
            string storyText = await aiService.AskAsync(prompt);
            
            if (string.IsNullOrEmpty(storyText))
            {
                Debug.LogError("Failed to generate story text from AI");
                return null;
            }
            
            // 输出AI返回的原始文本用于调试
            Debug.Log($"[AI Story] Raw response from AI:\n{storyText}");

            // 解析AI生成的故事文本，提取场景和对话
            var storyScenes = ParseStoryText(storyText);
            
            // 验证解析结果
            if (storyScenes == null || storyScenes.Count == 0)
            {
                Debug.LogError("[AI Story] Failed to parse story scenes from AI response");
                return null;
            }
            
            Debug.Log($"[AI Story] Successfully parsed {storyScenes.Count} scenes");
            
            // 验证场景数量是否符合要求
            int expectedCount = battleContext.SceneCount > 0 ? battleContext.SceneCount : 3;
            if (storyScenes.Count != expectedCount)
            {
                Debug.LogWarning($"[AI Story] Expected {expectedCount} scenes but got {storyScenes.Count} scenes. " +
                                $"The story may not match the requested page count.");
            }
            
            // 验证每个场景的文字行数
            int minLines = battleContext.MinLinesPerScene > 0 ? battleContext.MinLinesPerScene : 2;
            int maxLines = battleContext.MaxLinesPerScene > 0 ? battleContext.MaxLinesPerScene : 4;
            for (int i = 0; i < storyScenes.Count; i++)
            {
                int lineCount = storyScenes[i].Lines?.Count ?? 0;
                if (lineCount < minLines || lineCount > maxLines)
                {
                    Debug.LogWarning($"[AI Story] Scene {i + 1} has {lineCount} lines, " +
                                    $"expected between {minLines} and {maxLines} lines.");
                }
            }
            
            // 为每个场景生成对应的图片
            await GenerateStoryImagesAsync(storyScenes, battleContext);
            
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
    private string BuildStoryPrompt(BattleContext battleContext, string customPrompt)
    {
        if (!string.IsNullOrEmpty(customPrompt))
        {
            return customPrompt;
        }
        
        // 随机劳动场景类型
        string[] laborTypes = new string[]
        {
            "建筑工地搬运砖块",
            "农田里收割稻谷",
            "码头装卸货物",
            "山区修建道路",
            "工厂车间组装机械",
            "果园采摘水果",
            "仓库整理货物",
            "街道铺设石板",
            "菜地浇水除草",
            "搭建竹制脚手架"
        };
        
        // 随机选择劳动类型
        var random = new System.Random();
        string selectedLabor = laborTypes[random.Next(laborTypes.Length)];
        
        // 获取场景数量设置
        int sceneCount = battleContext.SceneCount > 0 ? battleContext.SceneCount : 3;
        int minLines = battleContext.MinLinesPerScene > 0 ? battleContext.MinLinesPerScene : 2;
        int maxLines = battleContext.MaxLinesPerScene > 0 ? battleContext.MaxLinesPerScene : 4;
        
        var prompt = $"请生成一个关于亚洲小伙子光着膀子劳动的连环画故事。\n\n";
        
        prompt += $"【连环画要求】\n";
        prompt += $"- 总页数：必须正好生成 {sceneCount} 个场景（每个场景对应一页/一张图）\n";
        prompt += $"- 每页文字：每个场景包含 {minLines}-{maxLines} 行文字（对话、独白或叙述）\n";
        prompt += $"- 故事连贯性：{sceneCount}个场景必须构成一个完整、连续的故事，有起承转合\n";
        prompt += $"- 场景编号：按照故事发展顺序，从场景1到场景{sceneCount}\n\n";
        
        prompt += $"【故事设定】\n";
        prompt += $"- 劳动类型：{selectedLabor}\n";
        prompt += $"- 主角：一位或多位身材健壮的亚洲年轻男性，光着上身\n";
        prompt += $"- 氛围：展现劳动的艰辛与美感，汗水挥洒，肌肉线条在阳光下闪耀\n";
        prompt += $"- 场景ID：{battleContext.BattleId}\n\n";
        
        prompt += $"【故事内容要求】\n";
        prompt += $"- 描写劳动过程中的动作细节和身体状态\n";
        prompt += $"- 体现劳动者的坚韧与努力\n";
        prompt += $"- 可以加入同伴间的对话或内心独白\n";
        prompt += $"- 展现健康向上的劳动美学\n";
        prompt += $"- 确保故事从开始到结束有完整的情节发展\n\n";
        
        prompt += $"【重要】请严格按照以下JSON格式返回，必须包含正好{sceneCount}个场景：\n\n";
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
        prompt += $"    // ... 继续到场景{sceneCount}\n";
        prompt += $"  ]\n";
        prompt += $"}}\n\n";
        
        prompt += $"注意：\n";
        prompt += $"1. 必须返回{sceneCount}个场景，不能多也不能少\n";
        prompt += $"2. 每个场景的lines数组包含{minLines}到{maxLines}个元素\n";
        prompt += $"3. 场景之间要有连贯的故事发展关系\n";
        prompt += $"4. description描述这一页的画面内容，lines是这一页配的文字\n";
        
        return prompt;
    }
    
    /// <summary>
    /// 解析AI生成的故事文本
    /// </summary>
    private List<StoryInfo.StoryScene> ParseStoryText(string storyText)
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
        
        Debug.Log($"[AI Story Parse] Final result: {scenes.Count} scenes");
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
    private async Task GenerateStoryImagesAsync(List<StoryInfo.StoryScene> scenes, BattleContext battleContext)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            try
            {
                var scene = scenes[i];
                var imagePrompt = BuildImagePrompt(scene, battleContext, i);
                
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
    /// 构建图片生成的提示词
    /// </summary>
    private string BuildImagePrompt(StoryInfo.StoryScene scene, BattleContext battleContext, int sceneIndex)
    {
        // 随机劳动场景元素
        string[] environments = new string[]
        {
            "烈日下的建筑工地",
            "金色阳光洒落的农田",
            "繁忙的码头",
            "崎岖山路旁的工程现场",
            "明亮的工厂车间",
            "翠绿的果园",
            "宽敞的仓库",
            "古朴的街道",
            "生机勃勃的菜园",
            "高处的施工平台"
        };
        
        string[] poses = new string[]
        {
            "弯腰搬运重物",
            "挥动工具劳作",
            "擦拭额头汗水",
            "用力抬举物品",
            "专注工作中",
            "休息喝水",
            "与同伴协作",
            "眺望远方片刻休息"
        };
        
        var random = new System.Random(sceneIndex + battleContext.BattleId.GetHashCode());
        string selectedEnv = environments[random.Next(environments.Length)];
        string selectedPose = poses[random.Next(poses.Length)];
        
        var prompt = $"A realistic photograph of young Asian men working shirtless, {selectedEnv}, {selectedPose}, ";
        prompt += "muscular and fit bodies glistening with sweat under natural lighting, ";
        prompt += "showing the beauty of physical labor, ";
        prompt += "detailed muscle definition, healthy tanned skin, ";
        prompt += "photorealistic style, high quality, natural colors, ";
        prompt += "cinematic composition, golden hour lighting, ";
        prompt += "capturing the dignity and strength of laborers, ";
        prompt += "professional photography, 8k resolution";
        
        return prompt;
    }
    
    /// <summary>
    /// 故事场景上下文信息类
    /// 注意：虽然类名为BattleContext，但现在已扩展为通用故事上下文，
    /// 不仅限于战斗场景，也可用于劳动、生活等各种场景的故事生成。
    /// 原有字段（EventType, IsPlayerWin等）保留以保持向后兼容性，但在非战斗场景下可忽略这些字段。
    /// </summary>
    [System.Serializable]
    public class BattleContext
    {
        // 原有战斗相关字段（保持向后兼容）
        public FightEventType EventType;
        public bool IsPlayerWin;
        public string BattleId;  // 在劳动场景下，可作为场景唯一ID使用
        public List<string> PlayerTeam;
        public List<string> EnemyTeam;
        public int PlayerScore;
        public int EnemyScore;
        
        // 扩展字段：通用场景类型（可选）
        public string SceneType;  // 例如："Labor", "Battle", "Daily" 等
        public string SceneDescription;  // 自定义场景描述
        
        // 连环画设置
        public int SceneCount;  // 总场景数（总页数/总图片数），默认为3
        public int MinLinesPerScene;  // 每个场景最少文字行数，默认为2
        public int MaxLinesPerScene;  // 每个场景最多文字行数，默认为4
        
        public BattleContext()
        {
            PlayerTeam = new List<string>();
            EnemyTeam = new List<string>();
            SceneType = "Labor";  // 默认为劳动场景
            SceneCount = 3;  // 默认3个场景
            MinLinesPerScene = 2;  // 每个场景至少2行文字
            MaxLinesPerScene = 4;  // 每个场景最多4行文字
        }
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
}
