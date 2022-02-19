using dataAccess;
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
        
        void EnterProcess()
        {
            // 竞技场结束：显示排名变化？
            // quest结束：显示技能石经验获得情况和报酬信息？
            // 自我战斗结束：显示战斗分析？
            // 技能测试：显示战斗分析？
            Debug.Log(" winner id " + FightOverControl.target.logger.GetWinnerId());

            switch (NetFightScene.Fight.GetEventType())
            {
                case FightEventType.Arena:
                    if (FightOverControl.target.logger.GetWinnerId() == PlayerAccountInfo.Me.playerID)
                    {
                        CloudScript.ArenaPointUp(
                            () =>
                            {
                                ArenaFightOver a = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArenaFightOver") as ArenaFightOver;
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
                        ArenaFightOver a = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArenaFightOver") as ArenaFightOver;
                        a.Initialise(FightOverControl.target.ReturnToFront);
                    }
                    //FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);
                break;
                case FightEventType.Quest:
                    if (FightOverControl.target.logger.GetWinnerId() == PlayerAccountInfo.Me.playerID)
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
                                    FightOverControl.target.ReturnToFront, 
                                    ()=>
                                    {
                                        FightOverControl.target.LocalGameRestart();
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
                        cc.Initialise(FightOverControl.target.ReturnToFront, 
                             ()=>
                             {
                                 FightOverControl.target.LocalGameRestart();
                                 UILayerLoader.Remove("ArcadeFightResult");
                             },
                             0, 
                             0);
                    }
                    break;
                case FightEventType.Self:
                    CommonFightResult c = UILayerLoader.Load(NetFightScene.target.T.gameObject, "CommonFightResult") as CommonFightResult;
                    c.Initialise(FightOverControl.target.ReturnToFront, 
                        () =>
                        {
                            FightOverControl.target.LocalGameRestart();
                            UILayerLoader.Remove("CommonFightResult");
                        });
                    c.ShowSKillSets(RTFightManager.target.team1, c.GetIconAndSKillShowUISetT());
                    break;
                case FightEventType.SkillTest:
                    NetFightScene.target.StartCoroutine(NetFightScene.target.SKillTestReload());
                    break;
            }
            
            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.Team1Members.GetValues());
            data_Centers.AddRange(RTFightManager.target.Team2Members.GetValues());
            FightOverControl.target.SkillLog(data_Centers);
            foreach (Data_Center one in data_Centers)
            {
                one.CleanClear();
            }
            FightOverControl.target.logger.WatchMissionsAbandon();
            SingleAssignmentDisposableCleaner.Clear();
            FightScenePauseSupport.target.ControlCanvas.gameObject.SetActive(false);
        }
        
        public override void ProcessEnter()
        {
            EnterProcess();
        }
        
        public override void ProcessEnd()
        {
            HurtObjectManager.ClearCurrent();
        }
    }
}