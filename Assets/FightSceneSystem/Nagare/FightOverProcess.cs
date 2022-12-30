using System;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;
using PlayFab.ClientModels;

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
            
            switch (FightScene.Fight.EventType)
            {
                case FightEventType.Arena:
                    if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabId)
                    {
                        CloudScript.ArenaPointUp(
                            PlayerAccountInfo.Me.arenaPoint,
                            FightScene.Fight.Team2ArenaPoint,
                            (x,y, z) =>
                            {
                                var a = UILayerLoader.Load<ArenaFightOver>();
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
                            FightScene.Fight.ID,
                            result =>
                            {
                                var jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                                var level = jsonResult.ContainsKey("progressLevel") ? jsonResult["progressLevel"] : 0;
                                var rewardGd = jsonResult.ContainsKey("gold") ? jsonResult["gold"] : 0;
                                var rewardDia = jsonResult.ContainsKey("diamond") ? jsonResult["diamond"] : 0;
                                var firstTime = jsonResult.ContainsKey("firstTime") ? jsonResult["firstTime"] : false;
                                
                                var levelInt = Convert.ToInt32(level);
                                PlayerAccountInfo.Me.arcadeProcess = levelInt;
                                var rewardGdInt = Convert.ToInt32(rewardGd);
                                var rewardGmInt = Convert.ToInt32(rewardDia);
                                var firstTimeBool = (bool)firstTime;
                                
                                var a = UILayerLoader.Load<ArenaFightOver>();
                                a.ShowAward(rewardGdInt, rewardGmInt);

                                if (levelInt == 1 && firstTimeBool)
                                {
                                    PopupLayer.ArrangeWarnWindowUnitIcon(" tetsuya 加入队伍 ", "2");
                                }
                            }
                        );
                        
                        if (FightScene.Fight.ID == "1")
                        {
                            PlayerAccountInfo.Me.tutorialProgress = "StageOneFinished";
                            PlayFabReadClient.UpdateUserData(
                                new UpdateUserDataRequest()
                                {
                                    Data = new Dictionary<string, string>()
                                    {
                                        { "TutorialProgress", "StageOneFinished" }
                                    }
                                },
                                () => {},
                                PreScene.ReturnToLobby
                            );
                            ReturnLayer.ReturnMissionList.Clear(); // 直接回到 front scene
                        }
                    }
                    else
                    {
                        var cc = UILayerLoader.Load<ArenaFightOver>();
                        cc.ShowAward(0, 0);
                    }
                    break;
                case FightEventType.Self:
                    var c = UILayerLoader.Load<CommonFightResult>();
                    c.Initialise(FightScene.target.ReturnToFront, 
                        () =>
                        {
                            LocalGameRestart();
                            UILayerLoader.Remove<CommonFightResult>();
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
            RTFightManager.Target.ClearUnits();
            FightScene.Fight = FightInfo.RandomSkillTestStage(TeamMode.Rotation);
            LocalGameRestart();
        }
    }
}