using DummyLayerSystem;
using UnityEngine;
using mainMenu;

public class SkillShowPage : MainSceneProcess
{
    private SkillShowLayer layer;
    public SkillShowPage()
    {
        Step = MainSceneStep.UnitSkillShow;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        var unitInfo = UnitInfo.GetUnitInfo(PreScene.target._focusing);
        layer = SkillShowLayer.Open();
        layer.SkillsPrintPageRefresh(unitInfo);
    }
    
    public override void ProcessEnd()
    {
        layer.ClearRenderPs();
        layer.EffectsManager.CloseShowingZokuseiTagEffects();
        UILayerLoader.Remove("SkillShowLayer");
    }
}
