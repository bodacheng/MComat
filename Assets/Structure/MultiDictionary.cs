using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 复合字典
/// </summary>
[System.Serializable]
public class MultiDictionary<Key1, Key2, Value>
{
    /// <summary>
    /// 字典结构
    /// </summary>
    private Dictionary<Key1, Dictionary<Key2, Value>> mDict1 = new Dictionary<Key1, Dictionary<Key2, Value>>();
    private Dictionary<Key1, List<Key2>> unnullkeys = new Dictionary<Key1, List<Key2>>();
    public List<Value> values = new List<Value>();
     
     /// <summary>
    /// 序列化对象
    /// </summary>
    public SerializableSets[] _SerializableSets;
    
    public void ConvertDictionaryToSerializableArray()
    {
        List<SerializableSets> temp = new List<SerializableSets>();
        foreach (KeyValuePair<Key1,Dictionary<Key2, Value>> keyValuePair in mDict1)
        {
            SerializableSets serializableSets = new SerializableSets();
            serializableSets.key1 = keyValuePair.Key;
            List<SerializableSet> set = new List<SerializableSet>();
            foreach(KeyValuePair<Key2, Value> _KeyValuePair in keyValuePair.Value)
            {
                SerializableSet serializableSet = new SerializableSet();
                serializableSet._Key2 = _KeyValuePair.Key;
                serializableSet._Value = _KeyValuePair.Value;
                set.Add(serializableSet);
            }
            serializableSets.value = set.ToArray();
            temp.Add(serializableSets);
        }
        _SerializableSets = temp.ToArray();
    }
    
    public void ConvertSerializableArrayToDictionary()
    {
        mDict1 = new Dictionary<Key1, Dictionary<Key2, Value>>();
        unnullkeys = new Dictionary<Key1, List<Key2>>();
        values.Clear();
        foreach(SerializableSets _oneSerializableSets in _SerializableSets)
        {
            Dictionary<Key2, Value> childDic = new Dictionary<Key2, Value>();
            foreach(SerializableSet set in _oneSerializableSets.value)
            {
                childDic.Add(set._Key2,set._Value);
                values.Add(set._Value);
            }
            mDict1.Add(_oneSerializableSets.key1,childDic);
            unnullkeys.Add(_oneSerializableSets.key1,childDic.Keys.ToList());
        }
    }
           
    /// <summary>
    /// 赋值
    /// </summary>
    public void Set(Key1 key1, Key2 key2, Value value)
    {
        if (mDict1.ContainsKey(key1))
        {
            var dict2 = mDict1[key1];
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
            var dict2 = new Dictionary<Key2, Value>();
            dict2.Add(key2, value);
            mDict1.Add(key1, dict2);
            values.Add(value);
            unnullkeys.Add(key1, new List<Key2>() { key2 });
        }
    }
 
    /// <summary>
    /// 取值
    /// </summary>
    public Value Get(Key1 key1, Key2 key2, Value defaultValue = default(Value))
    {
        if (mDict1.ContainsKey(key1))
        {
            var dict2 = mDict1[key1];
            if (dict2.ContainsKey(key2))
                return dict2[key2];
        }
        return defaultValue;
    }
    
    public Dictionary<Key1, List<Key2>> getAllUnNullKeys()
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
