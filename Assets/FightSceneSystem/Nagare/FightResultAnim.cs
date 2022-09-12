using System.Collections;
using UnityEngine;
using FightScene;
using System.Collections.Generic;
using dataAccess;
using DummyLayerSystem;

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
        var winners = new List<Data_Center>();
        
        switch (FightLogger.value.GetWinnerTeam())
        {
            case Team.player1 :
                winners = RTFightManager.target.team1.TeamMembers.GetValues();
                break;
            case Team.player2 :
                winners = RTFightManager.target.team2.TeamMembers.GetValues();
                break;
        }
        
        foreach (Data_Center _one in winners)
        {
            if (!_one.FightDataRef.IsDead.Value)
            {
                _one._MyBehaviorRunner.ChangeState("Victory");
            }
        }
        Time.timeScale = 1f;

        FightResultAnimLayer fightResultAnimLayer = UILayerLoader.Load
            (NetFightScene.target.T.gameObject, "FightResultAnimLayer") as FightResultAnimLayer;

        if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabId)
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
