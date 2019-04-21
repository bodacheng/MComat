using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class AccountSet
{
    public IEnumerator loadCustomerInfoFromRemoteServer(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                string a = webRequest.downloadHandler.text;
                localCustomerInfo = JsonConvert.DeserializeObject<PlayerAccountInfo>(a);
                Debug.Log("remoteplayerdetail:"+a);
            }
        }
    }

    public IEnumerator overrideAccountRemote(string uri)
    {
        string json = JsonConvert.SerializeObject(localCustomerInfo);
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:5000/user/login", json);
        request.SetRequestHeader("content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("api-version", "0.1");

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) 
            Debug.Log(request.error);

        Debug.Log("Response as byte:" + request.downloadHandler.data);
        Debug.Log("Response as string:"+ request.downloadHandler.text);
    }
}
