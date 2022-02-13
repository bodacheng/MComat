
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
            var teamMembers = new MultiDict<int, int, UnitInfo>();
            for (var i = 0; i < 3; i++)
            {
                if (PosKeySet.GetInstanceIdOnPos(i) == null)
                {
                    continue;
                }
                var info = MyMonsters.Get(PosKeySet.GetInstanceIdOnPos(i));
                if (info != null)
                {
                    var unitInfo = UnitInfo.GetUnitInfo(info);
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
