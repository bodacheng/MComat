using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using System.Linq;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class TeamSet
    {
        public IEnumerator LoadTeamSetsRemote(TeamSetGameMode teamSetGameMode, ApiLanguage apiLanguage)
        {
            GetMonsterTeamOfPlayerForm form = new GetMonsterTeamOfPlayerForm
            {
                sessionId = AccountSet.Instance.sessionId
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
       
                yield return ApiCaller.Instance.Post<BaseModel<GetMonsterTeamOfPlayerModel> , GetMonsterTeamOfPlayerForm> 
                ("http://160.16.187.230/AssetStoreFight/team/getMonsterTeamOfPlayer", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     GetMonsterTeamOfPlayerModel _GetMonsterTeamOfPlayerModel = model.data;
                     foreach(MonsterTeamOfPlayerModel _monsterTeamOfPlayerModel  in _GetMonsterTeamOfPlayerModel.monsterTeamOfPlayerList)
                     {
                         PositionLocalCharKeySet positionLocalCharKeySet = new PositionLocalCharKeySet
                         {
                             recordId = _monsterTeamOfPlayerModel.monsterTeamOfPlayerId
                         };
                         positionLocalCharKeySet.SetPosMemInfoByLocalID(0, _monsterTeamOfPlayerModel.bMonsterOfPlayerId);
                         positionLocalCharKeySet.SetPosMemInfoByLocalID(1, _monsterTeamOfPlayerModel.lMonsterOfPlayerId);
                         positionLocalCharKeySet.SetPosMemInfoByLocalID(2, _monsterTeamOfPlayerModel.fMonsterOfPlayerId);
                         positionLocalCharKeySet.SetPosMemInfoByLocalID(3, _monsterTeamOfPlayerModel.rMonsterOfPlayerId);
                         switch (_monsterTeamOfPlayerModel.teamType)
                         {
                            case "00":
                                 storyModeTeamSet = positionLocalCharKeySet;
                                 Debug.Log("quest模式阵型已经读取");
                                break;
                            case "11":
                                break;
                            case "12":
                                break;
                            case "13":
                                 Arena3V3 = positionLocalCharKeySet;
                                 Debug.Log("竞技场3v3模式阵型已经读取");
                                break;
                            case "14":
                                break;
                            default:
                                 Debug.Log("队伍阵型信息不明");
                                 break;
                         }
                     }
                 }
                ,
                 model => {
                    Debug.Log(teamSetGameMode+"阵容读取失败。");
                 }
            );
            yield break;
        }
        
        public IEnumerator SaveTeamSetsRemote(TeamSetGameMode teamSetGameMode, ApiLanguage apiLanguage)
        {
            SetMonsterTeamOfPlayerForm form = new SetMonsterTeamOfPlayerForm
            {
                sessionId = AccountSet.Instance.sessionId
            };
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    form.monsterTeamOfPlayerId = this.storyModeTeamSet.recordId;
                    string B = this.storyModeTeamSet.GetPositionMonsterOfPlayerId(0);
                    string L = this.storyModeTeamSet.GetPositionMonsterOfPlayerId(1);
                    string F = this.storyModeTeamSet.GetPositionMonsterOfPlayerId(2);
                    string R = this.storyModeTeamSet.GetPositionMonsterOfPlayerId(3);

                    form.bMonsterOfPlayerId = (B != null && B.Length == 20) ? B : null;
                    form.lMonsterOfPlayerId = (L != null && L.Length == 20) ? L : null;
                    form.fMonsterOfPlayerId = (F != null && F.Length == 20) ? F : null;
                    form.rMonsterOfPlayerId = (R != null && R.Length == 20) ? R : null;
                    break;
                case TeamSetGameMode.arena3V3:
                    form.monsterTeamOfPlayerId = this.Arena3V3.recordId;
                    form.bMonsterOfPlayerId = this.Arena3V3.GetPositionMonsterOfPlayerId(0);
                    form.lMonsterOfPlayerId = this.Arena3V3.GetPositionMonsterOfPlayerId(1);
                    form.fMonsterOfPlayerId = this.Arena3V3.GetPositionMonsterOfPlayerId(2);
                    form.rMonsterOfPlayerId = this.Arena3V3.GetPositionMonsterOfPlayerId(3);
                    break;
            }
            yield return ApiCaller.Instance.Post<BaseModel<BaseVoidModel> , SetMonsterTeamOfPlayerForm> 
            ("http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", form, ApiCaller.Instance.getHeader(apiLanguage),
             model => {
                Debug.Log(teamSetGameMode+"阵容修改成功。");
             }
            ,
             model => {
                Debug.Log(teamSetGameMode+"阵容修改失败。");
             }
            );
            yield break;
        }
    }
}