using System.Collections;
using mainMenu;

public class ArcadeFrontProcess : MainSceneProcess
{
    public bool loadFinished;
    
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowModel(null);
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(true);
        ArcadeManager.target.JumpToNewest();
        ArcadeManager.target.RefreshRender();
        loadFinished = true;
    }
    
    public ArcadeFrontProcess()
    {
        Step = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.target);
    }
        
    public override void ProcessEnter()
    {
        loadFinished = false;
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