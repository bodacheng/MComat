using mainMenu;
using FightScene;

// Tutorial 1 
public class ConfirmQuest1 : TutorialProcess
{
    public ConfirmQuest1()
    {
        Step = TutorialStep.ConfirmQuest1;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(FightPreparePage.target.EnterQuest.transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        if (FSceneProcessesRunner.Main.currentProcess == null)
            return false;
        return FSceneProcessesRunner.Main.currentProcess.Step == SceneStep.CountDown;
    }
    
    public override void LocalUpdate()
    {
    }
}