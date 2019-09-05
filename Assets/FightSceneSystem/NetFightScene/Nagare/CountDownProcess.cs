using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownProcess : NagareProcess
{    
    public CountDownProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.CountDown;
        this.nextProcessStep = SceneStep.none;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }

    public override bool canEnterNextProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {

    }
    
    public override void ProcessEnd()
    {
        fightTalksRunner.resetAll(3f);
    }
    
    public override void localUpdate()
    {
        fightTalksRunner.runBeforeFight(_RealTimeGameProcessManager.FightTeam2.teamMembers.values);
        if (fightTalksRunner.FightTalksEnded())
        {
            switch(FightSceneNote.Instance.nextBattle._fightEventType)
            {
                case fightEventType.Quest:
                    fightSceneProcessesRunner.changeProcess(SceneStep.Fighting);
                    break;
                case fightEventType.Tutorial_Basic:
                    fightSceneProcessesRunner.changeProcess(SceneStep.BasicTryTutorial);
                    break;
                case fightEventType.Tutorial_Story_AdamVsGuards:
                    fightSceneProcessesRunner.changeProcess(SceneStep.Fighting);
                    break;
                case fightEventType.Self:
                    fightSceneProcessesRunner.changeProcess(SceneStep.Fighting);
                    break;
            }
        }
    }
}
