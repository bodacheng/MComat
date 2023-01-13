using UnityEngine;

// 本模块定义的所有关键词只在运行模式下有效
[CreateAssetMenu(fileName = "CommonSetting", menuName = "ScriptableObjects/CommonSetting", order = 3)]
public class CommonSetting : ScriptableObject
{
    [SerializeField] bool devMode;
    [SerializeField] int maxStoneCount = 30;
    
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string skillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string skillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    public static bool DevMode;
    public static int MAXStoneCount;
    public static string SkillStaticAnalysis;
    public static string SkillDynamicAnalysis;
    
    public void Initialise()
    {
        DevMode = devMode;
        MAXStoneCount = maxStoneCount;
        SkillStaticAnalysis = skillStaticAnalysis;
        SkillDynamicAnalysis = skillDynamicAnalysis;
    }
}
