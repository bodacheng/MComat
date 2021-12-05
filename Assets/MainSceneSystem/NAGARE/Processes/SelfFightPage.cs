using mainMenu;

public class SelfFightPage : MainSceneProcess
{    
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.target);
    }

    private SelfFightLayer selfFightLayer;
    public override void ProcessEnter()
    {
        mainProcessRunner.RunFreely(PreScene.target.modelShower.ShowMyModel(null));
        
        UnitsLayer layer = UnitsLayer.Open();
        layer.DisplayUnitIcons(true);

        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        
        selfFightLayer = UILayerLoader.Load(PreScene.target.T,"SelfFightLayer") as SelfFightLayer;
        selfFightLayer.INI();
        selfFightLayer.AddHeroIconFeaturesToMonsterBox();
        selfFightLayer.SwitchToRotationMode();
    }
    
    public override void ProcessEnd()
    {
        UnitsLayer.Close();
        UILayerLoader.Remove("SelfFightLayer");
    }
}
