using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using mainMenu;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class FightMembers
{
    [NonSerialized] public MultiDic<int, int, UnitInfo> HeroSets = new MultiDic<int, int, UnitInfo>();
    public MultiDic<int, int, UnitInfo> EnemySets = new MultiDic<int, int, UnitInfo>();
    
    public static bool TeamLegal(MultiDic<int, int, UnitInfo> team, List<string> checkInstanceIds = null)
    {
        bool legal = team.GetValues().Count > 0;
        if (!legal)
            return false;
        foreach (var unit in team.GetValues())
        {
            if (checkInstanceIds != null && !checkInstanceIds.Contains(unit.id))
                continue;
            legal = unit.set.CheckEdit() == SkillSet.SkillEditError.Perfect && legal;
        }
        return legal;
    }
    
    public bool CheckStonesLegal(FightEventType eventType, List<string> checkInstanceIds = null)
    {
        switch (eventType)
        {
            case FightEventType.Quest:
            case FightEventType.Event:
                return TeamLegal(HeroSets, checkInstanceIds);//checkInstanceIds 是针对Gangbang的
            default:
                return TeamLegal(HeroSets) && TeamLegal(EnemySets);
        }
    }
    
    public FightMembers()
    {
    }
    
    static UnitInfo ArrangeUnitInfo(string unitRecordID, CriticalGaugeMode mode = CriticalGaugeMode.Normal)
    {
        var skillId = UnitPassiveTable.GetUnitPassiveRecordId(unitRecordID);
        var unitConfig = Units.GetUnitConfig(unitRecordID);
        SkillStonesBox.StoneFilterForm form = new SkillStonesBox.StoneFilterForm(unitConfig.TYPE);
        switch (mode)
        {
            case CriticalGaugeMode.DoubleGain:
                form.ExType = new[] { 1, 2, 3 };
                break;
            case CriticalGaugeMode.Unlimited:
                form.ExType = new[] { 1, 2, 3 };
                break;
            default:
                break;
        }
        
        var unitInfo = new UnitInfo
        {
            id = unitRecordID,
            r_id = unitRecordID,
            set = SkillSet.RandomSkillSet(skillId ,false, form, mode == CriticalGaugeMode.Unlimited)
        };
        return unitInfo;
    }
    
    public static FightMembers RandomFight(CriticalGaugeMode mode = CriticalGaugeMode.Normal, int unitCount = 3)
    {
        var unitIDsAndNames = Units.GetMonsterIDsAndNamesDic("human");
        var indexes = RandomSelect.Get(0, unitIDsAndNames.Count - 1, unitCount);
        var recordIds = unitIDsAndNames.Keys.ToList();
        var target = new FightMembers();
        for (int i = 0; i < unitCount; i++)
        {
            var unit = ArrangeUnitInfo(recordIds[indexes[i]], mode);
            target.EnemySets.Set(0, i, unit);
        }
        return target;
    }
    
    public static FightMembers RandomSkillTest(CriticalGaugeMode mode = CriticalGaugeMode.Normal)
    {
        var unitIDsAndNames = Units.GetMonsterIDsAndNamesDic("human");
        var indexes = RandomSelect.Get(0, unitIDsAndNames.Count - 1, 12);
        
        var unitIDsAndNamesDragon = Units.GetMonsterIDsAndNamesDic("e_dragon");
        var indexesD = RandomSelect.Get(0, unitIDsAndNamesDragon.Count - 1, 1);
        
        var recordIds = unitIDsAndNames.Keys.ToList();
        var recordIdsD = unitIDsAndNamesDragon.Keys.ToList();
        
        var target = new FightMembers();
        var char1 = ArrangeUnitInfo(recordIds[indexes[0]],mode);
        var char2 = ArrangeUnitInfo(recordIds[indexes[1]],mode);
        var char3 = ArrangeUnitInfo(recordIds[indexes[2]],mode);
        var char4 = ArrangeUnitInfo(recordIds[indexes[3]],mode);
        var char5 = ArrangeUnitInfo(recordIds[indexes[4]],mode);
        var char6 = ArrangeUnitInfo(recordIds[indexes[5]],mode);
        var char7 = ArrangeUnitInfo(recordIds[indexes[6]],mode);
        var char8 = ArrangeUnitInfo(recordIds[indexes[7]],mode);
        var char9 = ArrangeUnitInfo(recordIdsD[indexesD[0]],mode);
        var char10 = ArrangeUnitInfo(recordIds[indexes[9]],mode);
        var char11 = ArrangeUnitInfo(recordIds[indexes[10]],mode);
        var char12 = ArrangeUnitInfo(recordIdsD[indexesD[0]],mode);
        
        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char9);
        target.EnemySets.Set(0, 3, char7);
        target.EnemySets.Set(0, 4, char8);
        target.EnemySets.Set(0, 5, char3);
        
        target.HeroSets.Set(0, 0, char4);
        target.HeroSets.Set(0, 1, char5);
        target.HeroSets.Set(0, 2, char12);
        target.HeroSets.Set(0, 3, char10);
        target.HeroSets.Set(0, 4, char11);
        target.HeroSets.Set(0, 5, char6);
        
        return target;
    }
    
    public static FightMembers ScreenSaver(FightMode fightMode)
    {
        var type = "human";
        var target = new FightMembers();
        var char1 = ArrangeUnitInfo("1");
        var char2 = ArrangeUnitInfo("2");
        switch (fightMode)
        {
            case FightMode.Multi:
            case FightMode.Group:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
            case FightMode.Rotate:
            case FightMode.Evolve:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
        }
        return target;
    }
    
    public static FightMembers LoadEnemies_Json(TextAsset Script)
    {
        var _localFight = new FightMembers();
        try
        {
            var targetValue = JsonConvert.DeserializeObject<MultiDic<int, int, UnitInfo>.SerializableSet[]>(Script.text);
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
            var XmlSerializer = new XmlSerializer(typeof(MultiDic<int, int, UnitInfo>.SerializableSet[]));
            var FileStream = new FileStream(Application.dataPath + "/" + path, FileMode.Create);
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
        var _localFight = new FightMembers();
        
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