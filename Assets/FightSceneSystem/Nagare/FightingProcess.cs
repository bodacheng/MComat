using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace FightScene
{
    public class FightingProcess : FSceneProcess
    {
        readonly IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();

        public FightingProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.Fighting;
            nextProcessStep = SceneStep.FightOver;
            EelementsInherit(_NetFightScene);
        }
        
        public override bool CanEnterOtherProcess()
        {
            return fightLogger.gameOver.Value;
        }

        public override void ProcessEnter()
        {
            DicAdd<Team, List<Data_Center>>.Add(AllMembers, Team.player1, RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values);
            DicAdd<Team, List<Data_Center>>.Add(AllMembers, Team.player2, RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            BoundaryControllByGod.target.AllMembers = AllMembers;
            fightLogger.ReadyToLog(AllMembers);
            foreach (KeyValuePair<Team, List<Data_Center>> _set in AllMembers)
            {
                foreach (Data_Center _char in _set.Value)
                {
                    _char.Sensor.TeamMembers = AllMembers;
                }
            }
            FightScene.FightCanvas.gameObject.SetActive(true);
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
            FightScene.PreparingCanvas.gameObject.SetActive(false);
            FightScene.PressedStartButton();
        }

        public override void ProcessEnd()
        {
            FightScene.FightCanvas.gameObject.SetActive(false);
            FightScene.PreparingCanvas.gameObject.SetActive(false);
            mainProcessRunner.Run(FinalMoment(fightLogger.getWinner()));
        }

        public override void LocalUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                FightScene.PauseScene();
            }
            RealTimeGameProcessManager.target.FightingStepProcess();
        }

        IEnumerator FinalMoment(Team winner)
        {
            Time.timeScale = 0.4f;
            yield return new WaitForSeconds(2f);

            List<Data_Center> winners = new List<Data_Center>();
            if (winner == Team.player1)
                winners = AllMembers[Team.player1];
            if (winner == Team.player2)
                winners = AllMembers[Team.player2];

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