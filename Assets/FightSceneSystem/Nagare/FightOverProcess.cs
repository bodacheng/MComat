using UniRx;
using System.Linq;
using System.Collections.Generic;
using Api.Dto.Model;
using dataAccess;

namespace FightScene
{
    public class FightOverProcess : FSceneProcess
    {
        public FightOverProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.FightOver;
            nextProcessStep = SceneStep.FightSummary;
            EelementsInherit(_NetFightScene);
        }
        
        public override bool CanEnterOtherProcess()
        {
            return fightOverControl.CanGotoSummary.Value;
        }

        public override void ProcessEnter()
        {
            fightOverControl.FightOverCanvas.gameObject.SetActive(true);
            switch (fightLogger.getWinner())
            {
                case Team.player1:
                    mainProcessRunner.Run(fightOverControl.WINProcess());//这里是要根据情况的。。
                    break;
                case Team.player2:
                    mainProcessRunner.Run(fightOverControl.LoseProcess());//这里是要根据情况的。。
                    break;
            }
            
            switch (FightSceneNote.nextBattle._fightEventType)
            {
                case FightEventType.Arena:
                    mainProcessRunner.Run(fightOverControl.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    break;
                case FightEventType.Quest:
                    mainProcessRunner.Run(fightOverControl.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
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
                    mainProcessRunner.Run(fightOverControl.ShowSKillSets(RealTimeGameProcessManager.target.FightTeam1));//这里是要根据情况的。。
                    break;
            }
        }
    }
}