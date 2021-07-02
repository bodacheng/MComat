using System;
using System.Collections;
using mainMenu;
using UnityEngine;
using System.Collections.Generic;

public class ArenaProcess : MainSceneProcess
{
    public void EnterProcess()
    {
        CloudScript.GetLeaderboardAroundUser(
            (List<LeaderboardInfo> obj) =>
            {
                ArenaManager.target.LoadArena(obj);
            } ,
            () => {}
        );
        
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(true);
    }
    
    public ArenaProcess()
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
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(false);
    }
}