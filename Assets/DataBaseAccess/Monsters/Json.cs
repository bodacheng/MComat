using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;
using Json;

namespace dataAccess
{
    public partial class MyMonsters
    {
        public static List<MonsterOfPlayerInfo> LoadAll_Json(string filePath)
        {
            List<MonsterOfPlayerInfo> charList = new List<MonsterOfPlayerInfo>();
            MonsterOfPlayerInfo info;
            if (Directory.Exists(filePath))
            {
                foreach (string file in Directory.GetFiles(filePath))
                {
                    try
                    {
                        string dataAsJson = File.ReadAllText(file);
                        info = JsonConvert.DeserializeObject<MonsterOfPlayerInfo>(dataAsJson);
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
        
        public static MonsterOfPlayerInfo LoadAccCharInfoViaJsonFile(string monsterlocalid)
        {
            MonsterOfPlayerInfo info = null;
            string wholepath = Application.persistentDataPath + "/AccountCharacterInfos/" + monsterlocalid + ".json";
            if (File.Exists(wholepath))
            {
                string dataAsJson = File.ReadAllText(wholepath);
                info = JsonConvert.DeserializeObject<MonsterOfPlayerInfo>(dataAsJson);
            }
            return info;
        }
        
        public static IEnumerator AddNewCharToJsonSaveData(MonsterOfPlayerInfo _AccCharInfo)
        {
            MonsterOfPlayerInfo returnValue = null;
            try
            {
                DicAdd<string, MonsterOfPlayerInfo>.Add(Dic, _AccCharInfo.InstanceId, _AccCharInfo);            
                string json = JsonConvert.SerializeObject(_AccCharInfo);
                LocalJson.SaveToJsonFile_persistentDataPath("AccountCharacterInfos", _AccCharInfo.InstanceId + ".json", json);
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
            List<CharConfig> charList = MonstersConfigTable.RowToConfigList(MonstersConfigTable.rowList);
            int i = 0;
            foreach (CharConfig _CharConfig in charList)
            {
                MonsterOfPlayerInfo _Char = new MonsterOfPlayerInfo
                {
                    monsterId = _CharConfig.RECORD_ID,
                    InstanceId = i.ToString()
                };
                
                KeyValuePair<string, string> INHERENTSkills = INHERENT_SkillTable.GetINHERENTSkill(_CharConfig.RECORD_ID);
                if (INHERENTSkills.Key != null)
                {
                    StoneOfPlayerInfo stoneInfo = new StoneOfPlayerInfo
                    {
                        InstanceId = MySkillStones.GetNonRepeatID_LocalSave(),
                        skillId = INHERENTSkills.Key,
                        EXP = 0,
                        BreakThrough = 0,
                        Inherent = "true",
                        inUsingMonsterOfPlayerId = i.ToString(),
                        inUsingSkillSlot = "1"
                    };
                    MySkillStones.Add(stoneInfo);
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