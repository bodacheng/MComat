using System.Collections;
using UnityEngine;
using Api.Common;
using Api.Dto.Form;
using Newtonsoft.Json;
using Api.Dto.Model;

// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static IEnumerator SaveTeamSet(TeamSetGameMode teamSetGameMode)
        {
            SetMonsterTeamOfPlayerForm form = new SetMonsterTeamOfPlayerForm();
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    form.teamType = "00";
                    form.monsterTeamOfPlayerId = Default.recordId;
                    string B = Default.GetMonsterOfPlayerIdOnPos(0);
                    string L = Default.GetMonsterOfPlayerIdOnPos(1);
                    string F = Default.GetMonsterOfPlayerIdOnPos(2);
                    string R = Default.GetMonsterOfPlayerIdOnPos(3);
                    
                    form.bMonsterOfPlayerId = (B != null && B.Length == 20) ? B : null;
                    form.lMonsterOfPlayerId = (L != null && L.Length == 20) ? L : null;
                    form.fMonsterOfPlayerId = (F != null && F.Length == 20) ? F : null;
                    form.rMonsterOfPlayerId = (R != null && R.Length == 20) ? R : null;
                    break;
                case TeamSetGameMode.arena3V3:
                    form.teamType = "13";
                    form.monsterTeamOfPlayerId = Arena3V3.recordId;
                    form.bMonsterOfPlayerId = Arena3V3.GetMonsterOfPlayerIdOnPos(0);
                    form.lMonsterOfPlayerId = Arena3V3.GetMonsterOfPlayerIdOnPos(1);
                    form.fMonsterOfPlayerId = Arena3V3.GetMonsterOfPlayerIdOnPos(2);
                    form.rMonsterOfPlayerId = Arena3V3.GetMonsterOfPlayerIdOnPos(3);
                    break;
            }
            yield return Save(
                form,
                model => {
                    Debug.Log(teamSetGameMode+"阵容保存成功。");
                },
                model => {
                    Debug.Log(teamSetGameMode+"阵容保存失败。");
                }
                , ApiLanguage.EnUs);
        }
        
        public static IEnumerator Save(SetMonsterTeamOfPlayerForm form, SuccessDelegate<MonsterTeamOfPlayerModel> success, FailDelegate<MonsterTeamOfPlayerModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = OverrideTeamSetInfoOnJsonFile(form.teamType);
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        success((MonsterTeamOfPlayerModel)enumerator.Current);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<MonsterTeamOfPlayerModel, SetMonsterTeamOfPlayerForm> 
                        ("http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", form, ApiCaller.Instance.getHeader(apiLanguage),
                            model => {
                                success(model.data);
                            },
                            model => {
                                fail(model.data);
                            }
                        );
                    break;
            }
        }
        
        public static IEnumerator OverrideTeamSetInfoOnJsonFile(string monsterTeamOfPlayerId)
        {
            string json;
            MonsterTeamOfPlayerModel model = null;
            switch (monsterTeamOfPlayerId)
            {
                case "00":
                    model = Default.ToMonsterTeamOfPlayerModel(monsterTeamOfPlayerId);
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "TeamSet.json", json);
                break;
                case "13":
                    model = Arena3V3.ToMonsterTeamOfPlayerModel(monsterTeamOfPlayerId);
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "arena3V3TeamSet.json", json);
                break;
            }
            yield return model;
        }
    }
}