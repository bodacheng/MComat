using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public static List<GetMonsterOfPlayerDetailModel> LoadAll_Json()
        {
            List<GetMonsterOfPlayerDetailModel> charList = new List<GetMonsterOfPlayerDetailModel>();
            try
            {
                GetMonsterOfPlayerDetailModel info;
                string filePath = Application.persistentDataPath + "/AccountCharacterInfos";
                if (Directory.Exists(filePath))
                {
                    foreach (string file in Directory.GetFiles(filePath))
                    {
                        string dataAsJson = File.ReadAllText(file);
                        info = JsonConvert.DeserializeObject<GetMonsterOfPlayerDetailModel>(dataAsJson);
                        charList.Add(info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
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
            DicAdd<string, GetMonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, _AccCharInfo.monsterOfPlayerId, _AccCharInfo);            
            string json = JsonConvert.SerializeObject(_AccCharInfo);
            LocalJson.SaveInfoToJsonFile_persistentDataPath("AccountCharacterInfos", _AccCharInfo.monsterOfPlayerId + ".json", json);
            yield return _AccCharInfo;
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
                
                List<string> INHERENTSKs = INHERENT_SkillTable.GetINHERENTSKIDList(_CharConfig.RECORD_ID);
                for (int index = 0; index < INHERENTSKs.Count; index++)
                {
                    Debug.Log("角色"+ _CharConfig.RECORD_ID + "的原生技能："+ INHERENTSKs[index]);
                    SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
                    {
                        skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
                        skillId = INHERENTSKs[index],
                        level = 1.ToString(),
                        Inherent = "true",
                        inUsingMonsterOfPlayerId = i.ToString(),
                        inUsingSkillSlot = (index + 1).ToString()
                    };
                    yield return MySkillStonesReader.Add(stoneInfo);
                }
                
                Debug.Log("将角色" + _CharConfig.REAL_NAME + "加入存档");
                yield return AddToAccount(_Char);
                i++;
            }
            //yield return MonsterBox.DisplayMonsterIcons();
            yield break;
        }
    }
}