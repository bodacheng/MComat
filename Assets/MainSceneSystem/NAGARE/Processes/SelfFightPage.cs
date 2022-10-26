using Cysharp.Threading.Tasks;
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
        var layer = UILayerLoader.Load<UnitsLayer>();
        layer.DisplayUnitIcons(dataAccess.Units.Dic, true);
        
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        
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
