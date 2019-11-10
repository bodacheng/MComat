using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CountDownProcess : NagareProcess
{    
    public CountDownProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.CountDown;
        this.nextProcessStep = SceneStep.none;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
        fightTalksRunner.playersStartOff.Subscribe(x => { if (x == true) MoveToFight();});
    }
    
    public override void ProcessEnter()
    {
        fightTalksRunner.Step = 0;
        this.BoundaryControllByGod.ChangeMagicRingRadius(20f);
    }
    
    public override void ProcessEnd()
    {
        fightTalksRunner.Step = -1;
    }
    
    private void MoveToFight()
    {
        switch(FightSceneNote.Instance.nextBattle._fightEventType)
        {
            case fightEventType.Quest:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
            case fightEventType.Tutorial_Basic:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.BasicTryTutorial);
                break;
            case fightEventType.Tutorial_Story_AdamVsGuards:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
            case fightEventType.Self:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
        }
    }
}
