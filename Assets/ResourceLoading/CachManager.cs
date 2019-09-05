using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CachManager
{
    private static CachManager instance;
    public static CachManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CachManager();
            }
            return instance;
        }
    }
    
    //下面是针对下载一个包的固定流程，重的是下载并cache这个包。这个应该是在资源确认画面里去固定的跑，跑完了之后我们再获取包再走非协程方法。
    public IEnumerator DownloadAndCacheExactFile(string bundleURL, string fileName)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // if you want to always load from server, can clear cache first
        //Caching.CleanCache();

        // get current bundle hash from server, random value added to avoid caching
        UnityWebRequest www = UnityWebRequest.Get(bundleURL  +"/" + fileName+ ".manifest?r=" + (Random.value * 9999999));
        //Debug.Log("Loading manifest:"+ bundleURL + "/" + fileName + ".manifest");

        // wait for load to finish
        yield return www.SendWebRequest();

        // if received error, exit
        if (www.isNetworkError == true)
        {
            Debug.Log("www error: " + www.error);
            www.Dispose();
            www = null;
            yield break;
        }
        if (www.error != null)
        {
            Debug.Log("www error "+ www.error + "  this :"+bundleURL +"/" + fileName);
            www.Dispose();
            www = null;
            yield break;
        }

        // create empty hash string
        Hash128 hashString = (default(Hash128));// new Hash128(0, 0, 0, 0);

        // check if received data contains 'ManifestFileVersion'
        if (www.downloadHandler.text.Contains("ManifestFileVersion"))
        {
            // extract hash string from the received data, TODO should add some error checking here
            var hashRow = www.downloadHandler.text.ToString().Split("\n".ToCharArray())[5];
            hashString = Hash128.Parse(hashRow.Split(':')[1].Trim());

            if (hashString.isValid == true)
            {
                //List<string> cachePaths = new List<string>();
                //Caching.GetAllCachePaths(cachePaths);               
                //foreach (string path in cachePaths)
                //    Debug.Log(path);

                // we can check if there is cached version or not
                if (Caching.IsVersionCached(bundleURL + "/" + fileName, hashString) == true)
                {
                    Debug.Log("Bundle:" + fileName +"is already cached! version : " + bundleURL + "/" + fileName);
                }
                else
                {
                    Debug.Log("No cached version founded for this hash..,bundle :" + bundleURL + "/" + fileName);
                }
            }
            else
            {
                // invalid loaded hash, just try loading latest bundle
                Debug.Log("Invalid hash:" + hashString);
                yield break;//关于这个环节要不要yield break是有疑问的。貌似可以不断开，直接读旧文件？
            }
        }
        else
        {
            Debug.Log("Manifest doesn't contain string 'ManifestFileVersion': " + bundleURL + "/" + fileName + ".manifest");
            yield break;//关于这个环节要不要yield break是有疑问的。貌似可以不断开，直接读旧文件？
        }

        // now download the actual bundle, with hashString parameter it uses cached version if available.所以这个函数重点是这一步cach包
        www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL + "/" + fileName + "?r=" + (Random.value * 9999999), hashString, 0);

        //删除无用旧版本。我们的系统应该没什么为用户保留旧版本ab包的必要。无论哪个文件
        Caching.ClearOtherCachedVersions(fileName, hashString);

        // wait for load to finish
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("www error: " + www.error);
            www.Dispose();
            www = null;
            yield break;
        }

        // get bundle from downloadhandler
        AssetBundle bundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;
        www.Dispose();
        www = null;

        // try to cleanup memory
        Resources.UnloadUnusedAssets();
        if (bundle != null)
        {
            bundle.Unload(false);
        }
        else
        {
            Debug.Log("没有抓到包"+bundleURL + "/" + fileName);
            
        }
        yield break;
    }
    
    // 这个函数的重点：它就是完全在已经cach到资源的基础上去从cach读取资源，如果没有cach的话它是直接断的，并不会再去联网下载对应资源。
    // 其中yield return www.SendWebRequest();这一行别让它看起来的样子误导了，断网了的话它不会报错。
    public IEnumerator getABFromCach(string Path, string abName)
    {
        AssetBundle readingBundle = null;
        while (!Caching.ready)
            yield return null;

        List<Hash128> catchedVersions = new List<Hash128>();
        Caching.GetCachedVersions(abName, catchedVersions);
        if (catchedVersions.Count == 0)
        {
            Debug.Log(Path + "/" + abName+":没找到cached的版本");
            yield break;
        }//问题比较大的环节是，我怎么保证cached住的都是一个最新版本，而不要那些旧版本
        
        Hash128 hashString = catchedVersions[0];

        // now download the actual bundle, with hashString parameter it uses cached version if available
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(Path + "/" + abName + "?r=" + (Random.value * 9999999), hashString, 0);

        // wait for load to finish
        yield return www.SendWebRequest();//yield return www.Send();
        if (www.error != null)
        {
            Debug.Log("读取本地cach函数内部的联网类错误？ www error: " + www.error);
            www.Dispose();
            www = null;
            yield break;
        }

        // get bundle from downloadhandler
        readingBundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;
        www.Dispose();
        www = null;
        if (readingBundle != null)
        {
            Debug.Log(Path + "/" + abName+":成功抓到包");
        }
        yield return readingBundle;
    }
    
    public IEnumerator getABFromStreamingAssets(string subPath, string abName)
    {
        AssetBundle readingBundle = null;
        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/" + subPath + "/" + abName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingBundle = resultAssetBundle.assetBundle;
        yield break;
    }
}
