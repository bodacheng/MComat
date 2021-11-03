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
    
    public void EnterProcess(string teammode)
    {
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        PreScene.target.TeamEditor.INITeamPosButtons(teammode);
        if (MemberDetail.target._focusing != null)
            PreScene.target.TeamEditor._nineForShow.ShowStones_Acc(MemberDetail.target._focusing.id);
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(true);
        UnitsLayer layer = UnitsLayer.Open();
        layer.DisplayMonsterIcons(true);
        layer.SetUnitsIconOnClick((x) => PreScene.target.TeamEditor.MonsterIconButton(x, teamMode));
        PageTo.Go(MainSceneStep.TeamEditFront);
    }
    
    public override void ProcessEnter<T>(T mode)
    {
        teamMode = mode as string;
        EnterProcess(teamMode);
    }
    
    public override void ProcessEnd()
    {
        UnitsLayer.Close();
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
                        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
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
                        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
                    },
                    () => { PreScene.ReturnToLobby("返回大厅？");}
                );
                break;
        }
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
