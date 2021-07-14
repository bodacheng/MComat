using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightScene
{
    public class FightOverProcess : FSceneProcess
    {
        public FightOverProcess()
        {
            Step = SceneStep.FightOver;
        }
        
        IEnumerator EnterProcess()
        {
            MobileInputsManager.target.TurnOffButtons();
            yield return FinalMomentAnim(FightLogger.target.GetWinner());
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(true);
            switch (FightLogger.target.GetWinner())
            {
                case Team.player1:
                    yield return FightOverControl.target.WINProcess();
                    break;
                case Team.player2:
                    yield return FightOverControl.target.LoseProcess();
                    break;
            }
            
            // 不同模式下的战斗结束画面应该有个更加利索的分歧处理方式吧。。
            // 竞技场结束：显示排名变化？
            // quest结束：显示技能石经验获得情况和报酬信息？
            // 自我战斗结束：显示战斗分析？
            // 技能测试：显示战斗分析？
            switch (NetFightScene.Fight.GetEventType())
            {
                case FightEventType.Arena:
                    if (FightLogger.target.GetWinner() == Team.player1)
                    {
                        CloudScript.ArenaPointUpBy1(
                            () =>
                            {
                                ArenaFightOver a = UILayerLoader.Load(FightOverControl.target.Step2, "ArenaFightOver") as ArenaFightOver;
                                a.Initialise(FightOverControl.target.ReturnToFront);
                            },
                            () =>
                            {
                                Debug.Log("没能加分成功");
                            }
                        );
                    }
                    else
                    {
                        ArenaFightOver a = UILayerLoader.Load(FightOverControl.target.Step2, "ArenaFightOver") as ArenaFightOver;
                        a.Initialise(FightOverControl.target.ReturnToFront);
                    }
                    //FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);
                break;
                case FightEventType.Quest:
                    if (FightLogger.target.GetWinner() == Team.player1)
                    {
                        FightOverControl.target.CheckNextArcadeLevel();
                    }
                    CommonFightResult cc = UILayerLoader.Load(FightOverControl.target.Step2, "CommonFightResult") as CommonFightResult;
                    cc.Initialise(FightOverControl.target.ReturnToFront, FightOverControl.target.LocalGameRestart);
                    FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1,cc.GetIconAndSKillShowUISetT());
                break;
                case FightEventType.Self:
                    CommonFightResult c = UILayerLoader.Load(FightOverControl.target.Step2, "CommonFightResult") as CommonFightResult;
                    c.Initialise(FightOverControl.target.ReturnToFront, FightOverControl.target.LocalGameRestart);
                    FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1,c.GetIconAndSKillShowUISetT());
                    break;
                case FightEventType.SkillTest:
                    yield return NetFightScene.target.SKillTestReload();
                break;
            }
            
            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.GetValues());
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.GetValues());
            FightOverControl.target.SkillLog(data_Centers);
            foreach (Data_Center one in data_Centers)
            {
                one.CleanClear();
            }
            FightLogger.target.WatchMissionsAbandon();
            SingleAssignmentDisposableCleaner.Clear();
            LoadingCanvas.target.Loading_Canvas.gameObject.SetActive(false);
            FightScenePauseSupport.target.ControlCanvas.gameObject.SetActive(false);
        }
        
        public override void ProcessEnter()
        {
            SingleThreadProcesser.backup.RunAsQueued(EnterProcess());
        }
        
        public override void ProcessEnd()
        {
            HurtObjectManager.ClearCurrent();
            FightOverControl.target.Clear();
        }
        
        // 这纯粹是个动画，没什么必要被这种东西延迟相关的数值处理。
        IEnumerator FinalMomentAnim(Team winner)
        {
            Time.timeScale = 0.4f;
            yield return new WaitForSeconds(2f);
            List<Data_Center> winners = new List<Data_Center>();
            if (winner == Team.player1)
            {
                winners = RealTimeGameProcessManager.target.AllMembers[Team.player1];
            }
            if (winner == Team.player2)
            {
                winners = RealTimeGameProcessManager.target.AllMembers[Team.player2];
            }
            foreach (Data_Center _one in winners)
            {
                if (!_one.IsDead.Value)
                {
                    _one._MyBehaviorRunner.ChangeState("Victory");
                }
            }
            Time.timeScale = 1f;
        }
    }
}