using System.Collections;
using Api.Dto.Model;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using mainMenu;
using dataAccess;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager target;
    
    public RectTransform ArenaCanvas;
    public ArenaFightTeamDisplay myTeam; // 玩家队伍显示
    public ArenaFightTeamDisplay Fight1, Fight2, Fight3, Fight4; // 挑战玩家队伍显示
    
    void Awake()
    {
        target = this;
    }
    
    public void RefreshOpponent()
    {
        PreScene.target.mainProcessRunner.RunAsQueued(target.LoadArena());
    }

    public static void SaveDefend(MultiDict<int, int, CharDataInfo> myteam, Action<int> finished)
    {
        switch (Account.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                PlayFabClientAPI.UpdateUserData(
                    new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            {"defendTeam", JsonConvert.SerializeObject(myteam._SerializableSets) }
                        }
                    },
                    result => Debug.Log("Successfully Saved DefendTeam"),
                    errorCallback => {
                        Debug.Log(errorCallback.Error);
                    }
                );
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
        }
    }

    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    public IEnumerator LoadArena()
    {
        myTeam.ShowMyTeam();
        Debug.Log("asdafsdfs");
        yield break;
        Arena.GetPlayerRankInfo();

        PlayerArenaRankInfo opponent1Info = Arena.rankOpponentsModel.strongTeam;
        PlayerArenaRankInfo opponent2Info = Arena.rankOpponentsModel.normalTeam1;
        PlayerArenaRankInfo opponent3Info = Arena.rankOpponentsModel.normalTeam2;
        PlayerArenaRankInfo opponent4Info = Arena.rankOpponentsModel.weakTeam;
        
        //IEnumerator temp(ArenaFightTeamDisplay target, PlayerArenaRankInfo opponentInfo)
        //{
        //    if (opponent1Info.isRealPlayer)
        //    {
        //        IEnumerator opponentteam = Arena.GetOpponentTeamInfo(opponentInfo.playerID);
        //        yield return opponentteam;
        //        OneTeam oneTeam = (OneTeam)opponentteam.Current;
        //        yield return target.AddFightToList(FightInfo.ArenaStage(oneTeam.ToFightInfo()));
        //    }else{
        //        yield return target.AddFightToList(FightInfo.RandomStage());
        //    }
        //}

        //yield return temp(Fight1, opponent1Info);
        //yield return temp(Fight2, opponent2Info);
        //yield return temp(Fight3, opponent3Info);
        //yield return temp(Fight4, opponent4Info);
    }
}