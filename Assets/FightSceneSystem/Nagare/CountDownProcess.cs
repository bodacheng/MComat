using System.Collections;
using UnityEngine;

namespace FightScene
{
    public class CountDownProcess : FSceneProcess
    {
        float startTimestamp = 3f;
        bool AutoMoveToNext;
        public CountDownProcess()
        {
            Step = SceneStep.CountDown;
            nextProcessStep = SceneStep.Fighting;
        }
        
        public override void ProcessEnter()
        {
            startTimestamp = 3f;
            AutoMoveToNext = false;
            BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
            NetFightScene.target.mainProcessRunner.Run(BeforeFightCountDown());
        }
        
        IEnumerator BeforeFightCountDown()
        {
            NetFightScene.target.CountDown.gameObject.SetActive(true);
            while (startTimestamp > 0)
            {
                if (startTimestamp > 1.3 && startTimestamp < 1.7)
                {
                    //RealTimeGameProcessManager.target._CameraManager.Assign_SToEMode(FightScene.WatchTeam1.position, FightScene.Team1StandPoints[0], 3f, 50f);
                    RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
                }
                startTimestamp -= Time.deltaTime;
                NetFightScene.target.CountDown.text = "" + (1 + (int)(startTimestamp));
                yield return null;
            }
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            NetFightScene.target.CountDown.gameObject.SetActive(false);
            AutoMoveToNext = true;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return AutoMoveToNext;
        }
    }
}