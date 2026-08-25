using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AnimationResourceLoader
{
    private static AnimationResourceLoader instance;
    private static readonly IDictionary<string, UniTaskCompletionSource<AnimationClip>> PendingAnimationLoads =
        new Dictionary<string, UniTaskCompletionSource<AnimationClip>>();

    public static AnimationResourceLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimationResourceLoader();
            }

            return instance;
        }
    }

    public static IDictionary<string, List<AnimationClip>> SeriesAnimationClipsDic => AnimationResourceLoaderCore.SeriesAnimationClipsDic;

    public void Clear()
    {
        AnimationResourceLoaderCore.Clear();
    }

    public AnimationClip GetAnimationClip(string key)
    {
        return AnimationResourceLoaderCore.GetAnimationClip(key);
    }

    public static async UniTask LoadAnim(string type, string key)
    {
        var clipKey = AnimationResourceKeyUtility.SkillClipKey(type, key);
        if (AnimationResourceLoaderCore.HasAnimationClip(clipKey))
        {
            return;
        }

        if (PendingAnimationLoads.TryGetValue(clipKey, out var pendingLoad))
        {
            await pendingLoad.Task;
            return;
        }

        var loadSource = new UniTaskCompletionSource<AnimationClip>();
        PendingAnimationLoads.Add(clipKey, loadSource);
        try
        {
            var result = await AddressablesLogic.LoadT<AnimationClip>(
                AnimationResourceKeyUtility.SkillAnimationAddress(type, key));
            AnimationResourceLoaderCore.AddAnimationClip(clipKey, result);
            loadSource.TrySetResult(result);
        }
        catch (Exception exception)
        {
            loadSource.TrySetException(exception);
            throw;
        }
        finally
        {
            PendingAnimationLoads.Remove(clipKey);
        }
    }
}
