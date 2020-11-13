using System.Collections;
using mainMenu;

public class ArenaProcess : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        yield return ArenaManager.target.LoadArena();
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(true);
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