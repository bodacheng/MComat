using System.Collections.Generic;
using DummyLayerSystem;
using UnityEngine;
using Log;

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
            
            switch (NetFightScene.Fight.GetEventType())
            {
                case FightEventType.Arena:
                    if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabUsername)
                    {
                        CloudScript.ArenaPointUp(
                            () =>
                            {
                                var a = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArenaFightOver") as ArenaFightOver;
                                a.Initialise(NetFightScene.target.ReturnToFront);
                            },
                            () =>
                            {
                                Debug.Log("没能加分成功");
                            }
                        );
                    }
                    else
                    {
                        var a = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArenaFightOver") as ArenaFightOver;
                        a.Initialise(NetFightScene.target.ReturnToFront);
                    }
                    //FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);
                break;
                case FightEventType.Quest:
                    if (FightLogger.value.GetWinnerId() == PlayerAccountInfo.Me.PlayFabUsername)
                    {
                        CloudScript.ArcadeProgress(
                            NetFightScene.Fight.ID.ToString(),
                            result =>
                            {
                                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                                int levelInt, reward_GDInt, reward_DIAInt;
                                string levelstring = "0";
                                string reward_GDstring = "0";
                                string reward_DIAstring = "0";
                                if (jsonResult != null)
                                {
                                    object level, reward_GD, reward_DIA;
                                    
                                    jsonResult.TryGetValue("progressLevel", out level);
                                    jsonResult.TryGetValue("gold", out reward_GD);
                                    jsonResult.TryGetValue("diamond", out reward_DIA);
                                    
                                    levelstring = level.ToString();
                                    reward_GDstring = reward_GD != null ? reward_GD.ToString() : "0";
                                    reward_DIAstring = reward_DIA != null ? reward_DIA.ToString() : "0";
                                }
                                
                                int.TryParse(levelstring, out levelInt) ;
                                PlayerAccountInfo.Me.ArcadeProcess = levelInt;
                                int.TryParse(reward_GDstring, out reward_GDInt) ;
                                int.TryParse(reward_DIAstring, out reward_DIAInt) ;
                                
                                ArcadeFightResult cc = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArcadeFightResult") as ArcadeFightResult;
                                cc.Initialise(
                                    NetFightScene.target.ReturnToFront, 
                                    ()=>
                                    {
                                        LocalGameRestart();
                                        UILayerLoader.Remove("ArcadeFightResult");
                                    },
                                    reward_GDInt, reward_DIAInt
                                );
                                Debug.Log("hello"+ cc);
                            },
                            () =>
                            {

                            }
                        );
                    }
                    else
                    {
                        ArcadeFightResult cc = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArcadeFightResult") as ArcadeFightResult;
                        cc.Initialise(NetFightScene.target.ReturnToFront, 
                             ()=>
                             {
                                 LocalGameRestart();
                                 UILayerLoader.Remove("ArcadeFightResult");
                             },
                             0, 
                             0);
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
                    c.ShowSKillSets(FightingStepLayer.target.team1UI, c.GetIconAndSKillShowUISetT());
                    break;
                case FightEventType.SkillTest:
                    SkillTestReload();
                    break;
            }
            
            var data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.team1.TeamMembers.GetValues());
            data_Centers.AddRange(RTFightManager.target.team2.TeamMembers.GetValues());
            HitBoxLogTable.Instance.SkillLog(data_Centers);
            foreach (var one in data_Centers)
            {
                one.CleanClear();
            }
            FightLogger.value.WatchMissionsAbandon();
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