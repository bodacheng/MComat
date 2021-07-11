using mainMenu;
using UnityEngine;

public class ArenaPage : MainSceneProcess
{
    void EnterProcess()
    {
        ArenaManager.target.RefreshOpponent();
        PageTo.Go(MainSceneStep.Arena);
    }
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
    }
}