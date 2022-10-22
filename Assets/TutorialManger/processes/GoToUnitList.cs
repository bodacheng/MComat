using DummyLayerSystem;
using mainMenu;

public class GoToUnitList : TutorialProcess
{
    private FrontLayer _frontLayer;
    private UpperInfoBar _upperInfoBar;
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
            _frontLayer = UILayerLoader.Get<FrontLayer>();
            if (_frontLayer != null)
            {
                _frontLayer.PlsClickBtn("unit");
                Loaded = true;
            }
        }

        if (_upperInfoBar == null)
        {
            _upperInfoBar = UILayerLoader.Get<UpperInfoBar>();
            if (_upperInfoBar != null){}
                _upperInfoBar.Interactable(false);
        }
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitList;
    }
}