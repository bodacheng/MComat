using UniRx;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class EffectsManager
{
    public static Transform T;
    
    // 以下的重点是主界面和战斗界面通用问题
    static readonly IDictionary<string, DecompositionPool> EffectPoolsDic = new Dictionary<string, DecompositionPool>();
    static readonly List<string> keyExists = new();
    static AsyncOperationHandle<GameObject> _handle;
    
    public static async UniTask CheckExistedKey()
    {
        AsyncOperationHandle<IList<IResourceLocation>> locationHandle = Addressables.LoadResourceLocationsAsync("effect");
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
            Debug.Log("注册特效列表失败？");
        }
        Addressables.Release(locationHandle);
    }
    
    static UniTask<GameObject> TryLoadEffectPrefab(string key)
    {
        if (!keyExists.Contains(key))
        {
            return default;
        }
        else
        {
            var returnValue = AddressablesLogic.LoadT<GameObject>(key);
            return returnValue;
        }
    }
    
    public static void Clear()
    {
        foreach (var pool in EffectPoolsDic)
        {
            pool.Value.Clear();
        }
        EffectPoolsDic.Clear();
    }
    
    public static async UniTask<Decomposition> GenerateEffect(string resource_name, string EffectsPath, Vector3 Pos, Quaternion Qua, Transform parentT)
    {
        if (string.IsNullOrEmpty(resource_name))
            return default;
        var effectPool = await INIEffectsPool(resource_name, EffectsPath, 3);
        if (effectPool == null)
            return default;
        var processingEffectObj = effectPool.Rent();
        var myConstraintSource = new ConstraintSource();
        if (parentT != null)
        {
            myConstraintSource.sourceTransform = parentT;
            myConstraintSource.weight = 1;
            processingEffectObj.GetPositionConstraint().SetSources(new List<ConstraintSource> { myConstraintSource });
            processingEffectObj.GetPositionConstraint().locked = true;
            processingEffectObj.GetPositionConstraint().translationOffset = Vector3.zero;
            processingEffectObj.GetPositionConstraint().constraintActive = true;
        }else{
            myConstraintSource.weight = 0;
            processingEffectObj.GetPositionConstraint().constraintActive = false;
        }
        processingEffectObj.transform.position = Pos;
        processingEffectObj.transform.rotation = Qua;
        return processingEffectObj;
    }
    
    static DecompositionPool ConstructEffectPoolWithPrefabAndKey(GameObject prefab, string key, int ini_count)
    {
        var poolToConstruct = new DecompositionPool(prefab);
        poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")
        if (EffectPoolsDic.ContainsKey(key))
            EffectPoolsDic[key] = poolToConstruct;
        else
            EffectPoolsDic.Add(new KeyValuePair<string, DecompositionPool>(key, poolToConstruct));
        
        return poolToConstruct;
    }
    
    public static async UniTask<DecompositionPool> INIEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        DecompositionPool effectPool = null;
        if (EffectsPath != null)
        {
            if (EffectPoolsDic.ContainsKey("Effects/" + EffectsPath + "/" + resource_name))
            {
                EffectPoolsDic.TryGetValue("Effects/" + EffectsPath + "/" + resource_name, out effectPool);
                if (effectPool != null)
                    return effectPool;
            }
            
            var EffectPrefab = await TryLoadEffectPrefab("Effects/" + EffectsPath + "/" + resource_name + ".prefab");
            if (EffectPrefab != null)
            {
                effectPool = ConstructEffectPoolWithPrefabAndKey(EffectPrefab, "Effects/" + EffectsPath + "/" + resource_name, object_count);
                return effectPool;
            }
            if (EffectsPath == FightGlobalSetting.EffectPathDefine(Element.Null))
            {
                return null;//防止无限循环
            }
        }
        effectPool = await INIEffectsPool(resource_name, FightGlobalSetting.EffectPathDefine(Element.Null), object_count);
        return effectPool;
    }
}