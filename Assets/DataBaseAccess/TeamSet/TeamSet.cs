using System.Collections;
using Api.Dto.Model;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

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
        
        // 技能石升级
        public static void LoadTeamSet(TeamSetGameMode teamSetGameMode, Action<int> finished)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    switch (teamSetGameMode.ToString())
                    {
                        case "00":
                            Default = LoadMyTeamSetInfoViaJsonFile("TeamSet.json").ToPosKeySet();
                            break;
                        case "11":
                            break;
                        case "12":
                            break;
                        case "13":
                            Arena3V3 = LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json").ToPosKeySet();
                            break;
                        case "14":
                            break;
                        default:
                             Debug.Log("队伍阵型信息不明");
                            break;
                    }
                    finished.Invoke(1);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabClientAPI.GetUserData
                    (
                        new GetUserDataRequest() {
                            PlayFabId = AccountSet._AccInfo.PlayerName,
                            Keys = new List<string>() { teamSetGameMode.ToString() }
                        },
                        (GetUserDataResult obj) => {
                            UserDataRecord userDataRecord = obj.Data[teamSetGameMode.ToString()];
                            switch (teamSetGameMode.ToString())
                            {
                                case "00":
                                    Default = JsonConvert.DeserializeObject<TeamPos>(userDataRecord.Value).ToPosKeySet();
                                    break;
                                case "11":
                                    break;
                                case "12":
                                    break;
                                case "13":
                                    Arena3V3 = JsonConvert.DeserializeObject<TeamPos>(userDataRecord.Value).ToPosKeySet();
                                    break;
                                case "14":
                                    break;
                                default:
                                    Debug.Log("队伍阵型信息不明");
                                    break;
                            }
                            finished.Invoke(1);
                        },
                        errorCallback => {
                            finished.Invoke(-1);
                        }
                    );
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
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
                MonsterOfPlayerInfo myfighter = MyMonsters.Get(PosKeySet.GetMonsterOfPlayerIdOnPos(i));
                if (myfighter != null)
                {
                    CharDataInfo CharDataInfo = MonsterOfPlayerInfo.GetCharDataInfo(myfighter);
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
