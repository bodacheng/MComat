using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace dataAccess
{
    public class RemoteAccess
    {
        public static IEnumerator generalRemoteAccess(WWWForm form, string API)
        {
            Debug.Log($"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}");
            UnityWebRequest webRequest = UnityWebRequest.Post(API, form);
            Debug.Log("发送请求：" + webRequest.url);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("返回code：" + webRequest.responseCode);
            Debug.Log($"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}");
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                yield return null;
            }
            else
            {
                if (webRequest.responseCode == 200)
                {
                    Debug.Log(API + "请求成功");
                    yield return webRequest.downloadHandler;
                }else{
                    Debug.Log(API + "请求失败: " + webRequest.downloadHandler);
                    yield return null;
                }
            }
        }        
    }
}