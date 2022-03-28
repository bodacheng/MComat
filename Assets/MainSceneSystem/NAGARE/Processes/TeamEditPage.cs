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
        EelementsInherit(PreScene.target);
    }
    
    void EnterProcess(string teammode)
    {
        TeamEditLayer teamEditLayer = TeamEditLayer.Open(teammode);
        if (PreScene.target._focusing != null)
            teamEditLayer._nineForShow.ShowStones_Acc(PreScene.target._focusing.id);
        
        UnitsLayer unitsLayer = UnitsLayer.Open();
        unitsLayer.DisplayUnitIcons(true);
        unitsLayer.SetUnitsIconOnClick((x) => teamEditLayer.UnitIconClick(x, teamMode));
        
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
        
        TeamSet.SaveTeamSet(teamMode, TeamSaveFinished);
        switch (teamMode)
        {
            case "arena":
                CloudScript.ArenaDefendTeamSave(TeamSet.ToDic(TeamSet.Arena3V3) , ArenaDefendSaved);
                missionWatcher = new MissionWatcher(
                    new List<string>() {"arenaDefendSaved", "teamSavedFinished"},
                    () => {},
                    () => { PreScene.ReturnToLobby("返回大厅？"); }
                );
                break;
            case "arcade":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    () => {},
                    () => { PreScene.ReturnToLobby("返回大厅？");}
                );
                break;
        }
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, 10);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
