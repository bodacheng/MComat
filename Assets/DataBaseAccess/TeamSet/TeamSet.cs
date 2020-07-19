using System.Collections;
using Api.Dto.Model;
using UnityEngine;

// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static TeamSetGameMode targetTeamMode;
        public static PosKeySet Default = new PosKeySet();
        public static PosKeySet Arena3V3 = new PosKeySet();
        
        // 决定本模块将处理哪一组玩家队伍的编辑。竞技场还是arcade
        public static void SwitchTargetTeam(TeamSetGameMode mode)
        {
            targetTeamMode = mode;
        }
        
        public static PosKeySet GetTargetSet()
        {
            switch(targetTeamMode)
            {
                case TeamSetGameMode.story:
                    return Default;
                case TeamSetGameMode.arena3V3:
                    return Arena3V3;
            }
            return null;
        }
        
        public static IEnumerator LoadTeamSet(TeamSetGameMode teamSetGameMode)
        {
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.remoteTestPlayer:
                    yield return LoadTeamSetsRemote(teamSetGameMode, ApiLanguage.JaJp);
                    break;
                case playerInfoRefMode.formalVersion:
                    
                    break;
                case playerInfoRefMode.localTestSaveData:
                    switch (teamSetGameMode)
                    {
                        case TeamSetGameMode.story:
                            IEnumerator enumerator = LoadMyTeamSetInfoViaJsonFile("TeamSet.json");
                            yield return enumerator;
                            Default = (PosKeySet)enumerator.Current;
                            break;
                        case TeamSetGameMode.arena3V3:
                            IEnumerator enumerator1 = LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json");
                            yield return enumerator1;
                            Arena3V3 = (PosKeySet)enumerator1.Current;
                            break;
                    }
                    break;
            }
            yield break;
        }
        
        public static IEnumerator SaveTeamSet(TeamSetGameMode teamSetGameMode)
        {
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.remoteTestPlayer:
                    yield return SaveTeamSetsRemote(teamSetGameMode,ApiLanguage.JaJp);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
                    break;
                case playerInfoRefMode.formalVersion:
                    break;
                case playerInfoRefMode.localTestSaveData:
                    yield return OverrideTeamSetInfoOnJsonFile(teamSetGameMode);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
                    break;
            }
            yield break;
        }
        
        // 下面的函数让阵容配置可以跳格。比方说一个游戏只能入场2人，那么现在在back和right位置有人，其他位置为空，也可顺利以此两人入场。
        public static IEnumerator MyTeamByEntryLimit(int playerEntryNum, PosKeySet PosKeySet)
        {
            MultiDictionary<int, int, CharDataInfo> teamMembers = new MultiDictionary<int, int, CharDataInfo>();
            int membercount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (PosKeySet.GetMonsterOfPlayerIdOnPos(i) == null)
                {
                    continue;
                }
                GetMonsterOfPlayerDetailModel myfighter = AccountCharsSet.Get(PosKeySet.GetMonsterOfPlayerIdOnPos(i));
                if (myfighter != null)
                {
                    CharDataInfo CharDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(myfighter);
                    teamMembers.Set(0, i, CharDataInfo);
                    membercount += 1;
                    if (membercount == playerEntryNum)
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            yield return teamMembers;
        }
    }
    
    public enum TeamSetGameMode
    {
        story = 1,
        arena3V3 = 2,
        SelfFight = 3
    }
}
