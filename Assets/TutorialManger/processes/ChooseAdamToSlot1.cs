using DummyLayerSystem;
using mainMenu;
using UnityEngine;

// Tutorial 1 
public class ChooseAdamToSlot1 : TutorialProcess
{
    public override void ProcessEnter()
    {
        UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
        HighLightLayer.HighLightRect(PreScene.target.T, unitsLayer.GetUnitIcon("1").GetComponent<RectTransform>());
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return false;//TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(0) == "1";
    }
}