using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public partial class AnimationManger
{
    private FacialAnimManager facialAnimManager;

    public void CasualFace()
    {
        if (facialAnimManager != null)
            facialAnimManager.CasualFace();
    }

    public void TriggerExpression(Facial facial)
    {
        facialAnimManager?.TriggerExpression(facial);
    }

    public async UniTask PreloadBasicPersonalAnims(
        string type,
        string basicPackName,
        FacialAnimManager facialAnimManager = null,
        Action<float> onProgress = null)
    {
        onProgress?.Invoke(0f);
        var basicAnims = new List<AnimationClip>();
        var basicPackKey = AnimationResourceKeyUtility.BasicPackSeriesKey(type, basicPackName);
        if (AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(basicPackKey))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(basicPackKey, out basicAnims);
            onProgress?.Invoke(0.3f);
        }
        else
        {
            var loadPath = Addressables.LoadResourceLocationsAsync(AnimationResourceKeyUtility.BasicAnimationLabel);
            await loadPath;
            if (loadPath.Status == AsyncOperationStatus.Succeeded)
            {
                var checkedLocations = 0;
                var totalLocations = loadPath.Result.Count;
                foreach (var path in loadPath.Result)
                {
                    if (AnimationResourceKeyUtility.IsBasicAnimationLocation(path.PrimaryKey, type, basicPackName))
                    {
                        Object value = await AddressablesLogic.LoadT<AnimationClip>(path.PrimaryKey);
                        if (value != null)
                        {
                            var animationClip = (AnimationClip)value;
                            basicAnims.Add(animationClip);
                        }
                    }

                    checkedLocations++;
                    if (totalLocations > 0)
                    {
                        onProgress?.Invoke(Mathf.Lerp(0.05f, 0.3f, checkedLocations / (float)totalLocations));
                    }
                }
            }
            Addressables.Release(loadPath);
            DicAdd<string, List<AnimationClip>>.Add(AnimationResourceLoader.SeriesAnimationClipsDic, basicPackKey, basicAnims);
            onProgress?.Invoke(0.3f);
        }

        toLoadAnims = new Dictionary<string, AnimationClip>();
        if (basicAnims != null)
        {
            foreach (var animationClip in basicAnims)
            {
                if (animationClip.name == "death")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("death", animationClip));
                }

                if (animationClip.name == "rush")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rush", animationClip));
                }

                if (animationClip.name == "block")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block", animationClip));
                }

                if (animationClip.name == "block_break")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("block_break", animationClip));
                }
                // if (animationClip.name == "dash")
                // {
                //     toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("dash", animationClip));
                // }
                // if (animationClip.name == "rushback")
                // {
                //     toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("rushback", animationClip));
                // }

                if (animationClip.name == "getup")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("getup", animationClip));
                }

                if (animationClip.name == "victory")
                {
                    toLoadAnims.Add(new KeyValuePair<string, AnimationClip>("victory", animationClip));
                }
            }
        }
        else
        {
            Debug.Log("Basic Anim Pack Error:" + type + "  " + basicPackName);
        }

        async UniTask LoadHurtAnim(string type, string address, List<string> tags)
        {
            var key = AnimationResourceKeyUtility.SeriesKey(type, address);
            if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(key))
            {
                AnimationResourceLoader.SeriesAnimationClipsDic.Add(key, new List<AnimationClip>());
                var humanHurtAnimsObjects = new List<AnimationClip>();
                var loadPath = Addressables.LoadResourceLocationsAsync(tags, Addressables.MergeMode.Intersection);
                await loadPath;
                if (loadPath.Status == AsyncOperationStatus.Succeeded)
                {
                    foreach (var path in loadPath.Result)
                    {
                        if (AnimationResourceKeyUtility.IsSeriesAnimationLocation(path.PrimaryKey, key))
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
                    if (AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(key, out var value))
                    {
                        value.Add(clip);
                    }
                }
            }
        }

        var hurtLoadFinished = 0;
        async UniTask TrackHurtAnim(UniTask task)
        {
            await task;
            hurtLoadFinished++;
            onProgress?.Invoke(Mathf.Lerp(0.3f, 0.75f, hurtLoadFinished / 6f));
        }

        await UniTask.WhenAll(
            TrackHurtAnim(LoadHurtAnim(type, "basic_hurts/back", new List<string> { "hurt_anim" })),
            TrackHurtAnim(LoadHurtAnim(type, "basic_hurts/high", new List<string> { "hurt_anim" })),
            TrackHurtAnim(LoadHurtAnim(type, "basic_hurts/lay", new List<string> { "hurt_anim" })),
            TrackHurtAnim(LoadHurtAnim(type, "basic_hurts/low", new List<string> { "hurt_anim" })),
            TrackHurtAnim(LoadHurtAnim(type, "basic_hurts/press", new List<string> { "hurt_anim" })),
            TrackHurtAnim(LoadHurtAnim(type, "basic_knockoffs", new List<string> { "knock_anim" }))
        );

        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_knockoffs"), out knockoffAnimations);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_hurts/back"), out _hurtClipsBack);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_hurts/low"), out _hurtClipsLow);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_hurts/high"), out _hurtClipsHigh);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_hurts/press"), out _hurtClipsPress);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(AnimationResourceKeyUtility.SeriesKey(type, "basic_hurts/lay"), out _hurtClipsLay);

        if (Animator == null || Animator.gameObject == null)
        {
            onProgress?.Invoke(1f);
            return; // When the character model is displayed, there may be issues such as the menu suddenly closing
        }
        animatorOverride = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        onProgress?.Invoke(0.85f);

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

            // if (animationClip.name == "air")
            // {
            //     if (animatorOverride["air"])
            //         animatorOverride["air"] = animationClip;
            // }
        }

        this.facialAnimManager = facialAnimManager;
        this.facialAnimManager?.INI(Animator, animatorOverride);
        Animator.runtimeAnimatorController = animatorOverride;
        onProgress?.Invoke(1f);
    }

    public async UniTask PreloadPersonalAnimResourceMode(
        string animPath,
        string key,
        Element element,
        int preloadCount,
        Action<float> onProgress = null)
    {
        onProgress?.Invoke(0f);
        if (toLoadAnims.ContainsKey(key))
        {
            onProgress?.Invoke(1f);
            return;
        }

        await AnimationResourceLoader.LoadAnim(animPath, key);
        onProgress?.Invoke(0.3f);
        var clip = AnimationResourceLoader.Instance.GetAnimationClip(AnimationResourceKeyUtility.SkillClipKey(animPath, key));
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
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element, preloadCount));
                    }
                    if (e.functionName == "MagicForwardOnBody")
                    {
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element, preloadCount));
                    }
                    if (e.functionName == "MagicToEnemy")
                    {
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element, preloadCount));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        tasks.Add(HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, element, preloadCount));
                    }
                    if (e.functionName is "Bullet_shoot_from_body_part" or "Bullet_shoot_from_body_part_TD")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("bullet", element, preloadCount));
                                break;
                            case 2:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("big_bullet", element, preloadCount));
                                break;
                            case 3:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("super_bullet", element, preloadCount));
                                break;
                            default:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("bullet", element, preloadCount));
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("blast", element, 1));
                                break;
                            case 1:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("mid_blast", element, 1));
                                break;
                            case 2:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("big_blast", element, 1));
                                break;
                            default:
                                tasks.Add(HurtObjectManager.ConstructHurtObjectPool("blast", element, 1));
                                break;
                        }
                    }
                    if (e.functionName == "PlaySoundOnce")
                    {
                        tasks.Add(AudioResourceLoading.Instance.LoadAudioClipFromResourceAndPutItIntoDic(
                            AudioResourceLoaderCore.EffectAudioPath, e.stringParameter));
                    }
                }
                if (tasks.Count == 0)
                {
                    onProgress?.Invoke(1f);
                }
                else
                {
                    var completedResources = 0;
                    var trackedTasks = new List<UniTask>(tasks.Count);
                    async UniTask TrackResourceTask(UniTask task)
                    {
                        await task;
                        completedResources++;
                        onProgress?.Invoke(Mathf.Lerp(0.3f, 1f, completedResources / (float)tasks.Count));
                    }

                    foreach (var task in tasks)
                    {
                        trackedTasks.Add(TrackResourceTask(task));
                    }
                    await UniTask.WhenAll(trackedTasks);
                }
            }
        }
        onProgress?.Invoke(1f);
    }

    public async UniTask PreloadPersonalAnimsResourceMode(
        string type,
        List<string> toLoadSkillAnimsNames,
        Element element,
        int preloadCount,
        Action<float> onProgress = null)
    {
        const int maxConcurrentSkillPreloads = 3;
        onProgress?.Invoke(0f);
        if (toLoadSkillAnimsNames == null || toLoadSkillAnimsNames.Count == 0)
        {
            onProgress?.Invoke(1f);
            return;
        }

        var tasks = new List<UniTask>(maxConcurrentSkillPreloads);
        var completedSkills = 0;
        async UniTask TrackSkillLoad(string animName)
        {
            await PreloadPersonalAnimResourceMode(type, animName, element, preloadCount);
            completedSkills++;
            onProgress?.Invoke(completedSkills / (float)toLoadSkillAnimsNames.Count);
        }

        foreach (var animName in toLoadSkillAnimsNames)
        {
            tasks.Add(TrackSkillLoad(animName));
            if (tasks.Count < maxConcurrentSkillPreloads)
            {
                continue;
            }

            await UniTask.WhenAll(tasks);
            tasks.Clear();
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        if (tasks.Count > 0)
        {
            await UniTask.WhenAll(tasks);
        }
        onProgress?.Invoke(1f);
    }
}
