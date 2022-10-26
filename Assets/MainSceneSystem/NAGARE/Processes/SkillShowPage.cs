using DummyLayerSystem;
using mainMenu;

public class SkillShowPage : MSceneProcess
{
    private SkillShowLayer layer;
    public SkillShowPage()
    {
        Step = MainSceneStep.UnitSkillShow;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        var unitInfo = UnitInfo.GetUnitInfo(PreScene.target.Focusing);
        layer.SkillsPrintPageRefresh(unitInfo);
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        layer.ClearRenderPs();
        layer.EffectsManager.CloseShowingZokuseiTagEffects();
        UILayerLoader.Remove<SkillShowLayer>();
    }
}
