using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;
#endif

//这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
public partial class AccountCharsSet {

    public static AccountCharsSet instance;
    public static CharacterDataInfo[] ownedChars;//本单例模式的处理对象

    private AccountCharsSet()
    {
    }
    public static AccountCharsSet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AccountCharsSet();
            }
            return instance;
        }
    }

    //实际版本中我们不可能每次执行“玩家拥有角色”更新操作都去更新所有角色的信息，对应的json对象应该是针对单独的某个角色。。。
    // 换句话说可能就不存在整体更新所有拥有角色的函数，你只能把账户拥有的角色一个个添加，一个个修改或一个个删除。
    public IEnumerator overrideMyCharsInfo()
    {
        switch (AccountSet.Instance._playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.localTestSaveData:
                overrideMyCharsInfoOnJsonFile();
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                break;
            case playerinfoReferenceMode.formalVersion:
                break;
        }
        yield break;
    }

    public IEnumerator loadMyOwnedCharsInfo()
    {
        switch (AccountSet.Instance._playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.localTestSaveData:                
                loadMyOwnedCharsInfoViaJsonFile("myownedCharsJson.json");
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                yield return loadMyOwnedCharsInfoRemote("http://47.245.7.100:8080/monsters/of/player/list?playerId=1");
                break;
            case playerinfoReferenceMode.formalVersion:
                break;
        }
        yield break;
    }

    public void sellOneChar(int localID) // 这个必然是要建立在正确把握localid的基础上的
    {
        List<CharacterDataInfo> ownedCharsList = ownedChars.ToList();
        CharacterDataInfo toSellCharFound = null;
        foreach (CharacterDataInfo _CharacterDataInfo in ownedCharsList)
        {
            if (_CharacterDataInfo.localID == localID)
            {
                toSellCharFound = _CharacterDataInfo;
                break;
            }
        }
        if (toSellCharFound != null)
        {
            ownedCharsList.Remove(toSellCharFound);
            ownedChars = ownedCharsList.ToArray();
        }
        else
        {
            Debug.Log("严重错误，要卖的宠物的编号没找到");
        }
    }

    public static void updateMyCharInfo(int localID, CharacterDataInfo _CharacterDataInfo)
    {
        List<CharacterDataInfo> mycharlist = ownedChars.ToList();
        CharacterDataInfo old = getTheCharacterOfMine(localID);
        mycharlist.Remove(old);
        mycharlist.Add(_CharacterDataInfo);
        ownedChars = mycharlist.ToArray();
    }

    public static CharacterDataInfo getTheCharacterOfMine(int Key)
    {
        foreach (CharacterDataInfo _CharacterDataInfo in ownedChars)
        {
            if (_CharacterDataInfo.localID == Key)
            {
                return _CharacterDataInfo;
            }
        }
        return null;
    }
    
    public IEnumerator loadMonsterDataBaseRemote(int playerid)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://47.245.7.100:8080/monsters/7/detail"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string response = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                //string a = webRequest.downloadHandler.text;
                //Debug.Log(response);
                monstersConfigTable.monsterResponse testobject = JsonConvert.DeserializeObject<monstersConfigTable.monsterResponse>(response);
                if (testobject != null)
                {
                    Debug.Log(testobject.data.realName);
                    Debug.Log(testobject.data.accSkill);
                    Debug.Log(testobject.data.id);
                    Debug.Log(testobject.data.canDefend);
                }
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

    private int intCompare(int i1, int i2)
    {
        if (i1 > i2)
        {
            return 1;
        }
        if (i1 < i2)
        {
            return -1;
        }
        return 0;
    }
}
