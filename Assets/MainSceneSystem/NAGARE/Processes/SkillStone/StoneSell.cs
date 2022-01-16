using mainMenu;
using UnityEngine;

public class StoneSell : MainSceneProcess
{
    void EnterProcess()
    {
        //StonesPage.EnterProcess();  １１。８　临时逻辑
        //StoneDeleteManger.target.EnterDeleteMode();
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
    }
    
    public override void ProcessEnd()
    {
        //StoneDeleteManger.target.ExitDeleteMode();
        StoneListLayer.box._tabEffects.CloseShowingZokuseiTagEffects();
    }
}