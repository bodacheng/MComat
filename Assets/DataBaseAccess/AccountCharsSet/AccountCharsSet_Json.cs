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
        public static IEnumerator LoadAll_Json(string filePath)
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
            yield return charList;
        }
        
        public static IEnumerator LoadAccCharInfoViaJsonFile(string monsterlocalid)
        {
            GetMonsterOfPlayerDetailModel info = null;
            string wholepath = Application.persistentDataPath + "/AccountCharacterInfos/" + monsterlocalid + ".json";
            if (File.Exists(wholepath))
            {
                string dataAsJson = File.ReadAllText(wholepath);
                info = JsonConvert.DeserializeObject<GetMonsterOfPlayerDetailModel>(dataAsJson);
            }
            yield return info;
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
                
                KeyValuePair<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(_CharConfig.RECORD_ID);
                if (INHERENTSkills.Key != null)
                {
                    SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
                    {
                        skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
                        skillId = INHERENTSkills.Key,
                        EXP = 0,
                        BreakThrough = 0,
                        Inherent = "true",
                        inUsingMonsterOfPlayerId = i.ToString(),
                        inUsingSkillSlot = "1"
                    };
                    yield return MySkillStonesReader.Add(stoneInfo);
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