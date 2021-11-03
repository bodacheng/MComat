using mainMenu;

// Tutorial 1 
public class ChooseAdamToSlot1 : TutorialProcess
{
    public ChooseAdamToSlot1()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
        LoadingCanvas.target.HigtLightRect(unitsLayer.GetCharIcon("1").transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return false;//TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(0) == "1";
    }
}