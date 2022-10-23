using DummyLayerSystem;
using mainMenu;

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
}
