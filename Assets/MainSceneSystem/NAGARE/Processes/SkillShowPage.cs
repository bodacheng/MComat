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
        UnitInfo unitInfo = UnitInfo.GetUnitInfo(PreScene.target._focusing);
        SkillShowLayer = SkillShowLayer.Open();
        SkillShowLayer.SkillsPrintPageRefresh( unitInfo);
    }
    
    public override void ProcessEnd()
    {
        SkillShowLayer.ClearRenderPs();
        SkillShowLayer.EffectsManager.CloseShowingZokuseiTagEffects();
        UILayerLoader.Remove("SkillShowLayer");
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f,10);
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
