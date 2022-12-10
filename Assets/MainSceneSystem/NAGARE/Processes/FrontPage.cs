using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab;
using PlayFab.ClientModels;

public class FrontPage : MSceneProcess
{
    void AccountInfoFinished(bool value)
    {
        missionWatcher.Finish("accountInfoFinished", value);
    }
    
    void UserReadOnlyDataLoadFinished(bool value)
    {
        missionWatcher.Finish("userReadOnlyDataLoadLoaded", value);
    }
    
    void StatisticsLoadFinished(bool value)
    {
        missionWatcher.Finish("statisticsFinished", value);
    }
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    void ArenaTFinished(bool value)
    {
        missionWatcher.Finish("arenaTFinished", value);
    }
    
    void ArcadeTFinished(bool value)
    {
        missionWatcher.Finish("arcadeTFinished", value);
    }
    
    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
    }

    FrontLayer frontLayer;
    bool askedIfLinkDevice = false;
    void EnterProcess()
    {
        TutorialRunner.Main.TutorialCheck();

        frontLayer = UILayerLoader.Load<FrontLayer>();
        frontLayer.Initialise(PreScene.target);
        
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        //_CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);

        string focusInstanceID;
        if (PreScene.target.Focusing != null && dataAccess.Units.Get(PreScene.target.Focusing.id) != null)
        {
            focusInstanceID = PreScene.target.Focusing.id;
        }
        else
        {
            focusInstanceID = TeamSet.Default.GetInstanceIdOnPos(0);
            if (focusInstanceID == null)
            {
                foreach (var keyValuePair in dataAccess.Units.Dic)
                {
                    focusInstanceID = keyValuePair.Key;
                    break;
                }
            }
        }
        
        PreScene.target.SetFocusingUnit(focusInstanceID);
        frontLayer.CamConnector.ShowMyModel(focusInstanceID);

        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(() => PreScene.target.trySwitchToStep(MainSceneStep.Setting, true), 
            () => PreScene.target.trySwitchToStep(MainSceneStep.MailBox));

        // If account isn't linked to device, ask if link. Only ask once
        if (PlayerAccountInfo.Me.currentLinkedDeviceId != PlayFabReadClient.CustomId && !askedIfLinkDevice)
        {
            askedIfLinkDevice = true;
            var askIfLinkDeviceLayer = UILayerLoader.Load<AskIfLinkDeviceLayer>();
            askIfLinkDeviceLayer.Initialise(
                () =>
                {
                    PlayFabReadClient.LinkDevice(
                        () =>
                        {
                            PopupLayer.ArrangeWarnWindow("Account linked to device.");
                            PlayerAccountInfo.Me.currentLinkedDeviceId = PlayFabReadClient.CustomId;
                        },
                        (x) =>
                        {
                            PopupLayer.ArrangeWarnWindow("绑定失败"+ x.Error);
                        }
                    );
                    UILayerLoader.Remove<AskIfLinkDeviceLayer>();
                },
                () =>
                {
                    PopupLayer.ArrangeWarnWindow("U can link your account to this device later in setting.");
                    UILayerLoader.Remove<AskIfLinkDeviceLayer>();
                }
            );
        }
        
        SetLoaded(true);
    }
    
    public override void ProcessEnter()
    {
        ProgressLayer.Loading(">");
        PlayFabReadClient.GetAccountInfo(AccountInfoFinished);
        PlayFabReadClient.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        PlayFabReadClient.GetStatistics(StatisticsLoadFinished);
        
        //AccountCharsSet.LoadTutorial();
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        PlayFabReadClient.LoadTeamSet("arcade", ArcadeTFinished);
        
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "itemsLoadFinished", "statisticsFinished", 
                "userReadOnlyDataLoadLoaded", "arcadeTFinished", "arenaTFinished", "accountInfoFinished"
            },
            () =>
            {
                ProgressLayer.Close();
                if (PlayerAccountInfo.Me.TitleDisplayName == null)
                {
                    SettingPage.SetNickName((_) => EnterProcess(), false);
                }
                else
                {
                    EnterProcess();
                }
            },
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<FrontLayer>();
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        UILayerLoader.Remove<UpperInfoBar>();
    }
}
