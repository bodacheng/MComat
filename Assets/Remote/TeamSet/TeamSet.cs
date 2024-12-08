namespace dataAccess
{
    public static partial class TeamSet
    {
        public static PosKeySet Default = new PosKeySet();
        public static PosKeySet Origin = new PosKeySet();
        public static PosKeySet Arena3V3 = new PosKeySet();
        public static PosKeySet Gangbang = new PosKeySet();

        public static PosKeySet GetTargetSet(string mode)
        {
            switch(mode)
            {
                case "arcade":
                    return Default;
                case "arena":
                    return Arena3V3;
                case "gangbang":
                    return Gangbang;
                case "origin":
                    return Origin;
            }
            return null;
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
        
        public static bool Legal(string teamMode)
        {
            var qualified = true;
            var unitCount = 0;
            PosKeySet targetTeamSet = null;
            switch (teamMode)
            {
                case "arena":
                    targetTeamSet = TeamSet.Arena3V3;
                    break;
                case "arcade":
                    targetTeamSet = TeamSet.Default;
                    break;
                case "origin":
                    targetTeamSet = TeamSet.Origin;
                    break;
                case "gangbang":
                    targetTeamSet = TeamSet.Gangbang;
                    break;
            }
            
            var teamDic = targetTeamSet.LoadTeamDic();
            qualified = FightMembers.TeamLegal(teamDic);
            if (!qualified)
                return false;
            foreach (var kv in teamDic.mDict)
            {
                if (kv.Value.id != null && dataAccess.Units.Get(kv.Value.id) != null)
                {
                    qualified = qualified && (Stones.GetEquippingStones(kv.Value.id).Count == 9);
                    unitCount += 1;
                }
                else
                {
                    qualified = false;
                }
                if (!qualified)
                    break;
            }
            
            switch (teamMode)
            {
                case "arena":
                    qualified = qualified && unitCount == 3;
                    break;
                case "arcade":
                case "gangbang":
                case "origin":
                    qualified = qualified && unitCount > 0;
                    break;
            }
            return qualified;
        }
    }
}
