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

// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static void SaveTeamSet(string Mode)
        {
            TeamPos form = new TeamPos();
            switch (Mode)
            {
                case "arcade":
                    string F = Default.GetMonsterOfPlayerIdOnPos(0);
                    string L = Default.GetMonsterOfPlayerIdOnPos(1);
                    string R = Default.GetMonsterOfPlayerIdOnPos(2);
                    
                    form.l = (L != null) ? L : null;
                    form.f = (F != null) ? F : null;
                    form.r = (R != null) ? R : null;
                    break;
                case "arena":
                    form.f = Arena3V3.GetMonsterOfPlayerIdOnPos(0);
                    form.l = Arena3V3.GetMonsterOfPlayerIdOnPos(1);
                    form.r = Arena3V3.GetMonsterOfPlayerIdOnPos(2);
                    break;
            }

            switch (Account.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    TeamPos value = OverrideTeamSetInfoOnJsonFile(Mode);
                    switch (Mode)
                    {
                        case "arcade":
                            Default = value.ToPosKeySet();
                            break;
                        case "arena":
                            Arena3V3 = value.ToPosKeySet();
                            break;
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    string targetModeCode = "";
                    switch (Mode)
                    {
                        case "arcade":
                            targetModeCode = "arcade";
                            break;
                        case "arena":
                            targetModeCode = "arena";
                            break;
                    }
                    PlayFabClientAPI.UpdateUserData(
                        new UpdateUserDataRequest()
                        {
                            Data = new Dictionary<string, string>()
                            {
                                {targetModeCode, JsonConvert.SerializeObject(form) }
                            }
                        },
                        result => Debug.Log("Successfully updated Team Data of :"+ Mode),
                        errorCallback => {
                            Debug.Log(errorCallback.Error);
                        }
                    );
                    break;
            }
        }
    }
}