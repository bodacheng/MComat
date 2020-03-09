using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyModelPool {

    private static MyModelPool instance;
    public static MyModelPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MyModelPool();
            }
            return instance;
        }
    }
    
    public IDictionary<string, GameObject> ModelDicBasedOnPlayerLocalID = new Dictionary<string, GameObject>();
    public void SetAllMyCharactersModelActive(bool active)
    {
        List<string> problemKeys = new List<string>();
        foreach(KeyValuePair<string,GameObject> pair in ModelDicBasedOnPlayerLocalID)
        {
            if (pair.Value == null)
                problemKeys.Add(pair.Key);
            else
                pair.Value.SetActive(active);
        }
        for (int i = 0; i < problemKeys.Count;i++)
        {
            ModelDicBasedOnPlayerLocalID.Remove(problemKeys[i]);
        }
    }

    //我们希望这个字典来负责加载了的模型的重复利用。另外不同于各种特效是由default单例那个组件保存字典，这个模型的字典我觉得放在这里也有道理，因为毕竟这里保存的是一些展示用模型。
    public void AddToDic(string LocalID,GameObject Model,IDictionary<string, GameObject> ReferenceDic)
    {
        if (ReferenceDic.ContainsKey(LocalID))
            ReferenceDic[LocalID] = Model;
        else
            ReferenceDic.Add(LocalID, Model);
    }

    public GameObject GetMyModel(string localid)
    {
        if (string.IsNullOrEmpty(localid))
            return null;
        ModelDicBasedOnPlayerLocalID.TryGetValue(localid, out GameObject model);
        return model ? model : null;
    }
}
