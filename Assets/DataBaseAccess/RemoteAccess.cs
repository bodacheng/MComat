using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Api.Dto.Model;

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
                
        public static CharDataInfo GetCharDataInfo(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            try
            {
                CharDataInfo characterDataInfo = new CharDataInfo
                {
                    ResourceID = accountCharacterInfo.monsterId,
                    monsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId
                };

                List<SkillStoneOfPlayerInfoModel> targets = MySkillStonesReader.Instance.GetMonsterEquipingStones(accountCharacterInfo.monsterOfPlayerId);
                NineAndTwo nineAndTwo = new NineAndTwo();
                CharConfig _TempCharacterResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(accountCharacterInfo.monsterId));
                if (_TempCharacterResourceInfo == null)
                {
                    Debug.Log("角色定义信息错误。monsterId：" + accountCharacterInfo.monsterId);
                    return null;
                }
                for (int i = 0; i < targets.Count; i++)
                {
                    switch(targets[i].inUsingSkillSlot)
                    {
                        case "1":
                            nineAndTwo.A1skillid = targets[i].skillId;
                            nineAndTwo.A1level = int.Parse(targets[i].level);
                        break;
                        case "2":
                            nineAndTwo.A2skillid = targets[i].skillId;
                            nineAndTwo.A2level = int.Parse(targets[i].level);
                        break;
                        case "3":
                            nineAndTwo.A3skillid = targets[i].skillId;
                            nineAndTwo.A3level = int.Parse(targets[i].level);
                        break;
                        case "4":
                            nineAndTwo.B1skillid = targets[i].skillId;
                            nineAndTwo.B1level = int.Parse(targets[i].level);
                        break;
                        case "5":
                            nineAndTwo.B2skillid = targets[i].skillId;
                            nineAndTwo.B2level = int.Parse(targets[i].level);
                        break;
                        case "6":
                            nineAndTwo.B3skillid = targets[i].skillId;
                            nineAndTwo.B3level = int.Parse(targets[i].level);
                        break;
                        case "7":
                            nineAndTwo.C1skillid = targets[i].skillId;
                            nineAndTwo.C1level = int.Parse(targets[i].level);
                        break;
                        case "8":
                            nineAndTwo.C2skillid = targets[i].skillId;
                            nineAndTwo.C2level = int.Parse(targets[i].level);
                        break;
                        case "9":
                            nineAndTwo.C3skillid = targets[i].skillId;
                            nineAndTwo.C3level = int.Parse(targets[i].level);
                        break;
                    }
                }
                nineAndTwo.moveType = _TempCharacterResourceInfo.moveType;
                nineAndTwo.rushType = _TempCharacterResourceInfo.rushType;
                nineAndTwo.canDefend = _TempCharacterResourceInfo.DEFENDABLE_FLAG;
                characterDataInfo._NineAndTwo = nineAndTwo;
                characterDataInfo._NineAndTwo.SortNineAndTwo();
                return characterDataInfo;
            }
            catch (Exception e)
            {
                Debug.Log("数据库信息有错误:" + e);
                return null;
            }
        }
    }
}