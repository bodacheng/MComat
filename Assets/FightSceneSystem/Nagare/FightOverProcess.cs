using System;
using DummyLayerSystem;
using UnityEngine;

namespace FightScene
{
    public class FightOverProcess : FSceneProcess
    {
        public FightOverProcess()
        {
            Step = SceneStep.FightOver;
        }
        
        void EnterProcess()
        {
            // 竞技场结束：显示排名变化？
            // quest结束：显示技能石经验获得情况和报酬信息？
            // 自我战斗结束：显示战斗分析？
            // 技能测试：显示战斗分析？
            Debug.Log(" winner id " + FightLogger.value.GetWinnerId());
            
            switch (NetFightScene.Fight.EventType)
            {
                case FightEventType.Arena:
                    if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabId)
                    {
                        CloudScript.ArenaPointUp(
                            PlayerAccountInfo.Me.arenaPoint,
                            NetFightScene.Fight.Team2ArenaPoint,
                            (x,y, z) =>
                            {
                                var a = ArenaFightOver.Open();
                                a.Step2Anim();
                                a.ShowArenaPoint(x,y);
                                a.ShowAward(z,0);
                            }
                        );
                    }
                    //FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);
                break;
                case FightEventType.Quest:
                    if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabId)
                    {
                        CloudScript.ArcadeProgress(
                            NetFightScene.Fight.ID,
                            result =>
                            {
                                var jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                                var level = jsonResult.ContainsKey("progressLevel") ? jsonResult["progressLevel"] : 0;
                                var reward_GD = jsonResult.ContainsKey("gold") ? jsonResult["gold"] : 0;
                                var reward_DIA = jsonResult.ContainsKey("diamond") ? jsonResult["diamond"] : 0;
                                
                                int levelInt = Convert.ToInt32(level);
                                PlayerAccountInfo.Me.ArcadeProcess = levelInt;
                                int reward_GD_Int = Convert.ToInt32(reward_GD);
                                int reward_GM_Int = Convert.ToInt32(reward_DIA);
                                
                                var a = ArenaFightOver.Open();
                                a.ShowAward(reward_GD_Int, reward_GM_Int);
                            }
                        );
                    }
                    else
                    {
                        var cc = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArcadeFightResult") as ArenaFightOver;
                        cc.ShowAward(0, 0);
                    }
                    break;
                case FightEventType.Self:
                    CommonFightResult c = UILayerLoader.Load(NetFightScene.target.T.gameObject, "CommonFightResult") as CommonFightResult;
                    c.Initialise(NetFightScene.target.ReturnToFront, 
                        () =>
                        {
                            LocalGameRestart();
                            UILayerLoader.Remove("CommonFightResult");
                        });
                    //c.ShowSKillSets(FightingStepLayer.target.team1UI, c.GetIconAndSKillShowUISetT());
                    break;
                case FightEventType.SkillTest:
                    SkillTestReload();
                    break;
            }
            
            SingleAssignmentDisposableCleaner.Clear();
        }
        
        public override void ProcessEnter()
        {
            EnterProcess();
        }
        
        public override void ProcessEnd()
        {
        }
        
        void LocalGameRestart()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        }
        
        void SkillTestReload()
        {
            RTFightManager.target.ClearUnits();
            NetFightScene.Fight = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            LocalGameRestart();
        }
    }
}