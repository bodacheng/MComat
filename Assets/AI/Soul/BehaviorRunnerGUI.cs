#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Soul;
using Skill;

[CustomEditor(typeof(BehaviorRunner))]
public class BehaviorRunnerGUI : Editor {

    BehaviorRunner myScript;

    bool GUIIniDone;
    GUIStyle ButtonStyle;
    GUIStyle addCasualToButtonStyle,deleteCasualToButtonStyle;
    GUIStyle stateKeyGUI;
    GUIStyle attackRangeToggleGUI;

    
    string[] StateIndexListOptions;
    string[] casualToStateKeyOptions;

    string targetType;
    List<string> casualToStateKeyOptionsList;

    Behaviors_Incubator_ForLocalResourceCheck _States_Incubator_ForLocalResourceCheck;
    bool LocalResourceReferenceMode;
    readonly int[] exoptions = { 0, 1, 2, 3 };
    readonly string[] exoptions_display = {"normal","ex1","ex2","ex3"};

    public override void OnInspectorGUI()
    {
        if (GUIIniDone)
        {
            ButtonStyle = new GUIStyle(GUI.skin.button);
            ButtonStyle.normal.textColor = Color.white;
            ButtonStyle.fixedWidth = 100f;
            
            addCasualToButtonStyle = new GUIStyle(GUI.skin.box);
            addCasualToButtonStyle.normal.textColor = Color.red;
            addCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
            addCasualToButtonStyle.margin = new RectOffset(100, 22, 11, 11);
            
            deleteCasualToButtonStyle = new GUIStyle(GUI.skin.box);
            deleteCasualToButtonStyle.normal.textColor = Color.blue;
            deleteCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
            deleteCasualToButtonStyle.margin = new RectOffset(50, 22, 11, 11);
            
            GUIIniDone = true;
        }
      
        LocalResourceReferenceMode = EditorGUILayout.Toggle("本地资源参照模式",LocalResourceReferenceMode);
        
		myScript = (BehaviorRunner)target;
        
        targetType = EditorGUILayout.TextField("targetType: ", targetType);
        
        if (myScript.GetNowState() != null)
        {
            EditorGUILayout.TextField("current: ", myScript.GetNowState().StateKey);
        }
        
        if (GUILayout.Button(" refresh skill define "))
        {
            _States_Incubator_ForLocalResourceCheck = LocalResourceReferenceMode ? 
            new Behaviors_Incubator_ForLocalResourceCheck(targetType) : new Behaviors_Incubator_ForLocalResourceCheck(targetType, myScript.SkillEntity_List);
        }
        
        if (_States_Incubator_ForLocalResourceCheck != null && _States_Incubator_ForLocalResourceCheck.BehaviorIndexList != null)
        {
            StateIndexListOptions = _States_Incubator_ForLocalResourceCheck.BehaviorIndexList.ToArray();
        }
        else
        {
            return;
        }
        
        EditorGUILayout.BeginVertical();
        myScript.AI_States_path = EditorGUILayout.TextField("AI_States_path", myScript.AI_States_path);
        EditorGUILayout.EndVertical();

        // --追加--
        if (myScript.SkillEntity_List != null)
        {
            if (!isInitialized) InitializeList(myScript.SkillEntity_List.Count);
        }else{
            myScript.SkillEntity_List = new List<SkillEntity>();
            if (!isInitialized) InitializeList(myScript.SkillEntity_List.Count);
        }
        // --ここまで--

        if (state_folding_list = EditorGUILayout.Foldout(state_folding_list, "States"))
        {
            casualToStateKeyOptionsList = new List<string>();
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < myScript.SkillEntity_List.Count; i++)
            {
                if (!casualToStateKeyOptionsList.Contains(myScript.SkillEntity_List[i].REAL_NAME))
                    casualToStateKeyOptionsList.Add(myScript.SkillEntity_List[i].REAL_NAME);

                EditorGUI.indentLevel++;

                if (stateKeyGUI == null)
                    stateKeyGUI = new GUIStyle(GUI.skin.label);
                stateKeyGUI.normal.textColor = new Color(0.6f, 0.3f, 0.4f);
                myScript.SkillEntity_List[i].REAL_NAME =
                StateIndexListOptions.Contains(myScript.SkillEntity_List[i].REAL_NAME) ?
                StateIndexListOptions[EditorGUILayout.Popup("State Key", Array.IndexOf(StateIndexListOptions, myScript.SkillEntity_List[i].REAL_NAME), StateIndexListOptions, stateKeyGUI)] :
                StateIndexListOptions.Length > 0 ? StateIndexListOptions[0] : null;

                myScript.SkillEntity_List[i].StateType =
                (BehaviorType)EditorGUILayout.EnumPopup("Attack Type", myScript.SkillEntity_List[i].StateType);

                if (myScript.SkillEntity_List[i].StateType != BehaviorType.NONE || myScript.SkillEntity_List[i].StateType != BehaviorType.MV ||
                        myScript.SkillEntity_List[i].StateType != BehaviorType.Def || myScript.SkillEntity_List[i].StateType != BehaviorType.Hit ||
                            myScript.SkillEntity_List[i].StateType != BehaviorType.KnockOff)
                {
                    GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
                    if (attackRangeToggleGUI == null)
                    {
                        attackRangeToggleGUI = new GUIStyle(GUI.skin.toggle)
                        {
                            margin = new RectOffset(50, 22, 11, 11)
                        };
                        attackRangeToggleGUI.alignment = TextAnchor.MiddleLeft;
                        attackRangeToggleGUI.stretchWidth = false;
                    }

                    myScript.SkillEntity_List[i].AI_MIN_DIS = EditorGUILayout.FloatField("Distance Min",myScript.SkillEntity_List[i].AI_MIN_DIS);
                    myScript.SkillEntity_List[i].AI_MAX_DIS = EditorGUILayout.FloatField("Distance Max",myScript.SkillEntity_List[i].AI_MAX_DIS);
                    
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.BeginVertical();
                if (casualToFoldings[i] = EditorGUILayout.Foldout(casualToFoldings[i], " ****************** Casual To States ******************"))
                {
                    for (int y = 0; y < myScript.SkillEntity_List[i].CasualTo.Length; y++)
                    {
                        EditorGUI.indentLevel++;
                        if (casualToStateKeyOptions.Contains(myScript.SkillEntity_List[i].CasualTo[y]))
                        {
                            stateKeyGUI.normal.textColor = new Color(0.2f, 0.7f, 0.5f);                        
                            myScript.SkillEntity_List[i].CasualTo[y] =
                            casualToStateKeyOptions[EditorGUILayout.Popup(
                            "Casual To State Key",
                            Array.IndexOf(casualToStateKeyOptions, myScript.SkillEntity_List[i].CasualTo[y]),
                            casualToStateKeyOptions,
                            stateKeyGUI)];
                        }
                        else
                        {
                            myScript.SkillEntity_List[i].CasualTo[y] = casualToStateKeyOptions[0];
                        }
                        
                        stateKeyGUI.normal.textColor = new Color(0.6f, 0.3f, 0.4f);
                        deleteCasualToButtonStyle = new GUIStyle(GUI.skin.box);
                        deleteCasualToButtonStyle.normal.textColor = Color.blue;
                        deleteCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
                        deleteCasualToButtonStyle.margin = new RectOffset(50, 22, 11, 11);
                        if (GUILayout.Button("DeleteThis", deleteCasualToButtonStyle))
                        {
                            List<string> casualStateList = myScript.SkillEntity_List[i].CasualTo.ToList();
                            casualStateList.RemoveAt(y);
                            myScript.SkillEntity_List[i].CasualTo = casualStateList.ToArray();
                            EditorGUI.indentLevel--;
                            break;
                        }
                        EditorGUI.indentLevel--;
                    }
                    
                    addCasualToButtonStyle = new GUIStyle(GUI.skin.box);
                    addCasualToButtonStyle.normal.textColor = Color.red;
                    addCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
                    addCasualToButtonStyle.margin = new RectOffset(100, 22, 11, 11);

                    for (int z = 0; z < myScript.SkillEntity_List.Count;z++)
                    {
                        if (myScript.SkillEntity_List[z] != myScript.SkillEntity_List[i])
                        {
                            EditorGUI.indentLevel++;
                            if (GUILayout.Button("  +  " + myScript.SkillEntity_List[z].REAL_NAME, addCasualToButtonStyle))
                            {
                                List<string> casualStateList = myScript.SkillEntity_List[i].CasualTo.ToList();
                                casualStateList.Add(myScript.SkillEntity_List[z].REAL_NAME);
                                myScript.SkillEntity_List[i].CasualTo = casualStateList.ToArray();
                            }
                            EditorGUI.indentLevel--;
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                
                // 强制Force迁移
                EditorGUILayout.BeginVertical();
                if (forceToFoldings[i] = EditorGUILayout.Foldout(forceToFoldings[i], " !!! Force To States !!!"))
                {
                    try {
                        for (int y = 0; y < myScript.SkillEntity_List[i].ForcedTransitions.Length; y++)
                        {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("forceTo: ", myScript.SkillEntity_List[i].ForcedTransitions[y]);
                        EditorGUI.indentLevel--;
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                EditorGUILayout.EndVertical();
                
                myScript.SkillEntity_List[i].CANBECANCELLEDTO = EditorGUILayout.Toggle("superCancel", myScript.SkillEntity_List[i].CANBECANCELLEDTO);
                myScript.SkillEntity_List[i].EnterInput = (InputKey)EditorGUILayout.EnumPopup("enter input", myScript.SkillEntity_List[i].EnterInput);
                myScript.SkillEntity_List[i].ExitInput = (InputKey)EditorGUILayout.EnumPopup("exit input", myScript.SkillEntity_List[i].ExitInput);
                myScript.SkillEntity_List[i].SP_LEVEL = EditorGUILayout.IntPopup("SPLevel", myScript.SkillEntity_List[i].SP_LEVEL,exoptions_display,exoptions);
                GUI.backgroundColor = Color.blue;
                
                ButtonStyle = new GUIStyle(GUI.skin.button);
                ButtonStyle.normal.textColor = Color.white;
                ButtonStyle.fixedWidth = 100f;
                if (GUILayout.Button("Delete",ButtonStyle))
                {
                    myScript.SkillEntity_List.RemoveAt(i);
                    InitializeList(i, myScript.SkillEntity_List.Count);
                }
                GUI.backgroundColor = Color.white;
                EditorGUI.indentLevel--;
                GUILayout.Space(1f);
            }
            EditorGUILayout.EndVertical();
                    
            casualToStateKeyOptions = casualToStateKeyOptionsList.ToArray();
            
            if (GUILayout.Button("Add"))
            {
                GUI.color = Color.green;
                myScript.SkillEntity_List.Add(new SkillEntity("Empty", 0, 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0));
                InitializeList(-1, myScript.SkillEntity_List.Count);
            }
        }
        
        GUI.backgroundColor = Color.green; 
		if(GUILayout.Button("saveTrans"))
		{
			myScript.SaveTrans(targetType);
		}
        GUI.backgroundColor = Color.white; 
	}

    bool isInitialized;
    bool state_folding_list;
    bool[] casualToFoldings;
    bool[] forceToFoldings;
    bool[] foldings;

    // Listの長さを初期化
    void InitializeList(int count)
    {
        foldings = new bool[count];
        casualToFoldings = new bool[count];
        forceToFoldings = new bool[count];
        isInitialized = true;
    }

    // 指定した番号以外をキャッシュして初期化 (i = -1の時は全てキャッシュして初期化)
    void InitializeList(int i, int count)
    {
        bool[] foldings_temp = foldings;
        foldings = new bool[count];

        for (int k = 0, j = 0; k < count; k++)
        {
            if (i == j) j++;
            if (foldings_temp.Length - 1 < j) break;
            foldings[k] = foldings_temp[j++];
        }
        ////////////////////////////////////////////
        bool[] foldings_temp2 = casualToFoldings;
        casualToFoldings = new bool[count];

        for (int k = 0, j = 0; k < count; k++)
        {
            if (i == j) j++;
            if (foldings_temp2.Length - 1 < j) break;
            casualToFoldings[k] = foldings_temp2[j++];
        }
        ////////////////////////////////////////////
        bool[] foldings_temp3 = forceToFoldings;
        forceToFoldings = new bool[count];

        for (int k = 0, j = 0; k < count; k++)
        {
            if (i == j) j++;
            if (foldings_temp3.Length - 1 < j) break;
            forceToFoldings[k] = foldings_temp3[j++];
        }
    }
}
#endif
