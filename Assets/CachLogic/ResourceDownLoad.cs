using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// AssetBundle cache checker & loader with caching
// worsk by loading .manifest file from server and parsing hash string from it
// 资源下载策略：角色模型和技能动画先读取配置文件再根据配置文件一个个请求资源。
// controller的话从上面的步骤里搞一个type统计，从所有type里找对应的controller组件
// 魔法包直接代码引导去下那6个大包。
// 所有资源应该进行一个文件数量统计和容量统计。下载过程中应该是前台有一个动画告诉已经下载到多少。
// 还有个问题。。。我们看了下主场景里startup函数。。。发现的确很多加载性的东西分布在主场景的很多模块。。。
// 所以我们这么想，这个scene只负责资源加载而不负责信息加载。

// 一上来是要load所有的ab包，所有ab包包括什么呢 
// 魔法特效，所有角色，所有角色动画，所有角色controller文件，所有技能脚本，所有音乐包。
// 以上这些如果在load过程里出了任何问题，则应该终止程序运行。这些方法写在哪里都可以只要在头画面里运行就行。
// 然后如果程序运行途中这些下载完了的数据在读取时候出错，那怎么办？不管任何时候， 直接弹回资源确认画面。

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
        
        downLoadSize("basic_anim", () => { basic_anim = true; });
        downLoadSize("skill_anim", () => { skill_anim = true; });
        downLoadSize("hurt_anim",() => { hurt_anim = true; });
        downLoadSize("knock_anim",() => { knock_anim = true; });
        downLoadSize("unit",() => { unit = true; });
        downLoadSize("weapon",() => { weapon = true; });
        downLoadSize("effect",() => { effect = true; });
        downLoadSize("quest", () => { quest = true; });
        downLoadSize("skill_icon", () => { skill_icon = true; });
        
        void downLoadSize(string label, Action OnComplete)
        {
            AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(label);
            getDownloadSize.Completed += (AsyncOperationHandle) =>
            {
                wholeSize += AsyncOperationHandle.Result;
                OnComplete.Invoke();
            };
        }
        
        while (!(basic_anim && skill_anim && hurt_anim && knock_anim && unit && weapon && effect && quest && skill_icon))
        {
            Debug.Log("正在计算下载文件总大小");
            yield return null;
        }
        
        string warn = "总大小" + wholeSize;
        Complete(warn);
    }
    
    public IEnumerator ResourcePrepareProcess(Action Complete, Action<string,float> progressUIRefresh)
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
        
        Complete.Invoke();
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
            dl.Completed += (AsyncOperationHandle) =>
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
