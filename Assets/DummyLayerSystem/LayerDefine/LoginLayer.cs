using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class LoginLayer : UILayer
{
    [SerializeField] private TextMeshProUGUI ID;
    [SerializeField] private TextMeshProUGUI PASSWORD;
    [SerializeField] private Button LoginBtn;

    Action<LoginResult> success;
    Action<PlayFabError> fail;
    
    static LoginLayer Get()
    {
        var l = UILayerLoader.Get("LoginLayer");
        LoginLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as LoginLayer;
        }
        return returnValue;
    }
    
    public static LoginLayer Open(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        
        returnValue = UILayerLoader.Load(NetFightScene.target.T.gameObject,"LoginLayer") as LoginLayer;
        returnValue.Initialise(success, fail);
        return returnValue;
    }
    
    void Initialise(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        this.success = success;
        this.fail = fail;
        LoginBtn.onClick.AddListener(TryLogin);
    }
    
    void TryLogin()
    {
        PlayFabReadClient.PlayFabLogin(
            ID.text, PASSWORD.text, success, fail
        );
    }
}
