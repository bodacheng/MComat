
// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static PosKeySet Default = new PosKeySet();
        public static PosKeySet Arena3V3 = new PosKeySet();

        public static PosKeySet GetTargetSet(string mode)
        {
            switch(mode)
            {
                case "arcade":
                    return Default;
                case "arena":
                    return Arena3V3;
            }
            return null;
        }

        public static MultiDict<int, int, UnitInfo> ToDic(PosKeySet PosKeySet)
        {
            MultiDict<int, int, UnitInfo> teamMembers = new MultiDict<int, int, UnitInfo>();
            for (int i = 0; i < 3; i++)
            {
                if (PosKeySet.GetMonsterOfPlayerIdOnPos(i) == null)
                {
                    continue;
                }
                UnitInfo myfighter = MyMonsters.Get(PosKeySet.GetMonsterOfPlayerIdOnPos(i));
                if (myfighter != null)
                {
                    UnitInfo unitInfo = UnitInfo.GetCharDataInfo(myfighter);
                    teamMembers.Set(0, i, unitInfo);
                }
                else
                {
                    continue;
                }
            }
            return teamMembers;
        }
    }
}
