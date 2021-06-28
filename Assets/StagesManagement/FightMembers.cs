using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class FightMembers
{
    [NonSerialized]
    public MultiDict<int, int, CharDataInfo> HeroSets = new MultiDict<int, int, CharDataInfo>();
    public MultiDict<int, int, CharDataInfo> EnemySets = new MultiDict<int, int, CharDataInfo>();
    
    public FightMembers()
    {
    }

    public void SetEnemyLevel(int level)
    {
        foreach (var charData in EnemySets.GetValues())
        {
            charData._NineAndTwo.SetSkillLevel(level);
        }
    }

    public static FightMembers LoadEnemies_Json(TextAsset Script)
    {
        FightMembers _localFight = new FightMembers();
        MultiDict<int, int, CharDataInfo>.SerializableSet[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDict<int, int, CharDataInfo>.SerializableSet[]>(Script.text);
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