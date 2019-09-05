using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class MemberDetail_skillshow : MainSceneProcess
{
    public MemberDetail_skillshow(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.MemberDetail_show;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(this._MemberDetail.focusingCharacterDataInfo);
        this._MemberDetail._SkillsPrintOut.SkillsPrintGamenRefresh( characterDataInfo);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(false);
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        this._MemberDetail._TheNineSlot.NineSlotT.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(true);
        //this._CameraManager.Assign_LerpToCertainPlaceCamera(this._MemberDetail.MemDetailWatchPos.position, this._MemberDetail.MemDetailWatchPos.rotation);
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
        this._MemberDetail._SkillsPrintOut.skillShowLines.ClearDrawingLines();
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.showingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
