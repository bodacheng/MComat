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
            //CameraMode nowC = RealTimeGameProcessManager.target._CameraManager.CModeDic[C_Mode.OneVOne];
            //if (nowC is OneVOneMode)
            //{
            //    ((OneVOneMode)nowC).xzMax = 100f;
            //}
            startTimestamp = 3f;
            AutoMoveToNext = false;
            BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
            NetFightScene.target.mainProcessRunner.Run(BeforeFightCountDown());
        }
        
        IEnumerator BeforeFightCountDown()
        {
            //RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            NetFightScene.target.CountDown.gameObject.SetActive(true);
            while (startTimestamp > 0)
            {
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