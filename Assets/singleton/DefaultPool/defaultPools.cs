using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;


// 在这个管理一切的单例模式里我们需要一个关卡加载失败flag。因为现在关卡的初始化流程需要运行多个模块的协程，
//而协程没有返回值一说，那你没法靠单线程上的一些东西去判断这些初始化过程到底有没有问题
public partial class defaultPools {

    private static defaultPools instance;

    public ResourceLoadMode ConfigFileLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode ModelLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode IconLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode AnimationLoadingMode = ResourceLoadMode.Resource;
    public ResourceLoadMode MagicLoadingMode = ResourceLoadMode.Resource;
    
    public List<string> FightLoadErrors = new List<string>();
    public IDictionary<string, EZObjectPool> EffectAndHurtObjectPoolsDic = new Dictionary<string, EZObjectPool>();//现在这里面包含了特效和伤害类物体。这个的重点是主界面和战斗界面通用问题
    public IDictionary<string, AudioClip> soundClipsDic = new Dictionary<string, AudioClip>();
    public IDictionary<string, RuntimeAnimatorController> RuntimeAnimatorControllerIdic = new Dictionary<string, RuntimeAnimatorController>();
    public static IDictionary<string, AnimationClip> AnimationClipDic = new Dictionary<string, AnimationClip>();
    public static IDictionary<string, List<AnimationClip>> SeriesAnimationClipsDic = new Dictionary<string, List<AnimationClip>>();//这个是用来记录那些一个包里好几个动画的
    public AssetBundle readingBundle;

    public static defaultPools Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new defaultPools();
            }
            return instance;
        }
    }

    public void ReparentPooledObjects(bool AllorJustInactiveones)
    {
        foreach(KeyValuePair<string, EZObjectPool> keyValuePair in EffectAndHurtObjectPoolsDic)
        {
            EZObjectPool.ReparentPooledObjects(keyValuePair.Value, AllorJustInactiveones);
        }
    }

    //其实上面那些特效啦什么的也应该把先查字典再决定是否load的函数写出来，但为什么没写呢？因为特效物体的来源其实存在些问题。一个是视效物体在本地，而伤害物体在包里，再一个还存在个属性文件夹问题，
    //而现在这两类东西你却安排在一个字典中。这就是为什么可能应该把字典分成两个
    public EZObjectPool constructPoolWithPrefabAndKey(GameObject prefab,string key)
    {
        EZObjectPool poolToConstruct;
        if (prefab != null)
        {
            poolToConstruct = EZObjectPool.CreateObjectPool(prefab, key, 2, true, false, true);
            poolToConstruct.InstantiatePool();

            if (EffectAndHurtObjectPoolsDic.ContainsKey(key))
                this.EffectAndHurtObjectPoolsDic[key] = poolToConstruct;
            else
                this.EffectAndHurtObjectPoolsDic.Add(new KeyValuePair<string, EZObjectPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }

    //战斗场景下角色实时读取动作走的是这个，所以必然的我们有了getAnimationClip和ConstructAnimationClip
    AnimationClip _AnimationClip;
    public AnimationClip getAnimationClip(string key)
    {
        _AnimationClip = null;
        AnimationClipDic.TryGetValue(key, out _AnimationClip);
        if (_AnimationClip != null)
        {
            return _AnimationClip;
        }
        return null;
    }

    public List<AnimationClip> getAnimationPack(string packkey)
    {
        List<AnimationClip> clips = null;
        SeriesAnimationClipsDic.TryGetValue(packkey, out clips);
        if (clips != null)
        {
            return clips;
        }
        return null;
    }
    
     public IEnumerator ConstructHurtObjectPool(string resource_name, string MagicForwardPath, zokusei zokusei)
     {
         switch(MagicLoadingMode)
         {
                case ResourceLoadMode.CachAB:
                yield return ConstructHurtObjectPoolFromCach(resource_name, MagicForwardPath, zokusei);
                break;
                case ResourceLoadMode.Resource:
                yield return ConstructHurtObjectPoolResourceMode(resource_name, MagicForwardPath, zokusei);
                break;
                case ResourceLoadMode.StreamingAssetAB:
                break;
         }
        yield break;
     }

    public RuntimeAnimatorController getRuntimeAnimatorController(string type)
    {
        RuntimeAnimatorController toLoadRuntimeAnimatorController;
        RuntimeAnimatorControllerIdic.TryGetValue(type,out toLoadRuntimeAnimatorController);
        return toLoadRuntimeAnimatorController;
    }
}
