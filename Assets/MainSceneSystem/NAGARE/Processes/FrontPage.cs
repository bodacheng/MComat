using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using Api.Dto.Model;

public class FrontPage : MainSceneProcess
{
    ReactiveProperty<int> userDataLoadFinished = new ReactiveProperty<int>(0);
    void UserDataLoadFinished(int value)
    {
        userDataLoadFinished.Value = value;
    }

    ReactiveProperty<int> userReadOnlyDataLoadLoaded = new ReactiveProperty<int>(0);
    void UserReadOnlyDataLoadFinished(int value)
    {
        userReadOnlyDataLoadLoaded.Value = value;
    }

    ReactiveProperty<int> statisticsFinished = new ReactiveProperty<int>(0);
    void StatisticsLoadFinished(int value)
    {
        statisticsFinished.Value = value;
    }

    ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    ReactiveProperty<int> arenaTFinished = new ReactiveProperty<int>(0);
    void ArenaTFinished(int value)
    {
        arenaTFinished.Value = value;
    }

    ReactiveProperty<int> arcadeTFinished = new ReactiveProperty<int>(0);
    void ArcadeTFinished(int value)
    {
        arcadeTFinished.Value = value;
    }

    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
        EelementsInherit(PreScene.target);
    }

    public IEnumerator EnterProcess()
    {
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.2f, 0.1f);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);

        string focusLocalid = TeamSet.Default.GetMonsterOfPlayerIdOnPos(0);
        if (focusLocalid == null)
        {
            foreach (KeyValuePair<string, MonsterOfPlayerInfo> keyValuePair in MyMonsters.Dic)
            {
                focusLocalid = keyValuePair.Key;
                break;
            }
        }
        yield return MemberDetail.target.SetMemberDetailFocusingChar(focusLocalid);//确立focusing角色
        yield return ModelShower.target.ShowMyModel(focusLocalid);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(true);
        UpperInfoBar.target.T.gameObject.SetActive(true);
    }
    
    public override void ProcessEnter()
    {
        Account.GetUserData(UserDataLoadFinished);
        Account.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        Account.GetStatistics(StatisticsLoadFinished);

        //AccountCharsSet.LoadTutorial();
        ItemLoader.LoadAll(ItemsLoadFinished);

        TeamSet.LoadTeamSet("arena", ArenaTFinished);
        TeamSet.LoadTeamSet("arcade", ArcadeTFinished);

        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                userDataLoadFinished, itemsLoadFinished, statisticsFinished, userReadOnlyDataLoadLoaded, arcadeTFinished, arenaTFinished
            },
            () =>
            {
                mainProcessRunner.RunAsQueued(EnterProcess());
            },
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnd()
    {
        missionWatcher.DisposeAll();
        UserDataLoadFinished(0);
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);
        StatisticsLoadFinished(0);

        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        UpperInfoBar.target.T.gameObject.SetActive(false);
        BigButtonRender.target.TestOff();
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.22f, ModelShower._nearClipPlane);
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
