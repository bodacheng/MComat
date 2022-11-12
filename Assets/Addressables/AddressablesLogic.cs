using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressablesLogic
{
    private static readonly IDictionary<string, long> Sizes = new Dictionary<string, long>();
    
    public static async UniTask Essentials()
    {
        await HurtObjectManager.CheckExistedKey();
        await EffectsManager.CheckExistedKey();
        HurtObjectManager.ConstructDPool();
    }
    
    static async UniTask<long> DownLoadSize(string label, Action exceptionProcess)
    {
        var handle = Addressables.GetDownloadSizeAsync(label);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var result = handle.Result;
            if (result > 0)
            {
                DicAdd<string,long>.Add(Sizes, label, result);
            }
            Addressables.Release(handle);
            return result;
        }
        else
        {
            Debug.Log($"Failed to get size : {label}");
            Addressables.Release(handle);
            exceptionProcess.Invoke();
            return default;
        }
    }
    
    static async UniTask DownLoadMission(string label, Action<string, float> progressUIRefresh)
    {
        progressUIRefresh("Downloading "+ label + " asset", 0);
        if (Sizes.ContainsKey(label))
        {
            var handle = Addressables.DownloadDependenciesAsync(label);
            while (!handle.IsDone)
            {
                progressUIRefresh("Downloading "+ label + " asset", handle.GetDownloadStatus().Percent);
                await UniTask.DelayFrame(0);
            }
            Addressables.Release(handle);
        }
    }
    
    public static async UniTask<long> GetWholeDownLoadSize(Action exception)
    {
        //Caching.ClearCache();
        
        long wholeSize = 0;
        
        wholeSize += await DownLoadSize("basic_anim", exception);
        wholeSize += await DownLoadSize("skill_anim", exception);
        wholeSize += await DownLoadSize("hurt_anim", exception);
        wholeSize += await DownLoadSize("knock_anim", exception);
        wholeSize += await DownLoadSize("unit", exception);
        wholeSize += await DownLoadSize("weapon", exception);
        wholeSize += await DownLoadSize("effect", exception);
        wholeSize += await DownLoadSize("quest", exception);
        wholeSize += await DownLoadSize("skill_icon", exception);
        wholeSize += await DownLoadSize("unit_icon", exception);
        wholeSize += await DownLoadSize("battle_ground", exception);
        wholeSize += await DownLoadSize("btn_effect", exception);

        return wholeSize;
    }
    
    public static async UniTask ResourcePrepareProcess(Action complete, Action<string, float> progressUIRefresh)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        Units.LoadUnitConfigs();
        SkillConfigTable.LoadAllSkillConfigs();
        
        await DownLoadMission("pic", progressUIRefresh);
        await DownLoadMission("basic_anim", progressUIRefresh);
        await DownLoadMission("skill_anim", progressUIRefresh);
        await DownLoadMission("hurt_anim", progressUIRefresh);
        await DownLoadMission("knock_anim", progressUIRefresh);
        await DownLoadMission("unit", progressUIRefresh);
        await DownLoadMission("weapon", progressUIRefresh);
        await DownLoadMission("effect", progressUIRefresh);
        await DownLoadMission("quest", progressUIRefresh);
        await DownLoadMission("skill_icon", progressUIRefresh);
        await DownLoadMission("unit_icon", progressUIRefresh);
        await DownLoadMission("battle_ground", progressUIRefresh);
        await DownLoadMission("btn_effect", progressUIRefresh);
        
        await HighLightLayer.LoadBg();
        
        complete.Invoke();
    }
    
    public static async UniTask<GameObject> LoadObject(string prefabPathName)
    {
        var handle = Addressables.InstantiateAsync(prefabPathName);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Failed to load : {prefabPathName}");
            Addressables.ReleaseInstance(handle);
            return default;
        }
        else
        {
            var _object = handle.Result; // インスタンス化されたもの
            _object.AddOnDestroyCallback( () =>
            {
                Addressables.ReleaseInstance(handle);
            });
            return _object;
        }
    }
    
    public static async UniTask<T> LoadTOnObject<T>(string prefabPathName)
    {
        var handle = Addressables.InstantiateAsync(prefabPathName);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Failed to load : {prefabPathName}");
            Addressables.ReleaseInstance(handle);
            return default;
        }
        else
        {
            var _object = handle.Result; // インスタンス化されたもの
            _object.AddOnDestroyCallback( () =>
            {
                Addressables.ReleaseInstance(handle);
            });
            var returnValue = _object.GetComponent<T>();
            return returnValue;
        }
    }
    
    private static readonly List<AsyncOperationHandle> LoadingHandlerList = new ();
    
    public static async UniTask<T> LoadT<T>(string prefabPathName)
    {
        var handle = Addressables.LoadAssetAsync<T>(prefabPathName);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Failed to load : {prefabPathName}");
            Addressables.Release(handle);
            return default;
        }
        else
        {
            LoadingHandlerList.Add(handle);
            return handle.Result;
        }
    }
    
    public static void ReleaseAsyncOperationHandles()
    {
        foreach (var handle in LoadingHandlerList)
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }
        LoadingHandlerList.Clear();
    }
}
