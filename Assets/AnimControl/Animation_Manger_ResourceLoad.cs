using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public partial class Animation_Manger : MonoBehaviour{

    public IEnumerator PreloadBasicPersonalAnimsResourceMode(string animPath, string basicPackName)
    {
        List<AnimationClip> basicAnims = new List<AnimationClip>();
        string basicPackKey = animPath + "/" + basicPackName;
        if (AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(basicPackKey))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(basicPackKey,out basicAnims);
        }else{
            List<UnityEngine.Object> basicAnimsObject = Resources.LoadAll("Animations/" + animPath +  "/BasicPack/" +  basicPackName, typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in basicAnimsObject)
            {
                AnimationClip _AnimationClip = (AnimationClip)_object;
                if (_AnimationClip.name == "rush")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "rush",
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                }
                if (_AnimationClip.name == "rushback")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "rushback",
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                }
                if (_AnimationClip.name == "getup")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "getup",
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                }
                if (_AnimationClip.name == "block")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "block",
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                }
                if (_AnimationClip.name == "block_break")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "block_break",
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                }
                basicAnims.Add(_AnimationClip);
            }
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(basicPackKey, basicAnims);
            Debug.Log("基础动画包"+basicPackKey + "已经加入了defaultPools.SeriesAnimationClipsDic公用字典");
        }

        toLoadAnims = new Dictionary<string, AnimationClip>();        
        foreach (AnimationClip _AnimationClip in basicAnims)
        {
            if (_AnimationClip.name == "death")
            {
                if (toLoadAnims.ContainsKey("death"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("death", _AnimationClip));
                }
            }
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
        
        ///////////////////////////////////
        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts/back"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_hurts/back", new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_hurts/back", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_hurts/back"].Add(_object as AnimationClip);
            }
        }
        
        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts/high"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_hurts/high", new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_hurts/high", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_hurts/high"].Add(_object as AnimationClip);
            }
        }
        
        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts/lay"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_hurts/lay", new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_hurts/lay", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_hurts/lay"].Add(_object as AnimationClip);
            }
        }
        
        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts/low"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_hurts/low", new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_hurts/low", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_hurts/low"].Add(_object as AnimationClip);
            }
        }
        
        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_hurts/press"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_hurts/press", new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_hurts/press", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_hurts/press"].Add(_object as AnimationClip);
            }
        }

        if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(animPath + "/basic_knockoffs"))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.Add(animPath + "/basic_knockoffs",new List<AnimationClip>());
            List<UnityEngine.Object> humanHurtAnimsObjects = Resources.LoadAll("Animations/" + animPath + "/basic_knockoffs", typeof(AnimationClip)).ToList();
            foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
            {
                AnimationResourceLoader.SeriesAnimationClipsDic[animPath + "/basic_knockoffs"].Add(_object as AnimationClip);
            }
            Debug.Log("基础击飞动画包"+animPath + "/basic_knockoffs" + "已经加入公共字典");
        }else{
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
        yield break;
    }

    public IEnumerator PreloadPersonalAnimResourceMode(string animPath, string toLoadSkillAnimName, string personalMagic, Zokusei _zokusei)
    {
        if (toLoadAnims.ContainsKey(toLoadSkillAnimName))
        {
            yield break;
        }
        _clip = AnimationResourceLoader.Instance.GetAnimationClip(animPath + "/skill/" + toLoadSkillAnimName);
        if (_clip != null)
        {
            if (!toLoadAnims.ContainsKey(toLoadSkillAnimName))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(toLoadSkillAnimName, _clip));
                foreach (AnimationEvent e in _clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        yield return (HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        yield return (HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, _zokusei));
                    }
                    if (e.functionName == "Bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                            case 2:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("big_bullet", personalMagic, _zokusei));
                                break;
                            case 3:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("super_bullet", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                            case 1:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                            case 2:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("big_blast", personalMagic, _zokusei));
                                break;
                            default:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, _zokusei));
                                break;
                        }
                    }
                    if (e.functionName == "PlaySoundOnce")
                    {
                        yield return (AudioResourceLoading.Instance.LoadAudioClipFromResourceAndPutItIntoDic("effects", e.stringParameter));
                    }
                }
            }
        }else{
            yield return null;
        }
    }

    AnimationClip _clip;
    public IEnumerator PreloadPersonalAnimsResourceMode(string animPath, List<string> toLoadSkillAnimsNames, string personalMagic, Zokusei _zokusei)
    {
        if (toLoadSkillAnimsNames != null)
        {
            foreach (string anim_name in toLoadSkillAnimsNames)
            {
                yield return PreloadPersonalAnimResourceMode(animPath, anim_name, personalMagic, _zokusei);
            }
        }
    }
}
