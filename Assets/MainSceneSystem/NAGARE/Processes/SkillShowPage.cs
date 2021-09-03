using UnityEngine;
using mainMenu;
using dataAccess;

public class SkillShowPage : MainSceneProcess
{
    public SkillShowPage()
    {
        Step = MainSceneStep.UnitSkillShow;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        UnitInfo unitInfo = UnitInfo.GetCharDataInfo(MemberDetail.target._focusing);
        MemberDetail.target._SkillsPrintOut.SkillsPrintGamenRefresh( unitInfo);
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(MemberDetail.target._focusing.r_id);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(), 5f), 
            _CharConfig._zokusei
        );
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        PageTo.Go(MainSceneStep.UnitSkillShow);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
    }
    
    public override void ProcessEnd()
    {
        MemberDetail.target._SkillsPrintOut.ClearRenderPs();
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
