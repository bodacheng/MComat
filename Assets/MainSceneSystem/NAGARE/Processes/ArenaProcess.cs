using System.Collections;
using mainMenu;

public class ArenaProcess : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.target.mainProcessRunner.Run(ArenaManager.target.LoadArena());
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public ArenaProcess()
    {
        Step = MainSceneStep.Arena;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(false);
    }
}
