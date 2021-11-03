using mainMenu;

// Tutorial 1 
public class GoToStages : TutorialProcess
{
    public GoToStages()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(TutorialHelper.target.ArcadeMode.transform);
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.ArcadeFront;
    }
}