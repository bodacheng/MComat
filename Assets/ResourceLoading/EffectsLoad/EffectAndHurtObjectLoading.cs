using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Animations;

public partial class EffectAndHurtObjectLoading
{
    static EffectAndHurtObjectLoading instance;
    public static EffectAndHurtObjectLoading Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EffectAndHurtObjectLoading();
            }
            return instance;
        }
    }
    
    public IDictionary<string, DecompositionerPool> EffectObjectPoolsDic = new Dictionary<string, DecompositionerPool>();//现在这里面包含了特效和伤害类物体。这个的重点是主界面和战斗界面通用问题
    public IDictionary<string, DecompositionerPool> HurtObjectPoolsDic = new Dictionary<string, DecompositionerPool>();//现在这里面包含了特效和伤害类物体。这个的重点是主界面和战斗界面通用问题
    
    // 首先这个函数注定不能实时化，这个过程必须是初始化中一个流程
    public IEnumerator PrepareMagicFromCach(string Path,string magicPackName)
    {
        IEnumerator task = CachManager.Instance.DownloadAndCacheExactFile(Path,magicPackName);
        yield return task;
        yield break;
    }
    
    public IEnumerator ConstructHurtObjectPool(string resource_name, string MagicForwardPath, Zokusei zokusei)
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
    
    DecompositionerPool HurtObjectPool;
    public DecompositionerPool GetHurtObjectPool(string resource_name, string myMagicPath, string myDefaultMagicPath)
    {
        HurtObjectPool = null;
        
        // 第一轮
        if (myMagicPath != null)
        {
            if (HurtObjectPoolsDic.ContainsKey(myMagicPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(myMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }

        // 第二轮
        if (HurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
        {
            HurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
            if (HurtObjectPool != null)
            {
                return HurtObjectPool;
            }
        }

        // 第三轮
        if (myDefaultMagicPath != "defaultmagic")
        {
            myDefaultMagicPath = "defaultmagic";
            if (HurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }
        return null;
    }

    DecompositionerPool EffectPool;
    GameObject EffectPrefab;
    public DecompositionerPool IniEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        EffectPrefab = null;
        EffectPool = null;
        if (EffectsPath != null)
        {
            if (EffectObjectPoolsDic.ContainsKey("Effects/" + EffectsPath + "/" + resource_name))
            {
                EffectObjectPoolsDic.TryGetValue("Effects/" + EffectsPath + "/" + resource_name, out EffectPool);
                if (EffectPool != null)
                    return EffectPool;
            }

            EffectPrefab = Resources.Load("Effects/" + EffectsPath + "/" + resource_name, typeof(GameObject)) as GameObject;
            if (EffectPrefab != null)
            {
                EffectPool = ConstructEffectPoolWithPrefabAndKey(EffectPrefab, "Effects/" + EffectsPath + "/" + resource_name,object_count);
                return EffectPool;
            }
            if (EffectsPath == "defaultmagic")
            {
                return null;//防止无限循环
            }
        }
        EffectPool = IniEffectsPool(resource_name, "defaultmagic", object_count);
        return EffectPool;
    }

    Decompositioner processingEffectObj;
    ConstraintSource myConstraintSource;
    public Decompositioner GenerateEffect(string resource_name, string EffectsPath,Vector3 Pos,Quaternion Qua,Transform parentsetT)
    {
        if (string.IsNullOrEmpty(resource_name))
            return null;
        EffectPool = IniEffectsPool(resource_name, EffectsPath,3);
        if (EffectPool == null)
            return null;
        processingEffectObj = EffectPool.Rent();
        if (parentsetT != null)
        {
            myConstraintSource.sourceTransform = parentsetT;
            myConstraintSource.weight = 1;
            processingEffectObj.GetPositionConstraint().SetSources(new List<ConstraintSource>{myConstraintSource});
            processingEffectObj.GetPositionConstraint().constraintActive = true;
            processingEffectObj.GetPositionConstraint().locked = true;
            processingEffectObj.GetPositionConstraint().translationOffset = Vector3.zero;
        }
        processingEffectObj.transform.position = Pos;
        processingEffectObj.transform.rotation = Qua;
        return processingEffectObj;
    }
    
    public Decompositioner GenerateEffect(string resource_name,string EffectsPath,Vector3 Pos)
    {
        if (string.IsNullOrEmpty(resource_name))
            return null;
        EffectPool = IniEffectsPool(resource_name, EffectsPath,3);
        if (EffectPool == null)
            return null;
        processingEffectObj = EffectPool.Rent();
        processingEffectObj.transform.position = Pos;
        return processingEffectObj;
    }
    
    public IEnumerator PrepareMagicFromStreamingAssets(string magicPackName)
    {
        AssetBundle readingMagicBundle;
        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/Magics/" + magicPackName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingMagicBundle = resultAssetBundle.assetBundle;//AB包
        yield return readingMagicBundle;
    }

}
