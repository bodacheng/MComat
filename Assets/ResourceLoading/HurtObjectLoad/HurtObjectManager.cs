using Cysharp.Threading.Tasks;
using UnityEngine;

public static class HurtObjectManager
{
    static DecompositionPool _defaultHitBoxPool;
    static readonly ResourcePoolRegistry<DecompositionPool> HurtPools = new ResourcePoolRegistry<DecompositionPool>();
    
    static UniTask<GameObject> TryLoadWeaponPrefab(string key)
    {
        if (!AddressablesLogic.CheckKeyExist(EffectResourceKeyUtility.WeaponLabel, key))
        {
            return default;
        }
        else
        {
            var returnValue =  AddressablesLogic.LoadT<GameObject>(key);
            return returnValue;
        }
    }
    
    public static DecompositionPool GetDPool()
    {
        return _defaultHitBoxPool;
    }
    
    public static void Clear()
    {
        HurtPools.Clear(pool => pool.Clear());
    }
    
    // 默认攻击物件池的创建
    public static async UniTask ConstructDPool()
    {
        _defaultHitBoxPool?.Clear();
        var resultObject = await AddressablesLogic.LoadT<GameObject>(
            EffectResourceKeyUtility.DefaultHitBoxAddress(FightGlobalSetting.EffectPathDefine()));
        if (resultObject == null)
        {
            return;
        }
        
        _defaultHitBoxPool = new DecompositionPool(resultObject);
        _defaultHitBoxPool.PreloadAsync(10, 1);
    }
    
    public static async UniTask ConstructHurtObjectPool(string resourceName, Element element, int preloadCount)
    {
        var defaultEffectPath = FightGlobalSetting.EffectPathDefine();
        foreach (var resourcePath in EffectResourceKeyUtility.ResourcePathFallbacks(
                     FightGlobalSetting.EffectPathDefine(element),
                     defaultEffectPath))
        {
            if (await TryConstructHurtObjectPoolAtPath(resourceName, resourcePath, element, preloadCount))
            {
                return;
            }
        }
    }

    static async UniTask<bool> TryConstructHurtObjectPoolAtPath(string resourceName, string resourcePath, Element element, int preloadCount)
    {
        var resourceKey = EffectResourceKeyUtility.ResourceKey(resourcePath, resourceName);
        if (HurtPools.TryGet(resourceKey, out _))
            return true;

        var weaponPrefab = await TryLoadWeaponPrefab(EffectResourceKeyUtility.PrefabAddress(resourcePath, resourceName));
        if (weaponPrefab == null)
            return false;

        ConstructHitBoxPoolWithPrefabAndKey(weaponPrefab, resourceKey, preloadCount);
        await ConstructAttachmentPools(weaponPrefab.GetComponent<Decomposition>(), element, preloadCount);
        return true;
    }

    static async UniTask ConstructAttachmentPools(Decomposition decomposition, Element element, int preloadCount)
    {
        if (decomposition?.Attachments == null || decomposition.Attachments.Length == 0)
            return;

        for (var i = 0; i < decomposition.Attachments.Length; i++)
        {
            await ConstructHurtObjectPool(decomposition.Attachments[i], element, preloadCount);
        }
    }
    
    static DecompositionPool _hurtObjectPool;
    public static DecompositionPool GetHurtObjectPool(string resource_name, string myDefaultMagicPath)
    {
        _hurtObjectPool = null;
        
        foreach (var resourcePath in EffectResourceKeyUtility.ResourcePathFallbacks(
                     myDefaultMagicPath,
                     FightGlobalSetting.EffectPathDefine()))
        {
            var resourceKey = EffectResourceKeyUtility.ResourceKey(resourcePath, resource_name);
            if (HurtPools.TryGet(resourceKey, out _hurtObjectPool))
            {
                return _hurtObjectPool;
            }
        }
        return null;
    }
    
    static DecompositionPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab, string key, int iniCount)
    {
        if (prefab != null)
        {
            var poolToConstruct = new DecompositionPool(prefab);
            poolToConstruct.PreloadAsync(iniCount, 1);
            HurtPools.AddOrReplace(key, poolToConstruct, iniCount);
            return poolToConstruct;
        }
        return null;
    }
}
