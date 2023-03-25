namespace dataAccess
{
    public static partial class TeamSet
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

        public static MultiDic<int, int, UnitInfo> ToDic(PosKeySet posKeySet)
        {
            var teamMembers = new MultiDic<int, int, UnitInfo>();
            for (var i = 0; i < 3; i++)
            {
                if (posKeySet.GetInstanceIdOnPos(i) == null)
                {
                    continue;
                }
                var info = Units.Get(posKeySet.GetInstanceIdOnPos(i));
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
        
        public static PosKeySet DicToPosKeySet(MultiDic<int, int, UnitInfo> dic)
        {
            var posKeySet = new PosKeySet();
            foreach (var kv in dic.mDict)
            {
                posKeySet.SetPosMemInfoByInstanceID(kv.Key.Item2, kv.Value.id);
            }
            return posKeySet;
        }
    }
}
