using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public partial class Animation_Manger : MonoBehaviour
{
    /// <summary>
    /// StreamingAssetVersion
    /// </summary>
    /// <returns>The basic personal anims.</returns>
    /// <param name="animPath">Animation path.</param>
    /// <param name="basicPackName">Basic pack name.</param>
    /// <param name="hurtPackName">Hurt pack name.</param>
    /// <param name="toLoadSkillAnimsNames">To load skill anims names.</param>
    public IEnumerator preloadBasicPersonalAnimsStreamingAssetMode(string animPath, string basicPackName)
    {
        yield return (defaultPools.Instance.LoadAnimationPackFromStreamingAssets(animPath,basicPackName));
        List<AnimationClip> basicAnims = defaultPools.Instance.getAnimationPack("BasicPack/"+ animPath+ "/"+ basicPackName);

        toLoadAnims = new Dictionary<string, AnimationClip>();
        if (basicAnims != null)
        {
            foreach (AnimationClip _AnimationClip in basicAnims)
            {
                if (_AnimationClip.name == "rush")
                {
                    if (toLoadAnims.ContainsKey("rush"))
                    {
                        toLoadAnims["rush"] = _AnimationClip;
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
                        toLoadAnims["block"] = _AnimationClip;
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
                        toLoadAnims["block_break"] = _AnimationClip;
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
                        toLoadAnims["zhuangbi"] = _AnimationClip;
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
                        toLoadAnims["controlled"] = _AnimationClip;
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
                        toLoadAnims["dash"] = _AnimationClip;
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
                        toLoadAnims["rushback"] = _AnimationClip;
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rushback", _AnimationClip));
                    }
                }
                if (_AnimationClip.name == "victory")
                {
                    if (toLoadAnims.ContainsKey("victory"))
                    {
                        toLoadAnims["victory"] = _AnimationClip;
                    }
                    else
                    {
                        toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("victory", _AnimationClip));
                    }
                }
            }
        }

        if (!defaultPools.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts"))
        {
            yield return (defaultPools.Instance.LoadAnimationPackFromStreamingAssets(animPath, "basic_hurts"));
        }
        else
        {
            //认为已经存在了相应系列的动画包
        }
        
        /////
        
        if (!defaultPools.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_knockoffs"))
        {
            yield return (defaultPools.Instance.LoadAnimationPackFromStreamingAssets(animPath,"basic_knockoffs"));
        }
        else
        {
            //认为已经存在了相应系列的动画包
        }
        
        ////////////    以上内容为实时动画片段的预备和个性化片段对他们的覆盖   /////////////
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

    public IEnumerator preloadPersonalAnimStreamingAssetMode(string animPath, string toLoadSkillAnimsName, string personalMagic, zokusei _zokusei)
    {
        if (toLoadAnims.ContainsKey(toLoadSkillAnimsName))
        {
            yield break;
        }
        yield return (defaultPools.Instance.LoadAnimationClipFromStreamingAssetsAndPutItIntoDic(animPath, "skill", toLoadSkillAnimsName));
        _clip = defaultPools.Instance.getAnimationClip(animPath + "/skill/" + toLoadSkillAnimsName);
        if (_clip != null)
        {
            if (!toLoadAnims.ContainsKey(toLoadSkillAnimsName))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(toLoadSkillAnimsName, _clip));
                foreach (AnimationEvent e in _clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        yield return (defaultPools.Instance.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        yield return (defaultPools.Instance.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                            case 2:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("big_bullet", personalMagic, _zokusei));
                                break;
                            case 3:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("super_bullet", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "blastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                            case 1:
                                break;
                            case 2:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("big_blast", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (defaultPools.Instance.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "playSoundOnce")
                    {
                        //缺失
                    }
                }
            }
        }
        else
        {
            defaultPools.Instance.FightLoadErrors.Add(toLoadSkillAnimsName + "动作包有问题，无法从中加载动画片段");
            Debug.Log(toLoadSkillAnimsName + "动作包有问题，无法从中加载动画片段");
        }
    }

    public IEnumerator preloadPersonalAnimsStreamingAssetMode(string animPath, List<string> toLoadSkillAnimsNames, string personalMagic, zokusei _zokusei)
    {
        if (toLoadSkillAnimsNames != null)
        {
            foreach (string anim_name in toLoadSkillAnimsNames)
            {
                preloadPersonalAnimStreamingAssetMode(animPath, anim_name, personalMagic, _zokusei);
            }
        }
        yield break;
    }
}
