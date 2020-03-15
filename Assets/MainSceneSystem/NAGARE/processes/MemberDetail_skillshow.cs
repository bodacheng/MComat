using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class MemberDetail_skillshow : MainSceneProcess
{
    public MemberDetail_skillshow(preparingScene2 _preparingScene)
    {
        thisProcessStep = MainSceneStep.MemberDetail_show;
        this._preparingScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }
    
    public IEnumerator EnterProcess()
    {
        CharacterDataInfo characterDataInfo = RemoteAccess.GetCharacterDataInfo(this._MemberDetail.focusingCharacterDataInfo);
        _MemberDetail._SkillsPrintOut.SkillsPrintGamenRefresh( characterDataInfo);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(false);
        _MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        _MemberDetail.MemberSkillshowT.gameObject.SetActive(true);
        //this._CameraManager.Assign_LerpToCertainPlaceCamera(this._MemberDetail.MemDetailWatchPos.position, this._MemberDetail.MemDetailWatchPos.rotation);
        yield break;
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
        this._MemberDetail._SkillsPrintOut.skillShowLines.ClearDrawingLines();
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void LocalUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
