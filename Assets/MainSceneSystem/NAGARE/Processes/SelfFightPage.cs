using mainMenu;
using Cysharp.Threading.Tasks;

public class SelfFightPage : MainSceneProcess
{
    public void temp()
    {
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);       
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _SelfFightManager.SelfFightCanvas.gameObject.SetActive(true);
        _SelfFightManager.SwitchToRotationMode();
    }
    
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.target);
    }

    public async UniTask enter()
    {
        MonsterBox.DisplayMonsterIcons(true);
        _SelfFightManager.AddHeroIconFeaturesToMonsterBox();
        temp();
    }

    public override void ProcessEnter()
    {
        mainProcessRunner.RunFreely(ModelShower.target.ShowMyModel(null));
        mainProcessRunner.RunAsQueued(enter());        
    }
    
    public override void ProcessEnd()
    {
        _SelfFightManager.SelfFightCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }
}
