using UnityEngine;

// 本模块定义的所有关键词只在运行模式下有效
[CreateAssetMenu(fileName = "CommonSetting", menuName = "ScriptableObjects/CommonSetting", order = 3)]
public class CommonSetting : ScriptableObject
{
    [SerializeField] bool devMode;
    [SerializeField] int maxStoneCount = 30;
    
    [Tooltip("unit定义文件")]
    [SerializeField] string unitConfigFile = "mst_unit";
    
    [Tooltip("skill定义文件")]
    [SerializeField] string skillConfigFile = "mst_skill";
    [Tooltip("skill ai文件")]
    [SerializeField] string skillAIFile = "skill_ai_attrs";
    [Tooltip("skill name文件")]
    [SerializeField] string skillNameFile = "skill_name";
    
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string skillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string skillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    [Tooltip("语言code文件")]
    [SerializeField] string languageCodeFile = "LanguageCode";
    
    public static bool DevMode;
    public static int MAXStoneCount;
    public static string UnitConfigFile;
    public static string SkillConfigFile;
    public static string SkillAIFile;
    public static string SkillNameFile;
    public static string SkillStaticAnalysis;
    public static string SkillDynamicAnalysis;
    public static string LanguageCodeFile;
    
    public void Initialise()
    {
        DevMode = devMode;
        MAXStoneCount = maxStoneCount;
        SkillStaticAnalysis = skillStaticAnalysis;
        SkillDynamicAnalysis = skillDynamicAnalysis;
        UnitConfigFile = unitConfigFile;
        SkillConfigFile = skillConfigFile;
        SkillAIFile = skillAIFile;
        SkillNameFile = skillNameFile;
        LanguageCodeFile = languageCodeFile;
    }
}
