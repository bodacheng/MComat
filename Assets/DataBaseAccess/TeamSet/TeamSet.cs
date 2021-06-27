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

        public static void LoadTeamSet(string Mode, Action<int> finished)
        {
            switch (Account.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    switch (Mode)
                    {
                        case "arcade":
                            Default = LoadMyTeamSetInfoViaJsonFile("TeamSet.json").ToPosKeySet();
                            break;
                        case "arena":
                            Arena3V3 = LoadMyTeamSetInfoViaJsonFile("arena3V3TeamSet.json").ToPosKeySet();
                            break;
                        default:
                             Debug.Log("队伍阵型信息不明");
                            break;
                    }
                    finished.Invoke(1);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    string targetModeCode = Mode;
                    PlayFabClientAPI.GetUserData(
                        new GetUserDataRequest() {
                            PlayFabId = Account._AccInfo.playerID,
                            Keys = new List<string>() { targetModeCode }
                        },
                        (GetUserDataResult obj) => {
                            if (obj.Data.ContainsKey(targetModeCode))
                            {
                                UserDataRecord userData = obj.Data[targetModeCode];
                                switch (Mode)
                                {
                                    case "arcade":
                                        Default = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                                        break;
                                    case "arena":
                                        Arena3V3 = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                                        break;
                                    default:
                                        Debug.Log("队伍阵型信息不明");
                                        break;
                                }
                            }
                            else
                            {
                                switch (Mode)
                                {
                                    case "arcade":
                                        Default = new PosKeySet();
                                        break;
                                    case "arena":
                                        Arena3V3 = new PosKeySet();
                                        break;
                                    default:
                                        Debug.Log("队伍阵型信息不明");
                                        break;
                                }
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
        
        public static MultiDict<int, int, CharDataInfo> ToDic(PosKeySet PosKeySet)
        {
            MultiDict<int, int, CharDataInfo> teamMembers = new MultiDict<int, int, CharDataInfo>();
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
}
