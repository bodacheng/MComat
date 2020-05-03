using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HurtObjectManager
{
    public static IEnumerator ConstructHurtObjectPoolResourceMode(string resource_name, string MagicForwardPath, Zokusei _zokusei)
    {
        DecompositionerPool poolToConstruct;
        GameObject hurtObject;
        /////////////// 第一环节 : 搜索个性魔法//////////////////
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
        if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
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
            if (HurtPoolDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
            {
                HurtPoolDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct);
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
}
