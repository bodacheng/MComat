using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class Animation_Manger{

    public IEnumerator PreloadBasicPersonalAnimsResourceMode(string animPath, string basicPackName)
    {
        List<AnimationClip> basicAnims = new List<AnimationClip>();
        string basicPackKey = animPath + "/" + basicPackName;
        if (AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(basicPackKey))
        {
            AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(basicPackKey,out basicAnims);
        }
        else
        {
            var loadPath = Addressables.LoadResourceLocationsAsync("basic_anim", Addressables.MergeMode.Intersection);
            yield return loadPath;
            if (loadPath.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var path in loadPath.Result)
                {
                    if (path.PrimaryKey.Contains("Animation/" + animPath + "/BasicPack/" + basicPackName))
                    {
                        var handle = Addressables.LoadAssetAsync<AnimationClip>(path.PrimaryKey);
                        yield return handle;
                        if (handle.Status == AsyncOperationStatus.Succeeded)
                        {
                            Object value = handle.Result;
                            if (value != null)
                            {
                                AnimationClip _AnimationClip = (AnimationClip)value;
                                basicAnims.Add(_AnimationClip);
                            }
                        }
                        Addressables.Release(handle);
                    }
                }
            }
            Addressables.Release(loadPath);
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

        IEnumerator LoadHurtAnim(string type, string address, List<object> tags)
        {
            if (!AnimationResourceLoader.SeriesAnimationClipsDic.ContainsKey(type + "/" + address))
            {
                AnimationResourceLoader.SeriesAnimationClipsDic.Add(type + "/" + address, new List<AnimationClip>());
                var humanHurtAnimsObjects = new List<AnimationClip>();
                var loadPath = Addressables.LoadResourceLocationsAsync(tags, Addressables.MergeMode.Intersection);
                yield return loadPath;
                if (loadPath.Status == AsyncOperationStatus.Succeeded)
                {
                    foreach (var path in loadPath.Result)
                    {
                        if (path.PrimaryKey.Contains("Animation/" + type + "/" + address))
                        {
                            var handle = Addressables.LoadAssetAsync<AnimationClip>(path.PrimaryKey);
                            yield return handle;
                            if (handle.Status == AsyncOperationStatus.Succeeded)
                            {
                                Object value = handle.Result;
                                if (value != null)
                                {
                                    AnimationClip _AnimationClip = (AnimationClip)value;
                                    humanHurtAnimsObjects.Add(_AnimationClip);
                                }
                            }
                            Addressables.Release(handle);
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
        
        yield return LoadHurtAnim("human", "basic_hurts/back", new List<object> { "hurt_anim" });
        yield return LoadHurtAnim("human", "basic_hurts/high", new List<object> { "hurt_anim" });
        yield return LoadHurtAnim("human", "basic_hurts/lay", new List<object> { "hurt_anim" });
        yield return LoadHurtAnim("human", "basic_hurts/low", new List<object> { "hurt_anim" });
        yield return LoadHurtAnim("human", "basic_hurts/press", new List<object> { "hurt_anim" });
        yield return LoadHurtAnim("human", "basic_knockoffs", new List<object> { "knock_anim" });
        
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

        Animator.runtimeAnimatorController = animatorOverride;
        ////////////    以上内容为个性化动画片段对base层基础动画的覆盖   /////////////
    }

    public IEnumerator PreloadPersonalAnimResourceMode(string animPath, string key, string personalMagic, Element element)
    {
        if (toLoadAnims.ContainsKey(key))
        {
            yield break;
        }
        yield return AnimationResourceLoader.DownloadAnim(animPath, key);
        _clip = AnimationResourceLoader.Instance.GetAnimationClip(animPath + "/skill/" + key);
        if (_clip != null)
        {
            if (!toLoadAnims.ContainsKey(key))
            {
                toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(key, _clip));
                foreach (AnimationEvent e in _clip.events)
                {
                    if (e.functionName == "MagicForward")
                    {
                        yield return (HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, element));
                    }
                    if (e.functionName == "PrepareOneMagic")
                    {
                        yield return (HurtObjectManager.ConstructHurtObjectPool(e.stringParameter, personalMagic, element));
                    }
                    if (e.functionName == "Bullet_shoot_from_body_part")
                    {
                        switch (e.intParameter)
                        {
                            case 1:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, element));
                                break;
                            case 2:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("big_bullet", personalMagic, element));
                                break;
                            case 3:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("super_bullet", personalMagic, element));
                                break;
                            default:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("bullet", personalMagic, element));
                                break;
                        }
                    }
                    if (e.functionName == "BlastAttack")
                    {
                        switch (e.intParameter)
                        {
                            case 0:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element));
                                break;
                            case 1:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element));
                                break;
                            case 2:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("big_blast", personalMagic, element));
                                break;
                            default:
                                yield return (HurtObjectManager.ConstructHurtObjectPool("blast", personalMagic, element));
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
    public IEnumerator PreloadPersonalAnimsResourceMode(string type, List<string> toLoadSkillAnimsNames,string personalMagic, Element element)
    {
        foreach (string anim_name in toLoadSkillAnimsNames)
        {
            yield return PreloadPersonalAnimResourceMode(type, anim_name, personalMagic, element);
        }
    }
}
