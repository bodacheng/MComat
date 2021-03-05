using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Form;
using dataAccess;
using Api.Dto.Model;

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
            switch (FightSceneNote.nextBattle._fightEventType)
            {
                case FightEventType.Arena:
                    RequestRewardForm form1 = new RequestRewardForm
                    {
                        userId = AccountSet._AccInfo.playerID,
                        fightEventType = FightEventType.Arena,
                        eventNum = 0 // 对手的id
                    };
                    IEnumerator requestReward1 = RewardManager.RequestRewardsExaution(
                        form1,
                        model => {
                            int diamond = model.Diamond;
                            int gold = model.Gold;
                            FightOverControl.target.ShowRewards(gold, diamond);
                        },
                        model => {
                            // 再次请求报酬？？
                        },
                        Setting.Language
                    );
                    yield return requestReward1;
                    // 玩家rank远程已经进行了加分处理，这时候只需要以玩家id为key检索一下，本地刷新就可以
                    yield return Arena.GetPlayerRankInfo();
                    FightOverControl.target.rankInfo.gameObject.SetActive(true);
                    FightOverControl.target.rankInfo.RankPointChange(0,0);
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                break;
                case FightEventType.Quest:
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                    List<string> stoneids = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                    RequestRewardForm form = new RequestRewardForm
                    {
                        userId = AccountSet._AccInfo.playerID,
                        fightEventType = FightEventType.Quest,
                        eventNum = FightSceneNote.nextBattle.LocalFightID,
                        StoneOfPlayerIDs = stoneids
                    };
                    if (FightLogger.target.GetWinner() == Team.player1)
                    {
                        IEnumerator requestReward = RewardManager.RequestRewardsExaution(
                            form,
                            model => {
                                int diamond = model.Diamond;
                                int gold = model.Gold;
                                FightOverControl.target.ShowRewards(gold, diamond);
                                for (int i = 0; i < model.stonesToGetExp.Count; i++)
                                {
                                    SkillStoneOfPlayerInfoModel one = MySkillStones.Get(model.stonesToGetExp[i].skillStoneOfPlayerId);
                                    one.EXP = model.stonesToGetExp[i].EXP;
                                }
                            },
                            model => {
                                // 再次请求报酬？？
                            },
                            Setting.Language
                        );
                        yield return requestReward;
                        FightOverControl.target.CheckNextArcadeLevel();
                    }
                break;
                case FightEventType.Self:
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                    List<string> stoneidss = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                break;
                case FightEventType.SkillTest:
                    yield return SKillTestReload();
                break;
            }

            List<Data_Center> data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values);
            data_Centers.AddRange(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
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
        
        IEnumerator SKillTestReload()
        {
            int i = 0;
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef)
            {
                switch(i)
                {
                    case 0:
                    case 1:
                    case 2:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RandomSkillSet("human", null, 1, false);
                        break;
                    case 3:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RandomSkillSet("human", null, 1, false);
                        break;
                }
                
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
                i++;
            }
            i = 0;
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam2.CharDataInfoRef)
            {
                switch(i)
                {
                    case 0:
                    case 1:
                    case 2:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RandomSkillSet("human", null, 1, false);
                        break;
                    case 3:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RandomSkillSet("human", null, 1, false);
                        break;
                }
                
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
                i++;
            }
            FightOverControl.target.LocalGameRestart();
        }
    }
}