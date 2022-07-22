using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressablesLogic
{
    public static async void Essentials()
    {
        await HurtObjectManager.CheckExistedKey();
        await EffectsManager.CheckExistedKey();
        HurtObjectManager.ConstructDPool();
    }
    
    static async UniTask downLoadSize(string label, long size)
    {
        var dl = Addressables.GetDownloadSizeAsync(label);
        while (!dl.IsDone)
        {
            await UniTask.DelayFrame(1);
        }
        if (dl.Status == AsyncOperationStatus.Succeeded)
        {
            size += dl.Result;
        }
        Addressables.Release(dl);
    }
    
    static async UniTask downLoadMission(string label, Action<string,float> progressUIRefresh)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        //Check the download size
        var dl = Addressables.DownloadDependenciesAsync(label);
        while (!dl.IsDone)
        {
            progressUIRefresh("Downloading "+ label + " asset", dl.GetDownloadStatus().Percent);
            await UniTask.DelayFrame(1);
        }
        Addressables.Release(dl);
    }
    
    public static async UniTask GetWholeDownLoadSize(Action<string> Complete)
    {
        Caching.ClearCache();
        
        long wholeSize = 0;
        
        await downLoadSize("basic_anim", wholeSize);
        await downLoadSize("skill_anim", wholeSize);
        await downLoadSize("hurt_anim", wholeSize);
        await downLoadSize("knock_anim", wholeSize);
        await downLoadSize("unit", wholeSize);
        await downLoadSize("weapon", wholeSize);
        await downLoadSize("effect", wholeSize);
        await downLoadSize("quest", wholeSize);
        await downLoadSize("skill_icon", wholeSize);
        await downLoadSize("unit_icon", wholeSize);
        await downLoadSize("battle_ground", wholeSize);
        await downLoadSize("icon_frame", wholeSize);
        await downLoadSize("btn_effect", wholeSize);
        
        var warn = "Download size : " + wholeSize / 1024 + "MB" + "\n\n" + "Start to download";
        Complete(warn);
    }
    
    public static async UniTask ResourcePrepareProcess(Action complete, Action<string,float> progressUIRefresh)
    {
        Units.LoadMonstersConfig();
        SkillConfigTable.LoadAllSkillConfigs();
        
        await downLoadMission("basic_anim", progressUIRefresh);
        await downLoadMission("skill_anim", progressUIRefresh);
        await downLoadMission("hurt_anim", progressUIRefresh);
        await downLoadMission("knock_anim", progressUIRefresh);
        await downLoadMission("unit", progressUIRefresh);
        await downLoadMission("weapon", progressUIRefresh);
        await downLoadMission("effect", progressUIRefresh);
        await downLoadMission("quest", progressUIRefresh);
        await downLoadMission("skill_icon", progressUIRefresh);
        await downLoadMission("unit_icon", progressUIRefresh);
        await downLoadMission("battle_ground", progressUIRefresh);
        await downLoadMission("icon_frame", progressUIRefresh);
        await downLoadMission("btn_effect", progressUIRefresh);
        
        complete.Invoke();
    }
    
    public static GameObject LoadObject(string prefabPathName)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
        var prefab = op.WaitForCompletion();
        var _object = GameObject.Instantiate(prefab);
        _object.AddOnDestroyCallback( () =>
        {
            Debug.Log("释放："+prefabPathName);
            Addressables.Release(op);
        });
        return _object;
    }


    private static readonly List<AsyncOperationHandle> loadingHandlerList = new List<AsyncOperationHandle>();
    
    public static T LoadT<T>(string prefabPathName)
    {
        var op = Addressables.LoadAssetAsync<T>(prefabPathName);
        var target = op.WaitForCompletion();
        loadingHandlerList.Add(op);
        return target;
    }

    public static void ReleaseAsyncOperationHandles()
    {
        foreach (var handle in loadingHandlerList)
        {
            Addressables.Release(handle);
        }
        loadingHandlerList.Clear();
    }
    
    public static T LoadTOnObject<T>(string prefabPathName)
    {
        T returnValue = default;
        var op = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
        var prefab = op.WaitForCompletion();
        var _object = GameObject.Instantiate(prefab);
        _object.AddOnDestroyCallback( () =>
        {
            Debug.Log("释放："+prefabPathName);
            Addressables.Release(op);
        });
        returnValue = _object.GetComponent<T>();
        return returnValue;
    }
}
