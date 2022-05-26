using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceDownLoad : MonoBehaviour
{
    public IEnumerator GetWholeDownLoadSize(Action<string> Complete)
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
        
        void downLoadSize(string label, Action OnComplete)
        {
            AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(label);
            getDownloadSize.Completed += (AsyncOperationHandle) =>
            {
                wholeSize += AsyncOperationHandle.Result;
                OnComplete.Invoke();
            };
        }
        
        while (!(basic_anim && skill_anim && hurt_anim && knock_anim && unit && weapon && effect && quest && skill_icon && unit_icon && battle_ground))
        {
            Debug.Log("正在计算下载文件总大小");
            yield return null;
        }
        
        string warn = "总大小" + wholeSize;
        Complete(warn);
    }
    
    public IEnumerator ResourcePrepareProcess(Action complete, Action<string,float> progressUIRefresh)
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
        
        complete.Invoke();
    }
    
    IEnumerator downLoadMission(string label, Action<string,float> progressUIRefresh)
    {
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        //Addressables.ClearDependencyCacheAsync(label);
        
        //Check the download size
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(label);
        yield return getDownloadSize;
        
        Debug.Log(getDownloadSize.Status);
        
        if (getDownloadSize.Result > 0)
        {
            Debug.Log("尝试开始下载："+label + " size: " + getDownloadSize.Result);
            var dl = Addressables.DownloadDependenciesAsync(label);
            dl.Completed += (asyncOperationHandle) =>
            {
            };
            while (dl.PercentComplete < 1 && !dl.IsDone)
            {
                progressUIRefresh("Downloading Asset: "+label, dl.PercentComplete);
                yield return null;
            }
        }
        else
        {
            Debug.Log("没有get到远程？："+label + "："+ getDownloadSize.Result);
        }
    }
}
