using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightScene
{
    public class FightOverProcess : FSceneProcess
    {
        public FightOverProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.FightOver;
            EelementsInherit(_NetFightScene);
        }
                
        IEnumerator EnterProcess()
        {
            yield return FinalMomentAnim(fightLogger.GetWinner());
            FightScene.SkillLog(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values, RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(true);
            switch (fightLogger.GetWinner())
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
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                    break;
                case FightEventType.Quest:
                    yield return FightOverControl.target.ShowRewards(999, 999);
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                    List<string> stoneids = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                    yield return RewardManager.ExpUpForStones(stoneids, 100);
                    FightScene.CheckNextArcadeLevel();
                    break;
                case FightEventType.Self:
                    yield return FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1);//这里是要根据情况的。。
                    List<string> stoneidss = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                    yield return RewardManager.ExpUpForStones(stoneidss, 100);
                    break;
                case FightEventType.SkillTest:
                    yield return SKillTestReload();
                    break;
            }
        }
        
        public override void ProcessEnter()
        {
            mainProcessRunner.Run(EnterProcess());
        }
        
        public override void ProcessEnd()
        {
            HurtObjectManager.ClearCurrent();
            FightOverControl.target.Step2.gameObject.SetActive(false);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
            foreach(NineForShow nineForShow in FightOverControl.target.NineForShows)
            {
                nineForShow.ClearCurrent();
            }
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
                        keyValuePair.Value._NineAndTwo = NineAndTwo.BalanceStyle("human", 1);
                        break;
                    case 3:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RangedStyle("human", 1);
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
                        keyValuePair.Value._NineAndTwo = NineAndTwo.BalanceStyle("human", 1);
                        break;
                    case 3:
                        keyValuePair.Value._NineAndTwo = NineAndTwo.RangedStyle("human", 1);
                        break;
                }
                
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
                i++;
            }
            FightScene.LocalGameRestart();
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