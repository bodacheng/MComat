using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AudioResourceLoading
{
    private static AudioResourceLoading instance;
    private static readonly IDictionary<string, UniTaskCompletionSource<bool>> PendingAudioLoads =
        new Dictionary<string, UniTaskCompletionSource<bool>>();
    public static AudioResourceLoading Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioResourceLoading();
            }
            return instance;
        }
    }
    public IDictionary<string, AudioClip> SoundClipsDic => AudioResourceLoaderCore.SoundClipsDic;
    
    public async UniTask LoadAudioClipFromResourceAndPutItIntoDic(string additionalPath, string clipName)
    {
        if (string.IsNullOrWhiteSpace(clipName))
            return;

        var key = AudioResourceLoaderCore.AudioClipKey(additionalPath, clipName);
        if (AudioResourceLoaderCore.HasAudioClip(key))
            return;

        if (PendingAudioLoads.TryGetValue(key, out var pendingLoad))
        {
            await pendingLoad.Task;
            return;
        }

        var loadSource = new UniTaskCompletionSource<bool>();
        PendingAudioLoads.Add(key, loadSource);
        try
        {
            await AudioResourceLoadingCore.LoadAudioClipIntoCache(
                additionalPath,
                clipName,
                AddressablesLogic.HasIndexedTag,
                AddressablesLogic.CheckKeyExist,
                address => AddressablesLogic.LoadT<AudioClip>(address),
                Debug.LogWarning);
            loadSource.TrySetResult(true);
        }
        catch (Exception exception)
        {
            loadSource.TrySetException(exception);
            throw;
        }
        finally
        {
            PendingAudioLoads.Remove(key);
        }
    }
}
