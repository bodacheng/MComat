using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimationResourceLoader
{
    static AnimationResourceLoader instance;
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
    
    public static IDictionary<string, AnimationClip> AnimationClipDic = new Dictionary<string, AnimationClip>();
    public static IDictionary<string, List<AnimationClip>> SeriesAnimationClipsDic = new Dictionary<string, List<AnimationClip>>();//这个是用来记录那些一个包里好几个动画的
    //public IDictionary<string, RuntimeAnimatorController> RuntimeAnimatorControllerIdic = new Dictionary<string, RuntimeAnimatorController>();
    
    //战斗场景下角色实时读取动作走的是这个，所以必然的我们有了getAnimationClip和ConstructAnimationClip
    public AnimationClip GetAnimationClip(string key)
    {
        AnimationClipDic.TryGetValue(key, out AnimationClip _AnimationClip);
        return _AnimationClip;
    }

    public List<AnimationClip> GetAnimationPack(string packkey)
    {
        SeriesAnimationClipsDic.TryGetValue(packkey, out List<AnimationClip> clips);
        return clips ?? null;
    }

    public IEnumerator LoadAnimationPackFromCache(string url_withoutfilename,string dic_key, string packABName)
    {
        IEnumerator task = CachManager.Instance.DownloadAndCacheExactFile(url_withoutfilename,packABName);
        yield return task;
        task = CachManager.Instance.getABFromCach(url_withoutfilename,packABName);
        yield return task;       
        AssetBundle assetBundle = (AssetBundle)task.Current;
        if (assetBundle == null)
        {
            Debug.Log("读取动作包错误："+packABName);
            yield break;
        }
        
        List<AnimationClip> clipList = new List<AnimationClip>();
        AnimationClip _AnimationClip = null;
        foreach (string _oneAssetName in assetBundle.GetAllAssetNames())
        {
            _AnimationClip = assetBundle.LoadAsset<AnimationClip>(_oneAssetName);
            if (_AnimationClip != null)
            {
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
                if (_AnimationClip.name == "jump")
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = "jump",
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
                clipList.Add(_AnimationClip);
            }
        }
        assetBundle.Unload(false);
        if (SeriesAnimationClipsDic.ContainsKey(dic_key))
            SeriesAnimationClipsDic[dic_key] = clipList;
        else{
            Debug.Log("基础动画包"+dic_key+"加入内存");
            SeriesAnimationClipsDic.Add(dic_key, clipList);
        }
    }
    
    public IEnumerator LoadAnimationClipFromCachAndPutItIntoDic(string bundleURLwithoutclipname,string dic_key,string clip_name)
    {
        AnimationClip _AnimationClip = null;
        //IEnumerator task = DownloadAndCacheExactFile(bundleURLwithoutclipname,clip_name);//总之这个环节在确认包都下载好了的情况下应该可以忽略掉
        //yield return task;
        IEnumerator task = CachManager.Instance.getABFromCach(bundleURLwithoutclipname,clip_name);
        yield return task;
        AssetBundle readingBundle = (AssetBundle)task.Current;
        if (readingBundle == null)
        {
            Debug.Log("动作读取错误：" + clip_name);
            yield break;
        }

        //string key = type + (subkeyword == null ? null : ("/" + subkeyword + "/")) + clip_name;
        _AnimationClip = readingBundle.LoadAsset<AnimationClip>(clip_name);
        if (_AnimationClip != null)
        {
            readingBundle.Unload(false);
            if (!AnimationClipDic.ContainsKey(dic_key))
            {
                AnimationEvent endFlag = new AnimationEvent
                {
                    functionName = "ThisIsEndOfAnimation",
                    stringParameter = _AnimationClip.name,
                    time = _AnimationClip.length
                };
                _AnimationClip.AddEvent(endFlag);
                AnimationClipDic.Add(dic_key,_AnimationClip);
            }
            else
            {
                Debug.Log("动画key重复");
            }
                
            Debug.Log("动画片段以"+dic_key +"为key加入动画字典");
        }
        else
        {
            readingBundle.Unload(false);
            yield break;
        }
    }
    
    public IEnumerator LoadAnimationPackFromStreamingAssets(string type,string packABName)
    {
        string packkey;
        packkey = type + "/"+ packABName;//这种时候additionalPath兼具动画包的名字
        SeriesAnimationClipsDic.TryGetValue(packkey, out List<AnimationClip> clips);
        if (clips != null)
        {
            yield break;
        }

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + packABName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        AssetBundle readingBundle = resultAssetBundle.assetBundle;//AB包

        if (readingBundle != null)
        {
            List<AnimationClip> clipList = new List<AnimationClip>();
            AnimationClip _AnimationClip = null;
            foreach (string _oneAssetName in readingBundle.GetAllAssetNames())
            {
                _AnimationClip = readingBundle.LoadAsset<AnimationClip>(_oneAssetName);
                if (_AnimationClip != null)
                {
                    if (_AnimationClip.name == "rush")
                    {
                        AnimationEvent endFlag = new AnimationEvent
                        {
                            functionName = "ThisIsEndOfAnimation",
                            stringParameter = _AnimationClip.name,
                            time = _AnimationClip.length
                        };
                        _AnimationClip.AddEvent(endFlag);
                    }
                    if (_AnimationClip.name == "rushback")
                    {
                        AnimationEvent endFlag = new AnimationEvent
                        {
                            functionName = "ThisIsEndOfAnimation",
                            stringParameter = _AnimationClip.name,
                            time = _AnimationClip.length
                        };
                        _AnimationClip.AddEvent(endFlag);
                    }
                    if (_AnimationClip.name == "jump")
                    {
                        AnimationEvent endFlag = new AnimationEvent
                        {
                            functionName = "ThisIsEndOfAnimation",
                            stringParameter = _AnimationClip.name,
                            time = _AnimationClip.length
                        };
                        _AnimationClip.AddEvent(endFlag);
                    }
                    if (_AnimationClip.name == "getup")
                    {
                        AnimationEvent endFlag = new AnimationEvent
                        {
                            functionName = "ThisIsEndOfAnimation",
                            stringParameter = _AnimationClip.name,
                            time = _AnimationClip.length
                        };
                        _AnimationClip.AddEvent(endFlag);
                    }
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
    
    public IEnumerator LoadAnimationClipFromStreamingAssetsAndPutItIntoDic(string type, string additionalPath, string clip_name)
    {
        if (additionalPath == null)
        {
            yield break;//完全是为了思路清晰，否则我们看不明白各种针对这东西的分歧处理是些啥
        }
        string clipkey = type + "/" + additionalPath + "/" + clip_name;
        AnimationClipDic.TryGetValue(clipkey, out AnimationClip _AnimationClip);
        if (_AnimationClip != null)
        {
            //Debug.Log("动画：" + clipkey + "已经存在");
            yield break;
        }

        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/animClips/" + type + "/" + additionalPath + "/" + clip_name);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        AssetBundle readingBundle = resultAssetBundle.assetBundle;//AB包

        if (readingBundle != null)
        {
            _AnimationClip = readingBundle.LoadAsset<AnimationClip>(clip_name);
            if (_AnimationClip != null)
            {
                readingBundle.Unload(false);
                if (!AnimationClipDic.ContainsKey(clipkey))
                {
                    AnimationEvent endFlag = new AnimationEvent
                    {
                        functionName = "ThisIsEndOfAnimation",
                        stringParameter = _AnimationClip.name,
                        time = _AnimationClip.length
                    };
                    _AnimationClip.AddEvent(endFlag);
                    AnimationClipDic.Add(clipkey, _AnimationClip);
                }
                else
                    Debug.Log("严重错误：动画key值重复"+ clipkey);
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
        
    // 这个函数的构造就是说，我们虽然正式版本游戏各个type角色的攻击动画都是ab包放一起，但开发环境下他们还是按着自身特点放在那几个文件夹下面，
    // 那么测试环境下我们就干脆把所有resource文件夹下的攻击类动画全load来方便提取。
    // 正式版本游戏是根据脚本决定load哪些包从而构成字典的，不会产生load不用动画的问题
    public IEnumerator PrepareAllAttackAnimationClipsByTypeFromResourceAndPutItIntoDic(string type)
    {
        List<UnityEngine.Object> G_Attack_States = Resources.LoadAll("Animations/"+ type + "/" + "G_Attack_State", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> G_Attack_State_Stays = Resources.LoadAll("Animations/" + type + "/" + "G_Attack_State_Stay", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> GMStatess = Resources.LoadAll("Animations/" + type + "/" + "GMStates", typeof(AnimationClip)).ToList();

        string clipkey;
        foreach (UnityEngine.Object _object in G_Attack_States)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                Debug.Log("严重错误 技能动画key重复："+clipkey);
            else
            {
                AnimationClip theclip = (AnimationClip)_object;
                AnimationEvent endFlag = new AnimationEvent
                {
                    functionName = "ThisIsEndOfAnimation",
                    stringParameter = theclip.name,
                    time = theclip.length
                };
                theclip.AddEvent(endFlag);
                AnimationClipDic.Add(clipkey, theclip);
            }
        }
        foreach (UnityEngine.Object _object in G_Attack_State_Stays)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                Debug.Log("严重错误 技能动画key重复："+clipkey);
            else{
                AnimationClip theclip = (AnimationClip)_object;
                AnimationEvent endFlag = new AnimationEvent
                {
                    functionName = "ThisIsEndOfAnimation",
                    stringParameter = theclip.name,
                    time = theclip.length
                };
                theclip.AddEvent(endFlag);
                AnimationClipDic.Add(clipkey, theclip);
            }
        }
        foreach (UnityEngine.Object _object in GMStatess)
        {
            clipkey = type + "/skill/" + _object.name;
            if (AnimationClipDic.ContainsKey(clipkey))
                Debug.Log("严重错误 技能动画key重复："+clipkey);
            else{
                AnimationClip theclip = (AnimationClip)_object;
                AnimationEvent endFlag = new AnimationEvent
                {
                    functionName = "ThisIsEndOfAnimation",
                    stringParameter = theclip.name,
                    time = theclip.length
                };
                theclip.AddEvent(endFlag);
                AnimationClipDic.Add(clipkey, theclip);
            }
        }
        yield break;
    }
}

    //public IEnumerator LoadAnimatorFromCache(string url_withoutfilename,string dic_key, string packABName)
    //{
    //    IEnumerator task = CachManager.Instance.DownloadAndCacheExactFile(url_withoutfilename,packABName);
    //    yield return task;
    //    task = CachManager.Instance.getABFromCach(url_withoutfilename,packABName);
    //    yield return task;

    //    AssetBundle readingBundle = null;
        
    //    if (task.Current != null)
    //        readingBundle = (AssetBundle)task.Current;
    //    else
    //        readingBundle = null;

    //    if (readingBundle != null)
    //    {
    //        Debug.Log("定位到animator："+dic_key);
    //        RuntimeAnimatorController toLoadRuntimeAnimatorController = readingBundle.LoadAsset<RuntimeAnimatorController>(packABName);
    //        readingBundle.Unload(false);
    //        if (toLoadRuntimeAnimatorController != null)
    //        {
    //            Debug.Log("generic_controller"+dic_key+"貌似提取成功");
    //            if (AnimationResourceLoader.Instance.RuntimeAnimatorControllerIdic.ContainsKey(dic_key))
    //                AnimationResourceLoader.Instance.RuntimeAnimatorControllerIdic[dic_key] = toLoadRuntimeAnimatorController;
    //            else
    //                AnimationResourceLoader.Instance.RuntimeAnimatorControllerIdic.Add(dic_key, toLoadRuntimeAnimatorController);
    //        }else{
    //            Debug.Log("?");
    //        }
    //    }else{
    //        Debug.Log("generic_controller"+dic_key+"包获取失败");
    //    }
    //    yield break;
    //}
    
    //public RuntimeAnimatorController getRuntimeAnimatorController(string type)
    //{
    //    RuntimeAnimatorController toLoadRuntimeAnimatorController;
    //    RuntimeAnimatorControllerIdic.TryGetValue(type,out toLoadRuntimeAnimatorController);
    //    return toLoadRuntimeAnimatorController;
    //}
    
        //if (!RuntimeAnimatorControllerIdic.ContainsKey(type))
        //{
        //    RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        //    if (toLoadRuntimeAnimatorController != null)
        //        RuntimeAnimatorControllerIdic.Add(type, toLoadRuntimeAnimatorController);
        //    else
        //        Debug.Log("找不到动画控制器："+"Animations/" + type + "/generic_controller");
        //}else{
        //    if (RuntimeAnimatorControllerIdic[type] == null)
        //    {
        //        RuntimeAnimatorController toLoadRuntimeAnimatorController = Resources.Load("Animations/" + type + "/generic_controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        //        if (toLoadRuntimeAnimatorController != null)
        //            RuntimeAnimatorControllerIdic[type] = toLoadRuntimeAnimatorController;
        //        else
        //            Debug.Log("找不到动画控制器："+"Animations/" + type + "/generic_controller");
        //    }
        //}