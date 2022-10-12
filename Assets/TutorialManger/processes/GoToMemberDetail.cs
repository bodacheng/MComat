using mainMenu;

public class GoToMemberDetail : TutorialProcess
{
    private FrontLayer _frontLayer;
    
    public override void ProcessEnter()
    {
    }
    
    public override void ProcessEnd()
    {
        HighLightLayer.Close();
    }
    
    public override void LocalUpdate()
    {
        if (!Loaded)
        {
            _frontLayer = FrontLayer.Get();
            if (_frontLayer != null)
            {
                _frontLayer.PlsClickBtn("unit");
                Loaded = true;
            }
        }
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitList;
    }
}