using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class PlayFabReadClient
{
    static void AddUserNameAndPw(string PlayFabUsername)
    {
        var guidValue = Guid.NewGuid();
        Debug.Log(guidValue.ToString());
        PlayFabClientAPI.AddUsernamePassword(new PlayFab.ClientModels.AddUsernamePasswordRequest
            {
                Username = PlayFabUsername,
                Email = "xxx@xxx.com",
                Password = guidValue.ToString()
            }, addUsernamePasswordResult =>
            {
                Debug.Log("我们把玩家的PlayFab username设置成了他的PlayFabId:" + addUsernamePasswordResult.Username);
            }, 
            (x) =>
            {
                Debug.Log(x.Error);
            }
        );
    }
}
