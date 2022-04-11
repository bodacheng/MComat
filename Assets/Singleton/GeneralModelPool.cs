using System.Collections;
using dataAccess;
using UnityEngine;

public static class GeneralModelPool {
    
    public static IEnumerator GetModel(string rId)
    {
        Data_Center target = null;
        var process = CreateUnit(rId);
        yield return process;
        target = (Data_Center)process.Current;
        yield return target;
    }
    
    public static IEnumerator GetMyModel(string instanceId)
    {
        var targetInfo = MyMonsters.Get(instanceId);
        var get = GetModel(targetInfo.r_id);
        yield return get;
        yield return get.Current;
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
        var returnValue = (Data_Center)process.Current;
        yield return returnValue;
    }
}