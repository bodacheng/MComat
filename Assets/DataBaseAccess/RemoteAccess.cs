using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace dataAccess
{
    public class RemoteAccess
    {
        public static IEnumerator generalRemoteAccess(WWWForm form,string API)
        {
            Debug.Log("****************");
            Debug.Log($"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}");
            UnityWebRequest webRequest = UnityWebRequest.Post(API, form);
            Debug.Log("发送前code："+webRequest.responseCode);
            Debug.Log("Error: " + webRequest.error);
            Debug.Log("已经以这个url为目标发送登陆请求：" + webRequest.url);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("发送后code："+webRequest.responseCode);
            Debug.Log("////////////////////////////////////////");
            Debug.Log($"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}");
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                Debug.Log("已经以这个url为目标发送登陆请求：" + webRequest.url);
                Debug.Log("返回code：" + webRequest.responseCode);
                yield return null;
            }
            else
            {
                if (webRequest.responseCode == 200)
                {
                    Debug.Log(API + "请求成功");
                    yield return webRequest.downloadHandler;
                }else{
                    Debug.Log(API + "请求失败.  " + form);
                    yield return null;
                }
            }
        }        
    }
}