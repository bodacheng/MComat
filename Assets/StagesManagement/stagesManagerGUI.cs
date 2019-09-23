#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using dataAccess;
using Api.Dto.Model;

// 本脚本暂时commentout 化。 关卡生成器已经伴随着整个项目质的变化而彻底变化，主要是敌人战斗力能力上的表现


//后排敌人——〉角色ID，localID = 0，脚本ID，等级 前排中央敌人——〉角色ID，localID = 1，脚本ID，等级 前排左敌人——〉角色ID，localID = 2，脚本ID，等级 前排右敌人——〉角色ID，localID = 3，脚本ID，等级
[CustomEditor(typeof(stagesManager))]
public class stagesManagerGUI : Editor {

    GUIStyle ButtonStyle;
    GUIStyle addDeleteMember;
    GUIStyle ButtonStyle_selected;
    GUIStyle ButtonStyle_save;
    GUIStyle ButtonStyle_NineAndTwo;
    GUIStyle ButtonStyle_NineAndTwo_Selected;
    GUIStyle big_title;
    GUIStyle title;
    GUIStyle attackRangeToggleGUI;
    
    string pathAndNameForLocalSave = "/oneFight.xml";
    stagesManager _stagesManager;
    string focusingMemberRecordID = null;    
    CharacterDataInfo focusingCharInfo = null;
    CharacterResourceInfo focusingCharResourceInfo = null;
    CharacterDataInfo freeEditCharInfo;
    SkillConfig focusingSkillConfig = null;
    
    bool skillselectfilter = false;
    bool filterallranges = true;
    bool[] skillrangeselectfilter = new bool[4] { true, true, true,true };//close,near,far,out

    int selectedskillindex, selectedmonsterindex;
    int selectskillrarelevel = -1;
    int[] skillrarelevels = {-1,0,1,2,3};
    string[] skillrarelevelShow = {"ALL","0", "★", "★★", "★★★"};
    string focusingtype;

    int[] exoptions = new int[] { 0, 1, 2, 3 };
    string[] exoptions_display = new string[] {"normal","ex1","ex2","ex3"};

    void addOneCharToDebugLocalCoustomFile(GetMonsterOfPlayerDetailModel _CharInfo)
    {
    }

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
    
        attackRangeToggleGUI = new GUIStyle(GUI.skin.toggle);
        attackRangeToggleGUI.margin = new RectOffset(1, 1, 11, 11);
        attackRangeToggleGUI.alignment = TextAnchor.MiddleCenter;
        attackRangeToggleGUI.stretchWidth = false;
    
        _stagesManager = (stagesManager)target;

        // 关卡编辑器下，技能配置文件定走resource文件夹，所以不需要走SkillsConfigInfos.loadAllSkillConfigs(), 同理角色配置文件也是
        monsterTypeReferenceTable.Instance.loadLocalMonsterTypeReference();
        SkillConfigTable.loadAllSkillConfigFromLocalConfigFile();
        SkillConfigTable.refreshSkillConfigDicForReference();
        monstersConfigTable.loadMonstersConfigByResource();
        monstersConfigTable.refreshCharacterResourceInfoDic();
        
