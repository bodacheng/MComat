using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

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
    
    public static async UniTask GetWholeDownLoadSize(Action<string> Complete, Action exception)
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
        wholeSize += await DownLoadSize("icon_frame", exception);
        wholeSize += await DownLoadSize("btn_effect", exception);
        
        var warn = "Download size : " + wholeSize / 1024 + "MB" + "\n\n" + "Start to download";
        Complete(warn);
    }
    
    public static async UniTask ResourcePrepareProcess(Action complete, Action<string, float> progressUIRefresh)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        Units.LoadMonstersConfig();
        SkillConfigTable.LoadAllSkillConfigs();
        
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
        await DownLoadMission("icon_frame", progressUIRefresh);
        await DownLoadMission("btn_effect", progressUIRefresh);
        
        complete.Invoke();
    }
    
    public static async UniTask<GameObject> LoadObject(string prefabPathName)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Failed to load : {prefabPathName}");
            Addressables.Release(handle);
            return default;
        }
        else
        {
            var _object = Object.Instantiate(handle.Result);
            _object.AddOnDestroyCallback( () =>
            {
                Addressables.Release(handle);
            });
            return _object;
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
    
    public static async UniTask<T> LoadTOnObject<T>(string prefabPathName)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Failed to load : {prefabPathName}");
            Addressables.Release(handle);
            return default;
        }
        else
        {
            var prefab = handle.WaitForCompletion();
            var _object = Object.Instantiate(prefab);
            _object.AddOnDestroyCallback( () =>
            {
                Addressables.Release(handle);
            });
            var returnValue = _object.GetComponent<T>();
            return returnValue;
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
