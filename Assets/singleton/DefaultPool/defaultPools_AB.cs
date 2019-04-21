using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public partial class defaultPools
{
    public IEnumerator ConstructHurtObjectPoolFromCach(string resource_name, string MagicForwardPath, zokusei zokusei)
    {
        EZObjectPool poolToConstruct2;
        GameObject hurtObject;
        AssetBundle assetbundle = null;
        IEnumerator _loadingProcess;

        /////////////// 第一环节 //////////////////
        if (MagicForwardPath != null)
        {
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }
            _loadingProcess = defaultPools.Instance.getABFromCach("Magics",MagicForwardPath);
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
                    poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name);
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

        if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
            if (poolToConstruct2 != null)
                yield break;
        }
        
        _loadingProcess = defaultPools.Instance.getABFromCach("Magics",basicMagicForwardPath);
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
                poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name);
                yield break;
            }
        }

        //////////// 第三环节  /////////////
        if (basicMagicForwardPath != "defaultmagic")
        {
            basicMagicForwardPath = "defaultmagic";
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }
            _loadingProcess = defaultPools.Instance.getABFromCach("Magics",basicMagicForwardPath);
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
                    poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name);
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
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(myMagicPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(myMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }

        // 第二轮
        if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
        {
            defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
            if (HurtObjectPool != null)
            {
                return HurtObjectPool;
            }
        }

        // 第三轮
        if (myDefaultMagicPath != "defaultmagic")
        {
            myDefaultMagicPath = "defaultmagic";
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(myDefaultMagicPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(myDefaultMagicPath + "/" + resource_name, out HurtObjectPool);
                if (HurtObjectPool != null)
                {
                    return HurtObjectPool;
                }
            }
        }
        return null;
    }
}
