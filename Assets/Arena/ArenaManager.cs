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
        //PreScene.target.mainProcessRunner.RunAsQueued(target.LoadArena());
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
    public void LoadArena(List<LeaderboardInfo> leaderboards)
    {
        myTeam.ShowMyTeam();
        for (int i = 0; i < leaderboards.Count; i++)
        {
            switch (i)
            {
                case 0:
                    Fight1.AddFightToList(leaderboards[i]);
                    break;
                case 1:
                    Fight2.AddFightToList(leaderboards[i]);
                    break;
                case 2:
                    Fight3.AddFightToList(leaderboards[i]);
                    break;
                case 3:
                    Fight4.AddFightToList(leaderboards[i]);
                    break;
            }
        }
    }
}