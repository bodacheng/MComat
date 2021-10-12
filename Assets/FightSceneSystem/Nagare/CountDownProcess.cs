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
            SingleThreadProcesser.backup.RunAsQueued(BeforeFightCountDown());
        }
        
        IEnumerator BeforeFightCountDown()
        {
            startTimestamp = 3f;
            AutoMoveToNext = false;
            BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
            //RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            CountDownLayer cd = UILayerLoader.Load
                (NetFightScene.target.T.gameObject, "CountDownLayer") as CountDownLayer;
            yield return cd.BeforeFightCountDown();
            UILayerLoader.Remove("CountDownLayer");
            
            RTFightManager.target.ParaAdjustment(RTFightManager.playerTeam);
            AutoMoveToNext = true;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return AutoMoveToNext;
        }
    }
}