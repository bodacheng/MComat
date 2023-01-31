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
    
    public static async UniTask<long> GetWholeDownLoadSize(Action exception, List<string> downLoadLabel)
    {
        //Caching.ClearCache();
        
        long wholeSize = 0;
        var downLoadSizeCal = new List<UniTask<long>>();
        foreach (var label in downLoadLabel)
        {
            downLoadSizeCal.Add(DownLoadSize(label, exception));
        }
        await UniTask.WhenAll(downLoadSizeCal);

        foreach (var kv in Sizes)
        {
            wholeSize += kv.Value;
        }
        return wholeSize;
    }
    
    public static async UniTask ResourcePrepareProcess(Action complete, Action<string, float> progressUIRefresh, List<string> downLoadLabel)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        Units.LoadUnitConfigs();
        SkillConfigTable.LoadAllSkillConfigs();
        
        var downLoadTasks = new List<UniTask>();
        foreach (var label in downLoadLabel)
        {
            downLoadTasks.Add(DownLoadMission(label, progressUIRefresh));
        }
        await UniTask.WhenAll(downLoadTasks);
        await HighLightLayer.LoadBg();
        await UnitInstructionLayer.LoadUnitImage();
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
    
    public static async UniTask<T> LoadT<T>(string prefabPathName, GameObject memoryReleaseTarget = null)
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
            if (memoryReleaseTarget == null)
            {
                LoadingHandlerList.Add(handle);
            }
            else
            {
                memoryReleaseTarget.AddOnDestroyCallback( () =>
                {
                    Addressables.ReleaseInstance(handle);
                });
            }
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
