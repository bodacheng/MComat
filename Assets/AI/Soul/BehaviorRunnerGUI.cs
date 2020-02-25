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
            new Behaviors_Incubator_ForLocalResourceCheck(targetType) : new Behaviors_Incubator_ForLocalResourceCheck(targetType, myScript.State_Transition_Set_List);
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
        if (myScript.State_Transition_Set_List != null)
        {
            if (!isInitialized) InitializeList(myScript.State_Transition_Set_List.Count);
        }else{
            myScript.State_Transition_Set_List = new List<Behavior_Transition_Set>();
            if (!isInitialized) InitializeList(myScript.State_Transition_Set_List.Count);
        }
        // --ここまで--

        if (state_folding_list = EditorGUILayout.Foldout(state_folding_list, "States"))
        {
            casualToStateKeyOptionsList = new List<string>();
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < myScript.State_Transition_Set_List.Count; i++)
            {
                if (!casualToStateKeyOptionsList.Contains(myScript.State_Transition_Set_List[i].StateKey))
                    casualToStateKeyOptionsList.Add(myScript.State_Transition_Set_List[i].StateKey);

                EditorGUI.indentLevel++;

                if (stateKeyGUI == null)
                    stateKeyGUI = new GUIStyle(GUI.skin.label);
                stateKeyGUI.normal.textColor = new Color(0.6f, 0.3f, 0.4f);
                myScript.State_Transition_Set_List[i].StateKey =
                StateIndexListOptions.Contains(myScript.State_Transition_Set_List[i].StateKey) ?
                StateIndexListOptions[EditorGUILayout.Popup("State Key", Array.IndexOf(StateIndexListOptions, myScript.State_Transition_Set_List[i].StateKey), StateIndexListOptions, stateKeyGUI)] :
                StateIndexListOptions.Length > 0 ? StateIndexListOptions[0] : null;

                myScript.State_Transition_Set_List[i].stateType =
                (BehaviorType)EditorGUILayout.EnumPopup("Attack Type", myScript.State_Transition_Set_List[i].stateType);

                if (myScript.State_Transition_Set_List[i].stateType != BehaviorType.NONE)
                {
                    List<BehaviorEnterRange> _ranges = myScript.State_Transition_Set_List[i].AI_trigger_ranges == null ? new List<BehaviorEnterRange>() : myScript.State_Transition_Set_List[i].AI_trigger_ranges.ToList();
                    bool outrange, far, near, close;
                    outrange = _ranges.Contains(BehaviorEnterRange.out_of_range);
                    far = _ranges.Contains(BehaviorEnterRange.far_range);
                    near = _ranges.Contains(BehaviorEnterRange.mid_range);
                    close = _ranges.Contains(BehaviorEnterRange.inner_range);

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
                    outrange = EditorGUILayout.Toggle("超", outrange, attackRangeToggleGUI);
                    far = EditorGUILayout.Toggle("远", far, attackRangeToggleGUI);
                    near = EditorGUILayout.Toggle("中", near, attackRangeToggleGUI);
                    close = EditorGUILayout.Toggle("近", close, attackRangeToggleGUI);
                    GUI.backgroundColor = Color.white;
                    
                    List<BehaviorEnterRange> _finalranges = new List<BehaviorEnterRange>();
                    if (outrange) _finalranges.Add(BehaviorEnterRange.out_of_range);
                    if (far) _finalranges.Add(BehaviorEnterRange.far_range);
                    if (near) _finalranges.Add(BehaviorEnterRange.mid_range);
                    if (close) _finalranges.Add(BehaviorEnterRange.inner_range);
                    myScript.State_Transition_Set_List[i].AI_trigger_ranges = _finalranges.ToArray();
                }else{
                    myScript.State_Transition_Set_List[i].AI_trigger_ranges = null;
                }
                
                EditorGUILayout.BeginVertical();
                if (casualToFoldings[i] = EditorGUILayout.Foldout(casualToFoldings[i], " ****************** Casual To States ******************"))
                {
                    if (myScript.State_Transition_Set_List[i].Casual_To_Behaviours == null)
                    {
                        myScript.State_Transition_Set_List[i].Casual_To_Behaviours = new Behavior_Transition_Set[]{};
                    }
                    
                    for (int y = 0; y < myScript.State_Transition_Set_List[i].Casual_To_Behaviours.Length; y++)
                    {
                        EditorGUI.indentLevel++;
                        if (casualToStateKeyOptions.Contains(myScript.State_Transition_Set_List[i].Casual_To_Behaviours[y].StateKey))
                        {
                            stateKeyGUI.normal.textColor = new Color(0.2f, 0.7f, 0.5f);                        
                            myScript.State_Transition_Set_List[i].Casual_To_Behaviours[y].StateKey =
                            casualToStateKeyOptions[EditorGUILayout.Popup(
                            "Casual To State Key",
                            Array.IndexOf(casualToStateKeyOptions, myScript.State_Transition_Set_List[i].Casual_To_Behaviours[y].StateKey),
                            casualToStateKeyOptions,
                            stateKeyGUI)];
                        }
                        else
                        {
                            myScript.State_Transition_Set_List[i].Casual_To_Behaviours[y].StateKey = casualToStateKeyOptions[0];
                        }
                        
                        stateKeyGUI.normal.textColor = new Color(0.6f, 0.3f, 0.4f);
                        deleteCasualToButtonStyle = new GUIStyle(GUI.skin.box);
                        deleteCasualToButtonStyle.normal.textColor = Color.blue;
                        deleteCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
                        deleteCasualToButtonStyle.margin = new RectOffset(50, 22, 11, 11);
                        if (GUILayout.Button("DeleteThis", deleteCasualToButtonStyle))
                        {
                            List<Behavior_Transition_Set> casualStateList = myScript.State_Transition_Set_List[i].Casual_To_Behaviours.ToList();
                            casualStateList.RemoveAt(y);
                            myScript.State_Transition_Set_List[i].Casual_To_Behaviours = casualStateList.ToArray();
                            EditorGUI.indentLevel--;
                            break;
                        }
                        EditorGUI.indentLevel--;
                    }
                    
                    addCasualToButtonStyle = new GUIStyle(GUI.skin.box);
                    addCasualToButtonStyle.normal.textColor = Color.red;
                    addCasualToButtonStyle.alignment = TextAnchor.MiddleCenter;
                    addCasualToButtonStyle.margin = new RectOffset(100, 22, 11, 11);

                    for (int z = 0; z < myScript.State_Transition_Set_List.Count;z++)
                    {
                        if (myScript.State_Transition_Set_List[z] != myScript.State_Transition_Set_List[i])
                        {
                            EditorGUI.indentLevel++;
                            if (GUILayout.Button("  +  " + myScript.State_Transition_Set_List[z].StateKey, addCasualToButtonStyle))
                            {
                                List<Behavior_Transition_Set> casualStateList = myScript.State_Transition_Set_List[i].Casual_To_Behaviours.ToList();
                                casualStateList.Add(myScript.State_Transition_Set_List[z]);
                                myScript.State_Transition_Set_List[i].Casual_To_Behaviours = casualStateList.ToArray();
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
                        for (int y = 0; y < myScript.State_Transition_Set_List[i].forced_to_state_nums.Length; y++)
                        {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("forceTo: ", myScript.State_Transition_Set_List[i].forced_to_state_nums[y]);
                        EditorGUI.indentLevel--;
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                EditorGUILayout.EndVertical();
                
                myScript.State_Transition_Set_List[i].can_be_cancelled_to = EditorGUILayout.Toggle("superCancel", myScript.State_Transition_Set_List[i].can_be_cancelled_to);
                myScript.State_Transition_Set_List[i].enterInput = (Inputs_defined)EditorGUILayout.EnumPopup("enter input", myScript.State_Transition_Set_List[i].enterInput);
                myScript.State_Transition_Set_List[i].exitInput = (Inputs_defined)EditorGUILayout.EnumPopup("exit input", myScript.State_Transition_Set_List[i].exitInput);
                myScript.State_Transition_Set_List[i].SPLevel = EditorGUILayout.IntPopup("SPLevel", myScript.State_Transition_Set_List[i].SPLevel,exoptions_display,exoptions);
                GUI.backgroundColor = Color.blue;
                
                ButtonStyle = new GUIStyle(GUI.skin.button);
                ButtonStyle.normal.textColor = Color.white;
                ButtonStyle.fixedWidth = 100f;
                if (GUILayout.Button("Delete",ButtonStyle))
                {
                    myScript.State_Transition_Set_List.RemoveAt(i);
                    InitializeList(i, myScript.State_Transition_Set_List.Count);
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
                myScript.State_Transition_Set_List.Add(
                new Behavior_Transition_Set("Empty",
                BehaviorType.NONE,
                0,
                null,
                new Behavior_Transition_Set[0], 
                new string[0], 
                Inputs_defined.Null, 
                Inputs_defined.Null,
                0,
                0));
                InitializeList(-1, myScript.State_Transition_Set_List.Count);
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
