using System.Collections.Generic;

namespace dataAccess
{
    public class OneTeam
    {
        readonly string playerID;
        readonly List<MemberOfTeam> membersInfo;
        readonly TeamPos teamSetInfo;
        
        public FightMembers ToFightInfo()
        {
            FightMembers LocalFight = new FightMembers();
            PosKeySet posKeySet = teamSetInfo.ToPosKeySet();
            for (int i = 0; i < membersInfo.Count; i++)
            {
                global::UnitInfo unitInfo = membersInfo[i].ToCharDataInfo();
                for (int y = 0; y < posKeySet.PosNumsWithLocalKeys.Length; y++)
                {
                    if (posKeySet.PosNumsWithLocalKeys[y].instanceID == unitInfo.id)
                    {
                        LocalFight.EnemySets.Set(0, posKeySet.PosNumsWithLocalKeys[y].posNum, unitInfo);
                    }
                }
            }
            return LocalFight;
        }
    }
}