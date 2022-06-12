using System;
using System.Collections;
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
    
    public static IEnumerator GetWholeDownLoadSize(Action<string> Complete)
    {
        Caching.ClearCache();
        
        long wholeSize = 0;

        bool basic_anim = false;
        bool skill_anim = false;
        bool hurt_anim = false;
        bool knock_anim = false;
        bool unit = false;
        bool weapon = false;
        bool effect = false;
        bool quest = false;
        bool skill_icon = false;
        bool unit_icon = false;
        bool battle_ground = false;
        bool icon_frame = false;
        bool btn_effect = false;
        
        downLoadSize("basic_anim", () => { basic_anim = true; });
        downLoadSize("skill_anim", () => { skill_anim = true; });
        downLoadSize("hurt_anim",() => { hurt_anim = true; });
        downLoadSize("knock_anim",() => { knock_anim = true; });
        downLoadSize("unit",() => { unit = true; });
        downLoadSize("weapon",() => { weapon = true; });
        downLoadSize("effect",() => { effect = true; });
        downLoadSize("quest", () => { quest = true; });
        downLoadSize("skill_icon", () => { skill_icon = true; });
        downLoadSize("unit_icon", () => { unit_icon = true; });
        downLoadSize("battle_ground", () => { battle_ground = true; });
        downLoadSize("icon_frame", () => { icon_frame = true; });
        downLoadSize("btn_effect", () => { btn_effect = true; });
        
        void downLoadSize(string label, Action OnComplete)
        {
            var getDownloadSize = Addressables.GetDownloadSizeAsync(label);
            getDownloadSize.Completed += (AsyncOperationHandle) =>
            {
                wholeSize += AsyncOperationHandle.Result;
                OnComplete.Invoke();
                Addressables.Release(getDownloadSize);
            };
        }
        
        while (!(basic_anim && skill_anim && hurt_anim && knock_anim && unit && weapon && effect && quest && skill_icon && unit_icon && battle_ground && icon_frame && btn_effect))
        {
            Debug.Log("正在计算下载文件总大小");
            yield return null;
        }
        
        var warn = "Download size : " + wholeSize / 1024 + "MB" + "\n\n" + "Start to download";
        Complete(warn);
    }
    
    public static IEnumerator ResourcePrepareProcess(Action complete, Action<string,float> progressUIRefresh)
    {
        Units.LoadMonstersConfig();
        SkillConfigTable.LoadAllSkillConfigs();
        
        yield return downLoadMission("basic_anim", progressUIRefresh);
        yield return downLoadMission("skill_anim", progressUIRefresh);
        yield return downLoadMission("hurt_anim", progressUIRefresh);
        yield return downLoadMission("knock_anim", progressUIRefresh);
        yield return downLoadMission("unit", progressUIRefresh);
        yield return downLoadMission("weapon", progressUIRefresh);
        yield return downLoadMission("effect", progressUIRefresh);
        yield return downLoadMission("quest", progressUIRefresh);
        yield return downLoadMission("skill_icon", progressUIRefresh);
        yield return downLoadMission("unit_icon", progressUIRefresh);
        yield return downLoadMission("battle_ground", progressUIRefresh);
        yield return downLoadMission("icon_frame", progressUIRefresh);
        yield return downLoadMission("btn_effect", progressUIRefresh);
        
        complete.Invoke();
    }
    
    static IEnumerator downLoadMission(string label, Action<string,float> progressUIRefresh)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        //Check the download size
        var getDownloadSize = Addressables.GetDownloadSizeAsync(label);
        yield return getDownloadSize;
        if (getDownloadSize.Result > 0)
        {
            Debug.Log("尝试开始下载："+label + " size: " + getDownloadSize.Result);
            var dl = Addressables.DownloadDependenciesAsync(label);
            dl.Completed += (asyncOperationHandle) =>
            {
                Addressables.Release(dl);
            };
            while (dl.PercentComplete < 1 && !dl.IsDone)
            {
                progressUIRefresh("Downloading "+ label + " asset", dl.PercentComplete);
                yield return null;
            }
        }
        else
        {
            Debug.Log("没有get到远程？："+label + "："+ getDownloadSize.Result);
        }
        Addressables.Release(getDownloadSize);
    }

    public static GameObject LoadObject(string prefabPathName)
    {
        try
        {
            var op = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
            var prefab = op.WaitForCompletion();
            var _object = GameObject.Instantiate(prefab);
            Addressables.Release(op);
            return _object;
        }
        catch (Exception e)
        {
            Debug.Log("包读取错误："+ e);
            return null;
        }
    }
    
    public static GameObject LoadPrefab(string prefabPathName)
    {
        try
        {
            var op = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
            var prefab = op.WaitForCompletion();
            Addressables.Release(op);
            return prefab;
        }
        catch (Exception e)
        {
            Debug.Log("包读取错误："+ e);
            return null;
        }
    }
    
    public static T LoadTOnObject<T>(string prefabPathName)
    {
        T returnValue = default;
        try
        {
            var op = Addressables.LoadAssetAsync<GameObject>(prefabPathName);
            var prefab = op.WaitForCompletion();
            var _object = GameObject.Instantiate(prefab);
            Addressables.Release(op);
            returnValue = _object.GetComponent<T>();
            return returnValue;
        }
        catch (Exception e)
        {
            Debug.Log("包读取错误："+ e);
            return returnValue;
        }
    }
    
    public static T LoadT<T>(string prefabPathName)
    {
        T prefab = default;
        try
        {
            var op = Addressables.LoadAssetAsync<T>(prefabPathName);
            prefab = op.WaitForCompletion();
            Addressables.Release(op);
            return prefab;
        }
        catch (Exception e)
        {
            Debug.Log("包读取错误："+ e);
            return prefab;
        }
    }
}
