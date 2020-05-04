using UnityEngine.Playables;
using mainMenu;

public class StoryProcess : NagareProcess
{
    public StoryProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.StoryBeforeFight;
        nextProcessStep = SceneStep.CountDown;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }
    
    public override void ProcessEnter()
    {
        PreScene.Instance._CameraManager.Assign_Camera(C_Mode.RoundBoundary,null);
        if (FightSceneNote.nextBattle.beforefightstory != null)
        {
            AutoMoveToNext = false;
            FightScene.playableDirector.playableAsset = FightSceneNote.nextBattle.beforefightstory;
            FightScene.playableDirector.stopped += CanGoNext;
            FightScene.playableDirector.Play();
        }else{
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
