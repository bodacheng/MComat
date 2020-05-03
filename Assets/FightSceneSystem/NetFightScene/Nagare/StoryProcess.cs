using UnityEngine.Playables;

public class StoryProcess : NagareProcess
{
    public StoryProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this._NetFightScene = _NetFightScene;
        this.thisProcessStep = SceneStep.StoryBeforeFight;
        this.nextProcessStep = SceneStep.CountDown;
        this.fightSceneProcessesRunner = fightSceneProcessesRunner;
    }
    
    public override void ProcessEnter()
    {
        if (FightSceneNote.Instance.nextBattle.beforefightstory != null)
        {
            AutoMoveToNext = false;
            //_NetFightScene._FightTalksRunner.timelinefile = GoingToLoadFight.Instance.nextBattle.beforefightstory;
            _NetFightScene._FightTalksRunner.playableDirector.stopped += CanGoNext;
            _NetFightScene._FightTalksRunner.RunStoryTimeLine(FightSceneNote.Instance.nextBattle.beforefightstory);            
            //_NetFightScene._FightTalksRunner.playableDirector.Play();
        }else{
            AutoMoveToNext = true;
        }
    }
    
    void CanGoNext(PlayableDirector _a)
    {
        AutoMoveToNext = true;
    }
    
    
    public override void ProcessEnd()
    {
        _NetFightScene._FightTalksRunner.RPGTalk.gameObject.SetActive(false);
        AutoMoveToNext = false;
    }
    
    public override void LocalUpdate()
    {
        //播放timeline途中
    }
}
