using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class HurtObjectManager
{
    static DecompositionerPool default_hitBoxPool;
    static readonly IDictionary<string, DecompositionerPool> HurtPoolDic = new Dictionary<string, DecompositionerPool>();
    static AsyncOperationHandle<GameObject> _handle;
    static readonly List<string> keyExists = new();
    public static async UniTask CheckExistedKey()
    {
        AsyncOperationHandle<IList<IResourceLocation>> locationHandle = Addressables.LoadResourceLocationsAsync("weapon");
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

    static GameObject TryLoadWeaponPrefab(string key)
    {
        if (!keyExists.Contains(key))
        {
            return null;
        }
        else
        {
            var returnValue = AddressablesLogic.LoadPrefab(key);
            return returnValue;
        }
    }
    
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
            var resultObject = Resources.Load("FightBasic/d_hitbox") as GameObject;
            if (resultObject == null)
            {
                Debug.Log("未能读取到默认hitbox");
                return;
            }
            
            default_hitBoxPool = new DecompositionerPool(resultObject);
            default_hitBoxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }
    
    public static void ConstructHurtObjectPool(string resource_name, string MagicForwardPath, Element element)
    {
        DecompositionerPool poolToConstruct;
        GameObject hurtObject = null;
        
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
            
            hurtObject = TryLoadWeaponPrefab("HurtObjects/" + MagicForwardPath + "/" + resource_name + ".prefab");
            
            if (hurtObject != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name, FightGlobalSetting._HurtObjectPreLoadCount);
                var decomposition = hurtObject.GetComponent<Decompositioner>();
                if (decomposition != null)
                {
                    if (decomposition.Attachments != null && decomposition.Attachments.Length > 0)
                    {
                        for (var i = 0; i < decomposition.Attachments.Length; i++)
                        {
                            ConstructHurtObjectPool(decomposition.Attachments[i], MagicForwardPath, element);
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
        
        hurtObject = TryLoadWeaponPrefab("HurtObjects/" + basicMagicForwardPath + "/" + resource_name + ".prefab");
        
        if (hurtObject != null)
        {
            poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,FightGlobalSetting._HurtObjectPreLoadCount);
            
            var decomposition = hurtObject.GetComponent<Decompositioner>();
            if (decomposition != null)
            {
                if (decomposition.Attachments != null && decomposition.Attachments.Length > 0)
                {
                    for (var i = 0; i < decomposition.Attachments.Length; i++)
                    {
                        ConstructHurtObjectPool(decomposition.Attachments[i],MagicForwardPath,element);
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
            
            hurtObject = TryLoadWeaponPrefab("HurtObjects/" + basicMagicForwardPath + "/" + resource_name + ".prefab");
            
            if (hurtObject != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,FightGlobalSetting._HurtObjectPreLoadCount);
                
                var d = hurtObject.GetComponent<Decompositioner>();
                if (d != null)
                {
                    if (d.Attachments != null && d.Attachments.Length > 0)
                    {
                        for (int i = 0; i < d.Attachments.Length; i++)
                        {
                            ConstructHurtObjectPool(d.Attachments[i],MagicForwardPath,element);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
            }
        }
    }
    
    static DecompositionerPool _hurtObjectPool;
    public static DecompositionerPool GetHurtObjectPool(string resource_name, string myMagicPath, string myDefaultMagicPath)
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
    
    static DecompositionerPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab, string key, int ini_count)
    {
        if (prefab != null)
        {
            var poolToConstruct = new DecompositionerPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")
            DicAdd<string, DecompositionerPool>.Add(HurtPoolDic, key, poolToConstruct);
            return poolToConstruct;
        }
        return null;
    }
}
