using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AssetBundleLoader : MonoBehaviour
{
    private IEnumerator characterComponentsDownload()
    {
        AssetBundle readingbundle;
        ////////// 下面统计各个type角色的必要组件 ////////////
        foreach (KeyValuePair<string,List<string>> keyValuePair in characterTypeAndBasicMoveSets)
        {
            //for (int i = 0; i < keyValuePair.Value.Count; i++)
            //{
            //    yield return defaultPools.Instance.LoadAnimationPackFromCache(AssetBundleLoader.BundleURL +"/animClips/"+ keyValuePair.Key + "/BasicPack", 
            //                                                                           "BasicPack/"+ keyValuePair.Key + "/" +  keyValuePair.Value[i],
            //                                                                         keyValuePair.Value[i]);
            //} 
            ////////////    以上内容为实时动画片段的预备和个性化片段对他们的覆盖   /////////////
            
            //底下这一套有一个特别关键的逻辑：它在通过是不是有对应动画包的key来判断到底这个包的动画有没有load到内存。
            if (!defaultPools.SeriesAnimationClipsDic.ContainsKey(keyValuePair.Key + "/basic_hurts"))
                yield return (defaultPools.Instance.LoadAnimationPackFromCache(AssetBundleLoader.BundleURL+ "/animClips/"+ keyValuePair.Key + "/hurtAndKnockOff", 
                                                                                keyValuePair.Key + "/basic_hurts",
                                                                                "basic_hurts"));
                
                
            if (!defaultPools.SeriesAnimationClipsDic.ContainsKey(keyValuePair.Key + "/basic_knockoffs"))
                yield return (defaultPools.Instance.LoadAnimationPackFromCache(AssetBundleLoader.BundleURL +"/animClips/"+ keyValuePair.Key + "/hurtAndKnockOff", 
                                                                                keyValuePair.Key + "/basic_knockoffs",
                                                                                "basic_knockoffs"));
            //上面这些就是说什么呢。。各个type的伤害动作包和基础动作包就不放进DownLoadMissionDic进行之后的集中下载了。上面的函数已经执行下载处理了。
            // 但上面这些存在一个比较大的坑就是这些下载过程直接把所有type角色的伤害动画和基础动画都载入了内存，如果这样做负担很大我们要研究对上面两个函数进行修改。
            //这个地方还差个各个type的generic_controller包下载的问题。
            
            ////// 寻找对应type的animator //////////////////
            RuntimeAnimatorController _RuntimeAnimatorController;
            defaultPools.Instance.RuntimeAnimatorControllerIdic.TryGetValue(keyValuePair.Key, out _RuntimeAnimatorController);
            if (_RuntimeAnimatorController == null)
            {
                _loadingProcess = defaultPools.Instance.LoadAnimatorFromCache(AssetBundleLoader.BundleURL + "/animClips/"+ keyValuePair.Key + "/animator", keyValuePair.Key, "generic_controller");
                yield return _loadingProcess;
            }
        }
        yield return downloadingProcess();
    }
}
