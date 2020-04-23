using System.Collections;
using mainMenu;
using UnityEngine;

public class ArenaProcess : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.Instance.mainProcessRunner.Run(ArenaManager.target.LoadArena());
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public ArenaProcess()
    {
        thisProcessStep = MainSceneStep.Arena;
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
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    }
}
