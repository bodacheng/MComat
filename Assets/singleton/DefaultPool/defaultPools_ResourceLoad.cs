using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;

public partial class defaultPools
{
    // 这个函数的构造就是说，我们虽然正式版本游戏各个type角色的攻击动画都是ab包放一起，但开发环境下他们还是按着自身特点放在那几个文件夹下面，
    // 那么测试环境下我们就干脆把所有resource文件夹下的攻击类动画全load来方便提取。
    // 正式版本游戏是根据脚本决定load哪些包从而构成字典的，不会产生load不用动画的问题
    public void prepareAllAttackAnimationClipsByTypeFromResourceAndPutItIntoDic(string type)
    {
        if (!RuntimeAnimatorControllerIdic.ContainsKey(type))
        {
            RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
            if (toLoadRuntimeAnimatorController != null)
                RuntimeAnimatorControllerIdic.Add(type, toLoadRuntimeAnimatorController);
        }else{
            if (RuntimeAnimatorControllerIdic[type] == null)
            {
                RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                if (toLoadRuntimeAnimatorController != null)
                    RuntimeAnimatorControllerIdic[type] = toLoadRuntimeAnimatorController;
            }
        }
               
        List<UnityEngine.Object> G_Attack_States = Resources.LoadAll("Animations/"+ type + "/" + "G_Attack_State", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> G_Attack_State_Stays = Resources.LoadAll("Animations/" + type + "/" + "G_Attack_State_Stay", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> GMStatess = Resources.LoadAll("Animations/" + type + "/" + "GMStates", typeof(AnimationClip)).ToList();

        string clipkey;
        foreach (UnityEngine.Object _object in G_Attack_States)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                AnimationClipDic[clipkey] = _object as AnimationClip;
            else
                AnimationClipDic.Add(clipkey, _object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in G_Attack_State_Stays)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                AnimationClipDic[clipkey] = _object as AnimationClip;
            else
                AnimationClipDic.Add(clipkey, _object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in GMStatess)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                AnimationClipDic[clipkey] = _object as AnimationClip;
            else
                AnimationClipDic.Add(clipkey, _object as AnimationClip);
        }
        
        foreach (KeyValuePair<string,AnimationClip> keyValuePair in AnimationClipDic)
        {
            foreach (AnimationEvent e in keyValuePair.Value.events)
            {
                if (e.functionName == "SetAllBodyMarkerManagersIn")
                {
                    Debug.Log("SetAllBodyMarkerManagersIn:" + keyValuePair.Key);
                }
            }
        }
    }

    private EZObjectPool EffectPool;
    private GameObject EffectPrefab;
    public EZObjectPool iniEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        EffectPrefab = null;
        EffectPool = null;
        if (EffectsPath != null)
        {
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey("Effects" + "/" + EffectsPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue("Effects" + "/" + EffectsPath + "/" + resource_name, out EffectPool);
                if (EffectPool != null)
                    return EffectPool;
            }

            EffectPrefab = Resources.Load("Effects" + "/" + EffectsPath + "/" + resource_name, typeof(GameObject)) as GameObject;
            if (EffectPrefab != null)
            {
                EffectPool = constructPoolWithPrefabAndKey(EffectPrefab, "Effects" + "/" + EffectsPath + "/" + resource_name);
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
        EffectPool = iniEffectsPool(resource_name, EffectsPath,2);
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
            if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(MagicForwardPath + "/" + resource_name))
            {
                defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(MagicForwardPath + "/" + resource_name, out poolToConstruct2);
                if (poolToConstruct2 != null)
                    yield break;
            }

            hurtObject = Resources.Load("HurtObjects/" + MagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, MagicForwardPath + "/" + resource_name);
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
        if (defaultPools.Instance.EffectAndHurtObjectPoolsDic.ContainsKey(basicMagicForwardPath + "/" + resource_name))
        {
            defaultPools.Instance.EffectAndHurtObjectPoolsDic.TryGetValue(basicMagicForwardPath + "/" + resource_name, out poolToConstruct2);
            if (poolToConstruct2 != null)
                yield break;
        }
        hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
        if (hurtObject != null)
        {
            poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name);
            yield break;
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
            hurtObject = Resources.Load("HurtObjects/" + basicMagicForwardPath + "/" + resource_name) as GameObject;
            if (hurtObject != null)
            {
                poolToConstruct2 = defaultPools.Instance.constructPoolWithPrefabAndKey(hurtObject, basicMagicForwardPath + "/" + resource_name);
                yield break;
            }
        }
    }

    public IEnumerator LoadAudioClipFromResourceAndPutItIntoDic(string additionalPath, string clip_name)
    {
        string clipkey = "Audios/" + additionalPath + "/" + clip_name;
        AudioClip audioClip = Resources.Load(clipkey, typeof(AudioClip)) as AudioClip;
        if (soundClipsDic.ContainsKey(clipkey))
            soundClipsDic[clipkey] = audioClip;
        else
            soundClipsDic.Add(clipkey, audioClip);
        yield break;
    }
}
