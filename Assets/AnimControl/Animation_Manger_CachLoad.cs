using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Animation_Manger : MonoBehaviour
{
    /// <summary>
    /// CachVersion
    /// </summary>
    /// <returns>The basic personal anims.</returns>
    public IEnumerator PreloadBasicPersonalAnims(string animPath, string basicPackName)
    {
        string basicPackKey = "BasicPack/" + animPath + "/" + basicPackName;
        yield return (AnimationResourceLoader.Instance.LoadAnimationPackFromCache(ResourceLordSceneStarter.BundleURL + "/animClips/" + animPath, basicPackKey, basicPackName));
        List<AnimationClip> basicAnims = AnimationResourceLoader.Instance.GetAnimationPack(basicPackKey);

        toLoadAnims = new Dictionary<string, AnimationClip>();

        if (basicAnims != null)
        {
            foreach (AnimationClip _AnimationClip in basicAnims)
            {
                if (_AnimationClip.name == "rush")
                {
                    if (toLoadAnims.ContainsKey("rush"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rush", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "block")
                {
                    if (toLoadAnims.ContainsKey("block"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "block_break")
                {
                    if (toLoadAnims.ContainsKey("block_break"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block_break", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "zhuangbi")
                {
                    if (toLoadAnims.ContainsKey("zhuangbi"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("zhuangbi", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "controlled")
                {
                    if (toLoadAnims.ContainsKey("controlled"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("controlled", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "dash")
                {
                    if (toLoadAnims.ContainsKey("dash"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("dash", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "rushback")
                {
                    if (toLoadAnims.ContainsKey("rushback"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rushback", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "getup")
                {
                    if (toLoadAnims.ContainsKey("getup"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("getup", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "victory")
                {
                    if (toLoadAnims.ContainsKey("victory"))
                    {
                        Debug.Log("严重错误。基础包里有重名动画片段？");
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("victory", _AnimationClip));
                    }
                }
            }
        }

        animatorOverride = new AnimatorOverrideController(Animator.runtimeAnimatorController);

        if (basicAnims != null)
        {
            foreach (AnimationClip _AnimationClip in basicAnims)
            {
                if (_AnimationClip.name == "idle")
                {
                    if (animatorOverride["idle"])
                        animatorOverride["idle"] = _AnimationClip;
                }
                if (_AnimationClip.name == "walk")
                {
                    if (animatorOverride["walk"])
                        animatorOverride["walk"] = _AnimationClip;
                }
                if (_AnimationClip.name == "run")
                {
                    if (animatorOverride["run"])
                        animatorOverride["run"] = _AnimationClip;
                }
                if (_AnimationClip.name == "air")
                {
                    if (animatorOverride["air"])
                        animatorOverride["air"] = _AnimationClip;
                }
            }
        }

        Animator.runtimeAnimatorController = animatorOverride;// 622！！！！！
        ////////////    以上内容为个性化动画片段对base层基础动画的覆盖   /////////////
    }

    //这个函数的改造方向：由从服务器下载的来的包决定四大类攻击动作的列表，因为四大类动作都是每个动画片段自成一个包
    //本函数的改造方向是所有基础类动作全部从某个包里读取。
    //主意看参数toLoadSkillAnimsNames，这些只是技能类key的名字
    //另外，本模块现在肩负起提前构建对象池的任务。就是所有的hurtobject。这样一来这个模块现在是和BO_E模块产生密切关系
    public IEnumerator PreloadPersonalAnims(string url,string type, List<string> toLoadSkillAnimsNames, string personalMagic, Zokusei _zokusei)
    {
        if (toLoadSkillAnimsNames != null)
        {
            foreach (string anim_name in toLoadSkillAnimsNames)
            {
                yield return PreloadPersonalAnim(url,type, anim_name, personalMagic, _zokusei);
            }
        }
    }

    public IEnumerator PreloadPersonalAnim(string url,string type,string toLoadSkillAnimName, string personalMagic, Zokusei _zokusei)
    {
        if (toLoadAnims.ContainsKey(toLoadSkillAnimName))
        {
            yield break;
        }
    
        yield return (AnimationResourceLoader.Instance.LoadAnimationClipFromCachAndPutItIntoDic(url, type + "/skill/" + toLoadSkillAnimName, toLoadSkillAnimName));
        _clip = AnimationResourceLoader.Instance.GetAnimationClip(type + "/skill/" + toLoadSkillAnimName);
        if (_clip != null)
        {
            if (!toLoadAnims.ContainsKey(toLoadSkillAnimName))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(toLoadSkillAnimName, _clip));                              
                foreach (AnimationEvent e in _clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "Bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                            case 2:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("big_bullet", personalMagic, _zokusei));
                                break;
                            case 3:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("super_bullet", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                            case 1:
                                break;
                            case 2:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("big_blast", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (EffectAndHurtObjectLoading.Instance.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "PlaySoundOnce")
                    {
                        yield return (AudioResourceLoading.Instance.LoadAudioClipFromCachAndPutItIntoDic(url,"effects", e.stringParameter));
                    }
                }
            }
        }
        else
        {
            FightLoadError.Instance.FightLoadErrors.Add(toLoadSkillAnimName + "动作包有问题，无法从中加载动画片段");
            Debug.Log(toLoadSkillAnimName + "动作包有问题，无法从中加载动画片段");
        }
    }
}
