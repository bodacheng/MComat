using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using UniRx;

public static class GeneralModelPool {
    
    public static IDictionary<string, List<Data_Center>> ModelDic = new Dictionary<string, List<Data_Center>>();
    static List<SingleAssignmentDisposable> AutoReturnMissions = new List<SingleAssignmentDisposable>();
    
    public static void Clear()
    {
        for (int i = 0; i < AutoReturnMissions.Count; i++)
        {
            AutoReturnMissions[i].Dispose();
        }
        ModelDic.Clear();
    }
    
    static void AutoReturn(Data_Center data_Center, string resourceID)
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
    
    public static IEnumerator GetModel(string ModelID, bool FromPool)
    {
        Data_Center target = null;
        if (FromPool)
        {
            if (!ModelDic.ContainsKey(ModelID))
            {
                yield return ConstructPool(ModelID);
                if (!ModelDic.ContainsKey(ModelID))
                {
                    yield return null;
                    yield break;
                }
            }
            for (int i = 0; i < ModelDic[ModelID].Count; i++)
            {
                if (!ModelDic[ModelID][i].WholeT.gameObject.activeSelf)
                {
                    target = ModelDic[ModelID][i];
                }
            }
            if (target == null)
            {
                IEnumerator buildmodelproess = CreateUnit(ModelID);
                yield return buildmodelproess;
                target = (Data_Center)buildmodelproess.Current;
                ModelDic[ModelID].Add(target);
            }
            target.WholeT.gameObject.SetActive(true);
            AutoReturn(target, ModelID);
        }else{
            IEnumerator buildmodelproess = CreateUnit(ModelID);
            yield return buildmodelproess;
            target = (Data_Center)buildmodelproess.Current;
        }
        yield return target;
    }
    
    public static IEnumerator GetMyModel(string localid)
    {
        MonsterOfPlayerInfo targetInfo = MyMonsters.Get(localid);
        IEnumerator enumerator = GetModel(targetInfo.monsterId, true);
        yield return enumerator;
        yield return enumerator.Current;
    }
    
    public static IEnumerator ConstructPool(string rID)
    {
        IEnumerator proess = CreateUnit(rID);
        yield return proess;
        Data_Center _D = (Data_Center)proess.Current;
        if (_D != null)
        {
            _D.WholeT.gameObject.SetActive(false);
            List<Data_Center> data_Centers = new List<Data_Center>() { _D };
            DicAdd<string, List<Data_Center>>.Add(ModelDic, rID, data_Centers);
        }else{
            Debug.Log("角色"+rID+"构建失败");
            yield return null;
        }
    }
    
    public static IEnumerator CreateUnit(string rID)
    {
        IEnumerator proess = null;
        switch(ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
            yield return proess= (UnitCreator.CreateRawUnit_Cach(rID));
            break;
            case ResourceLoadMode.Resource:
            yield return proess = (UnitCreator.CreateRawUnit_Resource(rID));
            break;
            case ResourceLoadMode.StreamingAssetAB:
            yield return proess = (UnitCreator.CreateRawUnit_StreamingAssets(rID));
            break;
        }
        Data_Center _Temp = (Data_Center)proess.Current;
        yield return _Temp;
    }
}