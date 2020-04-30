using UniRx;
using System.Linq;

public class FightOverProcess : NagareProcess
{
    public FightOverProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.FightOver;
        nextProcessStep = SceneStep.FightSummary;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
        fightOverControl.CanGotoSummary.Subscribe(x => { if (x) AutoMoveToNext = true; });
    }
    
    public override void ProcessEnter()
    {
        fightOverControl.FightOverCanvas.gameObject.SetActive(true);
        switch (fightLogger.getWinner())
        {
            case Team.player1:
                mainProcessRunner.Run(fightOverControl.WINProcess());//这里是要根据情况的。。
                break;
            case Team.player2:
                mainProcessRunner.Run(fightOverControl.LoseProcess());//这里是要根据情况的。。
                break;
        }
        mainProcessRunner.Run(fightOverControl.ShowSKillSets(_RealTimeGameProcessManager.FightTeam1.CharDataInfoRef.Values.ToList()));//这里是要根据情况的。。
    }
    
    public override void ProcessEnd()
    {
        
    }
    
    public override void LocalUpdate()
    {
        
    }
}
