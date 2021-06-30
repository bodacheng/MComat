using System;
using System.Collections;
using mainMenu;
using UnityEngine;

public class ArenaProcess : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        Debug.Log("arena");
        
        CloudScript.GetLeaderboardAroundUser(() => {}, () => {});
        
        yield return ArenaManager.target.LoadArena();
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(true);
        Debug.Log("arena loaded");
    }
    
    public ArenaProcess()
    {
        Step = MainSceneStep.Arena;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(false);
    }
}