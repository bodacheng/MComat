using UnityEngine.Playables;

namespace FightScene
{
    public class StoryProcess : FSceneProcess
    {
        bool AutoMoveToNext;
        public StoryProcess()
        {
            Step = SceneStep.StoryBeforeFight;
            nextProcessStep = SceneStep.CountDown;
        }
        
        public override void ProcessEnter()
        {
            BoundaryControlByGod.target.ChangeMagicRingRadius(20f);
            if (NetFightScene.Fight.beforeFightStory != null)
            {
                AutoMoveToNext = false;
                NetFightScene.target.playableDirector.playableAsset = NetFightScene.Fight.beforeFightStory;
                NetFightScene.target.playableDirector.stopped += CanGoNext;
                NetFightScene.target.playableDirector.Play();
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