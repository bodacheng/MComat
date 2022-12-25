
using UnityEngine;

// 本模块定义的所有关键词只在运行模式下有效
[CreateAssetMenu(fileName = "KeywordSetting", menuName = "ScriptableObjects/KeywordSetting", order = 3)]
public class KeywordSetting : ScriptableObject
{
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string skillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string skillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    public static string SkillStaticAnalysis;
    public static string SkillDynamicAnalysis;
    
    public void Initialise()
    {
        SkillStaticAnalysis = skillStaticAnalysis;
        SkillDynamicAnalysis = skillDynamicAnalysis;
    }
}
