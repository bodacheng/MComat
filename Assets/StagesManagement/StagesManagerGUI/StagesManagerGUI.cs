#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Skill;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
[CustomEditor(typeof(StagesManager))]
public partial class StagesManagerGUI : Editor {

    StagesManager _stagesManager;
    
    string pathAndNameForLocalSave = "Resources/stageTemp/oneFight.json";
    IDictionary<string, string> CharIDsAndNames;
    UnitInfo focusingCharInfo;
    string focusingtype;
    
    public override void OnInspectorGUI()
    {
        if (!Initialized)
        {
            UIparamIni();
            Initialized = true;
        }
        _stagesManager = (StagesManager)target;
        
        GUILayout.Space(10);
        LoadScript();       
        GUILayout.Space(10);
        Members();
        GUILayout.Space(10);
        
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingCharInfo == null)
        {
            if (GUILayout.Button("Add", AddDeleteMember))
            {
                focusingPosID = focusingPosID ?? "0";
                focusingCharInfo = new UnitInfo
                {
                    id = focusingPosID
                };
                _stagesManager.EditoringFight.EnemySets.Set(0, int.Parse(focusingPosID), focusingCharInfo);
            }
        }
        if (focusingCharInfo != null)
        {
            if (GUILayout.Button("Delete", AddDeleteMember))
            {
                _stagesManager.EditoringFight.EnemySets.Set(0, int.Parse(focusingPosID), null);
                focusingCharInfo = null;
                targetSlot = 0;
            }
        }
        GUILayout.EndHorizontal();
        
        if (focusingCharInfo == null)
            goto A;
        
        CharSelect();
            
        // 九宫格
        NineSlotPart();
        
        if (GUILayout.Button("推荐（暂时未适配原生技能自动适应）", ButtonStyle))
        {
            targetSlot = 0;
            if (string.IsNullOrEmpty(focusingtype))
                return;
            KeyValuePair<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(focusingCharInfo.r_id);
            focusingCharInfo.set = SkillSet.RandomSkillSet(focusingtype, INHERENTSkills.Key, 1, false);
        }
                
        // 技能组评价
        SkillSetComent();
        
        if (targetSlot == 0)
            goto A;
                
        // 技能选择
        if (GetFocusSkillId() == null)
        {
            SelectInher(focusingCharInfo.r_id);
            if (selectedInhereskill == 0)
            {
                SkillSelect();
            }                
        }
        else
        {
            if (GUILayout.Button("重选技能", ButtonStyle))
            {
                SetSkillId(null);
                NineSlotPart();// 为了刷新格子颜色
            }
        }
                        
        SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(GetFocusSkillId());
        if (defaultSkillConfig == null)
        {
            goto A;
        }
        
        // 技能详细信息
        SkillInfo(defaultSkillConfig);
        
        A:
        
        // 基础进程
        GUILayout.Space(10);
        BasicStates(focusingCharInfo);
        
        GUILayout.BeginHorizontal();
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        if (GUILayout.Button("保存战斗关卡至本地文档json", ButtonStyle_save))
        {
            for (int i = 0; i < _stagesManager.EditoringFight.EnemySets.GetValues().Count; i++)
            {
                if (_stagesManager.EditoringFight.EnemySets.GetValues()[i].r_id == "-1")
                {
                    Debug.Log("未安排有效角色ID");
                    return;
                }
                _stagesManager.EditoringFight.EnemySets.GetValues()[i].set.SortNineAndTwo();
            }
            _stagesManager.SaveFightAsJson(pathAndNameForLocalSave, _stagesManager.EditoringFight.EnemySets);
        }
        //if (GUILayout.Button("保存战斗关卡至本地文档xml",ButtonStyle_save))
        //{
        //    _stagesManager.SaveFightAsXml(pathAndNameForLocalSave,_stagesManager.EditoringFight);
        //}
        GUILayout.EndHorizontal();
    }
}
#endif