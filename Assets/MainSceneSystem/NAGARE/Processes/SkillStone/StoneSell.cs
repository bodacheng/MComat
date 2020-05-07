using System.Collections;
using mainMenu;

public class StoneSell : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        if (ProcessesRunner.Instance.lastProcess.Step != MainSceneStep.SkillStones)
        {
            yield return SkillStones.EnterProcess();
        } else {
            SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        }
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
        mainProcessRunner.Run(EnterProcess());
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.NormalTab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX1Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX2Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX3Tab.gameObject,5f), 
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