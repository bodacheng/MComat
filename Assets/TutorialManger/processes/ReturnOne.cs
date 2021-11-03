using mainMenu;

// Tutorial 1 
public class ReturnOne : TutorialProcess
{
    MainSceneStep nowstep;
    
    public ReturnOne()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        nowstep = ProcessesRunner.Main.currentProcess.Step;
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(ReturnButtonManager.ToUseReturnButton.transform);
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step != nowstep;
    }
}