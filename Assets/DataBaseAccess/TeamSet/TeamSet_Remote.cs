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
        public IEnumerator loadTeamSetsRemote(TeamSetGameMode teamSetGameMode, ApiLanguage apiLanguage)
        {
            GetMonsterTeamOfPlayerForm form = new GetMonsterTeamOfPlayerForm();
            form.sessionId = AccountSet.Instance.sessionId;
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
                        positionLocalCharKeySet positionLocalCharKeySet = new positionLocalCharKeySet();
                        positionLocalCharKeySet.recordId = _monsterTeamOfPlayerModel.monsterTeamOfPlayerId;
                         positionLocalCharKeySet.setPosMemInfoByLocalID(PosNum.back, _monsterTeamOfPlayerModel.bMonsterOfPlayerId);
                         positionLocalCharKeySet.setPosMemInfoByLocalID(PosNum.left, _monsterTeamOfPlayerModel.lMonsterOfPlayerId);
                         positionLocalCharKeySet.setPosMemInfoByLocalID(PosNum.front, _monsterTeamOfPlayerModel.fMonsterOfPlayerId);
                         positionLocalCharKeySet.setPosMemInfoByLocalID(PosNum.right, _monsterTeamOfPlayerModel.rMonsterOfPlayerId);
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
        
        public IEnumerator saveTeamSetsRemote(TeamSetGameMode teamSetGameMode, ApiLanguage apiLanguage)
        {
            SetMonsterTeamOfPlayerForm form = new SetMonsterTeamOfPlayerForm();
            form.sessionId = AccountSet.Instance.sessionId;
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    form.monsterTeamOfPlayerId = this.storyModeTeamSet.recordId;
                    string B = this.storyModeTeamSet.getPositionMonsterOfPlayerId(PosNum.back);
                    string L = this.storyModeTeamSet.getPositionMonsterOfPlayerId(PosNum.left);
                    string F = this.storyModeTeamSet.getPositionMonsterOfPlayerId(PosNum.front);
                    string R = this.storyModeTeamSet.getPositionMonsterOfPlayerId(PosNum.right);

                    form.bMonsterOfPlayerId = (B != null && B.Length == 20) ? B : null;
                    form.fMonsterOfPlayerId = (F != null && F.Length == 20) ? F : null;
                    form.lMonsterOfPlayerId = (L != null && L.Length == 20) ? L : null;
                    form.rMonsterOfPlayerId = (R != null && R.Length == 20) ? R : null;
                    break;
                case TeamSetGameMode.arena3V3:
                    form.monsterTeamOfPlayerId = this.Arena3V3.recordId;
                    form.bMonsterOfPlayerId = this.Arena3V3.getPositionMonsterOfPlayerId(PosNum.back);
                    form.fMonsterOfPlayerId = this.Arena3V3.getPositionMonsterOfPlayerId(PosNum.front);
                    form.lMonsterOfPlayerId = this.Arena3V3.getPositionMonsterOfPlayerId(PosNum.left);
                    form.rMonsterOfPlayerId = this.Arena3V3.getPositionMonsterOfPlayerId(PosNum.right);
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