using mainMenu;
using UnityEngine;

// Tutorial 1 
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
            _frontLayer = FrontLayer.Get();
            if (_frontLayer != null)
            {
                Debug.Log("bodacheng");
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