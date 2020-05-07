using System.Collections;
using mainMenu;

public class ArcadeFrontProcess : MainSceneProcess
{    
    public IEnumerator EnterProcess()
    {
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public ArcadeFrontProcess()
    {
        Step = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.target);
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
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
