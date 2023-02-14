using DummyLayerSystem;
using mainMenu;

public class SelfFightPage : MSceneProcess
{
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
    }

    SelfFightLayer selfFightLayer;
    public override void ProcessEnter()
    {
        var layer = UILayerLoader.Load<UnitsLayer>();
        layer.DisplayUnitIcons(dataAccess.Units.Dic, true, true);
        
        selfFightLayer = UILayerLoader.Load<SelfFightLayer>();
        selfFightLayer.INI();
        selfFightLayer.AddUnitIconFeaturesToBox();
        selfFightLayer.SwitchToRotationMode();
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UnitsLayer>();
        UILayerLoader.Remove<SelfFightLayer>();
    }
}
