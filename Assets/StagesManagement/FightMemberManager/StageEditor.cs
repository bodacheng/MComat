#if UNITY_EDITOR
using System.Collections.Generic;
using mainMenu;
using UnityEditor;
using UnityEngine;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
public partial class StageEditor
{
    IDictionary<string, string> _unitIDsAndNames;
    UnitInfo _focusingUnitInfo;
    string _focusingType = "human";
    int stageLevel = 1;
    
    public void OnGUIView(FightMembers target)
    {
        if (!Initialized)
        {
            UIParamIni();
            // 认为所有敌人等级一致的简化处理
            foreach (var unitInfo in target.EnemySets.GetValues())
            {
                stageLevel = unitInfo.level;
            }
            Initialized = true;
        }
        
        GUILayout.Space(10);
        Members(target);
        GUILayout.Space(10);
        
        stageLevel =  EditorGUILayout.IntField("Stage Level:", stageLevel);
        target.SetEnemyLevel(stageLevel);
        GUILayout.Space(5);
        
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (_focusingUnitInfo == null)
        {
            if (GUILayout.Button("Add", AddDeleteMember))
            {
                _focusingPosID ??= "0";
                _focusingUnitInfo = new UnitInfo
                {
                    id = _focusingPosID
                };
                target.EnemySets.Set(0, int.Parse(_focusingPosID), _focusingUnitInfo);
            }
        }
        if (_focusingUnitInfo != null)
        {
            if (GUILayout.Button("Delete", AddDeleteMember))
            {
                target.EnemySets.Set(0, int.Parse(_focusingPosID), null);
                _focusingUnitInfo = null;
                _targetSlot = 0;
            }
        }
        GUILayout.EndHorizontal();
        
        if (_focusingUnitInfo == null)
            goto A;
        
        UnitSelect();
            
        // 九宫格
        NineSlotPart();
        
        GUILayout.BeginHorizontal();

        void Random(SkillStonesBox.StoneFilterForm form, bool noSpLimit = false)
        {
            _targetSlot = 0;
            if (string.IsNullOrEmpty(_focusingType))
                return;
            
            _focusingUnitInfo.set = SkillSet.RandomSkillSet(_focusingType,  null,  false, form, noSpLimit);
        }
        if (GUILayout.Button("一般", ButtonStyle))
        {
            var form = new SkillStonesBox.StoneFilterForm
            {
                Type = _focusingType,
                ExType = new[] { 0 }
            };
            Random(form);
        }
        if (GUILayout.Button("中boss", ButtonStyle))
        {
            var form = new SkillStonesBox.StoneFilterForm
            {
                Type = _focusingType,
                ExType = new[] { 0, 1, 2 }
            };
            Random(form);
        }
        if (GUILayout.Button("大boss", ButtonStyle))
        {
            var form = new SkillStonesBox.StoneFilterForm
            {
                Type = _focusingType,
                ExType = new[] { 2, 3 }
            };
            Random(form);
        }
        if (GUILayout.Button("超boss", ButtonStyle))
        {
            var form = new SkillStonesBox.StoneFilterForm
            {
                Type = _focusingType,
                ExType = new[] { 1, 2, 3 }
            };
            Random(form, true);
        }
        GUILayout.EndHorizontal();
                
        // 技能组评价
        SkillSetComment();
        
        if (_targetSlot == 0)
            goto A;
                
        // 技能选择
        if (GetFocusSkillId() == null)
        {
            SkillSelect();
        }
        else
        {
            if (GUILayout.Button("重选技能", ButtonStyle))
            {
                SetSkillId(null);
                NineSlotPart();// 为了刷新格子颜色
            }
        }
                        
        var defaultSkillConfig = SkillConfigTable.GetSkillConfig(GetFocusSkillId());
        if (defaultSkillConfig == null)
        {
            goto A;
        }
        
        // 技能详细信息
        SkillInfo(defaultSkillConfig);
        
        A:
        
        // 基础进程
        GUILayout.Space(10);
        BasicStates(_focusingUnitInfo);
    }
}
#endif