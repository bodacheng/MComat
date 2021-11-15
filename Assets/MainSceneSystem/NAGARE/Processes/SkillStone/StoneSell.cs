using mainMenu;
using UnityEngine;

public class StoneSell : MainSceneProcess
{
    void EnterProcess()
    {
        //StonesPage.EnterProcess();  １１。８　临时逻辑
        StoneDeleteManger.target.EnterDeleteMode();
    }
    
    public StoneSell()
    {
        Step = MainSceneStep.SkillStones_Sell;
        EelementsInherit(PreScene.target);
    }

    private StoneListLayer StoneListLayer;
    public override void ProcessEnter()
    {
        StoneListLayer = StoneListLayer.Open();
        EnterProcess();
        StoneListLayer.box._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, StoneListLayer.fxCamera, StoneListLayer.box.NormalTab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, StoneListLayer.fxCamera, StoneListLayer.box.EX1Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, StoneListLayer.fxCamera, StoneListLayer.box.EX2Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, StoneListLayer.fxCamera, StoneListLayer.box.EX3Tab.GetComponent<RectTransform>(), 5f), 
            Zokusei.Null
        );
    }
    
    public override void ProcessEnd()
    {
        StoneDeleteManger.target.ExitDeleteMode();
        StoneListLayer.box._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }
}