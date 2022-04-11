using UnityEngine;
using mainMenu;
using dataAccess;
using System.Collections.Generic;

public class TeamEditPage : MainSceneProcess
{
    string teamMode;
    
    void TeamSaveFinished(bool value)
    {
        missionWatcher.Finish("teamSavedFinished", value);
    }
    
    void ArenaDefendSaved(bool value)
    {
        missionWatcher.Finish("arenaDefendSaved", value);
    }
    
    public TeamEditPage()
    {
        Step = MainSceneStep.TeamEditFront;
        Inherit(PreScene.target);
    }
    
    void EnterProcess(string teamMode)
    {
        var teamEditLayer = TeamEditLayer.Open(teamMode);
        if (PreScene.target._focusing != null)
            teamEditLayer._nineForShow.ShowStones_Acc(PreScene.target._focusing.id);
        
        var unitsLayer = UnitsLayer.Open();
        unitsLayer.DisplayUnitIcons(true);
        unitsLayer.SetUnitsIconOnClick((x) => teamEditLayer.UnitIconClick(x, this.teamMode));
        
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
    }
    
    public override void ProcessEnter<T>(T mode)
    {
        teamMode = mode as string;
        EnterProcess(teamMode);
    }
    
    public override void ProcessEnd()
    {
        UnitsLayer.Close();
        TeamEditLayer.Close();
        PopupLayer.Loading(">", PreScene.target.T);
        TeamSet.SaveTeamSet(teamMode, TeamSaveFinished);
        switch (teamMode)
        {
            case "arena":
                CloudScript.ArenaDefendTeamSave(TeamSet.ToDic(TeamSet.Arena3V3) , ArenaDefendSaved);
                missionWatcher = new MissionWatcher(
                    new List<string>() {"arenaDefendSaved", "teamSavedFinished"},
                    PopupLayer.Close,
                    () => {}
                );
                break;
            case "arcade":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    PopupLayer.Close,
                    () => {}
                );
                break;
        }
    }
}
