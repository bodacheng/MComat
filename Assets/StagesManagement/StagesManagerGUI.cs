#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;
using Skill;

// 后排敌人——〉角色ID，
// localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
[CustomEditor(typeof(StagesManager))]
public class StagesManagerGUI : Editor {

    GUIStyle ButtonStyle;
    GUIStyle AddDeleteMember;
    GUIStyle ButtonStyle_selected;
    GUIStyle ButtonStyle_save;
    GUIStyle ButtonStyle_NineAndTwo;
    GUIStyle ButtonStyle_NineAndTwo_Selected;
    GUIStyle Big_title;
    GUIStyle Title;
    GUIStyle AttackRangeToggleGUI;

    StagesManager _stagesManager;
    string pathAndNameForLocalSave = "oneFight.json";    
    string focusingMemberRecordID;
    IDictionary<string, string> CharIDsAndNames;
    CharDataInfo focusingCharInfo;
    CharConfig focusingCharResourceInfo;
    SkillConfig targetSC;
    
    bool skillselectfilter;
    bool filterallranges = true;
    readonly bool[] skillrangeselectfilter = { true, true, true, true };//close,near,far,out
    int selectedskillindex, selectedmonsterindex;
    int selectskillrarelevel = -1;
    readonly int[] skillrarelevels = {-1,0,1,2,3};
    readonly string[] skillrarelevelShow = {"ALL","0", "★", "★★", "★★★"};
    string focusingtype;
    readonly int[] exoptions = { 0, 1, 2, 3 };
    readonly string[] exoptions_display = {"normal","ex1","ex2","ex3"};
    IDictionary<string, string> _SkillIDsAndNames;
    bool Initialized;
    
    bool Repeated(SkillConfig _target,string recordID)
    {
        foreach (KeyValuePair<SkillConfig,string> keyValuePair in GetFocusingCharSkillList())
        {
            if (keyValuePair.Value == recordID && keyValuePair.Key != _target)
            {
                return true;
            }
        }
        return false;
    }
    
    IDictionary<SkillConfig,string> GetFocusingCharSkillList()
    {
        IDictionary<SkillConfig,string> list = new Dictionary<SkillConfig,string>();
        
        SkillConfig A1 = focusingCharInfo._NineAndTwo.GetA1Config();
        SkillConfig A2 = focusingCharInfo._NineAndTwo.GetA2Config();
        SkillConfig A3 = focusingCharInfo._NineAndTwo.GetA3Config();
        SkillConfig B1 = focusingCharInfo._NineAndTwo.GetB1Config();
        SkillConfig B2 = focusingCharInfo._NineAndTwo.GetB2Config();
        SkillConfig B3 = focusingCharInfo._NineAndTwo.GetB3Config();
        SkillConfig C1 = focusingCharInfo._NineAndTwo.GetC1Config();
        SkillConfig C2 = focusingCharInfo._NineAndTwo.GetC2Config();
        SkillConfig C3 = focusingCharInfo._NineAndTwo.GetC3Config();
        
        if (A1 != null && A1.RECORD_ID != null)
        {
            list.Add(A1,A1.RECORD_ID);
        }
        if (A2 != null && A2.RECORD_ID != null)
        {
            list.Add(A2,A2.RECORD_ID);
        }
        if (A3 != null && A3.RECORD_ID != null)
        {
            list.Add(A3,A3.RECORD_ID);
        }
        if (B1 != null && B1.RECORD_ID != null)
        {
            if (!list.ContainsKey(B1))
            list.Add(B1,B1.RECORD_ID);
        }
        if (B2 != null && B2.RECORD_ID != null)
        {
            list.Add(B2,B2.RECORD_ID);
        }
        if (B3 != null && B3.RECORD_ID != null)
        {
            list.Add(B3,B3.RECORD_ID);
        }
        if (C1 != null && C1.RECORD_ID != null)
        {
            list.Add(C1,C1.RECORD_ID);
        }
        if (C2 != null && C2.RECORD_ID != null)
        {
            list.Add(C2,C2.RECORD_ID);
        }
        if (C3 != null && C3.RECORD_ID != null)
        {
            list.Add(C3,C3.RECORD_ID);
        }
        return list;
    }

