using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterConfigInfos
{
    private static MonsterConfigInfos instance;
    public static MonsterConfigInfos Instance
    {
        get
        {
            if (instance == null)
                instance = new MonsterConfigInfos();
            return instance;
        }
    }
    
    public static IDictionary<int, CharacterResourceInfo> CharacterResourceInfoDic = new Dictionary<int, CharacterResourceInfo>();
    public static monstersConfigTable _monstersConfigTable = new monstersConfigTable();
    
    public IEnumerator loadMonstersConfig()
    {
        switch (ResourceLoadingSetting.Instance.ConfigFileLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                loadMonstersConfigByResource();
                break;
        }
        refreshCharacterResourceInfoDic();
        yield break;    
    }
    
    public static CharacterResourceInfo getCharacterResourceInfo(int resourceId)
    {
        if (CharacterResourceInfoDic.ContainsKey(resourceId))
            return CharacterResourceInfoDic[resourceId];
        else
            return null;
    }
    
    public static void loadMonstersConfigByResource()
    {
        //暂时做如下处理
        TextAsset CSV = Resources.Load("Account/MonstersConfig") as TextAsset;
        if (CSV)
        {
            _monstersConfigTable.Load(CSV);
        }
        else
            Debug.Log("没能读取到角色数据库文件。");
    }
    
    public static void refreshCharacterResourceInfoDic()
    {
        List<monstersConfigTable.Row> rows = _monstersConfigTable.rowList;
        CharacterResourceInfoDic.Clear();
        List<CharacterResourceInfo> characterResourceInfos = _monstersConfigTable.RowToCharacterResourceInfoList(rows);
        foreach (CharacterResourceInfo one in characterResourceInfos)            
        {
            CharacterResourceInfoDic.Add(one.monsterId,one);
        }
    }
}
