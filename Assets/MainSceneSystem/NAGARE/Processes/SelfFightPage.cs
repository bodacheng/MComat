using mainMenu;

public class SelfFightPage : MainSceneProcess
{    
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        mainProcessRunner.RunFreely(ModelShower.target.ShowMyModel(null));
        
        UnitsLayer layer = UnitsLayer.Open();
        layer.DisplayMonsterIcons(true);
        _SelfFightManager.AddHeroIconFeaturesToMonsterBox();
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        _SelfFightManager.SwitchToRotationMode();
        PageTo.Go(MainSceneStep.SelfFightFront);
    }
    
    public override void ProcessEnd()
    {
        UnitsLayer.Close();
    }
}