    bool SanGong = false;
    int level = 1;// 参考等级。角色自身不存在等级，但为了设置方便所有技能等级可以一致
    public override void OnInspectorGUI()
    {
        if (!Initialized)
        {
            ButtonStyle = new GUIStyle(GUI.skin.button);
            ButtonStyle.normal.textColor = Color.red;
            ButtonStyle.fixedWidth = 100f;
            ButtonStyle.alignment = TextAnchor.MiddleCenter;
            
            AddDeleteMember = new GUIStyle(GUI.skin.button);
            AddDeleteMember.normal.textColor = new Color(1,0.3f,0f);
            AddDeleteMember.fixedWidth = 50f;
            AddDeleteMember.alignment = TextAnchor.MiddleCenter;
            
            ButtonStyle_selected = new GUIStyle(GUI.skin.button);
            ButtonStyle_selected.normal.textColor = Color.yellow;
            ButtonStyle_selected.fixedWidth = 100f;
            ButtonStyle_selected.alignment = TextAnchor.MiddleCenter;
            
            ButtonStyle_save = new GUIStyle(GUI.skin.button);
            ButtonStyle_save.normal.textColor = Color.blue;
            ButtonStyle_save.fixedWidth = 200f;
            ButtonStyle_save.alignment = TextAnchor.MiddleCenter;
    
            Title = new GUIStyle(GUI.skin.label);
            Title.normal.textColor = Color.blue;
            Title.alignment = TextAnchor.MiddleCenter;
    
            Big_title = new GUIStyle(GUI.skin.label);
            Big_title.normal.textColor = Color.red;
            Big_title.alignment = TextAnchor.UpperLeft;
    
            ButtonStyle_NineAndTwo = new GUIStyle(GUI.skin.button);
            ButtonStyle_NineAndTwo.normal.textColor = Color.blue;
            ButtonStyle_NineAndTwo.fixedWidth = 80f;
            ButtonStyle_NineAndTwo.alignment = TextAnchor.MiddleCenter;
            
            ButtonStyle_NineAndTwo_Selected = new GUIStyle(GUI.skin.button);
            ButtonStyle_NineAndTwo_Selected.normal.textColor = Color.yellow;
            ButtonStyle_NineAndTwo_Selected.fixedWidth = 80f;
            ButtonStyle_NineAndTwo_Selected.alignment = TextAnchor.MiddleCenter;
            
            AttackRangeToggleGUI = new GUIStyle(GUI.skin.toggle)
            {
                margin = new RectOffset(1, 1, 11, 11),
                alignment = TextAnchor.MiddleCenter,
                stretchWidth = false
            };
            
            // 关卡编辑器下，技能配置文件定走resource文件夹，所以不需要走SkillsConfigInfos.loadAllSkillConfigs(), 同理角色配置文件也是
            SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
            SkillConfigTable.RefreshSkillConfigDicForReference();
            MonstersConfigTable.LoadMonstersConfigByResource();
            MonstersConfigTable.RefreshCharacterResourceInfoDic();

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
                                CharConfig _CharacterResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(set._Value.ResourceID));
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

        EditorGUILayout.LabelField(" 关卡敌人信息  ", Title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("back", (focusingMemberRecordID != 0.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 0.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 0);
        }
        if (GUILayout.Button("left",(focusingMemberRecordID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 1.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 1);
        }
        if (GUILayout.Button("front", (focusingMemberRecordID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 2.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 2);
        }
        if (GUILayout.Button("right",(focusingMemberRecordID != 3.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 3.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 3);
        }
        GUILayout.EndHorizontal();
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
                _stagesManager.EditoringFight.EnemySets.Set(0,int.Parse(focusingMemberRecordID),focusingCharInfo);
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
        
        focusingCharResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.ResourceID));
        focusingtype = focusingCharResourceInfo != null ? EditorGUILayout.TextField("CharacerType", focusingCharResourceInfo.TYPE) : EditorGUILayout.TextField("CharacerType", focusingtype);
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
        focusingCharInfo.ResourceID = selectedmonsterindex == 0 ? null : CharIDsAndNames.ElementAt(selectedmonsterindex).Key;
        if (selectedmonsterindex == 0)
        {
            goto A;
        }

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
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("M", focusingCharInfo._NineAndTwo.GetMConfig() != targetSC ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetMConfig();
        }
        if (GUILayout.Button("D", focusingCharInfo._NineAndTwo.GetDConfig() != targetSC ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetDConfig();
        }
        if (GUILayout.Button("R", focusingCharInfo._NineAndTwo.GetRConfig() != targetSC ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetRConfig();
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        void SlotColorCal(SkillConfig targetC)
        {
            GUI.backgroundColor = Repeated(targetC, targetC.RECORD_ID) ? Color.red : SlotHasSkill(targetC.RECORD_ID) ? Color.yellow : Color.white;
        }

        SlotColorCal(focusingCharInfo._NineAndTwo.GetA1Config());
        if (GUILayout.Button("A1", targetSC != focusingCharInfo._NineAndTwo.GetA1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA1Config();
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA2Config());
        if (GUILayout.Button("A2", targetSC != focusingCharInfo._NineAndTwo.GetA2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA2Config();
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA3Config());
        if (GUILayout.Button("A3", targetSC != focusingCharInfo._NineAndTwo.GetA3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA3Config();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB1Config());
        if (GUILayout.Button("B1", targetSC != focusingCharInfo._NineAndTwo.GetB1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB1Config();
        }

        SlotColorCal(focusingCharInfo._NineAndTwo.GetB2Config());
        if (GUILayout.Button("B2", targetSC != focusingCharInfo._NineAndTwo.GetB2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB2Config();
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB3Config());
        if (GUILayout.Button("B3", targetSC != focusingCharInfo._NineAndTwo.GetB3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB3Config();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC1Config());
        if (GUILayout.Button("C1", targetSC != focusingCharInfo._NineAndTwo.GetC1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC1Config();
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC2Config());
        if (GUILayout.Button("C2", targetSC != focusingCharInfo._NineAndTwo.GetC2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC2Config();
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC3Config());
        if (GUILayout.Button("C3", targetSC != focusingCharInfo._NineAndTwo.GetC3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC3Config();
        }
        GUILayout.EndHorizontal();

        if (targetSC == null)
        {
            goto A;
        }
        GUI.backgroundColor = Color.white;

        GUILayout.BeginHorizontal();
        SanGong = false;
        if (targetSC == focusingCharInfo._NineAndTwo.GetMConfig())
        {
            focusingCharInfo._NineAndTwo.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", focusingCharInfo._NineAndTwo.moveType);
            SanGong = true;
        }
        if (targetSC == focusingCharInfo._NineAndTwo.GetDConfig())
        {
            focusingCharInfo._NineAndTwo.canDefend = EditorGUILayout.Toggle("有防御技能", focusingCharInfo._NineAndTwo.canDefend);
            SanGong = true;
        }
        if (targetSC == focusingCharInfo._NineAndTwo.GetRConfig())
        {
            focusingCharInfo._NineAndTwo.rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", focusingCharInfo._NineAndTwo.rushType);
            SanGong = true;
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        
        if (!SanGong)
        {
            skillselectfilter = EditorGUILayout.Toggle("限制技能选择条件", skillselectfilter, AttackRangeToggleGUI);
            if (skillselectfilter)
            {
                EditorGUILayout.LabelField(" ~~~~~  限制技能条件  ~~~~~ ", Title);
                
                filterallranges = EditorGUILayout.BeginToggleGroup("限定攻击范围", filterallranges);
                if (!filterallranges) 
                {
                    skillrangeselectfilter[0] = true;
                    skillrangeselectfilter[1] = true;
                    skillrangeselectfilter[2] = true;
                    skillrangeselectfilter[3] = true;
                }
                skillrangeselectfilter[0] = EditorGUILayout.Toggle("近", skillrangeselectfilter[0], AttackRangeToggleGUI);
                skillrangeselectfilter[1] = EditorGUILayout.Toggle("中", skillrangeselectfilter[1], AttackRangeToggleGUI);
                skillrangeselectfilter[2] = EditorGUILayout.Toggle("远", skillrangeselectfilter[2], AttackRangeToggleGUI);
                skillrangeselectfilter[3] = EditorGUILayout.Toggle("超", skillrangeselectfilter[3], AttackRangeToggleGUI);
                EditorGUILayout.EndToggleGroup();
                
                selectskillrarelevel = EditorGUILayout.IntPopup("技能rank:", selectskillrarelevel, skillrarelevelShow, skillrarelevels);
                EditorGUILayout.LabelField(" ~~~~~  以下将陈列根据条件删选出的技能  ~~~~~ ", Title);
                GUILayout.Space(20f);
            }
            
            _SkillIDsAndNames = SkillConfigTable.GetSkillIDAndNameDic(focusingtype, new bool[4] { skillrangeselectfilter[0], skillrangeselectfilter[1], skillrangeselectfilter[2], skillrangeselectfilter[3]}, selectskillrarelevel);
            
            int index2 = 0;
            selectedskillindex = 0;
            foreach (KeyValuePair<string, string> keyValuePair in _SkillIDsAndNames)
            {
                if (keyValuePair.Key == targetSC.RECORD_ID)
                {
                    selectedskillindex = index2;
                    break;
                }
                index2++;
            }
            selectedskillindex = EditorGUILayout.Popup("技能：", selectedskillindex, _SkillIDsAndNames.Values.ToArray());
            targetSC.RECORD_ID = selectedskillindex == 0 ? null : _SkillIDsAndNames.ElementAt(selectedskillindex).Key;                        
            SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(targetSC.RECORD_ID);
            if (defaultSkillConfig == null)
            {
                goto A;
            }
            targetSC.STATE_TYPE = (BehaviorType)EditorGUILayout.EnumPopup("Attack Type",(targetSC.STATE_TYPE == BehaviorType.NONE && defaultSkillConfig != null && defaultSkillConfig.STATE_TYPE != BehaviorType.NONE) ? defaultSkillConfig.STATE_TYPE : targetSC.STATE_TYPE);                                                    
            targetSC.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT", (defaultSkillConfig != null) ? defaultSkillConfig.ATTACK_WEIGHT : targetSC.ATTACK_WEIGHT);
            targetSC.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel",(targetSC.SP_LEVEL == -1 && defaultSkillConfig != null) ? defaultSkillConfig.SP_LEVEL : targetSC.SP_LEVEL, exoptions_display,exoptions);
            GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
            GUILayout.Space(5f);
            
            EditorGUILayout.LabelField("AI模式技能触发范围");
            defaultSkillConfig.AI_MIN_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MIN_DIS);
            defaultSkillConfig.AI_MAX_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MAX_DIS);
            GUILayout.Space(5f);
        }
        if (focusingCharInfo != null && focusingCharInfo._NineAndTwo != null)
        {
            focusingCharInfo._NineAndTwo.RefreshSkillNumsByConfigs();
        }
        /////// 九宫格end //////////
        GUILayout.Space(10f);
        
        A:
        GUI.backgroundColor = Color.white;
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
    
    bool SlotHasSkill(string RECORD_ID)
    {
        SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(RECORD_ID);
        return defaultSkillConfig != null;
    }
}
#endif