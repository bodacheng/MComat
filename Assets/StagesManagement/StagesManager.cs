using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class StagesManager : MonoBehaviour
{
    public string fightScriptPath;
    public TextAsset FightScript;//存档文件。是我们拖给这个位置的一个东西，但如果说这个文件不存在，那应该要自动新建并指定到这个位置上
    public FightMembers EditoringFight;

    public FightMembers LoadOneLocalFight_Json(TextAsset Script)
    {
        FightMembers _localFight = new FightMembers();
        MultiDictionary<int, int, CharDataInfo>.SerializableSets[] targetValue;
        try
        {
            targetValue = JsonConvert.DeserializeObject<MultiDictionary<int, int, CharDataInfo>.SerializableSets[]>(Script.text);
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
    
    public void SaveFightAsJson(string path, FightMembers localFight)
    {
        if (localFight == null || localFight.EnemySets == null)
            return;
               
        MultiDictionary<int, int, CharDataInfo> UnNullDic = new MultiDictionary<int, int, CharDataInfo>();
        foreach (MultiDictionary<int, int, CharDataInfo>.SerializableSets sets in localFight.EnemySets._SerializableSets)
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
