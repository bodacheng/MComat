using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOverProcess : NagareProcess
{
    public FightOverProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.FightOver;
        this.nextProcessStep = SceneStep.FightSummary;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }

    public override bool canEnterNextProcess()
    {
        return fightOverControl.ifCanGotoSummary();//想手动或fightOverControl模块把流程转向下一个环节。
    }
    
    public override void ProcessEnter()
    {
        fightOverControl.FightOverCanvas.gameObject.SetActive(true);
        switch (this.fightLogger.getWinner())
        {
            case Team.player1:
                mainProcessRunner.triggerMainProcess(this.fightOverControl.WINProcess());//这里是要根据情况的。。
                break;
            case Team.player2:
                mainProcessRunner.triggerMainProcess(this.fightOverControl.LoseProcess());//这里是要根据情况的。。
                break;
        }
    }
    
    public override void ProcessEnd()
    {
        
    }
    
    public override void localUpdate()
    {
        
    }
}
