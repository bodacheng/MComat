#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Skill;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
public partial class StagesManager : EditorWindow
{
    public TextAsset FightScript;//存档文件。是我们拖给这个位置的一个东西，但如果说这个文件不存在，那应该要自动新建并指定到这个位置上
    FightMembers target;
    
    string pathAndNameForLocalSave = "Resources/stageTemp/oneFight.json";
    IDictionary<string, string> UnitIDsAndNames;
    UnitInfo focusingUnitInfo;
    string focusingType;
    
    void OnGUI()
    {
        if (!Initialized)
        {
            target = new FightMembers();
            UIparamIni();
            Initialized = true;
        }

        GUILayout.Space(10);
        LoadScript();       
        GUILayout.Space(10);
        Members();
        GUILayout.Space(10);
        
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingUnitInfo == null)
        {
            if (GUILayout.Button("Add", AddDeleteMember))
            {
                focusingPosID = focusingPosID ?? "0";
                focusingUnitInfo = new UnitInfo
                {
                    id = focusingPosID
                };
                target.EnemySets.Set(0, int.Parse(focusingPosID), focusingUnitInfo);
            }
        }
        if (focusingUnitInfo != null)
        {
            if (GUILayout.Button("Delete", AddDeleteMember))
            {
                target.EnemySets.Set(0, int.Parse(focusingPosID), null);
                focusingUnitInfo = null;
                targetSlot = 0;
            }
        }
        GUILayout.EndHorizontal();
        
        if (focusingUnitInfo == null)
            goto A;
        
        UnitSelect();
            
        // 九宫格
        NineSlotPart();
        
        if (GUILayout.Button("推荐（暂时未适配原生技能自动适应）", ButtonStyle))
        {
            targetSlot = 0;
            if (string.IsNullOrEmpty(focusingType))
                return;
            KeyValuePair<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(focusingUnitInfo.r_id);
            focusingUnitInfo.set = SkillSet.RandomSkillSet(focusingType, INHERENTSkills.Key, 1, false);
        }
                
        // 技能组评价
        SkillSetComment();
        
        if (targetSlot == 0)
            goto A;
                
        // 技能选择
        if (GetFocusSkillId() == null)
        {
            SelectInher(focusingUnitInfo.r_id);
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
                        
        SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfig(GetFocusSkillId());
        if (defaultSkillConfig == null)
        {
            goto A;
        }
        
        // 技能详细信息
        SkillInfo(defaultSkillConfig);
        
        A:
        
        // 基础进程
        GUILayout.Space(10);
        BasicStates(focusingUnitInfo);
        
        GUILayout.BeginHorizontal();
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        if (GUILayout.Button("保存战斗关卡至本地文档json", ButtonStyle_save))
        {
            for (int i = 0; i < target.EnemySets.GetValues().Count; i++)
            {
                if (target.EnemySets.GetValues()[i].r_id == "-1")
                {
                    Debug.Log("未安排有效角色ID");
                    return;
                }
                target.EnemySets.GetValues()[i].set.SortNineAndTwo();
            }
            target.SaveFightAsJson(pathAndNameForLocalSave, target.EnemySets);
        }
        //if (GUILayout.Button("保存战斗关卡至本地文档xml",ButtonStyle_save))
        //{
        //    _stagesManager.SaveFightAsXml(pathAndNameForLocalSave,_stagesManager.EditoringFight);
        //}
        GUILayout.EndHorizontal();
    }
}
#endif