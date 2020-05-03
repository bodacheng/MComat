using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HurtObjectManager
{
    public static IEnumerator ConstructHurtObjectPoolFromCach(string resource_name, string MagicForwardPath, Zokusei zokusei)
    {
        DecompositionerPool poolToConstruct;
        GameObject hurtObject;
        AssetBundle assetbundle = null;
        IEnumerator _loadingProcess;
    
        /////////////// 第一环节 //////////////////
        if (MagicForwardPath != null)
        {
            if (HurtPoolDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct);
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
    
        if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
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
            if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
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
