using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using mainMenu;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public GetMonsterOfPlayerDetailModel LoadAccountCharacterInfoViaJsonFile(string monsterlocalid)
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

        public void LoadAccountCharacterInfoListObjectsViaJsonFile()
        {
            try
            {
                MonsterOfPlayerListModel[] info = new MonsterOfPlayerListModel[0];
                string wholepath = Application.persistentDataPath + "/AccountCharacterInfoList.json";
                if (File.Exists(wholepath))
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    info = JsonConvert.DeserializeObject<MonsterOfPlayerListModel[]>(dataAsJson);
                    AccountCharacterInfoListObjectsDictionary.Clear();
                    for (int i = 0; i < info.Length; i++)
                    {
                        CharConfig targetingCharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(info[i].monsterId);
                        if (targetingCharacterResourceInfo == null)
                        {
                            Debug.Log("严重错误，无法找到对应角色信息。monsterid:" + info[i].monsterId);
                            continue;
                        }
                        
                        if (!AccountCharacterInfoListObjectsDictionary.ContainsKey(info[i].monsterOfPlayerId))
                            AccountCharacterInfoListObjectsDictionary.Add(info[i].monsterOfPlayerId, info[i]);
                        else
                            Debug.Log("巨大逻辑错误，重复的monsterOfPlayerId:" + info[i].monsterOfPlayerId);
                    }
                }
                else
                {
                    AccountCharacterInfoListObjectsDictionary.Clear();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e + "角色列表读取过程有些问题。测试用json文档");
            }
        }

        public void OverrideAccountCharacterInfoListObjectsViaJsonFile()
        {
            try
            {
                List<MonsterOfPlayerListModel> accountCharacterInfoListObjects = new List<MonsterOfPlayerListModel>();
                foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValue in AccountCharacterInfoListObjectsDictionary)
                {
                    accountCharacterInfoListObjects.Add(keyValue.Value);
                }
                string json = JsonConvert.SerializeObject(accountCharacterInfoListObjects);
                LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "AccountCharacterInfoList.json", json);
            }
            catch (Exception e)
            {
                Debug.Log(e + "角色列表保存过程有些问题");
            }
        }

        public IEnumerator LocalSaveDataGetAllCharacters()
        {
            List<CharConfig> characterList = MonstersConfigTable.Instance.RowToCharacterResourceInfoList(MonstersConfigTable.Instance.rowList);
            int i = 0;
            foreach (CharConfig _CharacterResourceInfo in characterList)
            {
                GetMonsterOfPlayerDetailModel _CharacterDataInfo = new GetMonsterOfPlayerDetailModel
                {
                    monsterId = _CharacterResourceInfo.RECORD_ID,
                    monsterOfPlayerId = i.ToString()
                };
                Debug.Log("将角色" + _CharacterResourceInfo.REAL_NAME + "加入了存档");
                yield return AddOneCharacterToAccount(_CharacterDataInfo);
                i++;
            }
            OverrideAccountCharacterInfoListObjectsViaJsonFile();
            yield return MonsterBox.DisplayMonsterIcons();
            yield break;
        }

        public IEnumerator AddNewCharToJsonSaveData(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            List<int> CurrentLocalIDList = new List<int>();
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> one in AccountCharacterInfoListObjectsDictionary)
            {
                if (!CurrentLocalIDList.Contains(int.Parse(one.Value.monsterOfPlayerId)))
                {
                    CurrentLocalIDList.Add(int.Parse(one.Value.monsterOfPlayerId));
                }
                else
                {
                    Debug.Log("本地存档有重复的monsterOfPlayerId：" + int.Parse(one.Value.monsterOfPlayerId));
                }
            }
            CurrentLocalIDList.Sort(Instance.IntCompare);
            MonsterOfPlayerListModel AccountCharacterInfoListObject = new MonsterOfPlayerListModel
            {
                monsterId = _accountCharacterInfo.monsterId
            };

            if (CurrentLocalIDList.Contains(int.Parse(_accountCharacterInfo.monsterOfPlayerId)))
            {
                Debug.Log("本地存档产生角色信息覆盖行为，宠物的monsterOfPlayerId" + _accountCharacterInfo.monsterOfPlayerId + "但追加仍然继续");
            }

            AccountCharacterInfoListObject.monsterOfPlayerId = _accountCharacterInfo.monsterOfPlayerId;
            if (!AccountCharacterInfoListObjectsDictionary.ContainsKey(AccountCharacterInfoListObject.monsterOfPlayerId))
            {
                AccountCharacterInfoListObjectsDictionary.Add(AccountCharacterInfoListObject.monsterOfPlayerId, AccountCharacterInfoListObject);
            }
            else
            {
                AccountCharacterInfoListObjectsDictionary[AccountCharacterInfoListObject.monsterOfPlayerId] = AccountCharacterInfoListObject;
            }

            if (!AccountCharacterInfoDictionary.ContainsKey(AccountCharacterInfoListObject.monsterOfPlayerId))
            {
                AccountCharacterInfoDictionary.Add(AccountCharacterInfoListObject.monsterOfPlayerId, _accountCharacterInfo);
            }
            else
            {
                AccountCharacterInfoDictionary[AccountCharacterInfoListObject.monsterOfPlayerId] = _accountCharacterInfo;
            }

            string json = JsonConvert.SerializeObject(_accountCharacterInfo);
            LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", AccountCharacterInfoListObject.monsterOfPlayerId + ".json", json);
            yield return _accountCharacterInfo;
        }

        public IEnumerator UpdateCharJsonSaveData(GetMonsterOfPlayerDetailModel _CharacterDataInfo)
        {
            IEnumerator getchar = Instance.GetAccountCharInfo(_CharacterDataInfo.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel before = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (before == null)
                yield break;
            string json = JsonConvert.SerializeObject(_CharacterDataInfo);
            LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", _CharacterDataInfo.monsterOfPlayerId + ".json", json);
            yield break;
        }

        public IEnumerator PlusExpForAccountCharLocalSaveData(string charlocalID, int plusExp)
        {
            IEnumerator getchar = Instance.GetAccountCharInfo(charlocalID);
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