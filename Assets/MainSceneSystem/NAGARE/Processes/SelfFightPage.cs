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
        MonsterBox.DisplayMonsterIcons(true);
        _SelfFightManager.AddHeroIconFeaturesToMonsterBox();
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _SelfFightManager.SwitchToRotationMode();
        PageTo.Go(MainSceneStep.SelfFightFront);
        MonsterBox.target.Open(true);
    }
    
    public override void ProcessEnd()
    {
        MonsterBox.target.Open(false);
    }
}
