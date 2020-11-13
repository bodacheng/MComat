using System.Collections.Generic;

namespace Api.Dto.Model
{
    public class OneTeam
    {
        readonly string playerID;
        readonly List<MemberOfTeam> membersInfo;
        readonly MonsterTeamOfPlayerModel teamSetInfo;
        
        public LocalFight ToFightInfo()
        {
            LocalFight LocalFight = new LocalFight();
            PosKeySet posKeySet = teamSetInfo.ToPosKeySet();
            for (int i = 0; i < membersInfo.Count; i++)
            {
                CharDataInfo CharDataInfo = membersInfo[i].ToCharDataInfo();
                for (int y = 0; y < posKeySet.PosNumsWithLocalKeys.Length; y++)
                {
                    if (posKeySet.PosNumsWithLocalKeys[y].monsterOfPlayerId == CharDataInfo.monsterOfPlayerId)
                    {
                        LocalFight.EnemySets.Set(0, posKeySet.PosNumsWithLocalKeys[y].posNum, CharDataInfo);
                    }
                }
            }
            return LocalFight;
        }
    }
}