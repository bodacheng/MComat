using UniRx;

public class CountDownProcess : NagareProcess
{    
    public CountDownProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.CountDown;
        nextProcessStep = SceneStep.none;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
        fightTalksRunner.PlayersStartOff.Subscribe(x => { if (x == true) MoveToFight();});
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
        FightSceneProcessesRunner.ChangeProcess(SceneStep.Fighting);
    }
}
