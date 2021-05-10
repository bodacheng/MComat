using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Api.Dto.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Skill;
using Json;
using PlayFab;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void LoadAllLocal()
        {
            Clear();
            List<StoneOfPlayerInfoModel> list = LoadAll_Json(Application.persistentDataPath + "/MyStones");
            ConvertListToDic(list);
        }

        static List<StoneOfPlayerInfoModel> LoadAll_Json(string filePath)
        {
            if (Directory.Exists(filePath))
            {
                Debug.Log("正从以下路径获取技能石存档："+filePath);
                List<StoneOfPlayerInfoModel> list = new List<StoneOfPlayerInfoModel>();
                foreach (string file in Directory.GetFiles(filePath))
                {
                    try
                    {
                        string dataAsJson = File.ReadAllText(file);
                        StoneOfPlayerInfoModel info = JsonConvert.DeserializeObject<StoneOfPlayerInfoModel>(dataAsJson);
                        list.Add(info);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("尝试读取以下路径技能石结果出错："+ file);
                        Debug.Log(e.ToString());
                    }
                }
                return list;
            }
            return new List<StoneOfPlayerInfoModel>();
        }
        
        //新
        public static void Update_Json(StoneOfPlayerInfoModel stone)
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
            //LoadAMySkillstones();
            //LocalJson.DeleteAllUnderFolder(Application.persistentDataPath + "/MyStones");
            SkillConfigTable.LoadAllSkillConfigs();
            foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.SkillConfigRefDic)
            {
                //Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
                StoneOfPlayerInfoModel stoneInfo = new StoneOfPlayerInfoModel
                {
                    skillStoneOfPlayerId = GetNonRepeatID_LocalSave(),
                    skillId = _pair.Value.RECORD_ID,
                    BreakThrough = 0,
                    EXP = 0,
                    Inherent = "false"
                };
                yield return Add(stoneInfo);
            }
            PurchaseAllStones(SkillConfigTable.SkillConfigRefDic.Values.ToList(), 0);
        }

        public static void PurchaseAllStones(List<SkillConfig> stones, int i)
        {
            if (i != stones.Count - 1)
            {
                SkillConfig targetStoneConfig = stones[i];
                PlayFabClientAPI.PurchaseItem(
                    new PlayFab.ClientModels.PurchaseItemRequest()
                    {
                        CatalogVersion = "stoneTest2",
                        ItemId = targetStoneConfig.RECORD_ID,
                        StoreId = "stone",
                        VirtualCurrency = "GD",
                        Price = 0
                    }, result =>
                    {
                        Debug.Log("成功购买" + targetStoneConfig.RECORD_ID);
                        PurchaseAllStones(stones, i + 1);
                    }, error =>
                    {
                        Debug.Log(error.GenerateErrorReport());
                    }
                );
            }
        }
    }
}