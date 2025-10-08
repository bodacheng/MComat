using UnityEngine;

public enum ImageStyle
{
    Photorealistic,     // 真实摄影风格
    Anime,              // 动漫风格
    Watercolor,         // 水彩画风格
    OilPainting,        // 油画风格
    PencilSketch,       // 铅笔素描风格
    DigitalArt,         // 数字艺术风格
    Cinematic           // 电影风格
}

[CreateAssetMenu(fileName = "AIServiceConfig", menuName = "StoryBook/API/AI Service Config")]
public class AIServiceConfig : ScriptableObject
{
    [Header("Model Selection")]
    [SerializeField] private AIModelType currentModel = AIModelType.Gemini;
    
    [Header("Model Configurations")]
    [SerializeField] private GeminiConfig geminiConfig;
    [SerializeField] private OpenAIConfig openAIConfig;
    
    [Header("Settings")]
    [SerializeField] private bool allowModelSwitching = true;
    [SerializeField] private bool showModelInUI = true;
    
    [Header("Story Generation Settings")]
    [Tooltip("故事主旨列表，每次生成时会随机选择一个。可以自由添加任意数量的主题描述")]
    [SerializeField] [TextArea(2, 5)] private string[] storyThemes = new string[]
    {
        "关于亚洲小伙子光着膀子劳动的"
    };
    
    [Tooltip("连环画总页数")]
    [SerializeField] private int pageCount = 6;
    
    [Tooltip("图片风格")]
    [SerializeField] private ImageStyle imageStyle = ImageStyle.Photorealistic;
    
    [Tooltip("图片生成额外要求，在风格基础上补充说明")]
    [SerializeField] [TextArea(2, 5)] private string additionalImageRequirements = "";
    
    // Public properties
    public AIModelType CurrentModel => currentModel;
    public GeminiConfig GeminiConfig => geminiConfig;
    public OpenAIConfig OpenAIConfig => openAIConfig;
    public bool AllowModelSwitching => allowModelSwitching;
    public string[] StoryThemes => storyThemes;
    public int PageCount => pageCount;
    public ImageStyle ImageStyle => imageStyle;
    public string AdditionalImageRequirements => additionalImageRequirements;
    
    /// <summary>
    /// Switch to a different AI model
    /// </summary>
    public void SetModel(AIModelType model)
    {
        if (!allowModelSwitching)
        {
            Debug.LogWarning("Model switching is disabled in configuration");
            return;
        }
        
        currentModel = model;
        Debug.Log($"AI Model switched to: {model}");
    }
    
    /// <summary>
    /// Get the current model configuration
    /// </summary>
    public ScriptableObject GetCurrentConfig()
    {
        return currentModel switch
        {
            AIModelType.Gemini => geminiConfig,
            AIModelType.OpenAI => openAIConfig,
            _ => geminiConfig
        };
    }
    
    /// <summary>
    /// Check if the current model is properly configured
    /// </summary>
    public bool IsCurrentModelConfigured()
    {
        return currentModel switch
        {
            AIModelType.Gemini => geminiConfig != null && geminiConfig.IsValid(),
            AIModelType.OpenAI => openAIConfig != null && openAIConfig.IsValid(),
            _ => false
        };
    }
    
    /// <summary>
    /// Get available model types
    /// </summary>
    public AIModelType[] GetAvailableModels()
    {
        var available = new System.Collections.Generic.List<AIModelType>();
        
        if (geminiConfig != null && geminiConfig.IsValid())
            available.Add(AIModelType.Gemini);
            
        if (openAIConfig != null && openAIConfig.IsValid())
            available.Add(AIModelType.OpenAI);
            
        return available.ToArray();
    }
    
    // Validate in editor
    private void OnValidate()
    {
        if (geminiConfig == null)
        {
            Debug.LogWarning($"[{name}] Gemini config is not assigned");
        }
        
        if (openAIConfig == null)
        {
            Debug.LogWarning($"[{name}] OpenAI config is not assigned");
        }
        
        if (!IsCurrentModelConfigured())
        {
            Debug.LogWarning($"[{name}] Current model ({currentModel}) is not properly configured");
        }
    }
}

