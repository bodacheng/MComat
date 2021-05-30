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
        
        public static void LoadTeamSet(TeamSetGameMode Mode, Action<int> finished)
        {
            switch (Account.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    switch (Mode)
                    {
                        case TeamSetGameMode.story:
                            Default = LoadMyTeamSetInfoViaJsonFile("TeamSet.json").ToPosKeySet();
                            break;
                        case TeamSetGameMode.arena3V3:
                            Arena3V3 = LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json").ToPosKeySet();
                            break;
                        default:
                             Debug.Log("队伍阵型信息不明");
                            break;
                    }
                    finished.Invoke(1);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    string targetModeCode = "";
                    switch (Mode)
                    {
                        case TeamSetGameMode.story:
                            targetModeCode = "00";
                            break;
                        case TeamSetGameMode.arena3V3:
                            targetModeCode = "11";
                            break;
                    }
                    Debug.Log(Mode);
                    PlayFabClientAPI.GetUserData(
                        new GetUserDataRequest() {
                            PlayFabId = Account._AccInfo.playerID,
                            Keys = new List<string>() { targetModeCode }
                        },
                        (GetUserDataResult obj) => {
                            Debug.Log(obj.Data[targetModeCode]);
                            UserDataRecord userData = obj.Data[targetModeCode];
                            switch (Mode)
                            {
                                case TeamSetGameMode.story:
                                    Default = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                                    break;
                                case TeamSetGameMode.arena3V3:
                                    Arena3V3 = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                                    break;
                                default:
                                    Debug.Log("队伍阵型信息不明");
                                    break;
                            }
                            finished.Invoke(1);
                        },
                        errorCallback => {
                            Debug.Log(errorCallback);
                            finished.Invoke(-1);
                        }
                    );
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
        }
        
        public static MultiDictionary<int, int, CharDataInfo> ToDic(PosKeySet PosKeySet)
        {
            MultiDictionary<int, int, CharDataInfo> teamMembers = new MultiDictionary<int, int, CharDataInfo>();
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
                }
                else
                {
                    continue;
                }
            }
            return teamMembers;
        }
    }
    
    public enum TeamSetGameMode
    {
        story = 1,
        arena3V3 = 2,
        SelfFight = 3
    }
}
