using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryProcess : NagareProcess
{
    bool canGoNext = false;
    
    public StoryProcess(NetFightScene _NetFightScene)
    {
        this._NetFightScene = _NetFightScene;
        this.thisProcessStep = SceneStep.StoryBeforeFight;
        this.nextProcessStep = SceneStep.CountDown;
    }

    public override bool canEnterNextProcess()
    {
        return this.canGoNext;
    }
    
    public override void ProcessEnter()
    {
        if (GoingToLoadFight.Instance.nextBattle.beforefightstory != null)
        {
            canGoNext = false;
            //_NetFightScene._FightTalksRunner.timelinefile = GoingToLoadFight.Instance.nextBattle.beforefightstory;
            _NetFightScene._FightTalksRunner.playableDirector.stopped += CanGoNext;
            _NetFightScene._FightTalksRunner.runStoryTimeLine(GoingToLoadFight.Instance.nextBattle.beforefightstory);            
            //_NetFightScene._FightTalksRunner.playableDirector.Play();
        }else{
            canGoNext = true;
        }
    }
    
    void CanGoNext(PlayableDirector _a)
    {
        this.canGoNext = true;
    }
    
    
    public override void ProcessEnd()
    {
        _NetFightScene._FightTalksRunner.RPGTalk.gameObject.SetActive(false);
        this.canGoNext = false;
    }
    
    public override void localUpdate()
    {
        //播放timeline途中
    }
}
