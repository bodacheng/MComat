using System;
using DummyLayerSystem;
using mainMenu;

public class SettingPage : MSceneProcess
{
    private SettingLayer _layer;

    public SettingPage()
    {
        Step = MainSceneStep.Setting;
    }
    
    public override void ProcessEnter()
    {
        _layer = !CommonSetting.PcMode ? 
            UILayerLoader.Load<SettingLayer>() : 
            UILayerLoader.Load<SettingLayer>(false, "SettingLayer_st");
        
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
                        ProgressLayer.Loading("");
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
