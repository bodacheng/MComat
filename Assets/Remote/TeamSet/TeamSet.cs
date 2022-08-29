namespace dataAccess
{
    public static partial class TeamSet
    {
        public static PosKeySet Default = new ();
        public static PosKeySet Arena3V3 = new ();

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

        public static MultiDic<int, int, UnitInfo> ToDic(PosKeySet PosKeySet)
        {
            var teamMembers = new MultiDic<int, int, UnitInfo>();
            for (var i = 0; i < 3; i++)
            {
                if (PosKeySet.GetInstanceIdOnPos(i) == null)
                {
                    continue;
                }
                var info = Units.Get(PosKeySet.GetInstanceIdOnPos(i));
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
