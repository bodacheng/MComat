using UnityEngine;
using System.IO;
using System;
using mainMenu;
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
                yield return SkillStonesBox.Instance.GenerateOneStone(mySkillStones[i]);
            }
            yield break;
        }

        public SkillStoneOfPlayerInfoModel[] ReturnMySkillStonesViaLocalFile()
        {
            FileStream FileStream = null;
            SkillStoneOfPlayerInfoModel[] info = { };
            try
            {
                info = new SkillStoneOfPlayerInfoModel[] { };
                string wholepath = Application.persistentDataPath + "/MySkillStones.json";
                if (File.Exists(wholepath))
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    info = JsonConvert.DeserializeObject<SkillStoneOfPlayerInfoModel[]>(dataAsJson);
                    Debug.Log("玩家技能石信息读取成功");
                }
            }
            catch (Exception e)
            {
                Debug.Log("玩家拥有技能石信息读取失败，建立新技能石头本地测试文件");
                Debug.Log(e.ToString());
                List<SkillStoneOfPlayerInfoModel> skillStonesForTest = new List<SkillStoneOfPlayerInfoModel>();
                int i = 1;
                foreach (KeyValuePair<string, SkillConfig> _keyValuePair in SkillConfigTable.Instance.SkillConfigDicForReference)
                {
                    SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = new SkillStoneOfPlayerInfoModel
                    {
                        skillStoneOfPlayerId = String.Format("{0:D20}", i),
                        skillId = _keyValuePair.Value.RECORD_ID
                    };
                    skillStonesForTest.Add(skillStoneOfPlayerInfoModel);
                    i++;
                }
                if (FileStream != null)
                    FileStream.Close();
                return info;
            }
            return info;
        }

        public void OverrideMySkillStoneInfosOnLocalFile(List<SkillStoneOfPlayerInfoModel> stones)
        {
            try
            {
                string json = JsonConvert.SerializeObject(stones.ToArray());
                LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "MySkillStones.json", json);
                return;
            }
            catch (Exception e)
            {
                Debug.Log("玩家技能石信息保存失败");
                Debug.Log(e.ToString());
                return;
            }
        }
    }
}