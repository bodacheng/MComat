using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public partial class HurtObjectManager
{
    public static IDictionary<string, DecompositionerPool> HurtPoolDic = new Dictionary<string, DecompositionerPool>();
    
    public static IEnumerator ConstructHurtObjectPool(string resource_name, string MagicForwardPath, Zokusei zokusei)
    {
         switch(ResourceLoadingSetting.MagicLoadingMode)
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
    
    static DecompositionerPool HurtObjectPool;
    public static DecompositionerPool GetHurtObjectPool(string resource_name, string myMagicPath, string myDefaultMagicPath)
    {
        HurtObjectPool = null;
        
        // 第一轮
        if (myMagicPath != null)
        {
            if (HurtPoolDic.ContainsKey(myMagicPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(myMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }
    
        // 第二轮
        if (HurtPoolDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
        {
            HurtPoolDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
            if (HurtObjectPool != null)
            {
                return HurtObjectPool;
            }
        }
    
        // 第三轮
        if (myDefaultMagicPath != "defaultmagic")
        {
            myDefaultMagicPath = "defaultmagic";
            if (HurtPoolDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }
        return null;
    }
    
    //其实上面那些特效啦什么的也应该把先查字典再决定是否load的函数写出来，但为什么没写呢？因为特效物体的来源其实存在些问题。一个是视效物体在本地，而伤害物体在包里，再一个还存在个属性文件夹问题，
    //而现在这两类东西你却安排在一个字典中。这就是为什么可能应该把字典分成两个
    public static DecompositionerPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab,string key,int ini_count)
    {
        if (prefab != null)
        {
            DecompositionerPool poolToConstruct = new DecompositionerPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")
    
            if (HurtPoolDic.ContainsKey(key))
                HurtPoolDic[key] = poolToConstruct;
            else
                HurtPoolDic.Add(new KeyValuePair<string, DecompositionerPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }
}
