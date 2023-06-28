using System.Collections.Generic;
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
    [Tooltip("关卡模式记录文件")]
    [SerializeField] string stageModeFile = "stage_mode";
    
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string skillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string skillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    [Tooltip("语言code文件")]
    [SerializeField] string languageCodeFile = "LanguageCode";

    [Tooltip("audio source key")]
    [SerializeField] string startThemeAddressKey = "music/start";
    [SerializeField] string lobbyThemeAddressKey = "music/lobby";
    [SerializeField] string fightThemeAddressKey1 = "music/fight1";
    [SerializeField] string fightThemeAddressKey2 = "music/fight2";

    [Tooltip("essential effects")] 
    [SerializeField] private string hitGroundEffectCode = "hitGround";
    [SerializeField] private string wallCrackEffectCode = "wallCrack";
    [SerializeField] private string breakFreeEffectCode = "breakFree";
    [SerializeField] private string memberShiftEffectCode = "memberShift";

    [Tooltip("sound effects")] 
    [SerializeField] AudioClip btnTapSound;

    [Tooltip("角色动画平滑区间")] 
    [SerializeField] private float characterAnimDuration = 0.25f;
    
    [Tooltip("downLoad labels")] 
    [SerializeField] List<string> downLoadLabels;

    public List<string> DownLoadLabels => downLoadLabels;
    
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
    public static string StageModeFile;

    public static string StartThemeAddressKey;
    public static string LobbyThemeAddressKey;
    public static string FightThemeAddressKey1;
    public static string FightThemeAddressKey2;
    
    public static string HitGroundEffectCode;
    public static string WallCrackEffectCode;
    public static string BreakFreeEffectCode;
    public static string MemberShiftEffectCode;
    
    public static float CharacterAnimDuration;

    public static AudioClip BtnTapSound;
    
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
        StageModeFile = stageModeFile;
        PassiveSKillFile = passiveSKillFile;

        LobbyThemeAddressKey = lobbyThemeAddressKey;
        StartThemeAddressKey = startThemeAddressKey;
        FightThemeAddressKey1 = fightThemeAddressKey1;
        FightThemeAddressKey2 = fightThemeAddressKey2;
        
        HitGroundEffectCode = hitGroundEffectCode;
        WallCrackEffectCode = wallCrackEffectCode;
        BreakFreeEffectCode = breakFreeEffectCode;
        MemberShiftEffectCode = memberShiftEffectCode;

        CharacterAnimDuration = characterAnimDuration;

        BtnTapSound = btnTapSound;
    }
}
