using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using UniRx;

public class TeamEditPage : MainSceneProcess
{
    string teamMode;

    ReactiveProperty<int> team3v3LoadFinished = new ReactiveProperty<int>(0);
    void Team3V3LoadFinished(int value)
    {
        team3v3LoadFinished.Value = value;
    }
    ReactiveProperty<int> teamArenaLoadFinished = new ReactiveProperty<int>(0);
    void TeamArenaLoadFinished(int value)
    {
        teamArenaLoadFinished.Value = value;
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


        UnityEngine.Events.UnityAction unityAction = () =>
        {
            PreScene.target.TeamEditor.AddHeroIconFeaturesToMonsterBox(teammode);// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        };
        mainProcessRunner.RunAsQueued(MonsterBox.DisplayMonsterIcons(true), unityAction);
    }
    
    public TeamEditPage()
    {
        Step = MainSceneStep.TeamEditFront;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter<T>(T mode)
    {
        teamMode = mode as string;
        switch (teamMode)
        {
            case "arena":
                TeamSet.LoadTeamSet("arena", TeamArenaLoadFinished);
                missionWatcher = new MissionWatcher(
                    new List<ReactiveProperty<int>>() {
                        teamArenaLoadFinished
                    },
                    () => EnterProcess(teamMode),
                    () => { return; }
                );
                break;
            case "arcade":
                TeamSet.LoadTeamSet("arcade", Team3V3LoadFinished);
                missionWatcher = new MissionWatcher(
                    new List<ReactiveProperty<int>>() {
                        team3v3LoadFinished
                    },
                    () => EnterProcess(teamMode),
                    () => { return; }
                );
                break;
        }
    }
    
    public override void ProcessEnd()
    {
        TeamSet.SaveTeamSet(teamMode);

        missionWatcher.DisposeAll();
        Team3V3LoadFinished(0);
        TeamArenaLoadFinished(0);
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
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
