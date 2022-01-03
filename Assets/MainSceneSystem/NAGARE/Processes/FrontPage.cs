using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using PlayFab.ClientModels;

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

    MainTop mainTop;
    PopupLayer popupLayer;
    IEnumerator EnterProcess()
    {
        mainTop = UILayerLoader.Load(PreScene.target.T, "MainTop") as MainTop;
        mainTop.Initialise(PreScene.target);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.2f, 0.1f);
        //MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);

        string focus_instanceID = TeamSet.Default.GetInstanceIdOnPos(0);
        if (focus_instanceID == null)
        {
            foreach (KeyValuePair<string, UnitInfo> keyValuePair in MyMonsters.Dic)
            {
                focus_instanceID = keyValuePair.Key;
                break;
            }
        }
        PreScene.target.SetFocusingUnit(focus_instanceID);//确立focusing角色
        yield return PreScene.target.modelShower.ShowMyModel(focus_instanceID);
        //UnitOptionLayer.target.RefreshMemberDetailPageByFocusingChar();
        UpperInfoBar.Open(PreScene.target.OpenSetting, () => PreScene.target.trySwitchToStep(10));
        popupLayer.LoadingFinished();
    }
    
    public override void ProcessEnter()
    {
        popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.Loading(">");
        
        PlayFabReadClient.GetUserData(
            new GetUserDataRequest()
            {
                PlayFabId = Account._AccInfo.playerID,
                Keys = new List<string>() { "PlayerName" }
            },UserDataLoadFinished);
        PlayFabReadClient.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        PlayFabReadClient.GetStatistics(StatisticsLoadFinished);

        //AccountCharsSet.LoadTutorial();
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        PlayFabReadClient.LoadTeamSet("arcade", ArcadeTFinished);

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
        if (mainTop != null)
            GameObject.Destroy(mainTop.gameObject);

        missionWatcher.DisposeAll();
        UserDataLoadFinished(0);
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);
        StatisticsLoadFinished(0);

        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        UpperInfoBar.Close();
        BigButtonRender.target.TestOff();
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.22f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
        {
            PreScene.target.modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            PreScene.target.modelShower.CFollowCharZ();
        }
    }
}
