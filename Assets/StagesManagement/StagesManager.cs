using UnityEngine;
using System.IO;
using System;
using dataAccess;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StagesManager : MonoBehaviour
{
    public string fightScriptPath;
    public TextAsset FightScript;//存档文件。是我们拖给这个位置的一个东西，但如果说这个文件不存在，那应该要自动新建并指定到这个位置上
    public LocalFight EditoringFight;

    public LocalFight LoadOneLocalFight_XML(TextAsset Script)
    {
        LocalFight _localFight = new LocalFight();
        
        MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[] targetValue;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    targetValue = serializer.Deserialize(textReader) as MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[];
                }
                Debug.Log("读取了敌人战斗信息");
            }
            else
            {
                var reader = new StringReader(Script.text);
                targetValue = serializer.Deserialize(reader) as MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[];
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

    public void SaveFightAsXml(string path, LocalFight localFight)
    {
        if (localFight == null)
        {
            return;
        }

        localFight.EnemySets.ConvertDictionaryToSerializableArray();
        MultiDictionary<int, int, CharacterDataInfo> UnNullDic = new MultiDictionary<int, int, CharacterDataInfo>();
        foreach (MultiDictionary<int, int, CharacterDataInfo>.SerializableSets sets in localFight.EnemySets._SerializableSets)
        {
            for (int i = 0; i < sets.value.Length;i++)
            {
                List<CharacterDataInfo> unNullValues = new List<CharacterDataInfo>();
                if (!String.IsNullOrEmpty(sets.value[i]._Value.ResourceID))
                {
                    UnNullDic.Set(sets.key1,sets.value[i]._Key2,sets.value[i]._Value);
                }
            }
        }
        UnNullDic.ConvertDictionaryToSerializableArray();
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[]));
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
    
    public LocalFight LoadOneLocalFight_Json(TextAsset Script)
    {
        LocalFight _localFight = new LocalFight();
        
        MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDictionary<int, int, CharacterDataInfo>.SerializableSets[]>(Script.text);
            #if UNITY_EDITOR
            string _path = AssetDatabase.GetAssetPath(Script);
            string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
            _path = _path.Length > 1 ? pathsplit[1] : pathsplit[0];
            Debug.Log("文件已读取" + _path);
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
    
    public void SaveFightAsJson(string path, LocalFight localFight)
    {
        if (localFight == null)
        {
            return;
        }
        
        localFight.EnemySets.ConvertDictionaryToSerializableArray();
        
        MultiDictionary<int, int, CharacterDataInfo> UnNullDic = new MultiDictionary<int, int, CharacterDataInfo>();
        foreach (MultiDictionary<int, int, CharacterDataInfo>.SerializableSets sets in localFight.EnemySets._SerializableSets)
        {
            for (int i = 0; i < sets.value.Length;i++)
            {
                List<CharacterDataInfo> unNullValues = new List<CharacterDataInfo>();
                if (!String.IsNullOrEmpty(sets.value[i]._Value.ResourceID))
                {
                    UnNullDic.Set(sets.key1,sets.value[i]._Key2,sets.value[i]._Value);
                }
            }
        }
        UnNullDic.ConvertDictionaryToSerializableArray();
        try
        {
            string json = JsonConvert.SerializeObject(UnNullDic._SerializableSets);
            LocalJson.SaveInfoToJsonFile_dataPath(null, path, json);
        }
        catch (Exception e)
        {
            Debug.Log("战斗信息保存失败");
            Debug.Log(e.ToString());
        }
    }
}
