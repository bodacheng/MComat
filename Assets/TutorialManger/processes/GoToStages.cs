using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class GoToStages : TutorialProcess
{
    private FrontLayer _frontLayer;
    public override void ProcessEnter()
    {
        Debug.Log("Entered GoToStages");
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
                _frontLayer.PlsClickBtn("arcade");
                Loaded = true;
            }
        }
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.ArcadeFront;
    }
}