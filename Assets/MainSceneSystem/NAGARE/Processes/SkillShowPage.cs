using UnityEngine;
using mainMenu;

public class SkillShowPage : MainSceneProcess
{
    private SkillShowLayer SkillShowLayer;
    public SkillShowPage()
    {
        Step = MainSceneStep.UnitSkillShow;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        UnitInfo unitInfo = UnitInfo.GetCharDataInfo(PreScene.target._focusing);
        SkillShowLayer = UILayerLoader.Load(PreScene.target.T,"SkillShowLayer") as SkillShowLayer;
        SkillShowLayer.fx.transform.SetParent(null);
        SkillShowLayer.SkillsPrintPageRefresh( unitInfo);
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
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
    }
    
    public override void ProcessEnd()
    {
        SkillShowLayer.ClearRenderPs();
        GameObject.Destroy(SkillShowLayer.fx.gameObject);
        UILayerLoader.Remove("SkillShowLayer");
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
