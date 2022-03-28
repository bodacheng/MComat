using mainMenu;

public class SettingPage : MainSceneProcess
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
    }

    public override void ProcessEnd()
    {
        SettingLayer.Close();
    }
}
