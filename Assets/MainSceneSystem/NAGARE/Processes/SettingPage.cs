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
        layer = SettingLayer.Open();
        
        SetLoaded(true);
    }

    public override void ProcessEnd()
    {
        SettingLayer.Close();
    }
}
