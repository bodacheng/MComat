using UnityEngine;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using UniRx;

public class TeamEditPage : MainSceneProcess
{
    string teamMode;
    ReactiveProperty<int> teamSavedFinished = new ReactiveProperty<int>(0);
    void TeamSaveFinished(int value)
    {
        teamSavedFinished.Value = value;
    }
    ReactiveProperty<int> arenaDefendSaved = new ReactiveProperty<int>(0);
    void ArenaDefendSaved(int value)
    {
        arenaDefendSaved.Value = value;
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
                    new List<ReactiveProperty<int>>() {
                        arenaDefendSaved, teamSavedFinished
                    },
                    () => {
                        TeamSaveFinished(0);
                        ArenaDefendSaved(0); 
                        missionWatcher.DisposeAll();
                    },
                    () => { PreScene.ReturnToLobby("返回大厅？"); }
                );
                break;
            case "arcade":
                missionWatcher = new MissionWatcher(
                    new List<ReactiveProperty<int>>() {
                        teamSavedFinished
                    },
                    () => {                 
                        TeamSaveFinished(0);
                        ArenaDefendSaved(0);
                        missionWatcher.DisposeAll();
                    },
                    () => { PreScene.ReturnToLobby("返回大厅？");}
                );
                break;
        }
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, ModelShower._nearClipPlane);
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
