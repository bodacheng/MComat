using System;
using DummyLayerSystem;
using mainMenu;
using PlayFab;
using UnityEngine;

public class SettingPage : MSceneProcess
{
    private SettingLayer _layer;

    public SettingPage()
    {
        Step = MainSceneStep.Setting;
    }
    
    public override void ProcessEnter()
    {
        _layer = UILayerLoader.Load<SettingLayer>();
        _layer.Initialise();
        PlayFabReadClient.GetAccountInfo(
            (x) =>
            {
                if (x)
                {
                    if (PlayerAccountInfo.Me.Email != null)
                    {
                        _layer.AccountPhase_EmailSet();
                    }
                    else
                    {
                        _layer.AccountPhase_EmailToBeSet();
                    }
                    _layer.RefreshLinkDeviceBtn();
                }
            }
        );
        SetLoaded(true);
    }

    public override void ProcessEnd()
    {
        SettingLayer.Close();
    }

    public static void SetNickName(Action<string> success, bool closeBtnOn, Action extraOnClose = null)
    {
        var nickNameLayer = UILayerLoader.Load<NickNameLayer>();
        nickNameLayer.Setup(
            (x) =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    () =>
                    {
                        ProgressLayer.Loading(">");
                        nickNameLayer.LoadingRender(true);
                        PlayFabReadClient.UpdateUserTitleDisplayName(
                            x,
                            (result) =>
                            {
                                PlayerAccountInfo.Me.TitleDisplayName = result.DisplayName;
                                UILayerLoader.Remove<NickNameLayer>();
                                ProgressLayer.Close();
                                success?.Invoke(result.DisplayName);
                            },
                            (playFabError) =>
                            {
                                nickNameLayer.LoadingRender(false);
                                Debug.Log("nick name setting fail:"+ playFabError.Error);
                                switch (playFabError.Error)
                                {
                                    case PlayFabErrorCode.InvalidUsername:
                                        PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidUsername"));
                                        break;
                                    case PlayFabErrorCode.DuplicateUsername:
                                        PopupLayer.ArrangeWarnWindow(Translate.Get("DuplicateUsername"));
                                        break;
                                    case PlayFabErrorCode.InvalidParams:
                                        PopupLayer.ArrangeWarnWindow(Translate.Get("InvalidUsername"));
                                        break;
                                    default:
                                        PreScene.ReturnToLobby();
                                        break;
                                }
                                
                                ProgressLayer.Close();
                            }
                        );
                    }, 
                    Translate.Get("IfSetNickName"));
            },
            closeBtnOn,
            extraOnClose
        );
    }
}
