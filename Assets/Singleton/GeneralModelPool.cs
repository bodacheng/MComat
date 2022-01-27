using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using dataAccess;
using UniRx;

public static class GeneralModelPool {
    
    static readonly IDictionary<string, List<Data_Center>> ModelDic = new Dictionary<string, List<Data_Center>>();
    static readonly List<SingleAssignmentDisposable> AutoReturnMissions = new List<SingleAssignmentDisposable>();
    
    public static void Clear()
    {
        foreach (var t in AutoReturnMissions)
        {
            t.Dispose();
        }
        ModelDic.Clear();
    }
    
    static void AddAutoReturnMission(Data_Center data_Center, string resourceID)
    {
        SingleAssignmentDisposable one = null;
        one = new SingleAssignmentDisposable
        {
            Disposable = Observable.EveryUpdate().Subscribe(_ => 
                {
                    if (!data_Center.WholeT.gameObject.activeSelf)
                    {
                        ModelDic[resourceID].Add(data_Center);
                        if (AutoReturnMissions.Contains(one))
                            AutoReturnMissions.Remove(one);
                        one.Dispose();
                    }
                }
            )
        };
        AutoReturnMissions.Add(one);
    }
    
    public static IEnumerator GetModel(string rId, bool fromPool)
    {
        Data_Center target = null;
        if (fromPool)
        {
            if (!ModelDic.ContainsKey(rId))
            {
                yield return ConstructPool(rId);
                if (!ModelDic.ContainsKey(rId))
                {
                    yield return null;
                    yield break;
                }
            }
            for (var i = 0; i < ModelDic[rId].Count; i++)
            {
                if (!ModelDic[rId][i].WholeT.gameObject.activeSelf)
                {
                    target = ModelDic[rId][i];
                }
            }
            if (target == null)
            {
                var process = CreateUnit(rId);
                yield return process;
                target = (Data_Center)process.Current;
                ModelDic[rId].Add(target);
            }
            target.WholeT.gameObject.SetActive(true);
            AddAutoReturnMission(target, rId);
        } else {
            var process = CreateUnit(rId);
            yield return process;
            target = (Data_Center)process.Current;
        }
        yield return target;
    }
    
    public static IEnumerator GetMyModel(string instanceId)
    {
        var targetInfo = MyMonsters.Get(instanceId);
        var get = GetModel(targetInfo.r_id, true);
        yield return get;
        yield return get.Current;
    }
    
    static IEnumerator ConstructPool(string rId)
    {
        IEnumerator process = CreateUnit(rId);
        yield return process;
        var _D = (Data_Center)process.Current;
        if (_D != null)
        {
            _D.WholeT.gameObject.SetActive(false);
            var data_Centers = new List<Data_Center>() { _D };
            DicAdd<string, List<Data_Center>>.Add(ModelDic, rId, data_Centers);
        }else{
            Debug.Log("角色"+rId+"构建失败");
            yield return null;
        }
    }
    
    static IEnumerator CreateUnit(string rID)
    {
        IEnumerator process = null;
        switch(ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return process = (UnitCreator.CreateRawUnit_Cach(rID));
            break;
            case ResourceLoadMode.Resource:
                yield return process = (UnitCreator.CreateRawUnit_Resource(rID));
            break;
            case ResourceLoadMode.StreamingAssetAB:
                yield return process = (UnitCreator.CreateRawUnit_StreamingAssets(rID));
            break;
        }
        var _Temp = (Data_Center)process.Current;
        yield return _Temp;
    }
}