using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class SelfFightFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        preparingScene2.Instance.MainMenuBottonsT.gameObject.SetActive(false);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        this._SelfFightManager.Clear();
        yield return this._SelfFightManager.INITeamPosButtons();
        MonsterBox.target.MonsterBoxContainer.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        this._SelfFightManager.selfFightUI.gameObject.SetActive(true);
        yield return _modelShower.ShowThisCharacterModel(null);
    }
    
    public SelfFightFront(preparingScene2 _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.SelfFightFront;
        this._preparingScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._SelfFightManager.selfFightUI.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
