using mainMenu;

// Tutorial 1 
public class GoToStageOne : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private ArcadeTop _arcadeTop;
    ArcadeFrontPage _arcadeFrontPage;
    
    public override void ProcessEnter()
    {
    }
    
    public override void ProcessEnd()
    {
        HighLightLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.QuestInfo;
    }
    
    public override void LocalUpdate()
    {
        if (!Loaded)
        {
            if (_returnLayer == null)
                _returnLayer = ReturnLayer.Get();
            
            if (_arcadeTop == null)
                _arcadeTop = ArcadeTop.Get();
            
            if (_returnLayer != null && _arcadeTop != null)
            {
                _returnLayer.gameObject.SetActive(false);
                Loaded = true;
            }
        }
    }
}