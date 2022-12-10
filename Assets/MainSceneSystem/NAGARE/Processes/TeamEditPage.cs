using System;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using DummyLayerSystem;

public class TeamEditPage : MSceneProcess
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
    }
    
    void EnterProcess(string teamMode)
    {
        var teamEditLayer = UILayerLoader.Load<TeamEditLayer>();
        teamEditLayer.Ini(teamMode, Save, Legal);
        
        var unitsLayer = UILayerLoader.Load<UnitsLayer>();
        unitsLayer.SetDisplayUnitIconsAfterAction(() =>
        {
            unitsLayer.SetUnitsIconOnClick((x) => teamEditLayer.UnitIconClick(x, this.teamMode));
            unitsLayer.DisableLackSkillUnitIcon();
        });
        unitsLayer.DisplayUnitIcons(dataAccess.Units.Dic, true);
        if (PreScene.target.Focusing != null)
        {
            // Just wanna show a model when enter team edit page
            teamEditLayer.UnitIconClick(PreScene.target.Focusing.id, this.teamMode);
            unitsLayer.Selected.Value = null;
        }
        
        SetLoaded(true);
    }
    
    public override void ProcessEnter<T>(T mode)
    {
        teamMode = mode as string;
        EnterProcess(teamMode);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UnitsLayer>();
        UILayerLoader.Remove<TeamEditLayer>();
    }

    private Action extraArcadeTeamEditSuccess;
    public void SetExtraArcadeTeamEditSuccess(Action extraArcadeTeamEditSuccess)
    {
        this.extraArcadeTeamEditSuccess = extraArcadeTeamEditSuccess;
    }

    private bool Legal(string teamMode)
    {
        bool qualified = true;
        int unitCount = 0;

        PosKeySet targetTeamSet = null;
        switch (teamMode)
        {
            case "arena":
                targetTeamSet = TeamSet.Arena3V3;
                break;
            case "arcade":
                targetTeamSet = TeamSet.Default;
                break;
        }
        
        foreach (var set in targetTeamSet.PosNumsWithLocalKeys)
        {
            if (set.instanceID != null && dataAccess.Units.Get(set.instanceID) != null)
            {
                qualified = qualified && (Stones.GetEquippingStones(set.instanceID).Count == 9);
                unitCount += 1;
            }
            else
            {
                qualified = false;
            }
            if (!qualified)
                break;
        }
        
        switch (teamMode)
        {
            case "arena":
                qualified = qualified && unitCount == 3;
                break;
            case "arcade":
                qualified = qualified && unitCount > 0;
                break;
        }
        return qualified;
    }
    
    void Save()
    {
        ProgressLayer.Loading(">");
        switch (teamMode)
        {
            case "arena":
                missionWatcher = new MissionWatcher(
                    new List<string>() {"arenaDefendSaved", "teamSavedFinished"},
                    ProgressLayer.Close,
                    () =>
                    {
                        PreScene.ReturnToLobby();
                        ProgressLayer.Close();
                    }
                );
                
                bool qualified = Legal(teamMode);
                if (qualified)
                {
                    CloudScript.ArenaDefendTeamSave(TeamSet.ToDic(TeamSet.Arena3V3) , ArenaDefendSaved);
                }
                else
                {
                    ArenaDefendSaved(true);
                }
                break;
            case "arcade":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    ()=>
                    {
                        extraArcadeTeamEditSuccess?.Invoke();
                        ProgressLayer.Close();
                    },
                    () =>
                    {
                        PreScene.ReturnToLobby();
                        ProgressLayer.Close();
                    }
                );
                break;
        }
        TeamSet.SaveTeamSet(teamMode, TeamSaveFinished);
    }
}
