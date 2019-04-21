using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class MySkillStonesReader
{
    public IEnumerator loadMySkillstonesRemote(int playerid)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://47.245.7.100:8080/skill/stones/of/player/list?size=50&playerId=" + playerid.ToString()))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {


                //string a = webRequest.downloadHandler.text;
                //dynamic deserialized = JObject.Parse(a);

                //for (int i = 0; i < deserialized.Count;i++)
                //{
                //    Debug.Log("第"+i+"个json：" + deserialized[i]);
                //}

                //int[] stones = JsonConvert.DeserializeObject<int[]>(a);
                //mySkillStonesDicByType = convertSKillStoneNumListToDic(stones.ToList());
            }
        }
    }
}
