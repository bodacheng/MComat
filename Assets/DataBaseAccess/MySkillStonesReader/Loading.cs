using System.Collections.Generic;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void Read(StoneOfPlayerInfo one)
        {
            DicAdd<string, StoneOfPlayerInfo>.Add(Dic, one.InstanceId, one);
            GenerateStoneModelByAccID(one.InstanceId);
        }

        static void ConvertListToDic(List<StoneOfPlayerInfo> list)
        {
            foreach (StoneOfPlayerInfo stoneinfo in list)
            {
                Read(stoneinfo);
            }
        }
    }
}