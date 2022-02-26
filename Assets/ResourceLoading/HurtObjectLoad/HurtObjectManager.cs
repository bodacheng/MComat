using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public partial class HurtObjectManager
{
    static DecompositionerPool default_hitBoxPool;
    static readonly IDictionary<string, DecompositionerPool> HurtPoolDic = new Dictionary<string, DecompositionerPool>();
    
    
    public static DecompositionerPool GetDPool()
    {
        return default_hitBoxPool;
    }
    
    public static void Clear()
    {
        foreach (var keyValuePair in HurtPoolDic)
        {
            keyValuePair.Value.Clear();
        }
        HurtPoolDic.Clear();
    }
    
    // 默认攻击物件池的创建
    public static void ConstructDPool()
    {
        if (default_hitBoxPool == null)
        {
            var hurtObject = Resources.Load("HurtObjects/defaultmagic/d_hitbox") as GameObject;
            default_hitBoxPool = new DecompositionerPool(hurtObject);
            default_hitBoxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }

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
        if (myDefaultMagicPath != FightGlobalSetting.EffectPathDefine(Zokusei.Null))
        {
            myDefaultMagicPath = FightGlobalSetting.EffectPathDefine(Zokusei.Null);
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
    
    static DecompositionerPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab, string key, int ini_count)
    {
        if (prefab != null)
        {
            var poolToConstruct = new DecompositionerPool(prefab);
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
