using System;
using UnityEngine;
using FightScene;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        EnterProcess().Forget();
    }
    
    async UniTask EnterProcess()
    {
        animEnd = false;
        await FinalMomentAnim();
        animEnd = true;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return animEnd;
    }
    
    async UniTask FinalMomentAnim()
    {
        Time.timeScale = 0.4f;
        await UniTask.Delay(TimeSpan.FromSeconds(3));
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
        
        foreach (Data_Center one in winners)
        {
            if (!one.FightDataRef.IsDead.Value)
            {
                one._MyBehaviorRunner.ChangeState("Victory");
            }
        }
        Time.timeScale = 1f;

        var arenaFightOver = UILayerLoader.Load<ArenaFightOver>();
        arenaFightOver.Step1Anim();
        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }
}
