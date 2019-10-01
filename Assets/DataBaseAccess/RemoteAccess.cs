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
            UnityWebRequest webRequest = UnityWebRequest.Post(API, form);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
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
        
        public static List<string> getUsingStoneIDsOfAccountCharacter(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            List<string> list = new List<string>();
            if (accountCharacterInfo.a1_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.a1_skill_stone_record_id);
            if (accountCharacterInfo.a2_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.a2_skill_stone_record_id);
            if (accountCharacterInfo.a3_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.a3_skill_stone_record_id);
            if (accountCharacterInfo.b1_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.b1_skill_stone_record_id);
            if (accountCharacterInfo.b2_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.b2_skill_stone_record_id);
            if (accountCharacterInfo.b3_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.b3_skill_stone_record_id);
            if (accountCharacterInfo.c1_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.c1_skill_stone_record_id);
            if (accountCharacterInfo.c2_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.c2_skill_stone_record_id);
            if (accountCharacterInfo.c3_skill_stone_record_id != null)
            list.Add(accountCharacterInfo.c3_skill_stone_record_id);

            return list;
        }
        
        public static CharacterDataInfo getCharacterDataInfo(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            try
            {
                CharacterDataInfo characterDataInfo = new CharacterDataInfo();
                characterDataInfo.monsterId = accountCharacterInfo.monsterId;
                characterDataInfo.monsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                characterDataInfo.level = 100; //需要一个对应表

                NineAndTwo nineAndTwo = new NineAndTwo();
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelA1 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.a1_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelA2 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.a2_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelA3 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.a3_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelB1 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.b1_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelB2 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.b2_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelB3 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.b3_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelC1 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.c1_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelC2 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.c2_skill_stone_record_id);
                SkillStoneOfPlayerInfoModel _SkillStoneOfPlayerInfoModelC3 = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(accountCharacterInfo.c3_skill_stone_record_id);

                CharacterResourceInfo _TempCharacterResourceInfo = 
                monstersConfigTable.Instance.RowToCharacterResourceInfo(monstersConfigTable.Instance.Find_RECORD_ID(accountCharacterInfo.monsterId));
                if (_TempCharacterResourceInfo == null)
                {
                    Debug.Log("角色定义信息错误。monsterId：" + accountCharacterInfo.monsterId);
                    return null;
                }

                nineAndTwo.level = characterDataInfo.level;
                nineAndTwo.A1skillid = _SkillStoneOfPlayerInfoModelA1.skillId;
                nineAndTwo.A2skillid = _SkillStoneOfPlayerInfoModelA2.skillId;
                nineAndTwo.A3skillid = _SkillStoneOfPlayerInfoModelA3.skillId;
                nineAndTwo.B1skillid = _SkillStoneOfPlayerInfoModelB1.skillId;
                nineAndTwo.B2skillid = _SkillStoneOfPlayerInfoModelB2.skillId;
                nineAndTwo.B3skillid = _SkillStoneOfPlayerInfoModelB3.skillId;
                nineAndTwo.C1skillid = _SkillStoneOfPlayerInfoModelC1.skillId;
                nineAndTwo.C2skillid = _SkillStoneOfPlayerInfoModelC2.skillId;
                nineAndTwo.C3skillid = _SkillStoneOfPlayerInfoModelC3.skillId;
                nineAndTwo.moveType = _TempCharacterResourceInfo.moveType;
                nineAndTwo.rushType = _TempCharacterResourceInfo.rushType;
                nineAndTwo.canDefend = _TempCharacterResourceInfo.DEFENDABLE_FLAG;

                characterDataInfo._NineAndTwo = nineAndTwo;
                characterDataInfo._NineAndTwo.sortNineAndTwo();
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