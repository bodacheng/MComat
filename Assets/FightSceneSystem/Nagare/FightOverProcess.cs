using System.Collections.Generic;
using Api.Dto.Model;
using dataAccess;
using System.Collections;

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
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
        }
        
        public override void ProcessEnter()
        {
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
                    foreach (CharDataInfo charDataInfo in RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef.Values)
                    {
                        List<string> mystoneids = new List<string>();
                        List<SkillStoneOfPlayerInfoModel> mystones = MySkillStonesReader.GetMonsterEquipingStones(charDataInfo.monsterOfPlayerId);
                        for (int i = 0; i < mystones.Count; i++)
                        {
                            mystoneids.Add(mystones[i].skillStoneOfPlayerId);
                        }
                        mainProcessRunner.Run(RewardManager.ExpUpForStones(mystoneids, 1000f));
                    }
                    break;
                case FightEventType.Self: 
                    mainProcessRunner.Run(FightOverControl.target.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    break;
                case FightEventType.SkillTest:
                    mainProcessRunner.Run(SKillTestReload());
                    break;
            }
        }
        
        IEnumerator SKillTestReload()
        {
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef)
            {
                keyValuePair.Value._NineAndTwo = StagesManager.BalanceStyle("human", 1);
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
            }
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam2.CharDataInfoRef)
            {
                keyValuePair.Value._NineAndTwo = StagesManager.BalanceStyle("human", 1);
                CharConfig _CharConfig = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(keyValuePair.Value.ResourceID));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value._NineAndTwo, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
            }
            this.FightScene.LocalGameRestart();
        }
    }
}