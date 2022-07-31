using DummyLayerSystem;
using mainMenu;

public class SelfFightPage : MSceneProcess
{    
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
        Inherit(PreScene.target);
    }

    private SelfFightLayer selfFightLayer;
    public override void ProcessEnter()
    {
        var layer = UnitsLayer.Open();
        layer.DisplayUnitIcons(true);
        
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        
        selfFightLayer = UILayerLoader.Load(PreScene.target.T,"SelfFightLayer") as SelfFightLayer;
        selfFightLayer.INI();
        selfFightLayer.AddHeroIconFeaturesToMonsterBox();
        selfFightLayer.SwitchToRotationMode();
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UnitsLayer.Close();
        UILayerLoader.Remove("SelfFightLayer");
    }
}
