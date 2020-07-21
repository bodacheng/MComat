using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;
using System.Linq;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        static List<GetMonsterOfPlayerDetailModel> LoadAll_Json(string filePath)
        {
            List<GetMonsterOfPlayerDetailModel> charList = new List<GetMonsterOfPlayerDetailModel>();
            GetMonsterOfPlayerDetailModel info;
            if (Directory.Exists(filePath))
            {
                foreach (string file in Directory.GetFiles(filePath))
                {
                    try
                    {
                        string dataAsJson = File.ReadAllText(file);
                        info = JsonConvert.DeserializeObject<GetMonsterOfPlayerDetailModel>(dataAsJson);
                        charList.Add(info);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("尝试读取以下路径角色信息结果出错："+ file);
                        Debug.Log(e.ToString());
                    }
                }
            }
            return charList;
        }
        
        public static GetMonsterOfPlayerDetailModel LoadAccCharInfoViaJsonFile(string monsterlocalid)
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
        
        public static IEnumerator AddNewCharToJsonSaveData(GetMonsterOfPlayerDetailModel _AccCharInfo)
        {
            GetMonsterOfPlayerDetailModel returnValue = null;
            try
            {
                DicAdd<string, GetMonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, _AccCharInfo.monsterOfPlayerId, _AccCharInfo);            
                string json = JsonConvert.SerializeObject(_AccCharInfo);
                LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", _AccCharInfo.monsterOfPlayerId + ".json", json);
                returnValue = _AccCharInfo;
            }
            catch(Exception e)
            {
                Debug.Log(e);                
            }
            yield return returnValue;
        }
            
        public static IEnumerator UpdateCharJsonSaveData(GetMonsterOfPlayerDetailModel _CharInfo)
        {
            IEnumerator getchar = Load(_CharInfo.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel before = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (before == null)
            {
                Debug.Log("无法找到尝试更新的对象。monsterOfPlayerId："+ _CharInfo.monsterOfPlayerId);
                yield break;
            }
            string json = JsonConvert.SerializeObject(_CharInfo);
            LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", _CharInfo.monsterOfPlayerId + ".json", json);
            yield break;
        }
        
        public static IEnumerator LocalSaveDataGetAllCharacters()
        {
            LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/AccountCharacterInfos");
            List<CharConfig> charList = MonstersConfigTable.Instance.RowToCharacterResourceInfoList(MonstersConfigTable.Instance.rowList);
            int i = 0;
            foreach (CharConfig _CharConfig in charList)
            {
                GetMonsterOfPlayerDetailModel _Char = new GetMonsterOfPlayerDetailModel
                {
                    monsterId = _CharConfig.RECORD_ID,
                    monsterOfPlayerId = i.ToString()
                };
                
                IDictionary<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSKIDList(_CharConfig.RECORD_ID);
                if (INHERENTSkills != null && INHERENTSkills.Count > 0)
                {
                    List<string> dasfs = INHERENTSkills.Keys.ToList();
                    for (int index = 0; index < dasfs.Count; index++)
                    {
                        SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
                        {
                            skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
                            skillId = dasfs[index],
                            EXP = 0,
                            Inherent = "true",
                            inUsingMonsterOfPlayerId = i.ToString(),
                            inUsingSkillSlot = (index + 1).ToString()
                        };
                        yield return MySkillStonesReader.Add(stoneInfo);
                    }
                }
                
                Debug.Log("尝试将角色" + _CharConfig.REAL_NAME + "加入存档");
                yield return AddToAccount(_Char);
                i++;
            }
            //yield return MonsterBox.DisplayMonsterIcons();
            yield break;
        }
    }
}