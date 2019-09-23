using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public GetMonsterOfPlayerDetailModel loadAccountCharacterInfoViaJsonFile(string monsterlocalid)
        {
            try
            {
                GetMonsterOfPlayerDetailModel info = null;
                string wholepath = Application.persistentDataPath + "/AccountCharacterInfos/" + monsterlocalid + ".json";
                if (File.Exists(wholepath))
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    info = JsonConvert.DeserializeObject<GetMonsterOfPlayerDetailModel>(dataAsJson);
                }
                return info;
            }
            catch (Exception e)
            {
                Debug.Log(e + " 角色读取过程有些问题。测试用json文档格式？");
                return null;
            }
        }

        public void loadAccountCharacterInfoListObjectsViaJsonFile()
        {
            try
            {
                MonsterOfPlayerListModel[] info = new MonsterOfPlayerListModel[0];
                string wholepath = Application.persistentDataPath + "/AccountCharacterInfoList.json";
                if (File.Exists(wholepath))
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    info = JsonConvert.DeserializeObject<MonsterOfPlayerListModel[]>(dataAsJson);
                    accountCharacterInfoListObjectsDictionary.Clear();
                    for (int i = 0; i < info.Length; i++)
                    {
                        CharacterResourceInfo targetingCharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(info[i].monsterId);
                        if (targetingCharacterResourceInfo == null)
                        {
                            Debug.Log("严重错误，无法找到对应角色信息。monsterid:" + info[i].monsterId);
                            continue;
                        }
                        
                        if (!accountCharacterInfoListObjectsDictionary.ContainsKey(info[i].monsterOfPlayerId))
                            accountCharacterInfoListObjectsDictionary.Add(info[i].monsterOfPlayerId, info[i]);
                        else
                            Debug.Log("巨大逻辑错误，重复的monsterOfPlayerId:" + info[i].monsterOfPlayerId);
                    }
                }
                else
                {
                    accountCharacterInfoListObjectsDictionary.Clear();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e + "角色列表读取过程有些问题。测试用json文档");
            }
        }

        public void overrideAccountCharacterInfoListObjectsViaJsonFile()
        {
            try
            {
                List<MonsterOfPlayerListModel> accountCharacterInfoListObjects = new List<MonsterOfPlayerListModel>();
                foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValue in accountCharacterInfoListObjectsDictionary)
                {
                    accountCharacterInfoListObjects.Add(keyValue.Value);
                }
                string json = JsonConvert.SerializeObject(accountCharacterInfoListObjects);
                LocalJson.saveInfoToJsonFile(null, "AccountCharacterInfoList.json", json);
            }
            catch (Exception e)
            {
                Debug.Log(e + "角色列表保存过程有些问题");
            }
        }

        public IEnumerator localSaveDataGetAllCharacters()
        {
            List<CharacterResourceInfo> characterList = monstersConfigTable.Instance.RowToCharacterResourceInfoList(monstersConfigTable.Instance.rowList);
            int i = 0;
            foreach (CharacterResourceInfo _CharacterResourceInfo in characterList)
            {
                GetMonsterOfPlayerDetailModel _CharacterDataInfo = new GetMonsterOfPlayerDetailModel();
                _CharacterDataInfo.monsterId = _CharacterResourceInfo.RECORD_ID.ToString();
                _CharacterDataInfo.monsterOfPlayerId = i.ToString();
                Debug.Log("将角色" + _CharacterResourceInfo.REAL_NAME + "加入了存档");
                yield return addNewCharToJsonSaveData(_CharacterDataInfo);
                i++;
            }
            overrideAccountCharacterInfoListObjectsViaJsonFile();
            yield break;
        }

        public IEnumerator addNewCharToJsonSaveData(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            List<int> currentLocalIDList = new List<int>();
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> one in accountCharacterInfoListObjectsDictionary)
            {
                if (!currentLocalIDList.Contains(int.Parse(one.Value.monsterOfPlayerId)))
                    currentLocalIDList.Add(int.Parse(one.Value.monsterOfPlayerId));
                else
                    Debug.Log("本地存档有重复的monsterOfPlayerId：" + int.Parse(one.Value.monsterOfPlayerId));
            }
            currentLocalIDList.Sort((a, b) => AccountCharsSet.Instance.intCompare(a, b));

            MonsterOfPlayerListModel accountCharacterInfoListObject = new MonsterOfPlayerListModel();
            accountCharacterInfoListObject.monsterId = _accountCharacterInfo.monsterId;

            if (currentLocalIDList.Contains(int.Parse(_accountCharacterInfo.monsterOfPlayerId)))
                Debug.Log("本地存档产生角色信息覆盖行为，宠物的monsterOfPlayerId" + _accountCharacterInfo.monsterOfPlayerId + "但追加仍然继续");

            accountCharacterInfoListObject.monsterOfPlayerId = _accountCharacterInfo.monsterOfPlayerId;
            if (!accountCharacterInfoListObjectsDictionary.ContainsKey(accountCharacterInfoListObject.monsterOfPlayerId))
                accountCharacterInfoListObjectsDictionary.Add(accountCharacterInfoListObject.monsterOfPlayerId, accountCharacterInfoListObject);
            else
                accountCharacterInfoListObjectsDictionary[accountCharacterInfoListObject.monsterOfPlayerId] = accountCharacterInfoListObject;

            if (!AccountCharacterInfoDictionary.ContainsKey(accountCharacterInfoListObject.monsterOfPlayerId))
                AccountCharacterInfoDictionary.Add(accountCharacterInfoListObject.monsterOfPlayerId, _accountCharacterInfo);
            else
                AccountCharacterInfoDictionary[accountCharacterInfoListObject.monsterOfPlayerId] = _accountCharacterInfo;

            string json = JsonConvert.SerializeObject(_accountCharacterInfo);
            LocalJson.saveInfoToJsonFile("AccountCharacterInfos", accountCharacterInfoListObject.monsterOfPlayerId + ".json", json);
            yield break;
        }

        public IEnumerator updateCharJsonSaveData(GetMonsterOfPlayerDetailModel _CharacterDataInfo)
        {
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(_CharacterDataInfo.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel before = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (before == null)
                yield break;
            string json = JsonConvert.SerializeObject(_CharacterDataInfo);
            LocalJson.saveInfoToJsonFile("AccountCharacterInfos", _CharacterDataInfo.monsterOfPlayerId + ".json", json);
            yield break;
        }

        public IEnumerator plusExpForAccountCharLocalSaveData(string charlocalID, int plusExp)
        {
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(charlocalID);
            yield return getchar;
            GetMonsterOfPlayerDetailModel before = (GetMonsterOfPlayerDetailModel)getchar.Current;

            if (AccountSet.instance._PlayerAccountInfo.Coin < plusExp || before == null)
            {
                yield break;
            }
            AccountSet.instance._PlayerAccountInfo.Coin -= plusExp;
            int currentExp = before.experience;
            currentExp += plusExp;
            before.experience = currentExp;
            yield break;
        }
    }
}