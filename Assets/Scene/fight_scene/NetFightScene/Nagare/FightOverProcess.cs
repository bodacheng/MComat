using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOverProcess : NagareProcess
{
    
    public FightOverProcess(NetFightScene _NetFightScene)
    {
        this.thisProcessStep = SceneStep.FightOver;
        this.nextProcessStep = SceneStep.FightSummary;
        this._NetFightScene = _NetFightScene;
    }

    public override bool canEnterNextProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        _NetFightScene._FightOverControl.FightOverCanvas.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene._FightOverControl.FightOverCanvas.gameObject.SetActive(false);
    }
    
    public override void localUpdate()
    {
        
    }
}
