using System;
using DummyLayerSystem;
using mainMenu;
using PlayFab;

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

    public static void SetNickName(Action<string> success, bool closeBtnOn)
    {
        var nickNameLayer = UILayerLoader.Load<NickNameLayer>();
        nickNameLayer.Cancel.gameObject.SetActive(closeBtnOn);
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
                                if (playFabError.Error == PlayFabErrorCode.InvalidUsername)
                                {
                                    PopupLayer.ArrangeWarnWindow("InvalidUsername");
                                }
                                else
                                {
                                    PreScene.ReturnToLobby();
                                }
                                ProgressLayer.Close();
                            }
                        );
                    }, 
                    "Set as your nick name?");
            }
        );
    }
}
