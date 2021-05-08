using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void Read(StoneOfPlayerInfoModel one)
        {
            DicAdd<string, StoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            GenerateStoneModelByAccID(one.skillStoneOfPlayerId);
        }

        static void ConvertListToDic(List<StoneOfPlayerInfoModel> list)
        {
            foreach (StoneOfPlayerInfoModel stoneinfo in list)
            {
                Read(stoneinfo);
            }
        }

        public static void LoadAllLocal()
        {
            Clear();
            List<StoneOfPlayerInfoModel>  list = LoadAll_Json(Application.persistentDataPath + "/MyStones");
            ConvertListToDic(list);
        }
    }
}