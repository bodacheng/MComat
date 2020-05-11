using mainMenu;

// Tutorial 1 
public class ReturnOne : MainSceneProcess
{
    MainSceneStep nowstep;
    
    public ReturnOne()
    {
        Step = MainSceneStep.TutorialReturn;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        nowstep = ProcessesRunner.Main.currentProcess.Step;
        LoadingCanvas.target.HigtLightRect(ReturnButtonManager.ToUseReturnButton.transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step != nowstep;
    }
}