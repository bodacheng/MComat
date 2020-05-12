using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using Api.Dto.Model;

public class MemberDetail_skillshow : MainSceneProcess
{
    public MemberDetail_skillshow()
    {
        Step = MainSceneStep.MemberDetail_show;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        CharDataInfo characterDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(MemberDetail.target.focusingCharDataInfo);
        MemberDetail.target._SkillsPrintOut.SkillsPrintGamenRefresh( characterDataInfo);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(true);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
        MemberDetail.target._SkillsPrintOut.SkillShowT.gameObject.SetActive(true);
        //this._CameraManager.Assign_LerpToCertainPlaceCamera(this._MemberDetail.MemDetailWatchPos.position, this._MemberDetail.MemDetailWatchPos.rotation);
        yield break;
    }
       
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        MemberDetail.target._SkillsPrintOut.ClearRenderPs();
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        MemberDetail.target._SkillsPrintOut.SkillShowT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
