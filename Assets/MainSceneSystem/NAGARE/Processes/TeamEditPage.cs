using mainMenu;
using dataAccess;
using System.Collections.Generic;
using DummyLayerSystem;

public class TeamEditPage : MSceneProcess
{
    string _teamMode;
    private PosKeySet defaultPosKeySetBefore;

    private TeamEditLayer teamEditLayer;
    private TeamSingleSelectLayer teamSingleSelectLayer;
    
    void TeamSaveFinished(bool value)
    {
        missionWatcher.Finish("teamSavedFinished", value);
    }
    
    public TeamEditPage()
    {
        Step = MainSceneStep.TeamEditFront;
    }
    
    void EnterProcess(string teamMode)
    {
        int currentIndex;
        var unitsLayer = UILayerLoader.Load<UnitsLayer>();
        switch (teamMode)
        {
            case "arcade":
                teamSingleSelectLayer = UILayerLoader.Load<TeamSingleSelectLayer>();
                teamSingleSelectLayer.Ini(teamMode, Save, TeamSet.Legal, PlayerAccountInfo.Me.tutorialProgress != "Finished");
                unitsLayer.SetDisplayUnitIconsAfterAction(() =>
                {
                    unitsLayer.SetUnitsIconOnClick(
                        (x) => 
                            teamSingleSelectLayer.ChangeTeamPos(x, 0,this._teamMode)
                    );
                });
                currentIndex = teamSingleSelectLayer.transform.GetSiblingIndex();
                // 只有当当前索引大于0时，才能向上移动
                if (currentIndex > 0)
                {
                    teamSingleSelectLayer.transform.SetSiblingIndex(currentIndex - 1);
                }
                break;
            default:
                teamEditLayer = UILayerLoader.Load<TeamEditLayer>();
                teamEditLayer.Ini(teamMode, Save, TeamSet.Legal, PlayerAccountInfo.Me.tutorialProgress != "Finished");
                unitsLayer.SetDisplayUnitIconsAfterAction(() =>
                {
                    unitsLayer.SetUnitsIconOnClick((x) => teamEditLayer.UnitIconClick(x, this._teamMode));
                });
                currentIndex = teamEditLayer.transform.GetSiblingIndex();
                // 只有当当前索引大于0时，才能向上移动
                if (currentIndex > 0)
                {
                    teamEditLayer.transform.SetSiblingIndex(currentIndex - 1);
                }
                break;
        }
        
        unitsLayer.IniUnitIcons(dataAccess.Units.Dic, true, true);
        if (PreScene.target.Focusing != null)
        {
            // Just wanna show a model when enter team edit page
            teamEditLayer?.UnitIconClick(PreScene.target.Focusing.id, this._teamMode);
            unitsLayer.Selected.Value = null;
        }

        if (teamMode == "arcade")
        {
            defaultPosKeySetBefore = TeamSet.Default.Clone();
            var currentSelected = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(0);
            teamSingleSelectLayer.ChangeTeamPos(currentSelected, 0, this._teamMode);
            unitsLayer.Selected.Value = currentSelected;
        }
        if (teamMode == "origin")
        {
            defaultPosKeySetBefore = TeamSet.Origin.Clone();
        }
        if (teamMode == "gangbang")
        {
            defaultPosKeySetBefore = TeamSet.Gangbang.Clone();
        }
        unitsLayer.transform.SetAsLastSibling();
        SetLoaded(true);
    }
    
    public override void ProcessEnter<T>(T mode)
    {
        _teamMode = mode as string;
        EnterProcess(_teamMode);
    }
    
    public override void ProcessEnter()
    {
        EnterProcess(_teamMode);
    }
    
    public override void ProcessEnd()
    {
        if (_teamMode == "arcade")
            TeamSet.Default = defaultPosKeySetBefore;
        if (_teamMode == "origin")
            TeamSet.Origin = defaultPosKeySetBefore;
        if (_teamMode == "gangbang")
            TeamSet.Gangbang = defaultPosKeySetBefore;
        UILayerLoader.Remove<UnitsLayer>();
        UILayerLoader.Remove<TeamEditLayer>();
        UILayerLoader.Remove<TeamSingleSelectLayer>();
    }
    
    void Save()
    {
        ProgressLayer.Loading(string.Empty);
        switch (_teamMode)
        {
            case "arena":
                missionWatcher = new MissionWatcher(
                    new List<string>() { "teamSavedFinished" },
                    () =>
                    {
                        ReturnLayer.POP();
                        ProgressLayer.Close();
                    }
                );
                
                bool qualified = TeamSet.Legal(_teamMode);
                if (qualified)
                {
                    CloudScript.ArenaDefendTeamSave(TeamSet.Arena3V3.LoadTeamDic(), TeamSaveFinished);
                }
                else
                {
                    TeamSaveFinished(true);
                }
                break;
            case "arcade":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    ()=>
                    {
                        ReturnLayer.POP();
                        ProgressLayer.Close();
                    }
                );
                TeamSet.SaveTeamSet(_teamMode, TeamSaveFinished);
                defaultPosKeySetBefore = TeamSet.Default;
                break;
            case "gangbang":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    ()=>
                    {
                        ReturnLayer.POP();
                        ProgressLayer.Close();
                    }
                );
                TeamSet.SaveTeamSet(_teamMode, TeamSaveFinished);
                defaultPosKeySetBefore = TeamSet.Gangbang;
                break;
            case "origin":
                missionWatcher = new MissionWatcher(
                    new List<string>() {
                        "teamSavedFinished"
                    },
                    ()=>
                    {
                        ReturnLayer.POP();
                        ProgressLayer.Close();
                    }
                );
                TeamSet.SaveTeamSet(_teamMode, TeamSaveFinished);
                defaultPosKeySetBefore = TeamSet.Origin;
                break;
        }
    }
}
