using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    // 以下所有内容按说都是服务端的事情
    public class StoneExpManager
    {
        #region 转换比率
        public static float goldToExp;
        #endregion
        
        #region 转换比率
        public static int SkillStoneRankToExp_rank1 = 100;
        public static int SkillStoneRankToExp_rank2 = 200;
        public static int SkillStoneRankToExp_rank3 = 300;
        public static int SkillStoneRankToExp_rank4 = 400;
        public static int SkillStoneRankToExp_rank5 = 500;
        #endregion
        
        #region 智慧果实与经验值转换关系 可能改变位置
        public static int GoldToExp(int gold)
        {
            return (int)(gold * goldToExp);
        }
        
        public static int ExpToGold(int Exp)
        {
            return Exp * (int)(1f / goldToExp);
        }
        #endregion
        
        // 技能石转化为智慧果实数量评价
        public static int ConvertSKStoneToExp(string stoneID)
        {
            int point = 0;
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStones.Get(stoneID);
            point += skillStoneOfPlayerInfoModel.EXP;

            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillStoneOfPlayerInfoModel.skillId);
            switch (skillConfig.RARITY_LEVEL)
            {
                case 1:
                    point += SkillStoneRankToExp_rank1;
                    break;
                case 2:
                    point += SkillStoneRankToExp_rank2;
                    break;
                case 3:
                    point += SkillStoneRankToExp_rank3;
                    break;
                case 4:
                    point += SkillStoneRankToExp_rank4;
                    break;
                case 5:
                    point += SkillStoneRankToExp_rank5;
                    break;
                default:
                    point += SkillStoneRankToExp_rank1;
                    break;
            }
            return point;
        }
    }
}