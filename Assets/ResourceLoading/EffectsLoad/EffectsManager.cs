using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public partial class EffectsManager
{
    // 以下的重点是主界面和战斗界面通用问题
    static IDictionary<string, DecompositionerPool> EffectPoolsDic = new Dictionary<string, DecompositionerPool>();        
    public static Decompositioner GenerateEffect(string resource_name, string EffectsPath, Vector3 Pos, Quaternion Qua, Transform parentsetT)
    {
        if (string.IsNullOrEmpty(resource_name))
            return null;
        DecompositionerPool EffectPool = INIEffectsPool(resource_name, EffectsPath, 3);
        if (EffectPool == null)
            return null;
        Decompositioner processingEffectObj = EffectPool.Rent();
        ConstraintSource myConstraintSource = new ConstraintSource();
        if (parentsetT != null)
        {
            myConstraintSource.sourceTransform = parentsetT;
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
        
    public static DecompositionerPool ConstructEffectPoolWithPrefabAndKey(GameObject prefab, string key, int ini_count)
    {
        if (prefab != null)
        {
            DecompositionerPool poolToConstruct = new DecompositionerPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")
            if (EffectPoolsDic.ContainsKey(key))
                EffectPoolsDic[key] = poolToConstruct;
            else
                EffectPoolsDic.Add(new KeyValuePair<string, DecompositionerPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }
}