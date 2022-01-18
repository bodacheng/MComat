using mainMenu;
using UnityEngine;

// Tutorial 1 
public class GoToStages : TutorialProcess
{
    public GoToStages()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.HighLightRect(PreScene.target.T, TutorialHelper.target.ArcadeMode.GetComponent<RectTransform>());
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