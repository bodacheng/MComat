using System.Collections;
using UnityEngine;
using FightScene;
using System.Collections.Generic;
using dataAccess;

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
        yield return FinalMomentAnim();
        animEnd = true;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return animEnd;
    }
    
    IEnumerator FinalMomentAnim()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(2f);
        List<Data_Center> winners = new List<Data_Center>();

        switch (FightOverControl.target.logger.GetWinnerTeam())
        {
            case Team.player1 :
                winners = RTFightManager.target.Team1Members.GetValues();
                break;
            case Team.player2 :
                winners = RTFightManager.target.Team2Members.GetValues();
                break;
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
            (NetFightScene.target.T.gameObject, "FightResultAnimLayer") as FightResultAnimLayer;

        if (FightOverControl.target.logger.GetWinnerId() == PlayerAccountInfo.Me.playerID)
        {
            yield return fightResultAnimLayer.WINProcess();
        }
        else
        {
            yield return fightResultAnimLayer.LoseProcess();
        }
        
        UILayerLoader.Remove("FightResultAnimLayer");
    }
}
