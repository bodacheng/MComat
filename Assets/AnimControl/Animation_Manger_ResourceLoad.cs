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
            AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(basicPackKey, out basicAnims);
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
                            var animationClip = (AnimationClip)value;
                            basicAnims.Add(animationClip);
                        }
                    }
                }
            }
            Addressables.Release(loadPath);
            DicAdd<string, List<AnimationClip>>.Add(AnimationResourceLoader.SeriesAnimationClipsDic, basicPackKey, basicAnims);
        }
        
        toLoadAnims = new Dictionary<string, AnimationClip>();        
        foreach (var animationClip in basicAnims)
        {
            if (animationClip == null)
            {
                continue;
            }
            
            if (animationClip.name == "death")
            {
                if (toLoadAnims.ContainsKey("death"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("death", animationClip));
                }
            }
            if (animationClip.name == "rush")
            {
                if (toLoadAnims.ContainsKey("rush"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rush", animationClip));
                }
            }
            if (animationClip.name == "block")
            {
                if (toLoadAnims.ContainsKey("block"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block", animationClip));
                }
            }
            if (animationClip.name == "block_break")
            {
                if (toLoadAnims.ContainsKey("block_break"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block_break", animationClip));
                }
            }
            if (animationClip.name == "zhuangbi")
            {
                if (toLoadAnims.ContainsKey("zhuangbi"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("zhuangbi", animationClip));
                }
            }
            if (animationClip.name == "dash")
            {
                if (toLoadAnims.ContainsKey("dash"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("dash", animationClip));
                }
            }
            if (animationClip.name == "rushback")
            {
                if (toLoadAnims.ContainsKey("rushback"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rushback", animationClip));
                }
            }
            if (animationClip.name == "getup")
            {
                if (toLoadAnims.ContainsKey("getup"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("getup", animationClip));
                }
            }
            if (animationClip.name == "victory")
            {
                if (toLoadAnims.ContainsKey("victory"))
                {
                    Debug.Log("严重错误。基础包里有重名动画片段？");
                }
                else
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("victory", animationClip));
                }
            }
        }

        async UniTask LoadHurtAnim(string type, string address, List<string> tags)
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
                                var animationClip = (AnimationClip)value;
                                humanHurtAnimsObjects.Add(animationClip);
                            }
                        }
                    }
                }
                Addressables.Release(loadPath);
                foreach (var clip in humanHurtAnimsObjects)
                {
                    AnimationResourceLoader.SeriesAnimationClipsDic[type + "/" + address].Add(clip);
                }
            }
        }
        
        await LoadHurtAnim(type, "basic_hurts/back", new List<string> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/high", new List<string> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/lay", new List<string> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/low", new List<string> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_hurts/press",new List<string> { "hurt_anim" });
        await LoadHurtAnim(type, "basic_knockoffs", new List<string> { "knock_anim" });
        
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_knockoffs", out knockoffAnimations);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/back", out _hurtClipsBack);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/low", out _hurtClipsLow);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/high", out _hurtClipsHigh);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/press", out _hurtClipsPress);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/lay", out _hurtClipsLay);
        
        animatorOverride = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        
        // 以上内容为个性化动画片段对base层基础动画的覆盖
        foreach (var animationClip in basicAnims)
        {
            if (animationClip.name == "idle")
            {
                if (animatorOverride["idle"])
                    animatorOverride["idle"] = animationClip;
            }
            if (animationClip.name == "walk")
            {
                if (animatorOverride["walk"])
                    animatorOverride["walk"] = animationClip;
            }
            if (animationClip.name == "run")
            {
                if (animatorOverride["run"])
                    animatorOverride["run"] = animationClip;
            }
            if (animationClip.name == "air")
            {
                if (animatorOverride["air"])
                    animatorOverride["air"] = animationClip;
            }
        }
        Animator.runtimeAnimatorController = animatorOverride;
    }

    public async UniTask PreloadPersonalAnimResourceMode(string animPath, string key, Element element)
    {
        if (toLoadAnims.ContainsKey(key))
        {
            return;
        }
        
        await AnimationResourceLoader.LoadAnim(animPath, key);
        var clip = AnimationResourceLoader.Instance.GetAnimationClip(animPath + "/skill/" + key);
        if (clip != null)
        {
            if (!toLoadAnims.ContainsKey(key))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(key, clip));
                var tasks = new List<UniTask>();
                
                foreach (AnimationEvent e in clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element));
                    }
                    if (e.functionName == "Bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("bullet", element));
                                break;
                            case 2:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("big_bullet", element));
                                break;
                            case 3:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("super_bullet", element));
                                break;
                            default:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("bullet", element));
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("blast", element));
                                break;
                            case 1:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("blast", element));
                                break;
                            case 2:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("big_blast", element));
                                break;
                            default:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("blast", element));
                                break;
                        }
                    }
                    if (e.functionName == "PlaySoundOnce")
                    {
                        AudioResourceLoading.Instance.LoadAudioClipFromResourceAndPutItIntoDic("effects", e.stringParameter);
                    }
                }
                await UniTask.WhenAll(tasks);
            }
        }
    }
    
    public async UniTask PreloadPersonalAnimsResourceMode(string type, List<string> toLoadSkillAnimsNames,Element element)
    {
        var tasks = new List<UniTask>();
        foreach (var animName in toLoadSkillAnimsNames)
        {
            tasks.Add(PreloadPersonalAnimResourceMode(type, animName, element));
        }
        await UniTask.WhenAll(tasks);
    }
}
