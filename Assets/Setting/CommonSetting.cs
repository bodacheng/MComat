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
    [Tooltip("角色原生技能本地记录文件")]
    [SerializeField] string passiveSKillFile = "unit_passive";
    
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string skillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string skillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    [Tooltip("语言code文件")]
    [SerializeField] string languageCodeFile = "LanguageCode";

    [Tooltip("audio source key")] 
    [SerializeField] string lobbyThemeAddressKey = "music/lobby";
    [SerializeField] string fightThemeAddressKey1 = "music/fight1";
    [SerializeField] string fightThemeAddressKey2 = "music/fight2";
    [SerializeField] string fightThemeAddressKey3 = "music/fight3"; 
    
    public static bool DevMode;
    public static int MaxStoneCount;
    public static string UnitConfigFile;
    public static string SkillConfigFile;
    public static string SkillAIFile;
    public static string SkillNameFile;
    public static string SkillStaticAnalysis;
    public static string SkillDynamicAnalysis;
    public static string PassiveSKillFile;
    public static string LanguageCodeFile;

    public static string LobbyThemeAddressKey;
    public static string FightThemeAddressKey1;
    public static string FightThemeAddressKey2;
    public static string FightThemeAddressKey3;
    
    public void Initialise()
    {
        DevMode = devMode;
        MaxStoneCount = maxStoneCount;
        SkillStaticAnalysis = skillStaticAnalysis;
        SkillDynamicAnalysis = skillDynamicAnalysis;
        UnitConfigFile = unitConfigFile;
        SkillConfigFile = skillConfigFile;
        SkillAIFile = skillAIFile;
        SkillNameFile = skillNameFile;
        LanguageCodeFile = languageCodeFile;
        PassiveSKillFile = passiveSKillFile;

        LobbyThemeAddressKey = lobbyThemeAddressKey;
        FightThemeAddressKey1 = fightThemeAddressKey1;
        FightThemeAddressKey2 = fightThemeAddressKey2;
        FightThemeAddressKey3 = fightThemeAddressKey3;
    }
}
