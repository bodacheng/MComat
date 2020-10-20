using UnityEngine;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System;
using System.IO;

[Serializable]
public class LocalFight
{
    [NonSerialized]
    public MultiDictionary<int, int, CharDataInfo> HeroSets = new MultiDictionary<int, int, CharDataInfo>();
    public MultiDictionary<int, int, CharDataInfo> EnemySets = new MultiDictionary<int, int, CharDataInfo>();
    
    public LocalFight()
    {
    }
    
    public void SetEnemyLevel(int level)
    {
        for (int i = 0; i < EnemySets.values.Count; i++)
        {
            EnemySets.values[i]._NineAndTwo.SetSkillLevel(level);
        }
    }
       
    public static LocalFight LoadOneLocalFightByScript(TextAsset Script)
    {
        LocalFight _localFight = new LocalFight();
        MultiDictionary<int, int, CharDataInfo>.SerializableSets[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDictionary<int, int, CharDataInfo>.SerializableSets[]>(Script.text);
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
    
    public static LocalFight LoadOneLocalFightByScript_XML(TextAsset Script)
    {
        MultiDictionary<int, int, CharDataInfo>.SerializableSets[] enemySets;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MultiDictionary<int, int, CharDataInfo>.SerializableSets[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    enemySets = serializer.Deserialize(textReader) as MultiDictionary<int, int, CharDataInfo>.SerializableSets[];
                }
            }
            else
            {
                var reader = new StringReader(Script.text);
                enemySets = serializer.Deserialize(reader) as MultiDictionary<int, int, CharDataInfo>.SerializableSets[];
            }
            LocalFight fight = new LocalFight();
            fight.EnemySets._SerializableSets = enemySets;
            fight.EnemySets.ConvertSerializableArrayToDictionary();
            return fight;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }
}