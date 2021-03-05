using System.Collections;
using mainMenu;
using UnityEngine;

public class StoneSell : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        yield return SkillStonesList.EnterProcess();
        StoneDeleteManger.target.EnterDeleteMode();
    }
    
    public StoneSell()
    {
        Step = MainSceneStep.SkillStones_Sell;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        SkillStonesBox.target = PreScene.target._SkillStonesBox_Show;
        mainProcessRunner.RunAsQueued(EnterProcess());
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(), 5f), 
            Zokusei.Null
        );
    }
    
    public override void ProcessEnd()
    {
        StoneDeleteManger.target.ExitDeleteMode();
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
    }
}