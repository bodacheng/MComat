using System.Collections;
using UnityEngine;

namespace FightScene
{
    public class CountDownProcess : FSceneProcess
    {
        float startTimestamp = 3f;
        bool AutoMoveToNext;
        public CountDownProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.CountDown;
            nextProcessStep = SceneStep.Fighting;
            EelementsInherit(_NetFightScene);
        }
        
        public override void ProcessEnter()
        {
            startTimestamp = 3f;
            AutoMoveToNext = false;
            BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
            FightScene.mainProcessRunner.Run(BeforeFightCountDown());
        }
                
        IEnumerator BeforeFightCountDown()
        {
            FightScene.CountDown.gameObject.SetActive(true);
            while (startTimestamp > 0)
            {
                startTimestamp -= Time.deltaTime;
                FightScene.CountDown.text = "" + (1 + (int)(startTimestamp));
                yield return null;
            }
            FightScene.CountDown.gameObject.SetActive(false);
            AutoMoveToNext = true;
            yield break;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return AutoMoveToNext;
        }
    }
}