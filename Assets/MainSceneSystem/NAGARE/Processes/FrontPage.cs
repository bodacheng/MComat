using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab.ClientModels;

public class FrontPage : MSceneProcess
{
    void AccountInfoFinished(bool value)
    {
        missionWatcher.Finish("accountInfoFinished", value);
    }
    
    void UserDataLoadFinished(bool value)
    {
        missionWatcher.Finish("userDataLoadFinished", value);
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
        Inherit(PreScene.target);
    }

    FrontLayer frontLayer;
    void EnterProcess()
    {
        frontLayer = UILayerLoader.Load(PreScene.target.T, "FrontLayer") as FrontLayer;
        frontLayer.Initialise(PreScene.target);
        
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        //_CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);

        string focusInstanceID;
        if (PreScene.target._focusing != null && dataAccess.Units.Get(PreScene.target._focusing.id) != null)
        {
            focusInstanceID = PreScene.target._focusing.id;
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
        frontLayer._connector.ShowMyModel(focusInstanceID);
        
        UpperInfoBar.Open(() => PreScene.target.trySwitchToStep(MainSceneStep.Setting, true), 
            () => PreScene.target.trySwitchToStep(MainSceneStep.MailBox));
        
        SetLoaded(true);
    }
    
    public override void ProcessEnter()
    {
        ProgressLayer.Loading(">", PreScene.target.T);
        PlayFabReadClient.GetUserData(
            new GetUserDataRequest
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId
            }, UserDataLoadFinished);

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
                "userDataLoadFinished", "itemsLoadFinished", "statisticsFinished", 
                "userReadOnlyDataLoadLoaded", "arcadeTFinished", "arenaTFinished", "accountInfoFinished"
            },
            () =>
            {
                ProgressLayer.Close();
                if (PlayerAccountInfo.Me.TitleDisplayName == null)
                {
                    NickNameLayer.Open(
                        (x) =>
                        {
                            PopupLayer.ArrangeConfirmWindow(
                                PreScene.target.T,
                                () =>
                                {
                                    PlayFabReadClient.UpdateUserTitleDisplayName(
                                        x,
                                        (x) =>
                                        {
                                            UILayerLoader.Remove("NickNameLayer");
                                            EnterProcess();
                                        },
                                        () =>
                                        {
                                            PopupLayer.ArrangeWarnWindow(PreScene.target.T,"Network Error");
                                        }
                                    );
                                }, 
                            "Set as your nick name?");
                        }
                    );
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
        if (frontLayer != null)
            GameObject.Destroy(frontLayer.gameObject);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        UpperInfoBar.Close();
    }
}
