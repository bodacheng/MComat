using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeywordSetting", menuName = "ScriptableObjects/KeywordSetting", order = 3)]
public class KeywordSetting : ScriptableObject
{
    public string SkillStaticAnalysis = "SkillStaticAnalysis.csv";
    public string SkillDynamicAnalysis = "SkillDynamicAnalysis.csv";
    
    public static string _SkillStaticAnalysis = "SkillStaticAnalysis.csv";
    public static string _SkillDynamicAnalysis = "SkillDynamicAnalysis.csv";

    public void Initialise()
    {
        _SkillStaticAnalysis = SkillStaticAnalysis;
        _SkillDynamicAnalysis = SkillDynamicAnalysis;
    }
}
