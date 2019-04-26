#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

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
    
    int focusingMemberLocalID = 0;
    MoveType moveType;
    RushType rushType;
    bool canDefend;
    
    CharacterDataInfo focusingCharInfo = null;
    CharacterResourceInfo focusingCharResourceInfo = null;
    CharacterDataInfo freeEditCharInfo;
    SkillConfig focusingSkillConfig = null;
    List<SkillConfig> AllSkillsConfigs;
    
    bool skillselectfilter = false;
    bool filterallranges = true;
    bool[] skillrangeselectfilter = new bool[4] { true, true, true,true };//close,near,far,out
    
    int selectskillrarelevel = -1;
    int[] skillrarelevels = {-1,0,1,2,3};
    string[] skillrarelevelShow = {"ALL","0", "★", "★★", "★★★"};
    string focusingtype;

    void addOneCharToDebugLocalCoustomFile(CharacterDataInfo _CharInfo)
    {
        AccountCharsSet.Instance.loadMyOwnedCharsInfoViaJsonFile("myownedCharsJson.json");
        AccountCharsSet.addNewCharToJsonSaveData(_CharInfo);
    }

    public override void OnInspectorGUI()
    {
        CharsManager.loadMonsterDataBaseFileByResource();
        CharsManager.refreshCharacterResourceInfoDic();
    
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
    
        //第一步，读取配置文件。
        MySkillStonesReader.loadAllSkillConfigFromLocalConfigFile();
        MySkillStonesReader.refreshSkillConfigDicForReference();
    
        GUILayout.Space(5f);
        EditorGUILayout.LabelField(" 模块基本配件  ", big_title);
        _stagesManager._CharsManager = EditorGUILayout.ObjectField("CharsManager", _stagesManager._CharsManager, typeof(CharsManager), true) as CharsManager;
        _stagesManager._SkillStonesBox = EditorGUILayout.ObjectField("SkillStonesBox", _stagesManager._SkillStonesBox, typeof(SkillStonesBox), true) as SkillStonesBox;
    
        GUILayout.Space(10);
        EditorGUILayout.LabelField(" 战斗脚本读取  ", big_title);
        GUILayout.BeginHorizontal();
        _stagesManager.FightScript =
            EditorGUILayout.ObjectField("Read Fight Script", _stagesManager.FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (_stagesManager.FightScript != null)
            {
                LocalFight one = _stagesManager.loadOneLocalFight(_stagesManager.FightScript);
                if (one != null)
                {
                    _stagesManager.editoringFight = one;

                    foreach (CharacterDataInfo _one in _stagesManager.editoringFight.Enemies)
                    {
                        CharacterResourceInfo _CharacterResourceInfo =
                            CharsManager._monstersConfigTable.RowToCharacterResourceInfo(
                            CharsManager._monstersConfigTable.Find_ID(_one.resource_num.ToString())
                        );
                        if (_one._NineAndTwo == null)
                            _one._NineAndTwo = new NineAndTwo();
                        else
                            _one._NineAndTwo.sortNineAndTwo(_CharacterResourceInfo.getPassiveSkillConfigs());
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
        CharsManager._monstersConfigTable = new monstersConfigTable();
        TextAsset CSV = Resources.Load("Account/MonstersConfig") as TextAsset;
        if (CSV)
            CharsManager._monstersConfigTable.Load(CSV);
        else
            Debug.Log("没能读取到角色数据库文件。");
        GUILayout.Space(10);
        
        EditorGUILayout.LabelField(" stage信息  ", big_title);
        _stagesManager.editoringFight.BattleGroundID = EditorGUILayout.IntField("场景ID:", _stagesManager.editoringFight.BattleGroundID);
        GUILayout.Space(5f);
    
        _stagesManager.editoringFight.EntryMemberNum = EditorGUILayout.IntField("入场人数",_stagesManager.editoringFight.EntryMemberNum);
        _stagesManager.editoringFight.EntryMemberNum = (int)Mathf.Clamp(_stagesManager.editoringFight.EntryMemberNum, 1f, 4f);
    
        EditorGUILayout.LabelField(" 关卡敌人信息  ", title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("FreeEdit", (focusingMemberLocalID != -1) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberLocalID = -1;
            if (freeEditCharInfo == null)
            {
                freeEditCharInfo = new CharacterDataInfo();
                freeEditCharInfo.localID = 0;
                freeEditCharInfo._NineAndTwo = new NineAndTwo();
            }
            focusingCharInfo = freeEditCharInfo;
        }
        GUILayout.EndHorizontal();
    
        // 四站位 //
        GUILayout.BeginHorizontal();
        GUILayout.Space(100);
        if (GUILayout.Button("back", (focusingMemberLocalID != 0)?ButtonStyle:ButtonStyle_selected))
        {
            focusingMemberLocalID = 0;
            focusingCharInfo = _stagesManager.editoringFight.getCharacterDataInfoByLocalID(focusingMemberLocalID);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("right",(focusingMemberLocalID != 3) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberLocalID = 3;
            focusingCharInfo = _stagesManager.editoringFight.getCharacterDataInfoByLocalID(focusingMemberLocalID);
        }
        if (GUILayout.Button("front", (focusingMemberLocalID != 2) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberLocalID = 2;
            focusingCharInfo = _stagesManager.editoringFight.getCharacterDataInfoByLocalID(focusingMemberLocalID);
        }
        if (GUILayout.Button("left",(focusingMemberLocalID != 1) ? ButtonStyle : ButtonStyle_selected))
        {
            focusingMemberLocalID = 1;
            focusingCharInfo = _stagesManager.editoringFight.getCharacterDataInfoByLocalID(focusingMemberLocalID);
        }
        GUILayout.EndHorizontal();
        // 四站位end //
    
        // 指定站位人员的添加与删除 //
        GUILayout.BeginHorizontal();
        if (focusingCharInfo == null)
        {
            if (GUILayout.Button("Add", addDeleteMember))
            {
                focusingCharInfo = new CharacterDataInfo();
                focusingCharInfo.localID = focusingMemberLocalID;
                List<CharacterDataInfo> clist = _stagesManager.editoringFight.Enemies.ToList();
                clist.Add(focusingCharInfo);
                _stagesManager.editoringFight.Enemies = clist.ToArray();
            }
        }
        if (GUILayout.Button("Delete", addDeleteMember))
        {
            if (focusingCharInfo != null)
            {
                List<CharacterDataInfo> clist = _stagesManager.editoringFight.Enemies.ToList();
                if (clist.Contains(focusingCharInfo))
                    clist.Remove(focusingCharInfo);
                _stagesManager.editoringFight.Enemies = clist.ToArray();
                focusingCharInfo.Dissolve();
            }
            focusingCharInfo = null;
        }
        GUILayout.EndHorizontal();
        // 指定站位人员的添加与删除end //
    
        if (!(focusingCharInfo == null || focusingCharInfo.localID < 0))
        {
            // 角色type指定 //
            focusingCharResourceInfo = CharsManager._monstersConfigTable.RowToCharacterResourceInfo(
                            CharsManager._monstersConfigTable.Find_ID(focusingCharInfo.resource_num.ToString()));
            if (focusingCharInfo.resource_num != -1)
            {
                focusingtype = EditorGUILayout.TextField("characerType", focusingCharResourceInfo.type);
            }
            else
            {
                focusingtype = EditorGUILayout.TextField("characerType", focusingtype);
            }
            // 角色type指定end //
    
            int[] charResourceNums = CharsManager._monstersConfigTable.getIDList(focusingtype).ToArray();
            string[] charResourceNames = CharsManager._monstersConfigTable.getRealNameList(focusingtype).ToArray();
    
            if (focusingCharInfo != null && charResourceNums != null && charResourceNames != null && focusingtype != null && charResourceNums.Length != 0)
            {
                focusingCharInfo.resource_num =
                                  EditorGUILayout.IntPopup("角色名：", focusingCharInfo.resource_num, charResourceNames, charResourceNums);
    
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
                if (focusingCharInfo._NineAndTwo.getA1Config() == null || focusingCharInfo._NineAndTwo.getA1Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("A1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getA1Config() ?
                                     ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getA1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getA2Config() == null || focusingCharInfo._NineAndTwo.getA2Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("A2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getA2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getA2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getA3Config() == null || focusingCharInfo._NineAndTwo.getA3Config().id < 0)
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
    
                if (focusingCharInfo._NineAndTwo.getB1Config() == null || focusingCharInfo._NineAndTwo.getB1Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("B1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getB1Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getB1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getB2Config() == null || focusingCharInfo._NineAndTwo.getB2Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("B2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getB2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getB2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getB3Config() == null || focusingCharInfo._NineAndTwo.getB3Config().id < 0)
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
    
                if (focusingCharInfo._NineAndTwo.getC1Config() == null || focusingCharInfo._NineAndTwo.getC1Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("C1", focusingSkillConfig != focusingCharInfo._NineAndTwo.getC1Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getC1Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getC2Config() == null || focusingCharInfo._NineAndTwo.getC2Config().id < 0)
                    GUI.backgroundColor = Color.white;
                else
                    GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("C2", focusingSkillConfig != focusingCharInfo._NineAndTwo.getC2Config() ?
                                         ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
                {
                    focusingSkillConfig = focusingCharInfo._NineAndTwo.getC2Config();
                }
    
                if (focusingCharInfo._NineAndTwo.getC3Config() == null || focusingCharInfo._NineAndTwo.getC3Config().id < 0)
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
                        moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", moveType);
                        SanGong = true;
                    }
                    if (focusingSkillConfig == focusingCharInfo._NineAndTwo.getDConfig())
                    {
                        canDefend = EditorGUILayout.Toggle("有防御技能", canDefend);
                        SanGong = true;
                    }
                    if (focusingSkillConfig == focusingCharInfo._NineAndTwo.getRConfig())
                    {
                        rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", rushType);
                        SanGong = true;
                    }
                    GUILayout.Space(10f);
                    if (!SanGong)
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
    
                        SkillIDsAndNames _SkillIDsAndNames =
                                MySkillStonesReader.getSkillIDAndNameArray(focusingtype,
                                       new bool[4] { skillrangeselectfilter[0], skillrangeselectfilter[1], skillrangeselectfilter[2],skillrangeselectfilter[3] }, selectskillrarelevel);
                        int[] SkillIDsOfType = _SkillIDsAndNames.IDs;
                        string[] SkillKeysOfType = _SkillIDsAndNames.SkillNames;
    
                        focusingSkillConfig.id = EditorGUILayout.IntPopup("技能：", focusingSkillConfig.id, SkillKeysOfType, SkillIDsOfType);
                        SkillConfig defaultSkillConfig = MySkillStonesReader.getSkillConfigByID(focusingSkillConfig.id);
                        GUILayout.Space(10f);
    
                        if (focusingSkillConfig.id != -1)
                        {
                            focusingSkillConfig.stateType =
                                                   (stateType)EditorGUILayout.EnumPopup("Attack Type",
                                                                                         (focusingSkillConfig.stateType == stateType.NONE && defaultSkillConfig != null && defaultSkillConfig.stateType != stateType.NONE)
                                                                                         ?
                                                                                         defaultSkillConfig.stateType : focusingSkillConfig.stateType);
    
                            focusingSkillConfig.SkillPoint = EditorGUILayout.IntField("SkillPoint",
                                                                                      (focusingSkillConfig.SkillPoint < 0 && defaultSkillConfig != null)
                                                                                      ?
                                                                                      defaultSkillConfig.SkillPoint : focusingSkillConfig.SkillPoint);
    
                            focusingSkillConfig.SPLevel = (EX)EditorGUILayout.EnumPopup("SPLevel",
                                                                                        (focusingSkillConfig.SPLevel == EX.NULL && defaultSkillConfig != null)
                                                                                        ?
                                                                                        defaultSkillConfig.SPLevel : focusingSkillConfig.SPLevel);
    
                            focusingSkillConfig.canAirTrigger = EditorGUILayout.Toggle("CanAirTrigger(暂时全部不行)",
                                                                                       (defaultSkillConfig != null)
                                                                                       ?
                                                                                       defaultSkillConfig.canAirTrigger : focusingSkillConfig.canAirTrigger);

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
                                focusingCharInfo.localID,
                                focusingCharInfo.resource_num,
                                focusingCharInfo._NineAndTwo.DeepCopy()
                            );
    
                        List<CharacterDataInfo> toList = _stagesManager._FightReward._CharacterDataInfos.ToList();
                        toList.Add(award);
                        _stagesManager._FightReward._CharacterDataInfos = toList.ToArray();
                    }
                }
                if (GUILayout.Button("AddToLocalDebugAccount", ButtonStyle))
                {
                    if (focusingCharInfo != null)
                        addOneCharToDebugLocalCoustomFile(focusingCharInfo);
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
                CharsManager._monstersConfigTable.RowToCharacterResourceInfo(
                CharsManager._monstersConfigTable.Find_ID(_one.resource_num.ToString())
                );
    
                if (_CharacterResourceInfo != null)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(_CharacterResourceInfo.prefabName, ButtonStyle))
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