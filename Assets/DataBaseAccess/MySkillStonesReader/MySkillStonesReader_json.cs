using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Api.Dto.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Skill;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        public static void LoadAll_Json()
        {
            string filePath = Application.persistentDataPath + "/MyStones";
            if (Directory.Exists(filePath))
            {
                Debug.Log("正从以下路径获取技能石存档："+filePath);
                foreach (string file in Directory.GetFiles(filePath))
                {
                    try
                    {
                        //Debug.Log("处理技能石："+file);
                        string dataAsJson = File.ReadAllText(file);
                        SkillStoneOfPlayerInfoModel info = JsonConvert.DeserializeObject<SkillStoneOfPlayerInfoModel>(dataAsJson);
                        DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, info.skillStoneOfPlayerId, info);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.ToString());
                    }
                }
            }
        }
        
        //新
        public static void Update_Json(SkillStoneOfPlayerInfoModel stone)
        {
            try
            {
                string json = JsonConvert.SerializeObject(stone);
                LocalJson.SaveInfoToJsonFile_persistentDataPath("MyStones", stone.skillStoneOfPlayerId + ".json", json);
            }
            catch (Exception e)
            {
                Debug.Log("玩家技能石信息保存失败");
                Debug.Log(e.ToString());
            }
        }
        
        public static string GetNonRepeatID_LocalSave()
        {
            List<int> SKIdOfPlayerIntList = new List<int>();
            List<string> SKIdOfPlayerStringList = Dic.Keys.ToList();
            foreach (string SkOfPlayerID in SKIdOfPlayerStringList)
            {
                SKIdOfPlayerIntList.Add(int.Parse(SkOfPlayerID));
            }
            int max = SKIdOfPlayerIntList.Count == 0 ? 1 : SKIdOfPlayerIntList.Max();
            return (max + 1).ToString();
        }
        
        public static IEnumerator LocalSaveDataGetAllStones()
        {
            yield return LoadAll();
            //LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/MyStones");
            yield return SkillConfigTable.LoadAllSkillConfigs();
            foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.SkillConfigRefDic)
            {
                //Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
                SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
                {
                    skillStoneOfPlayerId = GetNonRepeatID_LocalSave(),
                    skillId = _pair.Value.RECORD_ID,
                    level = 1.ToString(),
                    Inherent = "false"
                };
                yield return Add(stoneInfo);
            }
        }
    }
}