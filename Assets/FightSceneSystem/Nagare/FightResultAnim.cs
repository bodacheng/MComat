using System;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;

namespace FightScene
{
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
            await FinalMomentAnim();
        }

        public override bool CanEnterOtherProcess()
        {
            return animEnd;
        }

        async UniTask FinalMomentAnim()
        {
            animEnd = false;
            Time.timeScale = 0.4f;
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            var winners = new List<Data_Center>();
            switch (FightLogger.value.GetWinnerTeam())
            {
                case Team.player1:
                    winners = RTFightManager.Target.team1.teamMembers.GetValues();
                    break;
                case Team.player2:
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
            var arenaFightOver = UILayerLoader.Load<ArenaFightOver>(false, null, true);

            async UniTask EndPart()
            {
                try
                {
                    arenaFightOver.Step1Anim();
                    await UniTask.Delay(TimeSpan.FromSeconds(1));
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[FightResultAnim] Result animation failed: {ex.Message}");
                }
                finally
                {
                    animEnd = true;
                }
            }

            async UniTask LoadNarrativeSafely(bool shortMessage, bool story)
            {
                try
                {
                    if (shortMessage)
                    {
                        await arenaFightOver.LoadShortMessage();
                    }
                    if (story)
                    {
                        await arenaFightOver.LoadStory();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[FightResultAnim] Narrative unavailable: {ex.Message}");
                }
            }
            
            switch (FightLoad.Fight.EventType)
            {
                case FightEventType.Quest:
                    if (FightLogger.value.IsLocalPlayerWinner(RTFightManager.playerTeam, PlayerAccountInfo.Me.PlayFabId))
                    {
                        await LoadNarrativeSafely(true, !CommonSetting.DemoMode);
                    }
                    arenaFightOver.Setup();
                    await EndPart();
                    break;
                case FightEventType.Event:
                    if (FightLogger.value.IsLocalPlayerWinner(RTFightManager.playerTeam, PlayerAccountInfo.Me.PlayFabId))
                    {
                        await LoadNarrativeSafely(false, !CommonSetting.DemoMode);
                    }
                    await EndPart();
                    break;
                default:
                    await EndPart();
                    break;
            }
        }
    }
}
