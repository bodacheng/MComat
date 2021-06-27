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
        
        MultiDict<int, int, CharDataInfo>.SerializableSets[] targetValue;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MultiDict<int, int, CharDataInfo>.SerializableSets[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    targetValue = serializer.Deserialize(textReader) as MultiDict<int, int, CharDataInfo>.SerializableSets[];
                }
                Debug.Log("读取了敌人战斗信息");
            }
            else
            {
                var reader = new StringReader(Script.text);
                targetValue = serializer.Deserialize(reader) as MultiDict<int, int, CharDataInfo>.SerializableSets[];
                Debug.Log("读取了敌人战斗信息");
            }
            
            #if UNITY_EDITOR
            string _path = AssetDatabase.GetAssetPath(Script);
            string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
            _path = _path.Length > 1 ? pathsplit[1] : pathsplit[0];
            Debug.Log("4V4模式文件" + _path);
            fightScriptPath = _path;
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
        MultiDict<int, int, CharDataInfo> UnNullDic = new MultiDict<int, int, CharDataInfo>();
        foreach (MultiDict<int, int, CharDataInfo>.SerializableSets sets in localFight.EnemySets._SerializableSets)
        {
            for (int i = 0; i < sets.value.Length;i++)
            {
                List<CharDataInfo> unNullValues = new List<CharDataInfo>();
                if (!String.IsNullOrEmpty(sets.value[i]._Value.ResourceID))
                {
                    UnNullDic.Set(sets.key1,sets.value[i]._Key2,sets.value[i]._Value);
                }
            }
        }

        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(MultiDict<int, int, CharDataInfo>.SerializableSets[]));
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
