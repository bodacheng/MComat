using System.Collections;
using mainMenu;

public class SelfFightFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;
        
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        _SelfFightManager.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        _SelfFightManager.SelfFightCanvas.gameObject.SetActive(true);
        yield return _modelShower.ShowModel(null);
    }
    
    public SelfFightFront()
    {
        thisProcessStep = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        _SelfFightManager.SelfFightCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
