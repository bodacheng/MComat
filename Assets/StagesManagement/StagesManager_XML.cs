using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class StagesManager : MonoBehaviour
{
    public FightMembers LoadOneLocalFight_XML(TextAsset Script)
    {
        FightMembers _localFight = new FightMembers();
        
        MultiDict<int, int, UnitInfo>.SerializableSet[] targetValue;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MultiDict<int, int, UnitInfo>.SerializableSet[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    targetValue = serializer.Deserialize(textReader) as MultiDict<int, int, UnitInfo>.SerializableSet[];
                }
                Debug.Log("读取了敌人战斗信息");
            }
            else
            {
                var reader = new StringReader(Script.text);
                targetValue = serializer.Deserialize(reader) as MultiDict<int, int, UnitInfo>.SerializableSet[];
                Debug.Log("读取了敌人战斗信息");
            }
            
            #if UNITY_EDITOR
            string _path = AssetDatabase.GetAssetPath(Script);
            string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
            _path = _path.Length > 1 ? pathsplit[1] : pathsplit[0];
            Debug.Log("4V4模式文件" + _path);
            #endif
            
            _localFight.EnemySets._SerializableSets = targetValue;
            _localFight.EnemySets.ConvertSerializableArrayToDictionary();
            return _localFight;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }

    public void SaveFightAsXml(string path, FightMembers localFight)
    {
        if (localFight == null)
        {
            return;
        }
        MultiDict<int, int, UnitInfo> UnNullDic = new MultiDict<int, int, UnitInfo>();
        foreach (MultiDict<int, int, UnitInfo>.SerializableSet sets in localFight.EnemySets._SerializableSets)
        {
            UnNullDic.Set(sets.key1, sets.key2 , sets.value);
        }

        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(MultiDict<int, int, UnitInfo>.SerializableSet[]));
            FileStream FileStream;
            FileStream = new FileStream(Application.dataPath + "/" + path, FileMode.Create);
            XmlSerializer.Serialize(FileStream, UnNullDic._SerializableSets);
            Debug.Log(Application.dataPath + path + " 尝试进行关卡存储");
            FileStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("战斗信息保存失败");
            Debug.Log(e.ToString());
        }
    }
}
