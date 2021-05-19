using System.Collections;
using UnityEngine;
using Api.Common;
using Api.Dto.Form;
using Newtonsoft.Json;
using Api.Dto.Model;
using Json;
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
        public static void SaveTeamSet(TeamSetGameMode teamSetGameMode)
        {
            TeamPos form = new TeamPos();
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    string B = Default.GetMonsterOfPlayerIdOnPos(0);
                    string L = Default.GetMonsterOfPlayerIdOnPos(1);
                    string F = Default.GetMonsterOfPlayerIdOnPos(2);
                    string R = Default.GetMonsterOfPlayerIdOnPos(3);
                    
                    form.b = (B != null && B.Length == 20) ? B : null;
                    form.l = (L != null && L.Length == 20) ? L : null;
                    form.f = (F != null && F.Length == 20) ? F : null;
                    form.r = (R != null && R.Length == 20) ? R : null;
                    break;
                case TeamSetGameMode.arena3V3:
                    form.b = Arena3V3.GetMonsterOfPlayerIdOnPos(0);
                    form.l = Arena3V3.GetMonsterOfPlayerIdOnPos(1);
                    form.f = Arena3V3.GetMonsterOfPlayerIdOnPos(2);
                    form.r = Arena3V3.GetMonsterOfPlayerIdOnPos(3);
                    break;
            }

            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    TeamPos value = OverrideTeamSetInfoOnJsonFile(teamSetGameMode);
                    switch (teamSetGameMode)
                    {
                        case TeamSetGameMode.story:
                            Default = value.ToPosKeySet();
                            break;
                        case TeamSetGameMode.arena3V3:
                            Arena3V3 = value.ToPosKeySet();
                            break;
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabClientAPI.UpdateUserData(
                        new UpdateUserDataRequest()
                        {
                            Data = new Dictionary<string, string>()
                            {
                                {"00", JsonConvert.SerializeObject(form) }
                            }
                        },
                        result => Debug.Log("Successfully updated user data"),
                        errorCallback => {
                            Debug.Log(errorCallback.Error);
                        }
                    );
                    break;
            }
        }    
        
        public static TeamPos OverrideTeamSetInfoOnJsonFile(TeamSetGameMode monsterTeamOfPlayerId)
        {
            string json;
            TeamPos model = null;
            switch (monsterTeamOfPlayerId)
            {
                case TeamSetGameMode.story:
                    model = Default.ToTeamPos();
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveToJsonFile_persistentDataPath(null, "TeamSet.json", json);
                break;
                case TeamSetGameMode.arena3V3:
                    model = Arena3V3.ToTeamPos();
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveToJsonFile_persistentDataPath(null, "arena3V3TeamSet.json", json);
                break;
            }
            return model;
        }
    }
}

//switch (teamSetGameMode.ToString())
//{
//    case "00":
//        Default = JsonConvert.DeserializeObject<TeamPos>(userDataRecord.Value).ToPosKeySet();
//        break;
//    case "11":
//        break;
//    case "12":
//        break;
//    case "13":
//        Arena3V3 = JsonConvert.DeserializeObject<TeamPos>(userDataRecord.Value).ToPosKeySet();
//        break;
//    case "14":
//        break;
//    default:
//        Debug.Log("队伍阵型信息不明");
//        break;
//}