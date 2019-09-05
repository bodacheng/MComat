using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class EffectAndHurtObjectLoading
{
    private static EffectAndHurtObjectLoading instance;
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
    
    public IDictionary<string, EZObjectPool> EffectAndHurtObjectPoolsDic = new Dictionary<string, EZObjectPool>();//现在这里面包含了特效和伤害类物体。这个的重点是主界面和战斗界面通用问题
    
    public void ReparentPooledObjects(bool AllorJustInactiveones)
    {
        foreach(KeyValuePair<string, EZObjectPool> keyValuePair in EffectAndHurtObjectPoolsDic)
        {
            EZObjectPool.ReparentPooledObjects(keyValuePair.Value, AllorJustInactiveones);
        }
    }
    
    // 首先这个函数注定不能实时化，这个过程必须是初始化中一个流程
    public IEnumerator PrepareMagicFromCach(string Path,string magicPackName)
    {
        IEnumerator task = CachManager.Instance.DownloadAndCacheExactFile(Path,magicPackName);
        yield return task;
        yield break;
    }
    
    //其实上面那些特效啦什么的也应该把先查字典再决定是否load的函数写出来，但为什么没写呢？因为特效物体的来源其实存在些问题。一个是视效物体在本地，而伤害物体在包里，再一个还存在个属性文件夹问题，
    //而现在这两类东西你却安排在一个字典中。这就是为什么可能应该把字典分成两个
    public EZObjectPool constructPoolWithPrefabAndKey(GameObject prefab,string key,int ini_count)
    {
        EZObjectPool poolToConstruct;
        if (prefab != null)
        {
            poolToConstruct = EZObjectPool.CreateObjectPool(prefab, key, ini_count, true, false, true);
            poolToConstruct.InstantiatePool();

            if (EffectAndHurtObjectPoolsDic.ContainsKey(key))
                this.EffectAndHurtObjectPoolsDic[key] = poolToConstruct;
            else
                this.EffectAndHurtObjectPoolsDic.Add(new KeyValuePair<string, EZObjectPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }
    
     public IEnumerator ConstructHurtObjectPool(string resource_name, string MagicForwardPath, zokusei zokusei)
     {
         switch(ResourceLoadingSetting.Instance.MagicLoadingMode)
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
     
    public IEnumerator ConstructHurtObjectPoolFromCach(string resource_name, string MagicForwardPath, zokusei zokusei)
    {
        EZObjectPool poolToConstruct2;
        GameObject hurtObject;
        AssetBundle assetbundle = null;
        IEnumerator _loadingProcess;

        /////////////// 第一环节 //////////////////
        if (MagicForwardPath != null)
        {
            if (EffectAndHurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }
            _loadingProcess = CachManager.Instance.getABFromCach("Magics",MagicForwardPath);
            yield return _loadingProcess;
            if (_loadingProcess .Current != null)
            {
                assetbundle = (AssetBundle)_loadingProcess.Current;
            }
            else
            {
                Debug.Log("魔法包："+MagicForwardPath+"读取失败");
                yield break;
            }
            
            if (assetbundle != null)
            {
                hurtObject = assetbundle.LoadAsset<GameObject>(resource_name);
                assetbundle.Unload(false);
                if (hurtObject != null)
                {
                    poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name,2);
                    yield break;
                }
            }
        }

        ///////////////第二环节//////////////////         
        string basicMagicForwardPath;
        switch (zokusei)
        {
            case zokusei.darkMagic:
                basicMagicForwardPath = "darkmagic";
                break;
            case zokusei.blueMagic:
                basicMagicForwardPath = "bluemagic";
                break;
            case zokusei.greenMagic:
                basicMagicForwardPath = "greenmagic";
                break;
            case zokusei.lightMagic:
                basicMagicForwardPath = "lightmagic";
                break;
            case zokusei.redMagic:
                basicMagicForwardPath = "redmagic";
                break;
            default:
                basicMagicForwardPath = "defaultmagic";
                break;
        }

        if (EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
            if (poolToConstruct2 != null)
                yield break;
        }
        
        _loadingProcess = CachManager.Instance.getABFromCach("Magics",basicMagicForwardPath);
        yield return _loadingProcess;
        if (_loadingProcess .Current != null)
        {
            assetbundle = (AssetBundle)_loadingProcess.Current;
        }
        else
        {
            Debug.Log("魔法包："+basicMagicForwardPath+"读取失败");
            yield break;
        }
        if (assetbundle != null)
        {
            hurtObject = assetbundle.LoadAsset<GameObject>(resource_name);
            assetbundle.Unload(false);
            if (hurtObject != null)
            {
                poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                yield break;
            }
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }
            _loadingProcess = CachManager.Instance.getABFromCach("Magics",basicMagicForwardPath);
            yield return _loadingProcess;
            if (_loadingProcess .Current != null)
            {
                assetbundle = (AssetBundle)_loadingProcess.Current;
            }
            else
            {
                Debug.Log("魔法包："+basicMagicForwardPath+"读取失败");
                yield break;
            }
            if (assetbundle != null)
            {
                hurtObject = assetbundle.LoadAsset<GameObject>(resource_name);
                assetbundle.Unload(false);
                if (hurtObject != null)
                {
                    poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                    yield break;
                }
            }
            else
            {
                Debug.Log("没能读取：" + resource_name + "，路径名：" + basicMagicForwardPath);
            }
        }
    }
    
    private EZObjectPool HurtObjectPool;
    private GameObject hurtObjectToHurt;
    public EZObjectPool getHurtObjectPool(string resource_name, string myMagicPath, string myDefaultMagicPath)
    {
        HurtObjectPool = null;
        hurtObjectToHurt = null;

        // 第一轮
        if (myMagicPath != null)
        {
            if (EffectAndHurtObjectPoolsDic.ContainsKey(myMagicPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(myMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }

        // 第二轮
        if (EffectAndHurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
        {
            EffectAndHurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
            if (HurtObjectPool != null)
            {
                return HurtObjectPool;
            }
        }

        // 第三轮
        if (myDefaultMagicPath != "defaultmagic")
        {
            myDefaultMagicPath = "defaultmagic";
            if (EffectAndHurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }
        return null;
    }
    
    private EZObjectPool EffectPool;
    private GameObject EffectPrefab;
    public EZObjectPool iniEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        EffectPrefab = null;
        EffectPool = null;
        if (EffectsPath != null)
        {
            if (EffectAndHurtObjectPoolsDic.ContainsKey("Effects" + "/" + EffectsPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue("Effects" + "/" + EffectsPath + "/" + resource_name, out EffectPool);
                if (EffectPool != null)
                    return EffectPool;
            }

            EffectPrefab = Resources.Load("Effects" + "/" + EffectsPath + "/" + resource_name, typeof(GameObject)) as GameObject;
            if (EffectPrefab != null)
            {
                EffectPool = constructPoolWithPrefabAndKey(EffectPrefab, "Effects" + "/" + EffectsPath + "/" + resource_name,object_count);
                return EffectPool;
            }
            if (EffectsPath == "defaultEffects")
            {
                return null;//防止无限循环
            }
        }
        EffectPool = iniEffectsPool(resource_name, "defaultEffects", object_count);
        return EffectPool;
    }

    private GameObject processingEffectObj;
    public GameObject GenerateEffect(string resource_name, string EffectsPath,Vector3 Pos,Quaternion Qua,Transform _setParent)
    {
        EffectPool = iniEffectsPool(resource_name, EffectsPath,3);
        if (EffectPool == null)
            return null;
        processingEffectObj = EffectPool.TryGetNextObject(Pos, Qua);
        if (_setParent != null)
        {
            processingEffectObj.transform.SetParent(_setParent);
            //processingEffectObj.transform.localScale = Vector3.one;
            processingEffectObj.transform.localPosition = Vector3.zero;
        }
        return processingEffectObj;
    }

    public IEnumerator ConstructHurtObjectPoolResourceMode(string resource_name, string MagicForwardPath, zokusei _zokusei)
    {
        EZObjectPool poolToConstruct2;
        GameObject hurtObject;
        /////////////// 第一环节 : 搜索个性魔法//////////////////
        if (MagicForwardPath != null)
        {
            if (EffectAndHurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }

            hurtObject = Resources.Load("HurtObjects/" + MagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name,2);
                yield break;
            }
        }

        ///////////////第二环节 ： 搜索属性魔法//////////////////
        string basicMagicForwardPath = "defaultmagic";
        switch (_zokusei)
        {
            case zokusei.darkMagic:
                basicMagicForwardPath = "darkmagic";
                break;
            case zokusei.blueMagic:
                basicMagicForwardPath = "bluemagic";
                break;
            case zokusei.greenMagic:
                basicMagicForwardPath = "greenmagic";
                break;
            case zokusei.lightMagic:
                basicMagicForwardPath = "lightmagic";
                break;
            case zokusei.redMagic:
                basicMagicForwardPath = "redmagic";
                break;
            default:
                basicMagicForwardPath = "defaultmagic";
                break;
        }
        if (EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
            if (poolToConstruct2 != null)
                yield break;
        }
        hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
        if (hurtObject != null)
        {
            poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
            yield break;
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }
            hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct2 = constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                yield break;
            }
        }
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
