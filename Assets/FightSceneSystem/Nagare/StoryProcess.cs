using UnityEngine.Playables;

namespace FightScene
{
    public class StoryProcess : NagareProcess
    {
        public StoryProcess(NetFightScene _NetFightScene, FightSceneProcessesRunner fightSceneProcessesRunner)
        {
            thisProcessStep = SceneStep.StoryBeforeFight;
            nextProcessStep = SceneStep.CountDown;
            EelementsInherit(_NetFightScene, fightSceneProcessesRunner);
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

        public override void LocalUpdate()
        {
            //播放timeline途中
        }
    }
}