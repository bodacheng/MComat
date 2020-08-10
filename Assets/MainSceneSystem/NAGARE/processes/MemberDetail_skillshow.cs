using System.Collections;
using UnityEngine;
using mainMenu;
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
        CharDataInfo _CharDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(MemberDetail.target._focusing);
        MemberDetail.target._SkillsPrintOut.SkillsPrintGamenRefresh( _CharDataInfo);
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(MemberDetail.target._focusing.monsterId);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(),5f), 
            _CharConfig._zokusei
        );
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        
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
