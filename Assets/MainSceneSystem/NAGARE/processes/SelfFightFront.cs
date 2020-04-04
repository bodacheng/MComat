using System.Collections;
using mainMenu;

public class SelfFightFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        this._SelfFightManager.Clear();
        yield return this._SelfFightManager.INITeamPosButtons();
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        this._SelfFightManager.SelfFightCanvas.gameObject.SetActive(true);
        yield return _modelShower.ShowThisCharacterModel(null);
    }
    
    public SelfFightFront()
    {
        this.thisProcessStep = MainSceneStep.SelfFightFront;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._SelfFightManager.SelfFightCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
