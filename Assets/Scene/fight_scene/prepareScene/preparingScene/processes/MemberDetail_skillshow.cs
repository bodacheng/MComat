using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberDetail_skillshow : MainSceneProcess
{
    RectTransform T;
    
    public MemberDetail_skillshow(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.MemberDetail_show;
        this._preparingScene = _preparingScene;
        this.T = T;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        this._MemberDetail._SkillsPrintOut.SkillsPrintGamenRefresh( this._MemberDetail.focusingCharacterDataInfo);
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(false);
        this._MemberDetail._TheNineSlot.NineSlotT.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        this._CameraManager.Assign_Camera(Camera_Mode_Num.LockCamera);
        this._CameraManager.current_Camera_Mode.targets = new List<Transform>() { this._MemberDetail.MemDetailWatchPos };
        yield break;     
    }
    
    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this._preparingScene.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
         this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
