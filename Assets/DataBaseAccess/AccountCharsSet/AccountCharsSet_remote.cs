using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

public partial class AccountCharsSet{
    public IEnumerator loadMyOwnedCharsInfoRemote(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string[] pages = url.Split('/');
            int page = pages.Length - 1;
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                string a = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                Debug.Log(a);
                CharacterDataInfoListJsonResponse characterDataInfoListJsonResponse = JsonConvert.DeserializeObject<CharacterDataInfoListJsonResponse>(a);

                List<CharacterDataInfoJson> characterDataInfoJson = new List<CharacterDataInfoJson>();
                if (characterDataInfoListJsonResponse.data.list != null)
                    characterDataInfoJson = characterDataInfoListJsonResponse.data.list.ToList();
                    
                List<CharacterDataInfo> mychars = new List<CharacterDataInfo>();
                foreach (CharacterDataInfoJson CharacterDataInfoJson in characterDataInfoJson)
                {
                    Debug.Log("玩家的一只狗加载了："+ CharacterDataInfoJson.monsterId);
                    
                    CharacterDataInfo monsterOfAPlayer = CharacterDataInfoJson.getCharacterDataInfo();
                    if (monsterOfAPlayer != null)
                        mychars.Add(monsterOfAPlayer);
                }
                AccountCharsSet.ownedChars = mychars.ToArray();
            }
        }
    }

    // 本地信息是整体保存的，但对于角色这种东西，应该是一个条目一个条目的去保存。
    public IEnumerator overrideAccountCharRemote(string uri,int charlocalID,CharacterDataInfo _characterDataInfo)
    {
        CharacterDataInfoJson characterDataInfoJson = _characterDataInfo.getCharacterDataInfoJson();
        string json = JsonConvert.SerializeObject(characterDataInfoJson);
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:5000/dsdaf/" + charlocalID, json);
        request.SetRequestHeader("content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("api-version", "0.1");

        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);

        Debug.Log("Response as byte:" + request.downloadHandler.data);
        Debug.Log("Response as string:" + request.downloadHandler.text);
    }

    // 针对玩家拥有角色的更新(monsters_of_player表的update操作),
    // 服务端需要有审核,下面是角色更新操作的一些要点
    // 1.monsterId，playerid 不可能变
    // 2.level与exp存在相互对应关系，待定
    // 3.a1Id到c3Id(技能id)都可能更新，在更新时，新的id需要索引skills查看type，
    //然后monsterId索引monsters表，查看对应的type与技能id对应type是否一致，如不一致不执行更新。
}
