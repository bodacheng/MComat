using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AudioResourceLoading
{
    private static AudioResourceLoading instance;
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
    public readonly IDictionary<string, AudioClip> SoundClipsDic = new Dictionary<string, AudioClip>();
    
    public IEnumerator LoadAudioClipFromCachAndPutItIntoDic(string bundleURL,string additionalPath, string clip_name)
    {
        // AudioClip _AudioClip = null;
        // string clipkey = additionalPath + "/" + clip_name;
        // soundClipsDic.TryGetValue(clipkey, out _AudioClip);
        // if (_AudioClip != null)
        // {
        //     Debug.Log("声音：" + clipkey + "已经存在");
        //     yield break;
        // }
        // IEnumerator task = CachManager.Instance.getABFromCach(bundleURL,clip_name);
        // yield return task;
        // AssetBundle readingBundle = (AssetBundle)task.Current;
        // if (readingBundle != null)
        // {
        //     _AudioClip = readingBundle.LoadAsset<AudioClip>(clip_name);
        //     if (_AudioClip != null)
        //     {
        //         readingBundle.Unload(false);
        //         if (!soundClipsDic.ContainsKey(clipkey))
        //             soundClipsDic.Add(clipkey, _AudioClip);
        //         else
        //             soundClipsDic[clipkey] = _AudioClip;
        //     }
        //     else
        //     {
        //         readingBundle.Unload(false);
        //         yield break;
        //     }
        // }
        
        yield break;
    }
    
    public async UniTask LoadAudioClipFromResourceAndPutItIntoDic(string additionalPath, string clipName)
    {
        string path = additionalPath + "/" + clipName;
        var audioClip = await AddressablesLogic.LoadT<AudioClip>(path);
        SoundClipsDic[path] = audioClip;
    }
}
