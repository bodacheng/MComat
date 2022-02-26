using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using DummyLayerSystem;
using UniRx;
using PlayFab.ClientModels;

public class FrontPage : MainSceneProcess
{
    readonly ReactiveProperty<int> userDataLoadFinished = new ReactiveProperty<int>(0);
    void UserDataLoadFinished(int value)
    {
        userDataLoadFinished.Value = value;
    }

    readonly ReactiveProperty<int> userReadOnlyDataLoadLoaded = new ReactiveProperty<int>(0);
    void UserReadOnlyDataLoadFinished(int value)
    {
        userReadOnlyDataLoadLoaded.Value = value;
    }

    readonly ReactiveProperty<int> statisticsFinished = new ReactiveProperty<int>(0);
    void StatisticsLoadFinished(int value)
    {
        statisticsFinished.Value = value;
    }

    readonly ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    readonly ReactiveProperty<int> arenaTFinished = new ReactiveProperty<int>(0);
    void ArenaTFinished(int value)
    {
        arenaTFinished.Value = value;
    }

    readonly ReactiveProperty<int> arcadeTFinished = new ReactiveProperty<int>(0);
    void ArcadeTFinished(int value)
    {
        arcadeTFinished.Value = value;
    }

    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
        EelementsInherit(PreScene.target);
    }

    FrontLayer frontLayer;
    IEnumerator EnterProcess()
    {
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        
        frontLayer = UILayerLoader.Load(PreScene.target.T, "FrontLayer") as FrontLayer;
        frontLayer.Initialise(PreScene.target);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.2f, 0.1f);
        //MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        
        var focus_instanceID = TeamSet.Default.GetInstanceIdOnPos(0);
        if (focus_instanceID == null)
        {
            foreach (var keyValuePair in MyMonsters.Dic)
            {
                focus_instanceID = keyValuePair.Key;
                break;
            }
        }
        PreScene.target.SetFocusingUnit(focus_instanceID);//确立focusing角色
        yield return ModelShower.target.ShowMyModel(focus_instanceID);
        //UnitOptionLayer.target.RefreshMemberDetailPageByFocusingChar();
        UpperInfoBar.Open(() => PreScene.target.trySwitchToStep(MainSceneStep.Setting, true), () => PreScene.target.trySwitchToStep(10));
        PopupLayer.Close();
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.Loading(">", PreScene.target.T);
        PlayFabReadClient.GetUserData(
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabUsername,
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
        if (frontLayer != null)
            GameObject.Destroy(frontLayer.gameObject);

        missionWatcher.DisposeAll();
        UserDataLoadFinished(0);
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);
        StatisticsLoadFinished(0);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        UpperInfoBar.Close();
        
        SingleThreadProcesser.backup.RunAsQueued(ModelShower.target.ShowMyModel(null));
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.22f, 10);
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
