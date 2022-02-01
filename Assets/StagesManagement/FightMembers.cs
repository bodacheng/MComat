using UnityEngine;
using System;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class FightMembers
{
    [NonSerialized]
    public MultiDict<int, int, UnitInfo> HeroSets = new MultiDict<int, int, UnitInfo>();
    public MultiDict<int, int, UnitInfo> EnemySets = new MultiDict<int, int, UnitInfo>();
    
    public FightMembers()
    {
    }

    public void SetEnemyLevel(float level)
    {
        foreach (var charData in EnemySets.GetValues())
        {
            charData.set.SetLevel(level);
        }
    }

    public static FightMembers LoadEnemies_Json(TextAsset Script)
    {
        var _localFight = new FightMembers();
        MultiDict<int, int, UnitInfo>.SerializableSet[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDict<int, int, UnitInfo>.SerializableSet[]>(Script.text);
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