        GUILayout.Space(10);
        EditorGUILayout.LabelField(" 战斗脚本读取  ", big_title);
        GUILayout.BeginHorizontal();
        _stagesManager.FightScript = EditorGUILayout.ObjectField("Read Fight Script", _stagesManager.FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (_stagesManager.FightScript != null)
            {
                LocalFight one = _stagesManager.loadOneLocalFight(_stagesManager.FightScript);
                if (one != null)
                {
                    _stagesManager.editoringFight = one;
                    foreach (MultiDictionary<int,int,CharacterDataInfo>.SerializableSets _one in _stagesManager.editoringFight.EnemySets._SerializableSets)
                    {
                        foreach (MultiDictionary<int,int,CharacterDataInfo>.SerializableSet set in _one.value)
                        {
                            CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(set._Value.monsterId.ToString()));
                            if (_CharacterResourceInfo == null)
                                continue;
                            if (set._Value._NineAndTwo == null)
                                set._Value._NineAndTwo = new NineAndTwo();
                            else
                                set._Value._NineAndTwo.sortNineAndTwo();
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
            monstersConfigTable.Instance.Load(CSV);
        else
            Debug.Log("没能读取到角色数据库文件。");
        GUILayout.Space(10);
        
        EditorGUILayout.LabelField(" stage信息  ", big_title);
        _stagesManager.editoringFight.BattleGroundID = EditorGUILayout.IntField("场景ID:", _stagesManager.editoringFight.BattleGroundID);
        GUILayout.Space(5f);
        
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
            focusingMemberRecordID = 0.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 0);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("right",(focusingMemberRecordID != 3.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberRecordID = 3.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 3);
        }
        if (GUILayout.Button("front", (focusingMemberRecordID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberRecordID = 2.ToString();
            focusingCharInfo = _stagesManager.editoringFight.EnemySets.Get(0, 2);
        }
        if (GUILayout.Button("left",(focusingMemberRecordID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
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
                    monsterOfPlayerId = focusingMemberRecordID.ToString()
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
            focusingCharResourceInfo = monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.monsterId));
            if (focusingCharInfo.monsterId != "-1" && focusingCharResourceInfo != null)
            {
                focusingtype = EditorGUILayout.TextField("characerType", focusingCharResourceInfo.type);
            }
            else
            {
                focusingtype = EditorGUILayout.TextField("characerType", focusingtype);
            }
            // 角色type指定end //

            RecordIDsAndNames RecordIDsAndNames = monstersConfigTable.getMonsterRecordIDsAndNamesArray(focusingtype);
            if (RecordIDsAndNames == null)
                return;
            string[] charResourceNums = RecordIDsAndNames.RecordIDs.ToArray();
            string[] charResourceNames = RecordIDsAndNames.Names.ToArray();
    
            if (focusingCharInfo != null && charResourceNums != null && charResourceNames != null && focusingtype != null && charResourceNums.Length != 0)
            {
                selectedmonsterindex = EditorGUILayout.Popup("角色名：", selectedmonsterindex, charResourceNames);
                focusingCharInfo.monsterId = charResourceNums[selectedmonsterindex];
                
                GUILayout.Space(10f);
                focusingCharInfo.HP = EditorGUILayout.IntField("HP :", focusingCharInfo.HP);    
                GUILayout.Space(10f);
                
                /////// 九宫格 //////////
                GUILayout.BeginHorizontal();
                GUI.backgroundColor = Color.gray;
                if (focusingCharInfo._NineAndTwo == null)
                    focusingCharInfo._NineAndTwo = new NineAndTwo();
                if (GUILayout.Button("M", focusingCharInfo._NineAndTwo.getMConfig() != focusingSkillConfig ?
                     ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getMConfig();
                }
                if (GUILayout.Button("D", focusingCharInfo._NineAndTwo.getDConfig() != focusingSkillConfig ?
                    ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getDConfig();
                }
                if (GUILayout.Button("R", focusingCharInfo._NineAndTwo.getRConfig() != focusingSkillConfig ?
                    ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getRConfig();
                }
                GUI.backgroundColor = Color.white;
                GUILayout.EndHorizontal();
    
                ButtonStyle_NineAndTwo.normal.textColor = Color.blue;
                GUILayout.BeginHorizontal();
                if (focusingCharInfo._NineAndTwo.getA1Config() == null || focusingCharInfo._NineAndTwo.getA1Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("A1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getA1Config() ?
                                     ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getA1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getA2Config() == null || focusingCharInfo._NineAndTwo.getA2Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("A2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getA2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getA2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getA3Config() == null || focusingCharInfo._NineAndTwo.getA3Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("A3", focusingSkillConfig != focusingCharInfo._NineAndTwo.getA3Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getA3Config();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
    
                if (focusingCharInfo._NineAndTwo.getB1Config() == null || focusingCharInfo._NineAndTwo.getB1Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("B1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getB1Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getB1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getB2Config() == null || focusingCharInfo._NineAndTwo.getB2Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("B2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getB2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getB2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getB3Config() == null || focusingCharInfo._NineAndTwo.getB3Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("B3", focusingSkillConfig != focusingCharInfo._NineAndTwo.getB3Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getB3Config();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
    
                if (focusingCharInfo._NineAndTwo.getC1Config() == null || focusingCharInfo._NineAndTwo.getC1Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("C1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getC1Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getC1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getC2Config() == null || focusingCharInfo._NineAndTwo.getC2Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("C2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getC2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getC2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getC3Config() == null || focusingCharInfo._NineAndTwo.getC3Config().RECORD_ID == null)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("C3", focusingSkillConfig != focusingCharInfo._NineAndTwo.getC3Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getC3Config();
                }
                GUI.backgroundColor = Color.white;
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
    
                bool SanGong = false;
                if (focusingSkillConfig != null)
                {
                    if (focusingSkillConfig == focusingCharInfo._NineAndTwo.getMConfig())
                    {
                        focusingCharInfo._NineAndTwo.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", focusingCharInfo._NineAndTwo.moveType);
                        SanGong = true;
                    }
                    if (focusingSkillConfig == focusingCharInfo._NineAndTwo.getDConfig())
                    {
                        focusingCharInfo._NineAndTwo.canDefend = EditorGUILayout.Toggle("有防御技能", focusingCharInfo._NineAndTwo.canDefend);
                        SanGong = true;
                    }
                    if (focusingSkillConfig == focusingCharInfo._NineAndTwo.getRConfig())
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
                                { skillrangeselectfilter[0] = true; skillrangeselectfilter[1] = true; skillrangeselectfilter[2] = true; skillrangeselectfilter[3] = true;}
                            skillrangeselectfilter[0] = EditorGUILayout.Toggle("近", skillrangeselectfilter[0], attackRangeToggleGUI);
                            skillrangeselectfilter[1] = EditorGUILayout.Toggle("中", skillrangeselectfilter[1], attackRangeToggleGUI);
                            skillrangeselectfilter[2] = EditorGUILayout.Toggle("远", skillrangeselectfilter[2], attackRangeToggleGUI);
                            skillrangeselectfilter[3] = EditorGUILayout.Toggle("超", skillrangeselectfilter[3], attackRangeToggleGUI);
                            EditorGUILayout.EndToggleGroup();
                            selectskillrarelevel = EditorGUILayout.IntPopup("技能rank:", selectskillrarelevel, skillrarelevelShow, skillrarelevels);
                            EditorGUILayout.LabelField(" &&&&&&&  以下将陈列根据条件删选出的技能  &&&&&&& ", title);
                            GUILayout.Space(20f);
                        }
    
                        RecordIDsAndNames _SkillRecordIDsAndNames = SkillConfigTable.getSkillIDAndNameArray(focusingtype, new bool[4] { skillrangeselectfilter[0], skillrangeselectfilter[1], skillrangeselectfilter[2], skillrangeselectfilter[3]}, selectskillrarelevel);
                        string[] SkillIDsOfType = _SkillRecordIDsAndNames.RecordIDs;
                        string[] SkillNamesOfType = _SkillRecordIDsAndNames.Names;

                        selectedskillindex = EditorGUILayout.Popup("技能：", selectedskillindex, SkillNamesOfType);
                        focusingSkillConfig.RECORD_ID = SkillIDsOfType[selectedskillindex];
                        SkillConfig defaultSkillConfig = SkillConfigTable.getSkillConfigByID(focusingSkillConfig.RECORD_ID);
                        if (defaultSkillConfig == null)
                        {
                            Debug.Log("技能读取严重错误。RECORD_ID："+ focusingSkillConfig.RECORD_ID);
                            return;
                        }
                        GUILayout.Space(10f);
    
                        if (focusingSkillConfig.RECORD_ID != null)
                        {
                            focusingSkillConfig.stateType = (stateType)EditorGUILayout.EnumPopup("Attack Type",
                                                                                         (focusingSkillConfig.stateType == stateType.NONE && defaultSkillConfig != null && defaultSkillConfig.stateType != stateType.NONE)
                                                                                         ?
                                                                                         defaultSkillConfig.stateType : focusingSkillConfig.stateType);
    
                            focusingSkillConfig.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT",
                                                                              (defaultSkillConfig != null)
                                                                              ?
                                                                              defaultSkillConfig.ATTACK_WEIGHT : focusingSkillConfig.ATTACK_WEIGHT);
    
                            focusingSkillConfig.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel",
                                                                                        (focusingSkillConfig.SP_LEVEL == -1 && defaultSkillConfig != null)
                                                                                        ?
                                                                                        defaultSkillConfig.SP_LEVEL : focusingSkillConfig.SP_LEVEL,
                                                                                        exoptions_display,exoptions);

                            bool far = false, near = false, close = false, outrange = false;
                            foreach (behaviorEnterRange _behaviorEnterRange in defaultSkillConfig.ai_trigger_ranges)
                            {
                                switch (_behaviorEnterRange)
                                {
                                    case behaviorEnterRange.inner_range:
                                        close = true;
                                        break;
                                    case behaviorEnterRange.mid_range:
                                        near = true;
                                        break;
                                    case behaviorEnterRange.far_range:
                                        far = true;
                                        break;
                                    case behaviorEnterRange.out_of_range:
                                        outrange = true;
                                        break;
                                }
                            }
    
                            GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
                            GUILayout.Space(5f);
                            EditorGUILayout.LabelField("AI模式技能触发范围");
                            close = EditorGUILayout.Toggle("近", close, attackRangeToggleGUI);
                            near = EditorGUILayout.Toggle("中", near, attackRangeToggleGUI);
                            far = EditorGUILayout.Toggle("远", far, attackRangeToggleGUI);
                            GUILayout.Space(5f);
                            GUI.backgroundColor = Color.white;
    
                            List<behaviorEnterRange> _finalranges = new List<behaviorEnterRange>();
                            if (outrange) _finalranges.Add(behaviorEnterRange.out_of_range);
                            if (far) _finalranges.Add(behaviorEnterRange.far_range);
                            if (near) _finalranges.Add(behaviorEnterRange.mid_range);
                            if (close) _finalranges.Add(behaviorEnterRange.inner_range);
                            focusingSkillConfig.ai_trigger_ranges = _finalranges.ToArray();
                        }
                    }
    
                }
                if (focusingCharInfo != null && focusingCharInfo._NineAndTwo != null)
                    focusingCharInfo._NineAndTwo.refreshSkillNumsByConfigs();
                /////// 九宫格end //////////
                /// 
                EditorGUILayout.LabelField(" 可将当前编辑中的角色...:  ", big_title);
                GUILayout.BeginHorizontal();
                ButtonStyle.fixedWidth = 150f;
                ButtonStyle.normal.textColor = Color.black;
                if (GUILayout.Button("AddToReward", ButtonStyle))
                {
                    if (_stagesManager._FightReward._CharacterDataInfos == null)
                        _stagesManager._FightReward._CharacterDataInfos = new CharacterDataInfo[] { };
    
                    if (focusingCharInfo != null)
                    {
                        CharacterDataInfo award = new CharacterDataInfo
                            (
                                focusingCharInfo.monsterOfPlayerId,
                                focusingCharInfo.monsterId,
                                focusingCharInfo._NineAndTwo.DeepCopy()
                            );
    
                        List<CharacterDataInfo> toList = _stagesManager._FightReward._CharacterDataInfos.ToList();
                        toList.Add(award);
                        _stagesManager._FightReward._CharacterDataInfos = toList.ToArray();
                    }
                }
                if (GUILayout.Button("AddToLocalDebugAccount(本功能已失效)", ButtonStyle))
                {
                    if (focusingCharInfo != null)
                        addOneCharToDebugLocalCoustomFile(focusingCharInfo.getCharacterDataInfoJson());
                }
                GUILayout.EndHorizontal();
            }
        }
        
        GUILayout.Space(5f);
        GUI.backgroundColor = new Color(0.7f,0.8f,0.8f);
        EditorGUILayout.LabelField(" 胜利报酬信息  ",big_title);
        _stagesManager._FightReward.diamond = EditorGUILayout.IntField("钻石", _stagesManager._FightReward.diamond);
        _stagesManager._FightReward.intell = EditorGUILayout.IntField("智慧果实", _stagesManager._FightReward.intell);
        GUILayout.Space(20);
        EditorGUILayout.LabelField(" 人员类奖励 (代码里存在一个关于传递值和传递地址的基本编程问题没解决，我们暂时不管他) ", title);
        if (_stagesManager._FightReward._CharacterDataInfos != null)
        {
            CharacterDataInfo toDelete = null;
            foreach (CharacterDataInfo _one in _stagesManager._FightReward._CharacterDataInfos)
            {
                CharacterResourceInfo _CharacterResourceInfo =
                monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(_one.monsterId.ToString()));
                if (_CharacterResourceInfo != null)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(_CharacterResourceInfo.REAL_NAME, ButtonStyle))
                    {
                        focusingCharInfo = _one;
                    }
                    if (GUILayout.Button("delete", addDeleteMember))
                    {
                        toDelete = _one;
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5);
                }else{
                    toDelete = _one;
                    continue;
                }
    
            }
            if (toDelete != null)
            {
                List<CharacterDataInfo> list = _stagesManager._FightReward._CharacterDataInfos.ToList();
                list.Remove(toDelete);
                _stagesManager._FightReward._CharacterDataInfos = list.ToArray();
            }
        }
    
        GUILayout.Space(10f);
        EditorGUILayout.LabelField(" 如何处理当前编辑中的战斗关卡信息  ", big_title);
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存战斗关卡至本地文档",ButtonStyle_save))
        {
            _stagesManager.saveFightAsXml(pathAndNameForLocalSave,_stagesManager.editoringFight);
        }
        if (GUILayout.Button("保存战斗报酬至本地文档",ButtonStyle_save))
        {
            _stagesManager.saveFightRwardAsXml(pathAndNameForLocalSave,_stagesManager._FightReward);
        }
        GUILayout.EndHorizontal();
    }
}
#endif