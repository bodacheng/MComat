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
        //读取技能石操作以后必然都是协程，因为这个是直接访问数据库。我们现在并没有用这个。
        public IEnumerator LoadMySkillStonesViaLocalJsonFile()
        {
            List<SkillStoneOfPlayerInfoModel> mySkillStones = ReturnMySkillStonesViaLocalFile().ToList();
            for (int i = 0; i < mySkillStones.Count;i++)
            {
                yield return Instance.GenerateOneStoneInfo(mySkillStones[i]);
            }
            yield break;
        }
        
        public SkillStoneOfPlayerInfoModel[] ReturnMySkillStonesViaLocalFile()
        {
            List<SkillStoneOfPlayerInfoModel> skillStonesForTest = new List<SkillStoneOfPlayerInfoModel>();
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
                        skillStonesForTest.Add(info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            return skillStonesForTest.ToArray();
        }
        
        //新
        public void OverrideMySkillStone(SkillStoneOfPlayerInfoModel stone)
        {
            try
            {
                string json = JsonConvert.SerializeObject(stone);
                LocalJson.SaveInfoToJsonFile_persistentDataPath("MyStones", stone.skillStoneOfPlayerId + ".json", json);
                return;
            }
            catch (Exception e)
            {
                Debug.Log("玩家技能石信息保存失败");
                Debug.Log(e.ToString());
                return;
            }
        }
        
        public IEnumerator LevelUpMySkillStone_LocalJson(string skillstoneofPlayerid, string targetLevel)
        {
            SkillStoneOfPlayerInfoModel st = GetStoneOfPlayerInfoModelByMyStoneId(skillstoneofPlayerid);
            st.level = targetLevel;
            yield return UpdateMySkillStone(skillstoneofPlayerid);
            yield return true;
        }
    }
}