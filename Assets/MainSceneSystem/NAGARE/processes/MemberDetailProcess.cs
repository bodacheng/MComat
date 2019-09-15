using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public class MemberDetailProcess : MainSceneProcess
{    
    public MemberDetailProcess(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.MemberDetail;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);       
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position,3f,25f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        yield return (this._MonsterBox.myMonsterBox());
        //this._MonsterBox.adjustAllIconsSize(null);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return _MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
    }
    
    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._MemberDetail.MemberInfoT.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.ifShowingSkill())
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
