
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordSetting", menuName = "ScriptableObjects/KeywordSetting", order = 3)]
public class KeywordSetting : ScriptableObject
{
    [Tooltip("SkillStaticAnalysis后不加.csv")]
    public string SkillStaticAnalysis = "SkillStaticAnalysis";
    [Tooltip("SkillDynamicAnalysis后加.csv")]
    public string SkillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    public static string _SkillStaticAnalysis;
    public static string _SkillDynamicAnalysis;

    public void Initialise()
    {
        _SkillStaticAnalysis = SkillStaticAnalysis;
        _SkillDynamicAnalysis = SkillDynamicAnalysis;
    }
}
