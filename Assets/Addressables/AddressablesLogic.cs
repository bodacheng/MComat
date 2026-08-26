using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

public static class AddressablesLogic
{
    public static UniTask CheckExistedKey(string tag)
    {
        return AddressablesAssetLoader.IndexTag(tag);
    }

    public static bool CheckKeyExist(string tag, string primaryKey)
    {
        return AddressablesAssetLoader.HasKey(tag, primaryKey);
    }

    public static bool HasIndexedTag(string tag)
    {
        return AddressablesAssetLoader.HasIndexedTag(tag);
    }

    public static async UniTask<bool> VersionConfirm()
    {
        await DownLoadMission(AddressablesResourcePolicy.AppVersionKey, _ => { });
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(AddressablesResourcePolicy.AppVersionKey);
        try
        {
            while (!handle.IsDone)
                await UniTask.DelayFrame(0);

            if (handle.Status != AsyncOperationStatus.Succeeded)
                return false;

            var jsonNode = JObject.Parse(handle.Result.text);
            var serverVersion = jsonNode[AddressablesResourcePolicy.VersionJsonProperty]?.ToString();
            var currentVersion = Application.version;
            Debug.Log("currentVersion:" + currentVersion);
            Debug.Log("serverVersion:" + serverVersion);
            return AddressablesResourcePolicy.IsServerVersionNewer(currentVersion, serverVersion);
        }
        finally
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }
    }

    public static async UniTask DownLoadConfig()
    {
        await DownLoadMission(AddressablesResourcePolicy.ConfigLabel, _ => { });
    }

    public static async UniTask<CommonSetting> GetCommonSetting()
    {
        return await AddressablesAssetLoader.LoadT<CommonSetting>(AddressablesResourcePolicy.CommonSettingKey);
    }

    public static async UniTask Essentials()
    {
        await UniTask.WhenAll(AddressablesResourcePolicy.MCombatEssentialLabels.Select(CheckExistedKey));
    }

    static async UniTask HandleLoadFailure<T>(string key)
    {
        Debug.LogWarning(AddressablesResourcePolicy.LoadFailureMessage(key));
        if (AddressablesResourcePolicy.ShouldReturnToStartOnLoadFailure<T>())
            await LoadErrorThenBackToStart();
    }

    static UniTask<bool> DownLoadMission(string label, Action<string> progressUIRefresh)
    {
        return AddressablesDependencyDownloader.DownloadDependencies(
            label,
            progressUIRefresh,
            AddressablesResourcePolicy.DownloadProgressText(AppSetting.Value.Language));
    }

    public static UniTask<long> GetWholeDownLoadSize(Action<string> exception, List<string> downLoadLabel)
    {
        return AddressablesDependencyDownloader.GetWholeDownloadSize(downLoadLabel, exception);
    }

    public static long DownloadedBytes => AddressablesDependencyDownloader.DownloadedBytes;

    public static async UniTask ResourcePrepareProcess(
        Action complete,
        Action<string> progressUIRefresh,
        List<string> downLoadLabel)
    {
        var success = await AddressablesDependencyDownloader.DownloadRequiredDependencies(
            downLoadLabel,
            progressUIRefresh,
            AddressablesResourcePolicy.DownloadProgressText(AppSetting.Value.Language));

        if (!success)
        {
            await LoadErrorThenBackToStart();
            return;
        }
        complete.Invoke();
    }

    public static UniTask<GameObject> LoadObject(
        string prefabPathName,
        Vector3 pos = default,
        Action<float> onProgress = null)
    {
        return AddressablesAssetLoader.LoadObject(
            prefabPathName,
            pos,
            _ => LoadErrorThenBackToStart(),
            onProgress);
    }

    public static UniTask<T> LoadTOnObject<T>(string prefabPathName)
    {
        return AddressablesAssetLoader.LoadTOnObject<T>(
            prefabPathName,
            onFailure: _ => LoadErrorThenBackToStart());
    }

    public static UniTask<T> LoadTOnObject<T>(
        string prefabPathName,
        GameObject memoryReleaseTarget = null,
        CancellationTokenSource cancellationTokenSource = null)
    {
        return AddressablesAssetLoader.LoadTOnObject<T>(
            prefabPathName,
            memoryReleaseTarget,
            cancellationTokenSource,
            _ => LoadErrorThenBackToStart());
    }

    public static UniTask<T> LoadT<T>(string prefabPathName, GameObject memoryReleaseTarget = null)
    {
        return AddressablesAssetLoader.LoadT<T>(
            prefabPathName,
            memoryReleaseTarget,
            HandleLoadFailure<T>);
    }

    public static UniTask<T> LoadT<T>(IResourceLocation location, GameObject memoryReleaseTarget = null)
    {
        return AddressablesAssetLoader.LoadT<T>(
            location,
            memoryReleaseTarget,
            HandleLoadFailure<T>);
    }

    public static void ReleaseAsyncOperationHandles()
    {
        AddressablesAssetLoader.ReleaseRetainedHandles();
    }

    static async UniTask LoadErrorThenBackToStart()
    {
        ProgressLayer.Loading("download error");
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        if (SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.LoadScene(0);
    }
}
