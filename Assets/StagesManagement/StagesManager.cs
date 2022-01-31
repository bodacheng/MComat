using UnityEngine;
using System;
using Newtonsoft.Json;
using Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class StagesManager : MonoBehaviour
{
    public TextAsset FightScript;//存档文件。是我们拖给这个位置的一个东西，但如果说这个文件不存在，那应该要自动新建并指定到这个位置上
    public FightMembers target;
    
    public void SaveFightAsJson(string path, MultiDict<int, int, UnitInfo> EnemySets)
    {
        if (EnemySets == null)
            return;

        EnemySets.ConvertDictionaryToSerializableArray();

        try
        {
            string json = JsonConvert.SerializeObject(EnemySets._SerializableSets);
            LocalJson.SaveInfoToJsonFile_dataPath(null, path, json);
        }
        catch (Exception e)
        {
            Debug.Log("战斗信息保存失败");
            Debug.Log(e.ToString());
        }
    }
}
