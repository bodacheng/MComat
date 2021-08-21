using dataAccess;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

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
                                ArenaFightOver a = UILayerLoader.Load(NetFightScene.target.T, "ArenaFightOver") as ArenaFightOver;
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
                        ArenaFightOver a = UILayerLoader.Load(NetFightScene.target.T, "ArenaFightOver") as ArenaFightOver;
                        a.Initialise(FightOverControl.target.ReturnToFront);
                    }
                    //FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);
                break;
                case FightEventType.Quest:
                    if (FightLogger.target.GetWinner() == Team.player1)
                    {
                        CloudScript.ArcadeProgress(
                            NetFightScene.Fight.ID.ToString(),
                            result =>
                            {
                                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                                object level, reward_GD, reward_DIA;
                                jsonResult.TryGetValue("progressLevel", out level);
                                jsonResult.TryGetValue("reward_GD", out reward_GD);
                                jsonResult.TryGetValue("reward_DIA", out reward_DIA);
                                
                                string levelstring = level.ToString();
                                string reward_GDstring = reward_GD != null ? reward_GD.ToString() : "0";
                                string reward_DIAstring = reward_DIA != null ? reward_DIA.ToString() : "0";
                                
                                int levelInt, reward_GDInt, reward_DIAInt;
                                int.TryParse(levelstring, out levelInt) ;
                                Account._AccInfo.ArcadeProcess = levelInt;
                                int.TryParse(reward_GDstring, out reward_GDInt) ;
                                int.TryParse(reward_DIAstring, out reward_DIAInt) ;
                                
                                void LoadNextLevel()
                                {
                                    NetFightScene.Fight = ArcadeManager.ArcadeStages[NetFightScene.Fight.ID + 1].stageConfig;
                                    FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
                                }
                                ArcadeFightResult cc = UILayerLoader.Load(NetFightScene.target.T, "ArcadeFightResult") as ArcadeFightResult;
                                cc.Initialise(FightOverControl.target.ReturnToFront, 
                                    FightOverControl.target.LocalGameRestart, 
                                    LoadNextLevel,
                                reward_GDInt, reward_DIAInt);
                            },
                            () =>
                            {
                            }
                        );
                    }
                    break;
                case FightEventType.Self:
                    CommonFightResult c = UILayerLoader.Load(NetFightScene.target.T, "CommonFightResult") as CommonFightResult;
                    c.Initialise(FightOverControl.target.ReturnToFront, FightOverControl.target.LocalGameRestart);
                    FightOverControl.target.ShowSKillSets(RTFightManager.target.team1,c.GetIconAndSKillShowUISetT());
                    break;
                case FightEventType.SkillTest:
                    NetFightScene.target.StartCoroutine(NetFightScene.target.SKillTestReload());
                    break;
            }
            
            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.team1.TeamMembers.GetValues());
            data_Centers.AddRange(RTFightManager.target.team2.TeamMembers.GetValues());
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
            EnterProcess();
        }
        
        public override void ProcessEnd()
        {
            HurtObjectManager.ClearCurrent();
            FightOverControl.target.Clear();
        }
    }
}