using PlayFab;
using PlayFab.ClientModels;
using System;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class LoginLayer : UILayer
{
    [SerializeField] private InputField ID;
    [SerializeField] private InputField PASSWORD;
    [SerializeField] private Button LoginBtn;
    [SerializeField] private Button cancelBtn;

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
    
    static void Close()
    {
        UILayerLoader.Remove("LoginLayer");
    }
    
    void Initialise(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        this.success = success;
        this.fail = fail;
        LoginBtn.onClick.AddListener(TryLogin);
        cancelBtn.onClick.AddListener(Close);
    }
    
    void TryLogin()
    {
        PlayFabReadClient.PlayFabLogin(ID.text.Trim(), PASSWORD.text.Trim(), success, fail);
    }
}
