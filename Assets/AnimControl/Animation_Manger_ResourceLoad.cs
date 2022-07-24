using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class Animation_Manger{

    public async UniTask PreloadBasicPersonalAnims(string type, string basicPackName)
    {
        var basicAnims = new List<AnimationClip>();
        string basicPackKey = type + "/" + basicPackName;
        if (AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(basicPackKey))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(basicPackKey,out basicAnims);
        }
        else
        {
            var loadPath = Addressables.LoadResourceLocationsAsync("basic_anim");
            await loadPath;
            if (loadPath.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var path in loadPath.Result)
                {
                    if (path.PrimaryKey.Contains("Animation/" + type + "/BasicPack/" + basicPackName))
                    {
                        Object value = await AddressablesLogic.LoadT<AnimationClip>(path.PrimaryKey);
                        if (value != null)
                        {
                            var _AnimationClip = (AnimationClip)value;
                            basicAnims.Add(_AnimationClip);
                        }
                    }
                }
            }
            Addressables.Release(loadPath);
            DicAdd<string, List<AnimationClip>>.Add(AnimationResourceLoader.SeriesAnimationClipsDic, basicPackKey, basicAnims);
            Debug.Log("基础动画包: "+basicPackKey + "已经加入公用字典");
        }
        
        toLoadAnims = new Dictionary<string, AnimationClip>();        
        foreach (AnimationClip _AnimationClip in basicAnims)
        {
            if (_AnimationClip == null)
            {
                continue;
            }
            
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

        async UniTask LoadHurtAnim(string type, string address, List<object> tags)
        {
            if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(type + "/" + address))
            {
                AnimationResourceLoader.SeriesAnimationClipsDic.Add(type + "/" + address, new List<AnimationClip>());
                var humanHurtAnimsObjects = new List<AnimationClip>();
                var loadPath = Addressables.LoadResourceLocationsAsync(tags, Addressables.MergeMode.Intersection);
                await loadPath;
                if (loadPath.Status == AsyncOperationStatus.Succeeded)
                {
                    foreach (var path in loadPath.Result)
                    {
                        if (path.PrimaryKey.Contains("Animation/" + type + "/" + address))
                        {
                            Object value = await AddressablesLogic.LoadT<AnimationClip>(path.PrimaryKey);
                            if (value != null)
                            {
                                AnimationClip _AnimationClip = (AnimationClip)value;
                                humanHurtAnimsObjects.Add(_AnimationClip);
                            }
                        }
                    }
                }
                Addressables.Release(loadPath);
                foreach (UnityEngine.Object _object in humanHurtAnimsObjects)
                {
                    AnimationResourceLoader.SeriesAnimationClipsDic[type + "/" + address].Add(_object as AnimationClip);
                }
            }
        }
        
        await LoadHurtAnim(type, "basic_hurts/back", new List<object> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/high", new List<object> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/lay", new List<object> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/low", new List<object> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/press",new List<object> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_knockoffs", new List<object> { "knock_anim" });
        
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_knockoffs", out knockoffAnimations);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/back", out _hurtClipsBack);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/low", out _hurtClipsLow);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/high", out _hurtClipsHigh);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/press", out _hurtClipsPress);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/lay", out _hurtClipsLay);
        
        animatorOverride = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        
        // 以上内容为个性化动画片段对base层基础动画的覆盖
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
        Animator.runtimeAnimatorController = animatorOverride;
    }

    public async UniTask PreloadPersonalAnimResourceMode(string animPath, string key, string personalMagic, Element element)
    {
        if (toLoadAnims.ContainsKey(key))
        {
            return;
        }
        await AnimationResourceLoader.DownloadAnim(animPath, key);
        var clip = AnimationResourceLoader.Instance.GetAnimationClip(animPath + "/skill/" + key);
        if (clip != null)
        {
            if (!toLoadAnims.ContainsKey(key))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(key, clip));
                foreach (AnimationEvent e in clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, element);
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, element);
                    }
                    if (e.functionName == "Bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, element);
                                break;
                            case 2:
                                HurtObjectManager.ConstructHurtObjectPool("big_bullet", personalMagic, element);
                                break;
                            case 3:
                                HurtObjectManager.ConstructHurtObjectPool("super_bullet", personalMagic, element);
                                break;
                            default:
                                HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, element);
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element);
                                break;
                            case 1:
                                HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element);
                                break;
                            case 2:
                                HurtObjectManager.ConstructHurtObjectPool("big_blast", personalMagic, element);
                                break;
                            default:
                                HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element);
                                break;
                        }
                    }
                    if (e.functionName == "PlaySoundOnce")
                    {
                        AudioResourceLoading.Instance.LoadAudioClipFromResourceAndPutItIntoDic("effects", e.stringParameter);
                    }
                }
            }
        }
    }
    
    public async UniTask PreloadPersonalAnimsResourceMode(string type, List<string> toLoadSkillAnimsNames,string personalMagic, Element element)
    {
        foreach (string anim_name in toLoadSkillAnimsNames)
        {
            await PreloadPersonalAnimResourceMode(type, anim_name, personalMagic, element);
        }
    }
}
