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
    
    public static IEnumerator GetModel(string ResourceID, bool FromPool)
    {
        Data_Center target = null;
        if (FromPool)
        {
            if (!ModelDic.ContainsKey(ResourceID))
            {
                yield return ConstructPool(ResourceID);
            }
            for (int i = 0; i < ModelDic[ResourceID].Count; i++)
            {
                if (!ModelDic[ResourceID][i].WholeT.gameObject.activeSelf)
                {
                    target = ModelDic[ResourceID][i];
                }
            }
            if (target == null)
            {
                IEnumerator buildmodelproess = CreateCharModel(ResourceID);
                yield return buildmodelproess;
                target = (Data_Center)buildmodelproess.Current;
                ModelDic[ResourceID].Add(target);
            }
            target.WholeT.gameObject.SetActive(true);
            AutoReturn(target, ResourceID);
        }else{
            IEnumerator buildmodelproess = CreateCharModel(ResourceID);
            yield return buildmodelproess;
            target = (Data_Center)buildmodelproess.Current;
        }
        yield return target;
    }
    
    public static IEnumerator GetMyModel(string localid)
    {
        GetMonsterOfPlayerDetailModel targetInfo = AccountCharsSet.Get(localid);
        IEnumerator enumerator = GetModel(targetInfo.monsterId, true);
        yield return enumerator;
        yield return enumerator.Current;
    }
    
    public static IEnumerator ConstructPool(string monsterID)
    {
        IEnumerator buildmodelproess = CreateCharModel(monsterID);
        yield return buildmodelproess;
        Data_Center _TempDATACENTER = (Data_Center)buildmodelproess.Current;
        if (_TempDATACENTER != null)
        {
            _TempDATACENTER.WholeT.gameObject.SetActive(false);
            List<Data_Center> data_Centers = new List<Data_Center>() { _TempDATACENTER };
            DicAdd<string, List<Data_Center>>.Add(ModelDic, monsterID, data_Centers);
        }else{
            Debug.Log("角色"+monsterID+"构建失败");
            yield return null;
        }
    }
    
    public static IEnumerator CreateCharModel(string ResourceID)
    {
        IEnumerator buildmodelproess = null;
        switch(ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
            yield return buildmodelproess= (CharsManager.target.CreateModelForShowingByCach(ResourceID));
            break;
            case ResourceLoadMode.Resource:
            yield return buildmodelproess = (CharsManager.target.CreateModelForShowingByResource(ResourceID));
            break;
            case ResourceLoadMode.StreamingAssetAB:
            yield return buildmodelproess = (CharsManager.target.CreateModelForShowingByStreamingAssets(ResourceID));
            break;
        }
        Data_Center _TempDATACENTER = (Data_Center)buildmodelproess.Current;
        yield return _TempDATACENTER;
    }
}