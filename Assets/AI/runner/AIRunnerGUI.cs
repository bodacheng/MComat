#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Soul;

[CustomEditor(typeof(AIStateRunner))]
public class AIRunnerGUI : Editor {

    AIStateRunner myScript;

    GUIStyle ButtonStyle;
    GUIStyle addCasualToButtonStyle,deleteCasualToButtonStyle;

    GUIStyle stateKeyGUI;
    GUIStyle attackRangeToggleGUI;

    string[] StateIndexListOptions;
    string[] casualToStateKeyOptions;

    List<string> casualToStateKeyOptionsList;

    States_Incubator_ForLocalResourceCheck _States_Incubator_ForLocalResourceCheck;
    bool LocalResourceReferenceMode = false;

    int[] exoptions = new int[] { 0, 1, 2, 3 };
    string[] exoptions_display = new string[] {"normal","ex1","ex2","ex3"};
    
    //6.14 casual to states里，一个状态的豪气虽然可以设置为不同于首发，但attack种类和范围是不可做设置的，因为这两个信息决定了一个状态的质，一个角色在跑大状态机的时候是从一个固定的状态字典里找状态，
    // 同样名字的状态不会有两个。但是，耗气是另一码事，因为我们的大状态机引擎只是参照了这个信息在状态原本的进入条件外额外作为一个触发条件对其进行“是否气足够”的判断。
    //首发时候我们用的耗气标准是状态首发时候的标准，而接续时候用的则是casual to states里的值，因此可以不一样。另外，牢记inspector上这个大列表无非是用以直观保存脚本，真正在战斗时候运行的是一个字典
	public override void OnInspectorGUI()
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

        stateKeyGUI = new GUIStyle(GUI.skin.label);
        stateKeyGUI.normal.textColor = new Color(0.6f,0.3f,0.4f);

        attackRangeToggleGUI = new GUIStyle(GUI.skin.toggle);
        attackRangeToggleGUI.margin = new RectOffset(50, 22, 11, 11);

        LocalResourceReferenceMode = EditorGUILayout.Toggle("本地资源参照模式",LocalResourceReferenceMode);

		myScript = (AIStateRunner)target;
        if (LocalResourceReferenceMode)
            _States_Incubator_ForLocalResourceCheck = new States_Incubator_ForLocalResourceCheck(myScript.characterType);
        else
            _States_Incubator_ForLocalResourceCheck = new States_Incubator_ForLocalResourceCheck(myScript.characterType,myScript.State_Transition_Set_List);
        if (_States_Incubator_ForLocalResourceCheck.StateIndexList != null)
        {
            StateIndexListOptions = _States_Incubator_ForLocalResourceCheck.StateIndexList.ToArray();
        }
        else
        {
            return;
        }//这个处理需要多个地方进行
        //DrawDefaultInspector();

        EditorGUILayout.TextField("current: ", myScript.getCurrentStateNum());
        EditorGUILayout.BeginVertical();
        myScript.AI_States_path = EditorGUILayout.TextField("AI_States_path", myScript.AI_States_path);
        EditorGUILayout.EndVertical();

        //Show(serializedObject.FindProperty("State_Transition_Set_List"),EditorListOption.All);

