#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;
using Skill;

// 后排敌人——〉角色ID，localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
[CustomEditor(typeof(StagesManager))]
public class StagesManagerGUI : Editor {

    GUIStyle ButtonStyle;
    GUIStyle addDeleteMember;
    GUIStyle ButtonStyle_selected;
    GUIStyle ButtonStyle_save;
    GUIStyle ButtonStyle_NineAndTwo;
    GUIStyle ButtonStyle_NineAndTwo_Selected;
    GUIStyle big_title;
    GUIStyle title;
    GUIStyle attackRangeToggleGUI;
    
    StagesManager _stagesManager;
    string pathAndNameForLocalSave = "/oneFight.xml";    
    string focusingMemberRecordID;
    IDictionary<string, string> RecordIDsAndNames;
    CharacterDataInfo focusingCharInfo;
    CharacterResourceInfo focusingCharResourceInfo;
    CharacterDataInfo freeEditCharInfo;
    SkillConfig focusingSkillConfig;
    
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


    public override void OnInspectorGUI()
    {
        ButtonStyle = new GUIStyle(GUI.skin.button);
        ButtonStyle.normal.textColor = Color.red;
        ButtonStyle.fixedWidth = 100f;
        ButtonStyle.alignment = TextAnchor.MiddleCenter;

        addDeleteMember = new GUIStyle(GUI.skin.button);
        addDeleteMember.normal.textColor = new Color(1,0.3f,0f);
        addDeleteMember.fixedWidth = 50f;
        addDeleteMember.alignment = TextAnchor.MiddleCenter;

        ButtonStyle_selected = new GUIStyle(GUI.skin.button);
        ButtonStyle_selected.normal.textColor = Color.yellow;
        ButtonStyle_selected.fixedWidth = 100f;
        ButtonStyle_selected.alignment = TextAnchor.MiddleCenter;

        ButtonStyle_save = new GUIStyle(GUI.skin.button);
        ButtonStyle_save.normal.textColor = Color.blue;
        ButtonStyle_save.fixedWidth = 200f;
        ButtonStyle_save.alignment = TextAnchor.MiddleCenter;

        title = new GUIStyle(GUI.skin.label);
        title.normal.textColor = Color.blue;
        title.alignment = TextAnchor.MiddleCenter;

        big_title = new GUIStyle(GUI.skin.label);
        big_title.normal.textColor = Color.red;
        big_title.alignment = TextAnchor.UpperLeft;

        ButtonStyle_NineAndTwo = new GUIStyle(GUI.skin.button);
        ButtonStyle_NineAndTwo.normal.textColor = Color.grey;
        ButtonStyle_NineAndTwo.fixedWidth = 80f;
        ButtonStyle_NineAndTwo.alignment = TextAnchor.MiddleCenter;

        ButtonStyle_NineAndTwo_Selected = new GUIStyle(GUI.skin.button);
        ButtonStyle_NineAndTwo_Selected.normal.textColor = Color.yellow;
        ButtonStyle_NineAndTwo_Selected.fixedWidth = 80f;
        ButtonStyle_NineAndTwo_Selected.alignment = TextAnchor.MiddleCenter;

        attackRangeToggleGUI = new GUIStyle(GUI.skin.toggle)
        {
            margin = new RectOffset(1, 1, 11, 11),
            alignment = TextAnchor.MiddleCenter,
            stretchWidth = false
        };
        _stagesManager = (StagesManager)target;
        
        // 关卡编辑器下，技能配置文件定走resource文件夹，所以不需要走SkillsConfigInfos.loadAllSkillConfigs(), 同理角色配置文件也是
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        SkillConfigTable.RefreshSkillConfigDicForReference();
        MonstersConfigTable.LoadMonstersConfigByResource();
        MonstersConfigTable.RefreshCharacterResourceInfoDic();
        
        GUILayout.Space(10);
        EditorGUILayout.LabelField(" 战斗脚本读取  ", big_title);
        GUILayout.BeginHorizontal();
        _stagesManager.FightScript = EditorGUILayout.ObjectField("Read Fight Script", _stagesManager.FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (_stagesManager.FightScript != null)
            {
                LocalFight one = _stagesManager.LoadOneLocalFight_Json(_stagesManager.FightScript);
                if (one != null)
                {
                    _stagesManager.editoringFight = one;
                    foreach (MultiDictionary<int,int,CharacterDataInfo>.SerializableSets _one in _stagesManager.editoringFight.EnemySets._SerializableSets)
                    {
                        foreach (MultiDictionary<int,int,CharacterDataInfo>.SerializableSet set in _one.value)
                        {
                            if (set._Value != null)
                            {
                                CharacterResourceInfo _CharacterResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(set._Value.ResourceID));
                                if (_CharacterResourceInfo == null)
                                {
                                    Debug.Log("一个不正常的角色："+set._Value.ResourceID);
                                    continue;
                                }
                                if (set._Value._NineAndTwo == null)
                                {
                                    Debug.Log("一个不正常的角色："+set._Value.ResourceID);
                                    set._Value._NineAndTwo = new NineAndTwo();
                                }
                                else
                                {
                                    Debug.Log("一个正常的角色："+set._Value.ResourceID);
                                    set._Value._NineAndTwo.SortNineAndTwo();
                                }
                            }else{
                                Debug.Log("??"+ set._Key2);
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
        
        //暂时做如下处理
        TextAsset CSV = Resources.Load("Account/mst_monster") as TextAsset;
        if (CSV)
            MonstersConfigTable.Instance.Load(CSV);
        else
            Debug.Log("没能读取到角色数据库文件。");
        GUILayout.Space(10);
        
        EditorGUILayout.LabelField(" 关卡敌人信息  ", title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("FreeEdit", (focusingMemberRecordID != null) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberRecordID = (-1).ToString();
            if (freeEditCharInfo == null)
            {
                freeEditCharInfo = new CharacterDataInfo
                {
                    monsterOfPlayerId = (-1).ToString(),
                    _NineAndTwo = new NineAndTwo()
                };
            }
            focusingCharInfo = freeEditCharInfo;
        }
        GUILayout.EndHorizontal();
        
        // 四站位 //
        GUILayout.BeginHorizontal();
        GUILayout.Space(100);
        if (GUILayout.Button("back", (focusingMemberRecordID != 0.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 0.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 0);
            Debug.Log(focusingCharInfo == null ? "没找到":"找到了");
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("right",(focusingMemberRecordID != 3.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 3.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 3);
        }
        if (GUILayout.Button("front", (focusingMemberRecordID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 2.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 2);
        }
        if (GUILayout.Button("left",(focusingMemberRecordID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberRecordID = 1.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 1);
        }
        GUILayout.EndHorizontal();
        // 四站位end //
    
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingCharInfo == null)
        {
            if (GUILayout.Button("Add", addDeleteMember))
            {
                focusingCharInfo = new CharacterDataInfo
                {
                    monsterOfPlayerId = focusingMemberRecordID
                };
                MultiDictionary<int, int, CharacterDataInfo> clist = _stagesManager.editoringFight.EnemySets;
                clist.Set(0,int.Parse(focusingMemberRecordID),focusingCharInfo);
                _stagesManager.editoringFight.EnemySets = clist;
            }
        }
        if (GUILayout.Button("Delete", addDeleteMember))
        {
            if (focusingCharInfo != null)
            {
                //List<CharacterDataInfo> clist = _stagesManager.editoringFight.Enemies.ToList();
                //if (clist.Contains(focusingCharInfo))
                //    clist.Remove(focusingCharInfo);
                //_stagesManager.editoringFight.Enemies = clist.ToArray();
                //focusingCharInfo.Dissolve();
            }
            focusingCharInfo = null;
        }
        GUILayout.EndHorizontal();
        // 指定站位人员的添加与删除end //
    
        if (focusingCharInfo != null)
        {
            focusingCharResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.ResourceID));
            focusingtype = focusingCharResourceInfo != null ? EditorGUILayout.TextField("CharacerType", focusingCharResourceInfo.type) : EditorGUILayout.TextField("CharacerType", focusingtype);
            RecordIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
            if (RecordIDsAndNames.Count == 0)
            {
                return;
            }
            int index = 0;
            foreach (KeyValuePair<string, string> keyValuePair in RecordIDsAndNames)
            {
                if (keyValuePair.Key == focusingCharInfo.ResourceID)
                {
                    selectedmonsterindex = index;
                    break;
                }
                index++;
            }
            selectedmonsterindex = EditorGUILayout.Popup("角色名：", selectedmonsterindex, RecordIDsAndNames.Values.ToArray());
            focusingCharInfo.ResourceID = selectedmonsterindex == 0 ? null : RecordIDsAndNames.ElementAt(selectedmonsterindex).Key;
            if (selectedmonsterindex == 0)
            {
                return;
            }
            GUILayout.Space(10f);
            focusingCharInfo.HP = EditorGUILayout.IntField("HP :", focusingCharInfo.HP);    
            GUILayout.Space(10f);
            
            /////// 九宫格 //////////
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.gray;
            if (focusingCharInfo._NineAndTwo == null)
                focusingCharInfo._NineAndTwo = new NineAndTwo();
            if (GUILayout.Button("M", focusingCharInfo._NineAndTwo.GetMConfig() != focusingSkillConfig ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetMConfig();
            }
            if (GUILayout.Button("D", focusingCharInfo._NineAndTwo.GetDConfig() != focusingSkillConfig ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetDConfig();
            }
            if (GUILayout.Button("R", focusingCharInfo._NineAndTwo.GetRConfig() != focusingSkillConfig ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetRConfig();
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            ButtonStyle_NineAndTwo.normal.textColor = Color.blue;
            
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetA1Config() != null && focusingCharInfo._NineAndTwo.GetA1Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("A1", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetA1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetA1Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetA2Config() != null && focusingCharInfo._NineAndTwo.GetA2Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("A2", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetA2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetA2Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetA3Config() != null && focusingCharInfo._NineAndTwo.GetA3Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("A3", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetA3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetA3Config();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetB1Config() != null && focusingCharInfo._NineAndTwo.GetB1Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("B1", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetB1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetB1Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetB2Config() != null && focusingCharInfo._NineAndTwo.GetB2Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("B2", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetB2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetB2Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetB3Config() != null && focusingCharInfo._NineAndTwo.GetB3Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("B3", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetB3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetB3Config();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();    
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetC1Config() != null && focusingCharInfo._NineAndTwo.GetC1Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("C1", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetC1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetC1Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetC2Config() != null && focusingCharInfo._NineAndTwo.GetC2Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("C2", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetC2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetC2Config();
            }
            GUI.backgroundColor = focusingCharInfo._NineAndTwo.GetC3Config() != null && focusingCharInfo._NineAndTwo.GetC3Config().RECORD_ID != null ? Color.yellow : Color.white;
            if (GUILayout.Button("C3", focusingSkillConfig != focusingCharInfo._NineAndTwo.GetC3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
            {
                selectedskillindex = 0;
                focusingSkillConfig = focusingCharInfo._NineAndTwo.GetC3Config();
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10f);
            bool SanGong = false;
            if (focusingSkillConfig != null)
            {
                if (focusingSkillConfig == focusingCharInfo._NineAndTwo.GetMConfig())
                {
                    focusingCharInfo._NineAndTwo.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", focusingCharInfo._NineAndTwo.moveType);
                    SanGong = true;
                }
                if (focusingSkillConfig == focusingCharInfo._NineAndTwo.GetDConfig())
                {
                    focusingCharInfo._NineAndTwo.canDefend = EditorGUILayout.Toggle("有防御技能", focusingCharInfo._NineAndTwo.canDefend);
                    SanGong = true;
                }
                if (focusingSkillConfig == focusingCharInfo._NineAndTwo.GetRConfig())
                {
                    focusingCharInfo._NineAndTwo.rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", focusingCharInfo._NineAndTwo.rushType);
                    SanGong = true;
                }
                GUILayout.Space(10f);
                if (!SanGong)//&& focusingSkillConfig.RECORD_ID != "-1"
                {
                    skillselectfilter = EditorGUILayout.Toggle("限制技能选择条件", skillselectfilter, attackRangeToggleGUI);
                    if (skillselectfilter)
                    {
                        EditorGUILayout.LabelField(" &&&&&&&  限制技能条件  &&&&&&& ", title);
                        filterallranges = EditorGUILayout.BeginToggleGroup("限定攻击范围", filterallranges);
                        if (!filterallranges) 
                        { 
                            skillrangeselectfilter[0] = true; skillrangeselectfilter[1] = true; skillrangeselectfilter[2] = true; skillrangeselectfilter[3] = true;
                        }
                        skillrangeselectfilter[0] = EditorGUILayout.Toggle("近", skillrangeselectfilter[0], attackRangeToggleGUI);
                        skillrangeselectfilter[1] = EditorGUILayout.Toggle("中", skillrangeselectfilter[1], attackRangeToggleGUI);
                        skillrangeselectfilter[2] = EditorGUILayout.Toggle("远", skillrangeselectfilter[2], attackRangeToggleGUI);
                        skillrangeselectfilter[3] = EditorGUILayout.Toggle("超", skillrangeselectfilter[3], attackRangeToggleGUI);
                        EditorGUILayout.EndToggleGroup();
                        selectskillrarelevel = EditorGUILayout.IntPopup("技能rank:", selectskillrarelevel, skillrarelevelShow, skillrarelevels);
                        EditorGUILayout.LabelField(" &&&&&&&  以下将陈列根据条件删选出的技能  &&&&&&& ", title);
                        GUILayout.Space(20f);
                    }

                    IDictionary<string,string> _SkillRecordIDsAndNames = SkillConfigTable.GetSkillIDAndNameDic(focusingtype, new bool[4] { skillrangeselectfilter[0], skillrangeselectfilter[1], skillrangeselectfilter[2], skillrangeselectfilter[3]}, selectskillrarelevel);
                    int index2 = 0;
                    foreach (KeyValuePair<string, string> keyValuePair in _SkillRecordIDsAndNames)
                    {
                        if (keyValuePair.Key == focusingSkillConfig.RECORD_ID)
                        {
                            selectedskillindex = index2;
                            break;
                        }
                        index2++;
                    }
                    selectedskillindex = EditorGUILayout.Popup("技能：", selectedskillindex, _SkillRecordIDsAndNames.Values.ToArray());
                    focusingSkillConfig.RECORD_ID = selectedskillindex == 0 ? null : _SkillRecordIDsAndNames.ElementAt(selectedskillindex).Key;
                    SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(focusingSkillConfig.RECORD_ID);
                    if (defaultSkillConfig == null)
                    {
                        return;
                    }
                    GUILayout.Space(10f);
                    
                    if (focusingSkillConfig.RECORD_ID != null)
                    {
                        focusingSkillConfig.STATE_TYPE = (BehaviorType)EditorGUILayout.EnumPopup("Attack Type",
                                                                                     (focusingSkillConfig.STATE_TYPE == BehaviorType.NONE && defaultSkillConfig != null && defaultSkillConfig.STATE_TYPE != BehaviorType.NONE)
                                                                                     ?
                                                                                     defaultSkillConfig.STATE_TYPE : focusingSkillConfig.STATE_TYPE);

                        focusingSkillConfig.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT",
                                                                          (defaultSkillConfig != null)
                                                                          ?
                                                                          defaultSkillConfig.ATTACK_WEIGHT : focusingSkillConfig.ATTACK_WEIGHT);

                        focusingSkillConfig.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel",
                                                                                    (focusingSkillConfig.SP_LEVEL == -1 && defaultSkillConfig != null)
                                                                                    ?
                                                                                    defaultSkillConfig.SP_LEVEL : focusingSkillConfig.SP_LEVEL,
                                                                                    exoptions_display,exoptions);
                        GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
                        GUILayout.Space(5f);
                        EditorGUILayout.LabelField("AI模式技能触发范围");
                        defaultSkillConfig.AI_MIN_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MIN_DIS);
                        defaultSkillConfig.AI_MAX_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MAX_DIS);
                        GUILayout.Space(5f);
                        GUI.backgroundColor = Color.white;
                    }
                }
            }
            if (focusingCharInfo != null && focusingCharInfo._NineAndTwo != null)
                focusingCharInfo._NineAndTwo.RefreshSkillNumsByConfigs();
            /////// 九宫格end //////////
        }
            
        GUILayout.Space(10f);
        EditorGUILayout.LabelField(" 如何处理当前编辑中的战斗关卡信息  ", big_title);
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存战斗关卡至本地文档xml",ButtonStyle_save))
        {
            _stagesManager.SaveFightAsXml(pathAndNameForLocalSave,_stagesManager.editoringFight);
        }
        if (GUILayout.Button("保存战斗关卡至本地文档json",ButtonStyle_save))
        {
            _stagesManager.SaveFightAsJson(pathAndNameForLocalSave,_stagesManager.editoringFight);
        }
        GUILayout.EndHorizontal();
    }
}
#endif