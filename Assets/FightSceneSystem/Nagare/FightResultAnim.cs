using System.Collections;
using UnityEngine;
using FightScene;
using System.Collections.Generic;
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
        yield return new WaitForSeconds(1f);
        var winners = new List<Data_Center>();
        
        switch (FightLogger.value.GetWinnerTeam())
        {
            case Team.player1 :
                winners = RTFightManager.Target.team1.teamMembers.GetValues();
                break;
            case Team.player2 :
                winners = RTFightManager.Target.team2.teamMembers.GetValues();
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

        var arenaFightOver = UILayerLoader.Load<ArenaFightOver>();
        arenaFightOver.Step1Anim();
        yield return new WaitForSeconds(0.5f);
    }
}
