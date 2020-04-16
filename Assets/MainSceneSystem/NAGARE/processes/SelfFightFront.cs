using System.Collections;
using mainMenu;

public class SelfFightFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_StartToEndModeCamera(_MemberDetail.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.current_Camera_Mode.target = _MemberDetail.MemDetailTargetPos;
        
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        
        _SelfFightManager.SelfFightCanvas.gameObject.SetActive(true);
        yield return _modelShower.ShowModel(null);
    }
    
    public SelfFightFront()
    {
        thisProcessStep = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.Instance);
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
