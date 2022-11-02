using System;
using DummyLayerSystem;
using mainMenu;
using PlayFab;

public class SettingPage : MSceneProcess
{
    private SettingLayer layer;

    public SettingPage()
    {
        Step = MainSceneStep.Setting;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        layer = UILayerLoader.Load<SettingLayer>();
        layer.Initialise();
        PlayFabReadClient.GetAccountInfo(
            (x) =>
            {
                if (x)
                {
                    if (PlayerAccountInfo.Me.Email != null)
                    {
                        layer.AccountPhase_EmailSet();
                    }
                    else
                    {
                        layer.AccountPhase_EmailToBeSet();
                    }
                    layer.RefreshLinkDeviceBtn();
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
                            (x) =>
                            {
                                PlayerAccountInfo.Me.TitleDisplayName = x.DisplayName;
                                UILayerLoader.Remove<NickNameLayer>();
                                ProgressLayer.Close();
                                success?.Invoke(x.DisplayName);
                            },
                            (x) =>
                            {
                                nickNameLayer.LoadingRender(false);
                                PopupLayer.ArrangeWarnWindow(x.Error == PlayFabErrorCode.InvalidUsername
                                    ? "InvalidUsername"
                                    : "Network Error");
                                ProgressLayer.Close();
                            }
                        );
                    }, 
                    "Set as your nick name?");
            }
        );
    }
}
