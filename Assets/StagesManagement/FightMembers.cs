using UnityEngine;
using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using mainMenu;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class FightMembers
{
    [NonSerialized] public MultiDic<int, int, UnitInfo> HeroSets = new ();
    public MultiDic<int, int, UnitInfo> EnemySets = new ();
    
    public FightMembers()
    {
    }
    
    public void SetEnemyLevel(int level)
    {
        foreach (var unitInfo in EnemySets.GetValues())
        {
            unitInfo.level = level;
        }
    }
    
    public static FightMembers RandomFight()
    {
        var type = "human";
        
        var unitIDsAndNames = Units.GetMonsterIDsAndNamesDic(type);
        var Indexes = RandomSelect.Get(0, unitIDsAndNames.Count - 1, 3);
        var charRecordIds = unitIDsAndNames.Keys.ToList();
        
        var target = new FightMembers();
        
        var char1 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[0]],
            set = SkillSet.RandomSkillSet("human", null,  false)
        };
        var char2 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[1]],
            set = SkillSet.RandomSkillSet("human", null,  false)
        };
        var char3 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[2]],
            set = SkillSet.RandomSkillSet("human", null,  false)
        };
        
        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char3);
        
        return target;
    }
    
    public static FightMembers ScreenSaver(TeamMode teamMode)
    {
        string focusingtype = "human";
        
        var CharIDsAndNames = Units.GetMonsterIDsAndNamesDic(focusingtype);
        var Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 6);
        var monsterIds = CharIDsAndNames.Keys.ToList();

        var target = new FightMembers();
        var filterForm = new SkillStonesBox.StoneFilterForm
        {
            type = focusingtype,
            exType = new int[1] { 0 },
            close = false,
            near = false,
            far = false
        };
        
        var char1 = new UnitInfo
        {
            r_id = monsterIds[Indexes[0]],
            set = SkillSet.RandomSkillSet(focusingtype, null, false, filterForm)
        };
        var char2 = new UnitInfo
        {
            r_id = monsterIds[Indexes[1]],
            set = SkillSet.RandomSkillSet(focusingtype, null, false, filterForm)
        };
        
        switch (teamMode)
        {
            case TeamMode.multiRaid:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
            case TeamMode.rotation:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
        }
        
        return target;
    }
    
    public static FightMembers RandomSkillTest(TeamMode teamMode)
    {
        string focusingtype = "human";
        
        var CharIDsAndNames = Units.GetMonsterIDsAndNamesDic(focusingtype);
        var Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 6);
        var charRecordIds = CharIDsAndNames.Keys.ToList();

        var target = new FightMembers();

        var char1 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[0]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };
        var char2 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[1]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };
        var char3 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[2]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };

        var char4 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[3]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };
        var char5 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[4]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };
        var char6 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[5]],
            set = SkillSet.RandomSkillSet("human", null,false)
        };

        switch (teamMode)
        {
            case TeamMode.multiRaid:
                target.EnemySets.Set(0, 0, char1);
                target.EnemySets.Set(0, 1, char6);
                target.HeroSets.Set(0, 0, char4);
                target.HeroSets.Set(0, 1, char5);
                break;
            case TeamMode.rotation:
                target.EnemySets.Set(0, 0, char1);
                target.EnemySets.Set(0, 1, char2);
                target.EnemySets.Set(0, 2, char3);
                target.HeroSets.Set(0, 0, char4);
                target.HeroSets.Set(0, 1, char5);
                target.HeroSets.Set(0, 2, char6);
                break;
        }

        return target;
    }
    
    public static FightMembers LoadEnemies_Json(TextAsset Script)
    {
        var _localFight = new FightMembers();
        MultiDic<int, int, UnitInfo>.SerializableSet[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDic<int, int, UnitInfo>.SerializableSet[]>(Script.text);
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
        MultiDic<int, int, UnitInfo> UnNullDic = new MultiDic<int, int, UnitInfo>();
        foreach (MultiDic<int, int, UnitInfo>.SerializableSet sets in localFight.EnemySets._SerializableSets)
        {
            UnNullDic.Set(sets.key1, sets.key2 , sets.value);
        }

        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(MultiDic<int, int, UnitInfo>.SerializableSet[]));
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
    
    public FightMembers LoadOneLocalFight_XML(TextAsset Script)
    {
        FightMembers _localFight = new FightMembers();
        
        MultiDic<int, int, UnitInfo>.SerializableSet[] targetValue;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MultiDic<int, int, UnitInfo>.SerializableSet[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    targetValue = serializer.Deserialize(textReader) as MultiDic<int, int, UnitInfo>.SerializableSet[];
                }
                Debug.Log("读取了敌人战斗信息");
            }
            else
            {
                var reader = new StringReader(Script.text);
                targetValue = serializer.Deserialize(reader) as MultiDic<int, int, UnitInfo>.SerializableSet[];
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
}