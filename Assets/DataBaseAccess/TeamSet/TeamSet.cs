using System.Collections;
using Api.Dto.Model;
using Api.Dto.Form;
using UnityEngine;
using Api.Common;

// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static TeamSetGameMode targetTeamMode;
        public static PosKeySet Default = new PosKeySet();
        public static PosKeySet Arena3V3 = new PosKeySet();

        // 决定本模块将处理哪一组玩家队伍的编辑。竞技场还是arcade
        #region targetTeam
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
        #endregion

        public static IEnumerator LoadTeamSet(TeamSetGameMode teamSetGameMode)
        {
            GetMonsterTeamOfPlayerForm form = new GetMonsterTeamOfPlayerForm
            {
            };
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    form.teamType = "00";
                    break;
                case TeamSetGameMode.arena3V3:
                    form.teamType = "13";
                    break;
            }
            
            yield return Load(
                form,
                model => {
                        MonsterTeamOfPlayerModel MonsterTeamOfPlayerModel = model;
                        switch (form.teamType)
                        {
                            case "00":
                            Default = MonsterTeamOfPlayerModel.ToPosKeySet();
                            Debug.Log("quest模式阵型已经读取");
                            break;
                            case "13":
                            Arena3V3 = MonsterTeamOfPlayerModel.ToPosKeySet();
                            Debug.Log("竞技场3v3模式阵型已经读取");
                            break;
                            default:
                            Debug.Log("队伍阵型信息不明");
                            break;
                        }
                    },
                model => {
                    Debug.Log(teamSetGameMode+"阵容读取失败。");
                }
                , ApiLanguage.EnUs
            );
            yield break;
        }
        
        // 技能石升级
        static IEnumerator Load(GetMonsterTeamOfPlayerForm form, SuccessDelegate<MonsterTeamOfPlayerModel> success, FailDelegate<MonsterTeamOfPlayerModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = null;
                    switch (form.teamType)
                    {
                        case "00":
                            enumerator = LoadMyTeamSetInfoViaJsonFile("TeamSet.json");
                            break;
                        case "11":
                            break;
                        case "12":
                            break;
                        case "13":
                             enumerator = LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json");
                            break;
                        case "14":
                            break;
                        default:
                             Debug.Log("队伍阵型信息不明");
                             yield break;
                    }
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        success((MonsterTeamOfPlayerModel)enumerator.Current);
                    }else{
                        fail(new MonsterTeamOfPlayerModel());
                    }
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return TestGS2(
                        form,
                        success,
                        fail,
                        apiLanguage
                    );

                    //yield return ApiCaller.Instance.Post<MonsterTeamOfPlayerModel , GetMonsterTeamOfPlayerForm> 
                    //    ("http://160.16.187.230/AssetStoreFight/team/getMonsterTeamOfPlayer", form, ApiCaller.Instance.getHeader(apiLanguage), 
                    //    model => {
                    //        success(model.data);
                    //    },
                    //    model => {
                    //        fail(model.data);
                    //    }
                    //);
                    break;
                case PlayerInfoRefMode.formalVersion:
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
                MonsterOfPlayerDetailModel myfighter = AccountCharsSet.Get(PosKeySet.GetMonsterOfPlayerIdOnPos(i));
                if (myfighter != null)
                {
                    CharDataInfo CharDataInfo = MonsterOfPlayerDetailModel.GetCharDataInfo(myfighter);
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
