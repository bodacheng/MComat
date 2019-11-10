using UniRx;
using System.Collections.Generic;
using UnityEngine;

public class FightOverProcess : NagareProcess
{
    public FightOverProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.FightOver;
        this.nextProcessStep = SceneStep.FightSummary;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
        fightOverControl.canGotoSummary.Subscribe(x => { if (x) AutoMoveToNext = true; });
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
    
    public override void LocalUpdate()
    {
        
    }
}
