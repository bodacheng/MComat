using System.Collections;
using mainMenu;

public class ArcadeFrontProcess : MainSceneProcess
{    
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        ArcadeManager.Instance._ArcadeCanvas.gameObject.SetActive(true);
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public ArcadeFrontProcess()
    {
        thisProcessStep = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.Instance);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArcadeManager.Instance._ArcadeCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
