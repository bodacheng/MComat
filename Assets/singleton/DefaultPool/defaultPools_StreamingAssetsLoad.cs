using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//缺少StreamingAssets版本的音声资源加载函数
public partial class defaultPools
{
    public IEnumerator getABFromStreamingAssets(string subPath, string abName)
    {
        readingBundle = null;

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/" + subPath + "/" + abName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingBundle = resultAssetBundle.assetBundle;
        yield break;
    }

    public IEnumerator PrepareMagicFromStreamingAssets(string magicPackName)
    {
        AssetBundle readingMagicBundle;

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/Magics/" + magicPackName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingMagicBundle = resultAssetBundle.assetBundle;//AB包
        yield return readingMagicBundle;
    }

    public IEnumerator LoadAnimationClipFromStreamingAssetsAndPutItIntoDic(string type, string additionalPath, string clip_name)
    {
        if (additionalPath == null)
        {
            yield break;//完全是为了思路清晰，否则我们看不明白各种针对这东西的分歧处理是些啥
        }
        _AnimationClip = null;
        string clipkey = type + "/" + additionalPath + "/" + clip_name;
        AnimationClipDic.TryGetValue(clipkey, out _AnimationClip);
        if (_AnimationClip != null)
        {
            //Debug.Log("动画：" + clipkey + "已经存在");
            yield break;
        }

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + additionalPath + "/" + clip_name);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingBundle = resultAssetBundle.assetBundle;//AB包

        if (readingBundle != null)
        {
            _AnimationClip = readingBundle.LoadAsset<AnimationClip>(clip_name);
            if (_AnimationClip != null)
            {
                readingBundle.Unload(false);
                if (!AnimationClipDic.ContainsKey(clipkey))
                    AnimationClipDic.Add(clipkey, _AnimationClip);
                else
                    AnimationClipDic[clipkey] = _AnimationClip;
                //GameObject.DontDestroyOnLoad(_AnimationClip);
            }
            else
            {
                readingBundle.Unload(false);
            }
        }
        else
        {
            Debug.Log("没从StreamingAssets里找到动画ab包：" + Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + additionalPath + "/" + clip_name);
        }
    }

    public IEnumerator LoadAnimationPackFromStreamingAssets(string type,string packABName)
    {
        string packkey;
        packkey = type + "/"+ packABName;//这种时候additionalPath兼具动画包的名字
        List<AnimationClip> clips = null;
        SeriesAnimationClipsDic.TryGetValue(packkey, out clips);
        if (clips != null)
        {
            yield break;
        }

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + packABName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingBundle = resultAssetBundle.assetBundle;//AB包

        if (readingBundle != null)
        {
            List<AnimationClip> clipList = new List<AnimationClip>();
            _AnimationClip = null;
            foreach (string _oneAssetName in readingBundle.GetAllAssetNames())
            {
                _AnimationClip = readingBundle.LoadAsset<AnimationClip>(_oneAssetName);
                if (_AnimationClip != null)
                {
                    //if (!AnimationClipDic.ContainsKey(_AnimationClip.name))
                    //{
                    //    AnimationClipDic.Add(_AnimationClip.name, _AnimationClip);
                    //}
                    //成包的基础类动画我们就不放进AnimationClipDic了
                    if (!clipList.Contains(_AnimationClip))
                        clipList.Add(_AnimationClip);
                }
            }
            if (SeriesAnimationClipsDic.ContainsKey(packkey))
            {
                SeriesAnimationClipsDic[packkey] = clipList;
            }
            else
            {
                SeriesAnimationClipsDic.Add(packkey, clipList);
            }
        }
        else
        {
            Debug.Log("从StreamingAssets读取包失败：" + Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + packABName);
        }
    }
}
