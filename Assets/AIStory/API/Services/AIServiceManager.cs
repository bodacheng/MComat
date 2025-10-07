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
        var context = new BattleContext {
            EventType = FightEventType.Arena,
            IsPlayerWin = true,
            BattleId = "battle_001"
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
    /// 使用示例：
    /// <code>
    /// var context = new BattleContext {
    ///     EventType = FightEventType.Arena,
    ///     IsPlayerWin = true,
    ///     BattleId = "battle_001"
    /// };
    /// var storyInfo = await arenaFightOver.GenerateAIStoryAsync(context);
    /// // 之后可以自行处理storyInfo的显示逻辑
    /// </code>
    /// </summary>
    /// <param name="battleContext">战斗上下文信息，用于生成相关的故事内容</param>
    /// <param name="storyPrompt">可选的自定义故事提示词，如果提供则使用自定义提示词而非默认的战斗信息提示词</param>
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

            // 解析AI生成的故事文本，提取场景和对话
            var storyScenes = ParseStoryText(storyText);
            
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
        
        var prompt = $"请为以下战斗结果生成一个简短的故事剧情，包含2-3个场景，每个场景包含1-2句对话。\n\n";
        prompt += $"战斗信息：\n";
        prompt += $"- 战斗类型：{battleContext.EventType}\n";
        prompt += $"- 胜利者：{(battleContext.IsPlayerWin ? "玩家" : "敌人")}\n";
        prompt += $"- 战斗ID：{battleContext.BattleId}\n";
        
        if (battleContext.PlayerTeam != null && battleContext.PlayerTeam.Count > 0)
        {
            prompt += $"- 玩家队伍：{string.Join(", ", battleContext.PlayerTeam)}\n";
        }
        
        if (battleContext.EnemyTeam != null && battleContext.EnemyTeam.Count > 0)
        {
            prompt += $"- 敌人队伍：{string.Join(", ", battleContext.EnemyTeam)}\n";
        }
        
        prompt += $"\n请以JSON格式返回故事内容，格式如下：\n";
        prompt += $"{{\n";
        prompt += $"  \"scenes\": [\n";
        prompt += $"    {{\n";
        prompt += $"      \"description\": \"场景描述\",\n";
        prompt += $"      \"lines\": [\"对话1\", \"对话2\"]\n";
        prompt += $"    }}\n";
        prompt += $"  ]\n";
        prompt += $"}}\n";
        
        return prompt;
    }
    
    /// <summary>
    /// 解析AI生成的故事文本
    /// </summary>
    private List<StoryInfo.StoryScene> ParseStoryText(string storyText)
    {
        var scenes = new List<StoryInfo.StoryScene>();
        
        try
        {
            // 尝试解析JSON格式的故事内容
            var storyData = JsonUtility.FromJson<AIStoryData>(storyText);
            if (storyData?.scenes != null)
            {
                foreach (var sceneData in storyData.scenes)
                {
                    var scene = new StoryInfo.StoryScene
                    {
                        Lines = new List<string>(sceneData.lines ?? new string[0])
                    };
                    scenes.Add(scene);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Failed to parse JSON story format: {ex.Message}, using fallback parsing");
            
            // 如果JSON解析失败，使用简单的文本分割作为后备方案
            var lines = storyText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var currentScene = new StoryInfo.StoryScene
            {
                Lines = new List<string>()
            };
            
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    currentScene.Lines.Add(trimmedLine);
                }
            }
            
            if (currentScene.Lines.Count > 0)
            {
                scenes.Add(currentScene);
            }
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
        var prompt = "A dramatic fantasy battle scene, ";
        
        if (battleContext.IsPlayerWin)
        {
            prompt += "victorious heroes celebrating their triumph, ";
        }
        else
        {
            prompt += "defeated warriors in a somber moment, ";
        }
        
        prompt += "anime style, detailed character design, vibrant colors, epic atmosphere";
        
        return prompt;
    }
    
    /// <summary>
    /// 战斗上下文信息类
    /// </summary>
    [System.Serializable]
    public class BattleContext
    {
        public FightEventType EventType;
        public bool IsPlayerWin;
        public string BattleId;
        public List<string> PlayerTeam;
        public List<string> EnemyTeam;
        public int PlayerScore;
        public int EnemyScore;
        
        public BattleContext()
        {
            PlayerTeam = new List<string>();
            EnemyTeam = new List<string>();
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
