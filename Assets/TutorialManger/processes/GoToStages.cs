using mainMenu;
using UnityEngine;

// Tutorial 1 
public class GoToStages : TutorialProcess
{
    public override void ProcessEnter()
    {
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