        // --追加--
        if (myScript.State_Transition_Set_List != null)
        {
            if (!isInitialized) InitializeList(myScript.State_Transition_Set_List.Count);
        }else{
            myScript.State_Transition_Set_List = new List<State_Transition_Set>();
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

                if (StateIndexListOptions.Contains<string>(myScript.State_Transition_Set_List[i].StateKey))
                {
                    myScript.State_Transition_Set_List[i].StateKey =
						        StateIndexListOptions[EditorGUILayout.Popup("State Key", 
						                                                    Array.IndexOf(StateIndexListOptions, myScript.State_Transition_Set_List[i].StateKey), 
						                                                    StateIndexListOptions, 
						                                                    stateKeyGUI
						                                                   )];
                }
                else
                {
                    if (StateIndexListOptions.Length > 0)
                        myScript.State_Transition_Set_List[i].StateKey = StateIndexListOptions[0];
                    else
                        myScript.State_Transition_Set_List[i].StateKey = null;
                }

                myScript.State_Transition_Set_List[i].stateType = 
                    (stateType)EditorGUILayout.EnumPopup("Attack Type", myScript.State_Transition_Set_List[i].stateType);

                if (myScript.State_Transition_Set_List[i].stateType != stateType.NONE)
                {
                    List<behaviorEnterRange> _ranges;
                    if (myScript.State_Transition_Set_List[i].ai_trigger_ranges == null)
                        _ranges = new List<behaviorEnterRange>();
                    else
                        _ranges = myScript.State_Transition_Set_List[i].ai_trigger_ranges.ToList();
                    bool outrange,far, near, close;
                    outrange = _ranges.Contains(behaviorEnterRange.out_of_range) ? true : false;
                    far = _ranges.Contains(behaviorEnterRange.far_range) ? true : false;
                    near = _ranges.Contains(behaviorEnterRange.mid_range) ? true : false;
                    close = _ranges.Contains(behaviorEnterRange.inner_range) ? true : false;

                    attackRangeToggleGUI.alignment = TextAnchor.MiddleLeft;
                    attackRangeToggleGUI.stretchWidth = false;
                    GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
                    
                    outrange = EditorGUILayout.Toggle("超", outrange, attackRangeToggleGUI);
                    far = EditorGUILayout.Toggle("远", far, attackRangeToggleGUI);
                    near = EditorGUILayout.Toggle("中", near, attackRangeToggleGUI);
                    close = EditorGUILayout.Toggle("近", close, attackRangeToggleGUI);
                    GUI.backgroundColor = Color.white;

                    List<behaviorEnterRange> _finalranges = new List<behaviorEnterRange>();
                    if (outrange) _finalranges.Add(behaviorEnterRange.out_of_range);
                    if (far) _finalranges.Add(behaviorEnterRange.far_range);
                    if (near) _finalranges.Add(behaviorEnterRange.mid_range);
                    if (close) _finalranges.Add(behaviorEnterRange.inner_range);
                    myScript.State_Transition_Set_List[i].ai_trigger_ranges = _finalranges.ToArray();
                }else{
                    myScript.State_Transition_Set_List[i].ai_trigger_ranges = null;
                }

                EditorGUILayout.BeginVertical();
                if (casualToFoldings[i] = EditorGUILayout.Foldout(casualToFoldings[i], " ****************** Casual To States ******************"))
                {
                    try
                    {
                        if (myScript.State_Transition_Set_List[i].casual_to_state_Sets != null)
                        {
                            for (int y = 0; y < myScript.State_Transition_Set_List[i].casual_to_state_Sets.Length; y++)
                            {
                                EditorGUI.indentLevel++;
                                if (casualToStateKeyOptions.Contains<string>(myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].AI_State_Number))
                                {
                                    stateKeyGUI.normal.textColor = new Color(0.2f, 0.7f, 0.5f);
                                    myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].AI_State_Number =
                                        casualToStateKeyOptions[EditorGUILayout.Popup(
                                                    "Casual To State Key",
                                                    Array.IndexOf(casualToStateKeyOptions, myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].AI_State_Number),
                                                    casualToStateKeyOptions,
                                                    stateKeyGUI
                                                   )];
                                }
                                else
                                {
                                    myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].AI_State_Number = casualToStateKeyOptions[0];
                                }
                                stateKeyGUI.normal.textColor = new Color(0.6f, 0.3f, 0.4f);
                                myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].can_be_cancelled_to = EditorGUILayout.Toggle("superCancel", myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].can_be_cancelled_to);
                                myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].enterInput = (inputs_defined)EditorGUILayout.EnumPopup("enter Input", myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].enterInput);
                                myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].exitInput = (inputs_defined)EditorGUILayout.EnumPopup("exit Input", myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].exitInput);


                                //State_Transition temp_state_Transition = null;
                                //if (state_Transition_Dictionary != null)
                                //{
                                //    state_Transition_Dictionary.TryGetValue(myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].AI_State_Number,out temp_state_Transition);
                                //}
                                //myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].SPLevel = (EX)EditorGUILayout.EnumPopup("SPLevel", (temp_state_Transition !=null)? temp_state_Transition.SPLevel : myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].SPLevel);
								myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].SPLevel = EditorGUILayout.IntPopup("SPLevel", myScript.State_Transition_Set_List[i].casual_to_state_Sets[y].SPLevel,exoptions_display,exoptions);
                                if (GUILayout.Button("DeleteThis", deleteCasualToButtonStyle))
                                {
                                    List<State_Rate_Set> casualStateList = myScript.State_Transition_Set_List[i].casual_to_state_Sets.ToList();
                                    casualStateList.RemoveAt(y);
                                    myScript.State_Transition_Set_List[i].casual_to_state_Sets = casualStateList.ToArray();
                                    EditorGUI.indentLevel--;
                                    break;
                                }
                                EditorGUI.indentLevel--;
                            }
                        }else{
                            
                        }

                        if (GUILayout.Button("  +  ",addCasualToButtonStyle))
                        {
                            List<State_Rate_Set> casualStateList = myScript.State_Transition_Set_List[i].casual_to_state_Sets.ToList();
                            casualStateList.Add(new State_Rate_Set("Empty",
                                                                   stateType.NONE,
                                                                   0,
                                                                   null,
                                                                   false,
                                                                   inputs_defined.Null,inputs_defined.Null,
                                                                   0,
                                                                   skillEmergentLevel.none));
                            myScript.State_Transition_Set_List[i].casual_to_state_Sets = casualStateList.ToArray();
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                EditorGUILayout.EndVertical();

                // 强制Force迁移
                EditorGUILayout.BeginVertical();
                if (forceToFoldings[i] = EditorGUILayout.Foldout(forceToFoldings[i], " !!! Force To States !!!"))
                {
                    try {
                        if (myScript.State_Transition_Set_List[i].forced_to_state_nums != null)
                        {
                            for (int y = 0; y < myScript.State_Transition_Set_List[i].forced_to_state_nums.Length; y++)
                            {
                                EditorGUI.indentLevel++;
                                EditorGUILayout.TextField("forceTo: ", myScript.State_Transition_Set_List[i].forced_to_state_nums[y]);
                                EditorGUI.indentLevel--;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                EditorGUILayout.EndVertical();
                myScript.State_Transition_Set_List[i].enterInput = (inputs_defined)EditorGUILayout.EnumPopup("enter input", myScript.State_Transition_Set_List[i].enterInput);
                myScript.State_Transition_Set_List[i].exitInput = (inputs_defined)EditorGUILayout.EnumPopup("exit input", myScript.State_Transition_Set_List[i].exitInput);
                myScript.State_Transition_Set_List[i].SPLevel = EditorGUILayout.IntPopup("SPLevel", myScript.State_Transition_Set_List[i].SPLevel,exoptions_display,exoptions);
                GUI.backgroundColor = Color.blue; 
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
                    new State_Transition_Set("Empty",
                                             stateType.NONE,
                                             0,
                                             null,
                                             new State_Rate_Set[0], 
                                             new string[0], 
                                             inputs_defined.Null, 
                                             inputs_defined.Null,
                                             0,
                                             skillEmergentLevel.none,
                                             0
                                            ));
                InitializeList(-1, myScript.State_Transition_Set_List.Count);
            }                       
        }

        GUI.backgroundColor = Color.green; 
		if(GUILayout.Button("saveTrans"))
		{
			myScript.saveTrans();
		}
        GUI.backgroundColor = Color.white; 
	}

    bool isInitialized = false;
    bool state_folding_list = false;
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
