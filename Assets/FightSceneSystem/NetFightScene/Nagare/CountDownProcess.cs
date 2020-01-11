using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CountDownProcess : NagareProcess
{    
    public CountDownProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.CountDown;
        nextProcessStep = SceneStep.none;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
        fightTalksRunner.playersStartOff.Subscribe(x => { if (x == true) MoveToFight();});
    }
    
    public override void ProcessEnter()
    {
        fightTalksRunner.Step = 0;
        BoundaryControllByGod.ChangeMagicRingRadius(20f);
    }
    
    public override void ProcessEnd()
    {
        fightTalksRunner.Step = -1;
    }
    
    void MoveToFight()
    {
        switch(FightSceneNote.Instance.nextBattle._fightEventType)
        {
            case FightEventType.Quest:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
            case FightEventType.Tutorial_Basic:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.BasicTryTutorial);
                break;
            case FightEventType.Tutorial_Story_AdamVsGuards:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
            case FightEventType.Self:
                FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
                break;
        }
    }
}
