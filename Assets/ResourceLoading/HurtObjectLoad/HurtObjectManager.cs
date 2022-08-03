using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class HurtObjectManager
{
    static DecompositionPool default_hitBoxPool;
    static readonly IDictionary<string, DecompositionPool> HurtPoolDic = new Dictionary<string, DecompositionPool>();
    static AsyncOperationHandle<GameObject> _handle;
    static readonly List<string> keyExists = new();
    public static async UniTask CheckExistedKey()
    {
        var locationHandle = Addressables.LoadResourceLocationsAsync("weapon");
        await locationHandle.Task;
        if (locationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var weapon in locationHandle.Result)
            {
                if (!keyExists.Contains(weapon.PrimaryKey))
                {
                    keyExists.Add(weapon.PrimaryKey);
                }
            }
        }
        else
        {
            Debug.Log(" 注册武器列表失败？");
        }
        Addressables.Release(locationHandle);
    }

    static UniTask<GameObject> TryLoadWeaponPrefab(string key)
    {
        if (!keyExists.Contains(key))
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
            var resultObject = Resources.Load("FightBasic/d_hitbox") as GameObject;
            if (resultObject == null)
            {
                Debug.Log("未能读取到默认hitbox");
                return;
            }
            
            default_hitBoxPool = new DecompositionPool(resultObject);
            default_hitBoxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }
    
    public static async UniTask ConstructHurtObjectPool(string resource_name, string MagicForwardPath, Element element)
    {
        DecompositionPool poolToConstruct;
        GameObject weaponPrefab = null;
        
        /////////////// 第一环节 : 搜索个性魔法//////////////////
        if (MagicForwardPath != null)
        {
            if (HurtPoolDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
                {
                    return;
                }
            }
            
            weaponPrefab = await TryLoadWeaponPrefab("HurtObjects/" + MagicForwardPath + "/" + resource_name + ".prefab");
            
            if (weaponPrefab != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(weaponPrefab, MagicForwardPath + "/" + resource_name, FightGlobalSetting._HurtObjectPreLoadCount);
                var decomposition = weaponPrefab.GetComponent<Decomposition>();
                if (decomposition != null)
                {
                    if (decomposition.Attachments != null && decomposition.Attachments.Length > 0)
                    {
                        for (var i = 0; i < decomposition.Attachments.Length; i++)
                        {
                            await ConstructHurtObjectPool(decomposition.Attachments[i], MagicForwardPath, element);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
                return;
            }
        }
        
        ///////////////第二环节 ： 搜索属性魔法//////////////////
        string basicMagicForwardPath = FightGlobalSetting.EffectPathDefine(element);
        if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
            if (poolToConstruct != null)
                return;
        }
        
        weaponPrefab = await TryLoadWeaponPrefab("HurtObjects/" + basicMagicForwardPath + "/" + resource_name + ".prefab");
        
        if (weaponPrefab != null)
        {
            poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(weaponPrefab, basicMagicForwardPath + "/" + resource_name,FightGlobalSetting._HurtObjectPreLoadCount);
            
            var decomposition = weaponPrefab.GetComponent<Decomposition>();
            if (decomposition != null)
            {
                if (decomposition.Attachments != null && decomposition.Attachments.Length > 0)
                {
                    for (var i = 0; i < decomposition.Attachments.Length; i++)
                    {
                        await ConstructHurtObjectPool(decomposition.Attachments[i],MagicForwardPath,element);
                    }
                }
            }else{
                Debug.Log(resource_name + "没有Decompositioner！？");
            }
            return;
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
                    return;
            }
            
            weaponPrefab = await TryLoadWeaponPrefab("HurtObjects/" + basicMagicForwardPath + "/" + resource_name + ".prefab");
            
            if (weaponPrefab != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(weaponPrefab, basicMagicForwardPath + "/" + resource_name,FightGlobalSetting._HurtObjectPreLoadCount);
                
                var d = weaponPrefab.GetComponent<Decomposition>();
                if (d != null)
                {
                    if (d.Attachments != null && d.Attachments.Length > 0)
                    {
                        for (int i = 0; i < d.Attachments.Length; i++)
                        {
                            await ConstructHurtObjectPool(d.Attachments[i],MagicForwardPath,element);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
            }
        }
    }
    
    static DecompositionPool _hurtObjectPool;
    public static DecompositionPool GetHurtObjectPool(string resource_name, string myMagicPath, string myDefaultMagicPath)
    {
        _hurtObjectPool = null;
        
        // 第一轮
        if (myMagicPath != null)
        {
            if (HurtPoolDic.ContainsKey(myMagicPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(myMagicPath + "/" + resource_name, out _hurtObjectPool);
                if (_hurtObjectPool != null)
                {
                    return _hurtObjectPool;
                }
            }
        }
        
        // 第二轮
        if (HurtPoolDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
        {
            HurtPoolDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out _hurtObjectPool);
            if (_hurtObjectPool != null)
            {
                return _hurtObjectPool;
            }
        }
        
        // 第三轮
        if (myDefaultMagicPath != FightGlobalSetting.EffectPathDefine(Element.Null))
        {
            myDefaultMagicPath = FightGlobalSetting.EffectPathDefine(Element.Null);
            if (HurtPoolDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out _hurtObjectPool);
                if (_hurtObjectPool != null)
                {
                    return _hurtObjectPool;
                }
            }
        }
        return null;
    }
    
    static DecompositionPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab, string key, int ini_count)
    {
        if (prefab != null)
        {
            var poolToConstruct = new DecompositionPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});
            DicAdd<string, DecompositionPool>.Add(HurtPoolDic, key, poolToConstruct);
            return poolToConstruct;
        }
        return null;
    }
}
