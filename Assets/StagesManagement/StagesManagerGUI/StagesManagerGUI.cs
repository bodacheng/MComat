#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;
using Skill;
using System;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
[CustomEditor(typeof(StagesManager))]
public partial class StagesManagerGUI : Editor {

    StagesManager _stagesManager;
    
    string pathAndNameForLocalSave = "Resources/stageTemp/oneFight.json";    
    string focusingMemberRecordID;
    IDictionary<string, string> CharIDsAndNames;
    CharDataInfo focusingCharInfo;
    CharConfig focusingCharConfig;
    SkillConfig targetSC;
    string focusingtype;
    
    // 参考等级。角色自身不存在等级，但为了设置方便所有技能等级可以一致
    int level = 1;
    public override void OnInspectorGUI()
    {
        if (!Initialized)
        {
            UIparamIni();
            Initialized = true;
        }
        _stagesManager = (StagesManager)target;

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        _stagesManager.FightScript = EditorGUILayout.ObjectField("战斗脚本读取", _stagesManager.FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (_stagesManager.FightScript != null)
            {
                LocalFight one = _stagesManager.LoadOneLocalFight_Json(_stagesManager.FightScript);
                if (one != null)
                {
                    _stagesManager.EditoringFight = one;
                    foreach (MultiDictionary<int,int,CharDataInfo>.SerializableSets _one in _stagesManager.EditoringFight.EnemySets._SerializableSets)
                    {
                        foreach (MultiDictionary<int,int,CharDataInfo>.SerializableSet set in _one.value)
                        {
                            if (set._Value != null)
                            {
                                CharConfig _CharacterResourceInfo = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(set._Value.ResourceID));
                                if (_CharacterResourceInfo == null)
                                {
                                    Debug.Log("检测到存档错误：ResourceID");
                                    continue;
                                }
                                set._Value._NineAndTwo.SortNineAndTwo();
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("读取本地信息失败");
                }
            }
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        Members();
        GUILayout.Space(10);

        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingCharInfo == null)
        {
            if (GUILayout.Button("Add", AddDeleteMember))
            {
                focusingCharInfo = new CharDataInfo
                {
                    monsterOfPlayerId = focusingMemberRecordID
                };
                _stagesManager.EditoringFight.EnemySets.Set(0, int.Parse(focusingMemberRecordID), focusingCharInfo);
            }
        }
        if (focusingCharInfo != null)
        {
            if (GUILayout.Button("Delete", AddDeleteMember))
            {
                focusingCharInfo = null;
            }
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        // 指定站位人员的添加与删除end //
        
        if (focusingCharInfo == null)
        {
            goto A;
        }

        // 角色选择
        focusingCharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.ResourceID));
        focusingtype = focusingCharConfig != null ? EditorGUILayout.TextField("CharacerType", focusingCharConfig.TYPE) : EditorGUILayout.TextField("CharacerType", focusingtype);
        CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        if (CharIDsAndNames.Count == 0)
        {
            goto A;
        }
        int index = 0;
        foreach (KeyValuePair<string, string> keyValuePair in CharIDsAndNames)
        {
            if (keyValuePair.Key == focusingCharInfo.ResourceID)
            {
                selectedmonsterindex = index;
                break;
            }
            index++;
        }
        selectedmonsterindex = EditorGUILayout.Popup("角色名：", selectedmonsterindex, CharIDsAndNames.Values.ToArray());
        focusingCharInfo.ResourceID = CharIDsAndNames.ElementAt(selectedmonsterindex).Key;
        
        GUILayout.BeginHorizontal();
        level = EditorGUILayout.IntField("参考等级",level);
        if (GUILayout.Button("设置角色所有技能等级为参考等级"))
        {
            focusingCharInfo._NineAndTwo.A1level = level;
            focusingCharInfo._NineAndTwo.A2level = level;
            focusingCharInfo._NineAndTwo.A3level = level;
            focusingCharInfo._NineAndTwo.B1level = level;
            focusingCharInfo._NineAndTwo.B2level = level;
            focusingCharInfo._NineAndTwo.B3level = level;
            focusingCharInfo._NineAndTwo.C1level = level;
            focusingCharInfo._NineAndTwo.C2level = level;
            focusingCharInfo._NineAndTwo.C3level = level;
        }
        GUILayout.EndHorizontal();

        /////// 九宫格 //////////
        NineSlotPart();
        
        // 技能组评价
        SkillSetComent();
        
        if (targetSC == null)
        {
            goto A;
        }

        // 原生技能
        SelectInher();
        if (selectedInhereskill != 0)
        {
            targetSC.RECORD_ID = SelectInhere.ElementAt(selectedInhereskill).Key;
            goto B;
        }

        // 技能选择
        SkillSelect();
        
        B:
        
        SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(targetSC.RECORD_ID);
        if (defaultSkillConfig == null)
        {
            goto A;
        }
        
        // 技能详细信息
        SkillInfo(defaultSkillConfig);
        
        GUILayout.Space(5f);
        if (focusingCharInfo != null && focusingCharInfo._NineAndTwo != null)
        {
            focusingCharInfo._NineAndTwo.RefreshSkillNumsByConfigs();
        }
        /////// 九宫格end //////////
        GUILayout.Space(10f);
        
        A:
        GUI.backgroundColor = Color.white;
        
        // 三被动
        if (focusingCharInfo != null)
        {
            GUILayout.Space(30f);
            focusingCharInfo._NineAndTwo.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", focusingCharInfo._NineAndTwo.moveType);
            focusingCharInfo._NineAndTwo.canDefend = EditorGUILayout.Toggle("有防御技能", focusingCharInfo._NineAndTwo.canDefend);
            focusingCharInfo._NineAndTwo.rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", focusingCharInfo._NineAndTwo.rushType);
            GUILayout.Space(30f);
        }
        
        GUILayout.BeginHorizontal();
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        if (GUILayout.Button("保存战斗关卡至本地文档json",ButtonStyle_save))
        {
            _stagesManager.SaveFightAsJson(pathAndNameForLocalSave,_stagesManager.EditoringFight);
        }
        //if (GUILayout.Button("保存战斗关卡至本地文档xml",ButtonStyle_save))
        //{
        //    _stagesManager.SaveFightAsXml(pathAndNameForLocalSave,_stagesManager.EditoringFight);
        //}
        GUILayout.EndHorizontal();
    }
}
#endif