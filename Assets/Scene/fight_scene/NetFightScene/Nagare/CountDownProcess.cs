using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownProcess : NagareProcess
{
    FightTalksRunner _FightTalksRunner;
    CharsManager _CharSetManager;
    
    public CountDownProcess(NetFightScene _NetFightScene)
    {
        this.thisProcessStep = SceneStep.CountDown;
        this.nextProcessStep = SceneStep.Fighting;
        this._NetFightScene = _NetFightScene;
        this._FightTalksRunner = this._NetFightScene._FightTalksRunner;
        this._CharSetManager = this._NetFightScene._CharSetManager;
    }

    public override bool canEnterNextProcess()
    {
        return _FightTalksRunner.FightTalksEnded();
    }
    
    public override void ProcessEnter()
    {

    }
    
    public override void ProcessEnd()
    {
        this._NetFightScene._FightTalksRunner.resetAll(3f);
    }
    
    public override void localUpdate()
    {
        _FightTalksRunner.runBeforeFight(_CharSetManager.TeamMembers[_CharSetManager.EnemyTeamConfig.myTeam]);
    }
}
