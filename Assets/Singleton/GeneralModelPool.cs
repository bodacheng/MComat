using System.Collections;
using dataAccess;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        //以上这个信息就包括了全部的“我的角色”信息，下面别的信息都是据此各种由此索引出来的。
        Data_Center _D;
        UnitConfig unitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(rID));
        if (unitConfig == null)
        {
            Debug.Log("资源号码错误");
            yield break;
        }
        GameObject _TempModel = null;

        GameObject resultObject;
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(unitConfig.TYPE + "/" + unitConfig.REAL_NAME + ".prefab");
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            resultObject = handle.Result;
            Addressables.Release(handle);
        }
        else
        {
            Debug.Log("资源错误："+"CharPretabs/" + unitConfig.TYPE + "/" + unitConfig.REAL_NAME);
            Addressables.Release(handle);
            yield break;
        }
        
        _TempModel = GameObject.Instantiate((GameObject)resultObject, Vector3.zero, Quaternion.identity);
        OutsideDataLink _ODL = _TempModel.GetComponent<OutsideDataLink>();
        if (_ODL == null)
        {
            yield return null;
            yield break;
        }
        _D = _ODL._C;        
        _TempModel.SetActive(true);
        // 在角色生成的瞬间各个组件的awake和onenable就已经都开了，而一些数据的初始化是从下一行开始，所以要确保这个过程不会有一些因为变量没被初始化而形成的报错。
        _D.element = unitConfig.element;
        yield return (_D.Step1Initialize(unitConfig.TYPE, unitConfig.BASIC_MOVEMENT_PACK,unitConfig.SPECIAL_ZOKUSEI));
        yield return _D;
    }
}