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
        
        public override void ProcessEnd()
        {
            HurtObjectManager.ClearCurrent();
            FightOverControl.target.Step2.gameObject.SetActive(false);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
            foreach(NineForShow nineForShow in FightOverControl.target.NineForShows)
            {
                nineForShow.CloseStoneInfo();
            }
        }
        
        IEnumerator EnterProcess()
        {
            yield return FinalMoment(fightLogger.GetWinner());
            FightScene.SkillLog(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values, RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(true);
            switch (fightLogger.GetWinner())
            {
                case Team.player1:
                    yield return FightOverControl.target.WINProcess();//这里是要根据情况的。。
                    break;
                case Team.player2:
                    yield return FightOverControl.target.LoseProcess();//这里是要根据情况的。。
                    break;
            }
            
            FightScene.CheckNextArcadeLevel();
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
        
        IEnumerator SKillTestReload()
        {
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef)
            {
                keyValuePair.Value._NineAndTwo = StagesManager.BalanceStyle("human", 1);
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
            }
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam2.CharDataInfoRef)
            {
                keyValuePair.Value._NineAndTwo = StagesManager.BalanceStyle("human", 1);
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
            }
            FightScene.LocalGameRestart();
        }
        
        IEnumerator FinalMoment(Team winner)
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