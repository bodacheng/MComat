using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public partial class EffectAndHurtObjectLoading
{
    //其实上面那些特效啦什么的也应该把先查字典再决定是否load的函数写出来，但为什么没写呢？因为特效物体的来源其实存在些问题。一个是视效物体在本地，而伤害物体在包里，再一个还存在个属性文件夹问题，
    //而现在这两类东西你却安排在一个字典中。这就是为什么可能应该把字典分成两个
    public DecompositionerPool ConstructHitBoxPoolWithPrefabAndKey(GameObject prefab,string key,int ini_count)
    {
        if (prefab != null)
        {
            DecompositionerPool poolToConstruct = new DecompositionerPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")

            if (HurtObjectPoolsDic.ContainsKey(key))
                HurtObjectPoolsDic[key] = poolToConstruct;
            else
                HurtObjectPoolsDic.Add(new KeyValuePair<string, DecompositionerPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }
    
    public DecompositionerPool ConstructEffectPoolWithPrefabAndKey(GameObject prefab,string key,int ini_count)
    {
        if (prefab != null)
        {
            DecompositionerPool poolToConstruct = new DecompositionerPool(prefab);
            poolToConstruct.PreloadAsync(ini_count, 1).Subscribe(_ => {});//Debug.Log("已经为对象池:"+key+"预留"+ini_count+"个物件")

            if (EffectObjectPoolsDic.ContainsKey(key))
                EffectObjectPoolsDic[key] = poolToConstruct;
            else
                EffectObjectPoolsDic.Add(new KeyValuePair<string, DecompositionerPool>(key, poolToConstruct));
            return poolToConstruct;
        }
        return null;
    }
    
    public IEnumerator ConstructHurtObjectPoolResourceMode(string resource_name, string MagicForwardPath, Zokusei _zokusei)
    {
        DecompositionerPool poolToConstruct;
        GameObject hurtObject;
        /////////////// 第一环节 : 搜索个性魔法//////////////////
        if (MagicForwardPath != null)
        {
            if (HurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
                {
                    yield break;
                }
            }
            hurtObject = Resources.Load("HurtObjects/" + MagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name,2);
                
                Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                if (decompositioner != null)
                {
                    if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                    {
                        for (int i = 0; i < decompositioner.Attachments.Length; i++)
                        {
                            yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,_zokusei);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
                
                yield break;
            }
        }

        ///////////////第二环节 ： 搜索属性魔法//////////////////
        string basicMagicForwardPath = FightGlobalSetting.EffectPathDefine(_zokusei);
        if (HurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            HurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
            if (poolToConstruct != null)
                yield break;
        }
        hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
        if (hurtObject != null)
        {
            poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
            
            Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
            if (decompositioner != null)
            {
                if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                {
                    for (int i = 0; i < decompositioner.Attachments.Length; i++)
                    {
                        yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,_zokusei);
                    }
                }
            }else{
                Debug.Log(resource_name + "没有Decompositioner！？");
            }
            
            yield break;
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (HurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
                    yield break;
            }
            hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                
                Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                if (decompositioner != null)
                {
                    if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                    {
                        for (int i = 0; i < decompositioner.Attachments.Length; i++)
                        {
                            yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,_zokusei);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
                
                yield break;
            }
        }
    }
    
    public IEnumerator ConstructHurtObjectPoolFromCach(string resource_name, string MagicForwardPath, Zokusei zokusei)
    {
        DecompositionerPool poolToConstruct;
        GameObject hurtObject;
        AssetBundle assetbundle = null;
        IEnumerator _loadingProcess;

        /////////////// 第一环节 //////////////////
        if (MagicForwardPath != null)
        {
            if (HurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
                {
                    yield break;
                }
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
                    poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name,2);
                    Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                    if (decompositioner != null)
                    {
                        if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                        {
                            for (int i = 0; i < decompositioner.Attachments.Length; i++)
                            {
                                yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,zokusei);
                            }
                        }
                    }else{
                        Debug.Log(resource_name + "没有Decompositioner！？");
                    }
                    yield break;
                }
            }
        }

        ///////////////第二环节//////////////////         
        string basicMagicForwardPath = FightGlobalSetting.EffectPathDefine(zokusei);

        if (HurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            HurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
            if (poolToConstruct != null)
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
                poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                if (decompositioner != null)
                {
                    if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                    {
                        for (int i = 0; i < decompositioner.Attachments.Length; i++)
                        {
                            yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,zokusei);
                        }
                    }
                }else{
                    Debug.Log(resource_name + "没有Decompositioner！？");
                }
                yield break;
            }
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (HurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                HurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
                if (poolToConstruct != null)
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
                    poolToConstruct = ConstructHitBoxPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name,2);
                    Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                    if (decompositioner != null)
                    {
                        if (decompositioner.Attachments != null && decompositioner.Attachments.Length > 0)
                        {
                            for (int i = 0; i < decompositioner.Attachments.Length; i++)
                            {
                                yield return ConstructHurtObjectPool(decompositioner.Attachments[i],MagicForwardPath,zokusei);
                            }
                        }
                    }else{
                        Debug.Log(resource_name + "没有Decompositioner！？");
                    }
                    yield break;
                }
            }
            else
            {
                Debug.Log("没能读取：" + resource_name + "，路径名：" + basicMagicForwardPath);
            }
        }
    }
}
