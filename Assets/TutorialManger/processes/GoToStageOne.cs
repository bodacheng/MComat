using mainMenu;

// Tutorial 1 
public class GoToStageOne : TutorialProcess
{
    bool missionCompleted;
    ArcadeFrontPage _arcadeFrontPage;
    
    public GoToStageOne()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        missionCompleted = false;
        _arcadeFrontPage = (ArcadeFrontPage)ProcessesRunner.Main.GetProcess(MainSceneStep.ArcadeFront);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.QuestInfo;
    }
    
    public override void LocalUpdate()
    {
        if (!missionCompleted)
        {
            if (_arcadeFrontPage.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(ArcadeManager.target.GetStageButton(1).button.transform);
                missionCompleted = true;
            }
        }
    }
}