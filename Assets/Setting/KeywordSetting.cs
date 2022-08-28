
using UnityEngine;

// 本模块定义的所有关键词只在运行模式下有效
[CreateAssetMenu(fileName = "KeywordSetting", menuName = "ScriptableObjects/KeywordSetting", order = 3)]
public class KeywordSetting : ScriptableObject
{
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    [SerializeField] string SkillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    [SerializeField] string SkillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    public static string _SkillStaticAnalysis;
    public static string _SkillDynamicAnalysis;
    
    public void Initialise()
    {
        _SkillStaticAnalysis = SkillStaticAnalysis;
        _SkillDynamicAnalysis = SkillDynamicAnalysis;
    }
}
