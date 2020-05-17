using UnityEngine.Playables;

namespace FightScene
{
    public class StoryProcess : FSceneProcess
    {
        bool AutoMoveToNext;
        
        public StoryProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.StoryBeforeFight;
            nextProcessStep = SceneStep.CountDown;
            EelementsInherit(_NetFightScene);
        }

        public override void ProcessEnter()
        {
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            BoundaryControllByGod.target.ChangeMagicRingRadius(20f);
            if (FightSceneNote.nextBattle.beforefightstory != null)
            {
                AutoMoveToNext = false;
                FightScene.playableDirector.playableAsset = FightSceneNote.nextBattle.beforefightstory;
                FightScene.playableDirector.stopped += CanGoNext;
                FightScene.playableDirector.Play();
            }
            else
            {
                AutoMoveToNext = true;
            }
        }

        // 参数是timeline编程的一个特殊写法
        void CanGoNext(PlayableDirector _a)
        {
            AutoMoveToNext = true;
        }

        public override void ProcessEnd()
        {
            AutoMoveToNext = false;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return AutoMoveToNext;
        }
    }
}