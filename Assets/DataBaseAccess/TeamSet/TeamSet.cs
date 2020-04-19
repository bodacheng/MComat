using System.Collections;
using Api.Dto.Model;

//站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static TeamSet instance;
        public PosKeySet Default = new PosKeySet();
        public PosKeySet Arena3V3 = new PosKeySet();

        private TeamSet()
        {
        }
        public static TeamSet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TeamSet();
                }
                return instance;
            }
        }

        public IEnumerator LoadTeamSet(TeamSetGameMode teamSetGameMode)
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return TeamSet.Instance.LoadTeamSetsRemote(teamSetGameMode, ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    
                    break;
                case playerinfoReferenceMode.localTestSaveData:
                    switch (teamSetGameMode)
                    {
                        case TeamSetGameMode.story:
                            IEnumerator enumerator = TeamSet.Instance.LoadMyTeamSetInfoViaJsonFile("TeamSet.json");
                            yield return enumerator;
                            Default = (PosKeySet)enumerator.Current;
                            break;
                        case TeamSetGameMode.arena3V3:
                            IEnumerator enumerator1 = TeamSet.Instance.LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json");
                            yield return enumerator1;
                            Arena3V3 = (PosKeySet)enumerator1.Current;
                            break;
                    }
                    break;
            }
        }

        public IEnumerator SaveTeamSet(TeamSetGameMode teamSetGameMode)
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return Instance.SaveTeamSetsRemote(teamSetGameMode,ApiLanguage.JaJp);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
                case playerinfoReferenceMode.localTestSaveData:
                    yield return Instance.OverrideTeamSetInfoOnJsonFile(teamSetGameMode);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
                    break;
            }
            yield break;
        }
        
        // 下面的函数让阵容配置可以跳格。比方说一个游戏只能入场2人，那么现在在back和right位置有人，其他位置为空，也可顺利以此两人入场。
        public IEnumerator MyTeamByEntryLimit(int playerEntryNum, PosKeySet positionLocalCharKeySet)
        {
            MultiDictionary<int, int, CharDataInfo> teamMembers = new MultiDictionary<int, int, CharDataInfo>();
            int membercount = 0;
            for (int i = 0; i < 4; i++)
            {
                IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(positionLocalCharKeySet.GetPositionMonsterOfPlayerId(i));
                yield return getchar;
                GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
                if (myfighter != null)
                {
                    CharDataInfo characterDataInfo = RemoteAccess.GetCharDataInfo(myfighter);
                    teamMembers.Set(0,i,characterDataInfo);
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
    }
}
