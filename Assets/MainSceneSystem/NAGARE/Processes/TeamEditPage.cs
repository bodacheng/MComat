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
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        PreScene.target.TeamEditor.INITeamPosButtons(teammode);
        if (MemberDetail.target._focusing != null)
            PreScene.target.TeamEditor._nineForShow.ShowStones_Acc(MemberDetail.target._focusing.InstanceId);
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(true);
        MonsterBox.DisplayMonsterIcons(true);
        PreScene.target.TeamEditor.AddHeroIconFeaturesToMonsterBox(teammode);// 该处理紧随MonsterBox.DisplayMonsterIcons之后
    }
    
    public override void ProcessEnter<T>(T mode)
    {
        teamMode = mode as string;
        EnterProcess(teamMode);
    }
    
    public override void ProcessEnd()
    {
        TeamSet.SaveTeamSet(teamMode, TeamSaveFinished);
        switch (teamMode)
        {
            case "arena":
                TeamSet.ArenaDefendTeamSave(ArenaDefendSaved);
                missionWatcher = new MissionWatcher(
                    new List<ReactiveProperty<int>>() {
                        arenaDefendSaved, teamSavedFinished
                    },
                    () => {
                        TeamSaveFinished(0);
                        ArenaDefendSaved(0); 
                        missionWatcher.DisposeAll();
                        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
                        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
                    },
                    () => { Debug.Log("返回大厅？"); }
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
                        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
                    },
                    () => { Debug.Log("返回大厅？"); }
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
