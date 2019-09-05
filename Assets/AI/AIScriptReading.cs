using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Soul;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AIScriptReading {

    public static List<State_Transition_Set> readKongfuBook(AIStateRunner _AIStateRunner,TextAsset Script,string type,int AI_level)
    {
        try
        {
            List<State_Transition_Set> list = new List<State_Transition_Set>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<State_Transition_Set>));
            
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                #if UNITY_EDITOR
                string _path = AssetDatabase.GetAssetPath(Script);
                string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
                if (_path.Length > 1)
                {
                    _path = pathsplit[1];
                }
                else
                {
                    _path = pathsplit[0];
                }
                _AIStateRunner.AI_States_path = _path;
                #endif
                
                using (TextReader textReader = new StringReader(Script.text))
                {
                    list = serializer.Deserialize(textReader) as List<State_Transition_Set>;
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                var reader = new System.IO.StringReader(Script.text);
                list = serializer.Deserialize(reader) as List<State_Transition_Set>;
            }
            list = _AIStateRunner.sortStateTransitionSetList(list, type,AI_level);
            _AIStateRunner.usingScript = Script;
            _AIStateRunner.usingScriptLevel = AI_level;
            if (list == null)
            {
                list = new List<State_Transition_Set>() {
                new State_Transition_Set("Empty",
                                        stateType.NONE,
                                        0,
                                        null,
                                        null, null,
                                         enterInput: inputs_defined.Null, exitInput: inputs_defined.Null, 
                                         SPMove:0,
                                         skillEmergentLevel:skillEmergentLevel.none,
                                         rarelevel:0)
                };
            }else{
                if (list.Count == 0)
                {
                    list.Add(new State_Transition_Set(
                                   "Empty",
                                   stateType.NONE,
                                   0,
                                   null,
                                   null, null,
                                    enterInput: inputs_defined.Null, exitInput: inputs_defined.Null,
                                    SPMove: 0,
                                    skillEmergentLevel:skillEmergentLevel.none, 
                                    rarelevel: 0));
                }
            }
            return list;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("状态迁移信息读取失败,返回只有空状态的列表");
            Debug.Log(e.ToString());
            return new List<State_Transition_Set>() {
                new State_Transition_Set("Empty",
                                        stateType.NONE,
                                        0,
                                        null,
                                        null, null,
                                        enterInput: inputs_defined.Null, 
                                         exitInput: inputs_defined.Null, 
                                         SPMove:0,skillEmergentLevel:skillEmergentLevel.none,
                                         rarelevel:0)
                };
        }
    }
}
