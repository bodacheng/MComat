using System;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        Inherit(PreScene.target);
    }
    
    void EnterProcess(string teamMode)
    {
        var teamEditLayer = UILayerLoader.Load<TeamEditLayer>();
        teamEditLayer.INI(teamMode, Save);
        
        var unitsLayer = UILayerLoader.Load<UnitsLayer>();
        unitsLayer.SetDisplayUnitIconsAfterAction(() =>
        {
            unitsLayer.SetUnitsIconOnClick((x) => teamEditLayer.UnitIconClick(x, this.teamMode));
            unitsLayer.DisableLackSkillUnitIcon();
        });
        unitsLayer.DisplayUnitIcons(dataAccess.Units.Dic, true).Forget();
        if (PreScene.target._focusing != null)
        {
            // Just wanna show a model when enter team edit page
            teamEditLayer.UnitIconClick(PreScene.target._focusing.id, this.teamMode);
            unitsLayer.CancelSelect();
        }
        
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
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

    void Save()
    {
        ProgressLayer.Loading(">", PreScene.target.T);
        TeamSet.SaveTeamSet(teamMode, TeamSaveFinished);
        switch (teamMode)
        {
            case "arena":
                missionWatcher = new MissionWatcher(
                    new List<string>() {"arenaDefendSaved", "teamSavedFinished"},
                    ProgressLayer.Close,
                    () =>
                    {
                        PopupLayer.ArrangeWarnWindow(PreScene.target.T,"network error");
                        ProgressLayer.Close();
                    }
                );
                
                #region 合格认证 
                // 不符合要求的队伍不进行保存
                bool qualified = true;
                int teamCount = 0;
                foreach (var set in TeamSet.Arena3V3.PosNumsWithLocalKeys)
                {
                    if (set.instanceID != null && dataAccess.Units.Get(set.instanceID) != null)
                    {
                        qualified = qualified && (Stones.GetEquippingStones(set.instanceID).Count == 9);
                        teamCount += 1;
                    }
                    else
                    {
                        qualified = false;
                    }
                    if (!qualified)
                        break;
                }
                qualified = qualified && teamCount == 3;
                #endregion
                
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
                        PopupLayer.ArrangeWarnWindow(PreScene.target.T,"network error");
                        ProgressLayer.Close();
                    }
                );
                break;
        }
    }
}
