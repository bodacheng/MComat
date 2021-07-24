using System.Collections;
using UnityEngine;
using FightScene;
using System.Collections.Generic;

public class FightResultAnim : FSceneProcess
{
    private bool animEnd = false;
    public FightResultAnim()
    {
        Step = SceneStep.FightResultAnim;
        nextProcessStep = SceneStep.FightOver;
    }
    
    public override void ProcessEnter()
    {
        SingleThreadProcesser.backup.RunAsQueued(EnterProcess());
    }
    
    IEnumerator EnterProcess()
    {
        animEnd = false;
        yield return FinalMomentAnim(FightLogger.target.GetWinner());
        animEnd = true;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return animEnd;
    }
    
    IEnumerator FinalMomentAnim(Team winner)
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(2f);
        List<Data_Center> winners = new List<Data_Center>();
        if (winner == Team.player1)
        {
            winners = RTFightManager.target.Team1Members.GetValues();
        }
        if (winner == Team.player2)
        {
            winners = RTFightManager.target.Team2Members.GetValues();
        }
        foreach (Data_Center _one in winners)
        {
            if (!_one.IsDead.Value)
            {
                _one._MyBehaviorRunner.ChangeState("Victory");
            }
        }
        Time.timeScale = 1f;

        FightResultAnimLayer fightResultAnimLayer = UILayerLoader.Load
            (NetFightScene.target.T, "FightResultAnimLayer") as FightResultAnimLayer;
        
        switch (FightLogger.target.GetWinner())
        {
            case Team.player1:
                yield return fightResultAnimLayer.WINProcess();
                break;
            case Team.player2:
                yield return fightResultAnimLayer.LoseProcess();
                break;
        }
        UILayerLoader.Remove("FightResultAnimLayer");
    }
}
