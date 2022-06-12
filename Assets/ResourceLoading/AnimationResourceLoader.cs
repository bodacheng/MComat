using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AnimationResourceLoader
{
    static AnimationResourceLoader instance;
    public static AnimationResourceLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimationResourceLoader();
            }
            return instance;
        }
    }
    
    public static readonly IDictionary<string, AnimationClip> AnimationClipDic = new Dictionary<string, AnimationClip>();
    public static readonly IDictionary<string, List<AnimationClip>> SeriesAnimationClipsDic = new Dictionary<string, List<AnimationClip>>();//这个是用来记录那些一个包里好几个动画的
    //public IDictionary<string, RuntimeAnimatorController> RuntimeAnimatorControllerIdic = new Dictionary<string, RuntimeAnimatorController>();
    
    //战斗场景下角色实时读取动作走的是这个，所以必然的我们有了getAnimationClip和ConstructAnimationClip
    public AnimationClip GetAnimationClip(string key)
    {
        AnimationClipDic.TryGetValue(key, out AnimationClip _AnimationClip);
        return _AnimationClip;
    }

    public static IEnumerator DownloadAnim(string type, string key)
    {
        var clipKey = type + "/skill/" + key;
        if (AnimationClipDic.ContainsKey(clipKey))
        {
            yield break;
        }
        
        var load = Addressables.LoadAssetAsync<AnimationClip>("Animation/"+ type+"/skill/"+ key +".anim");
        yield return load;
        if (load.Status == AsyncOperationStatus.Succeeded)
        {
            AnimationClipDic.Add(clipKey, load.Result);
        }
        Addressables.Release(load);
    }
}

//public RuntimeAnimatorController getRuntimeAnimatorController(string type)
    //{
    //    RuntimeAnimatorController toLoadRuntimeAnimatorController;
    //    RuntimeAnimatorControllerIdic.TryGetValue(type,out toLoadRuntimeAnimatorController);
    //    return toLoadRuntimeAnimatorController;
    //}
    
        //if (!RuntimeAnimatorControllerIdic.ContainsKey(type))
        //{
        //    RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        //    if (toLoadRuntimeAnimatorController != null)
        //        RuntimeAnimatorControllerIdic.Add(type, toLoadRuntimeAnimatorController);
        //    else
        //        Debug.Log("找不到动画控制器："+"Animations/" + type + "/generic_controller");
        //}else{
        //    if (RuntimeAnimatorControllerIdic[type] == null)
        //    {
        //        RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        //        if (toLoadRuntimeAnimatorController != null)
        //            RuntimeAnimatorControllerIdic[type] = toLoadRuntimeAnimatorController;
        //        else
        //            Debug.Log("找不到动画控制器："+"Animations/" + type + "/generic_controller");
        //    }
        //}