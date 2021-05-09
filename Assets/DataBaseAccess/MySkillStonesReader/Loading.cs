using System.Collections.Generic;
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
    }
}