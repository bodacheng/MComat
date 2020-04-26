using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {        
        public static void LoadAll_Json()
        {
            try
            {
                SkillStoneOfPlayerInfoModel info;
                string filePath = Application.persistentDataPath + "/MyStones";
                if (Directory.Exists(filePath))
                {
                    foreach (string file in Directory.GetFiles(filePath))
                    {
                        string dataAsJson = File.ReadAllText(file);
                        info = JsonConvert.DeserializeObject<SkillStoneOfPlayerInfoModel>(dataAsJson);
                        DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, info.skillStoneOfPlayerId, info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
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
    }
}