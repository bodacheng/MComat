#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using Skill;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
public partial class FightMemberManager
{
    IDictionary<string, string> UnitIDsAndNames;
    UnitInfo focusingUnitInfo;
    string focusingType = "human";
    
    public void OnGUIView(FightMembers target)
    {
        if (!Initialized)
        {
            UIParamIni();
            Initialized = true;
        }
        
        GUILayout.Space(10);
        Members(target);
        GUILayout.Space(10);
        
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingUnitInfo == null)
        {
            if (GUILayout.Button("Add", AddDeleteMember))
            {
                focusingPosID ??= "0";
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
            var INHERENTSkills = SkillConfigTable.GetPassiveSkill(focusingUnitInfo.r_id);
            focusingUnitInfo.set = SkillSet.RandomSkillSet(focusingType,  INHERENTSkills?.RECORD_ID,  false);
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
    }
}
#endif