using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 复合字典
/// </summary>
[System.Serializable]
public class MultiDictionary<Key1, Key2, Value>
{
    /// <summary>
    /// 字典结构
    /// </summary>
    public Dictionary<Key1, Dictionary<Key2, Value>> mDict = new Dictionary<Key1, Dictionary<Key2, Value>>();
    public Dictionary<Key1, List<Key2>> unnullkeys = new Dictionary<Key1, List<Key2>>();
    public List<Value> values = new List<Value>();

     /// <summary>
    /// 序列化对象
    /// </summary>
    public SerializableSets[] _SerializableSets;
    
    public void ConvertDictionaryToSerializableArray()
    {
        List<SerializableSets> temp = new List<SerializableSets>();
        foreach (KeyValuePair<Key1,Dictionary<Key2, Value>> keyValuePair in mDict)
        {
            SerializableSets serializableSets = new SerializableSets
            {
                key1 = keyValuePair.Key
            };
            List<SerializableSet> set = new List<SerializableSet>();
            foreach(KeyValuePair<Key2, Value> _KeyValuePair in keyValuePair.Value)
            {
                SerializableSet serializableSet = new SerializableSet
                {
                    _Key2 = _KeyValuePair.Key,
                    _Value = _KeyValuePair.Value
                };
                set.Add(serializableSet);
            }
            serializableSets.value = set.ToArray();
            temp.Add(serializableSets);
        }
        _SerializableSets = temp.ToArray();
    }
    
    public void ConvertSerializableArrayToDictionary()
    {
        mDict = new Dictionary<Key1, Dictionary<Key2, Value>>();
        unnullkeys = new Dictionary<Key1, List<Key2>>();
        values.Clear();
        foreach(SerializableSets _oneSerializableSets in _SerializableSets)
        {
            Dictionary<Key2, Value> childDic = new Dictionary<Key2, Value>();
            foreach(SerializableSet set in _oneSerializableSets.value)
            {
                Set(_oneSerializableSets.key1, set._Key2,set._Value);
            }
        }
    }
    
    /// <summary>
    /// 赋值
    /// </summary>
    public void Set(Key1 key1, Key2 key2, Value value)
    {
        if (mDict.ContainsKey(key1))
        {
            var dict2 = mDict[key1];
            if (dict2.ContainsKey(key2))
            {
                values.Remove(dict2[key2]);
                dict2[key2] = value;
                values.Add(value);
            } 
            else
            {
                dict2.Add(key2, value);
                values.Add(value);
                unnullkeys[key1].Add(key2);
            }
        }
        else
        {
            var dict2 = new Dictionary<Key2, Value>
            {
                { key2, value }
            };
            mDict.Add(key1, dict2);
            values.Add(value);
            unnullkeys.Add(key1, new List<Key2>() { key2 });
        }
    }
 
    /// <summary>
    /// 取值
    /// </summary>
    public Value Get(Key1 key1, Key2 key2, Value defaultValue = default)
    {
        if (mDict.ContainsKey(key1))
        {
            var dict2 = mDict[key1];
            if (dict2.ContainsKey(key2))
                return dict2[key2];
        }
        return defaultValue;
    }
        
    public void Clear()
    {
        mDict.Clear();
        values.Clear();
        unnullkeys.Clear();
        _SerializableSets = null;
    }
    
    public Dictionary<Key1, List<Key2>> GetAllUnNullKeys()
    {
        return unnullkeys;
    }
    
    [System.Serializable]
    public struct SerializableSets
    {
        public Key1 key1;
        public SerializableSet[] value;
    }
    
    [System.Serializable]
    public struct SerializableSet
    {
        public Key2 _Key2;
        public Value _Value;
    }
}

public class SSIMultiDictionary
{
    public SSIMultiDictionary()
    {
        main = new MultiDictionary<string, string, int>();        
    }
    public MultiDictionary<string, string, int> main = new MultiDictionary<string, string, int>();
    public List<KeyValuePair<string, string>> GiveOutMin()
    {
        List<KeyValuePair<string, List<string>>> temp = new List<KeyValuePair<string, List<string>>>();//各个大key所属的对应最终最小值的小key
        foreach (KeyValuePair<string,Dictionary<string, int>> BigPair in main.mDict)
        {
            Dictionary<string, int> LittleDic = BigPair.Value;
            List<string> minkeys = LittleDic.Keys.Select(x => new { x, y = LittleDic[x] }).GroupBy(x => x.y).OrderBy(x => x.Key).First().Select(x => x.x).ToList();
            if (minkeys.Count > 0)
            {
                temp.Add(new KeyValuePair<string, List<string>>(BigPair.Key,minkeys));
            }
        }

        int Minimum = 9;
        string minusBigKey;
        List<KeyValuePair<string, List<string>>> allMinusBigKeys = new List<KeyValuePair<string, List<string>>>();
        for (int i = 0; i < temp.Count; i++)
        {
            if (main.Get(temp[i].Key, temp[i].Value[0]) < Minimum)
            {
                Minimum = main.Get(temp[i].Key, temp[i].Value[0]);
                minusBigKey = temp[i].Key;
                allMinusBigKeys.Clear();
                allMinusBigKeys.Add(new KeyValuePair<string, List<string>> (temp[i].Key, temp[i].Value));
            }
            else if (main.Get(temp[i].Key, temp[i].Value[0]) == Minimum)
            {
                allMinusBigKeys.Add(new KeyValuePair<string, List<string>> (temp[i].Key, temp[i].Value));
            }
        }
        List<KeyValuePair<string, string>> final_minkeys = new List<KeyValuePair<string, string>>();
        for (int i = 0; i < allMinusBigKeys.Count; i++)
        {
            for (int y = 0; y < allMinusBigKeys[i].Value.Count;y++)
            {
                final_minkeys.Add(new KeyValuePair<string, string>(allMinusBigKeys[i].Key,allMinusBigKeys[i].Value[y]));
            }
        }
        
        return final_minkeys;
    }
}