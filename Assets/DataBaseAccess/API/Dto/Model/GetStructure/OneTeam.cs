using System.Collections.Generic;

namespace Api.Dto.Model
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
                CharDataInfo CharDataInfo = membersInfo[i].ToCharDataInfo();
                for (int y = 0; y < posKeySet.PosNumsWithLocalKeys.Length; y++)
                {
                    if (posKeySet.PosNumsWithLocalKeys[y].instanceID == CharDataInfo.monsterOfPlayerId)
                    {
                        LocalFight.EnemySets.Set(0, posKeySet.PosNumsWithLocalKeys[y].posNum, CharDataInfo);
                    }
                }
            }
            return LocalFight;
        }
    }
}