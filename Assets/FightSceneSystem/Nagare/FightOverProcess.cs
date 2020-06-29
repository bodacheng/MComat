using System.Collections.Generic;

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
            FightOverControl.target.Step2.gameObject.SetActive(false);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
        }
        
        public override void ProcessEnter()
        {
            FightScene.SkillLog(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values,RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(true);
            switch (fightLogger.GetWinner())
            {
                case Team.player1:
                    mainProcessRunner.Run(FightOverControl.target.WINProcess());//这里是要根据情况的。。
                    break;
                case Team.player2:
                    mainProcessRunner.Run(FightOverControl.target.LoseProcess());//这里是要根据情况的。。
                    break;
            }
            
            switch (FightSceneNote.nextBattle._fightEventType)
            {
                case FightEventType.Arena:
                    mainProcessRunner.Run(FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    break;
                case FightEventType.Quest:
                    mainProcessRunner.Run(FightOverControl.target.ShowRewards(999, 999));
                    mainProcessRunner.Run(FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    List<string> stoneids = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                    mainProcessRunner.Run(RewardManager.ExpUpForStones(stoneids,100));
                    break;
                case FightEventType.Self:
                    mainProcessRunner.Run(FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    List<string> stoneidss = RealTimeGameProcessManager.target.FightTeam1.GetAllUsingStoneOfAcc();
                    mainProcessRunner.Run(RewardManager.ExpUpForStones(stoneidss,100));
                    break;
                case FightEventType.SkillTest:
                    mainProcessRunner.Run(SKillTestReload());
                    break;
            }
        }
        
        System.Collections.IEnumerator SKillTestReload()
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
    }
}