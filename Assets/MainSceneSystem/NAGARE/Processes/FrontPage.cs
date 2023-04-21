using mainMenu;
using dataAccess;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;

public class FrontPage : MSceneProcess
{
    void UserReadOnlyDataLoadFinished(bool value)
    {
        missionWatcher.Finish("userReadOnlyDataLoadLoaded", value);
    }
    
    void StatisticsLoadFinished(bool value)
    {
        missionWatcher.Finish("statisticsFinished", value);
    }
    
    void MailCatalogFinished(bool value)
    {
        missionWatcher.Finish("mailCatalogFinished", value);
    }
    
    void UnitCatalogFinished(bool value)
    {
        missionWatcher.Finish("unitCatalogFinished", value);
    }
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    void ArcadeTFinished(bool value)
    {
        missionWatcher.Finish("arcadeTFinished", value);
    }
    
    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
    }
    
    FrontLayer _frontLayer;
    bool _askedIfLinkDevice;

    void EnterProcess()
    {
        if (PlayerAccountInfo.Me.tutorialProgress == "Started")
        {
            var titleBgLayer = UILayerLoader.Load<TitleBgLayer>();
            titleBgLayer.Setup(true, _EnterProcess);
        }
        else
        {
            _EnterProcess();
        }
    }
    
    void _EnterProcess()
    {
        TutorialRunner.Main.TutorialCheck();
        
        _frontLayer = UILayerLoader.Load<FrontLayer>();
        _frontLayer.Initialise(PreScene.target);
        
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
        _frontLayer.ShowMyModel(focusInstanceID).Forget();
        
        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(PlayerAccountInfo.Me.TitleDisplayName,
            () => PreScene.target.trySwitchToStep(MainSceneStep.Setting), 
            () => PreScene.target.trySwitchToStep(MainSceneStep.MailBox),
            () => PreScene.target.trySwitchToStep(MainSceneStep.ShopTop));
        
        // If account isn't linked to device, ask if link. Only ask once
        if (PlayerAccountInfo.Me.currentLinkedDeviceId != PlayFabReadClient.CustomId && !_askedIfLinkDevice)
        {
            _askedIfLinkDevice = true;
            var askIfLinkDeviceLayer = UILayerLoader.Load<AskIfLinkDeviceLayer>();
            askIfLinkDeviceLayer.Initialise(
                () =>
                {
                    PlayFabReadClient.LinkDevice(
                        () =>
                        {
                            PopupLayer.ArrangeWarnWindow("Account linked to device.");
                            PlayerAccountInfo.Me.currentLinkedDeviceId = PlayFabReadClient.CustomId;
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
        
        if (PlayerAccountInfo.Me.tutorialProgress == "Finished" && Stones.TooManyStones())
        {
            _frontLayer.PlsClickBtn("stones");
        }
        
        SetLoaded(true);
    }
    
    public override void ProcessEnter()
    {
        ProgressLayer.Loading(">");
        PlayFabReadClient.GetBasicReadOnlyData(UserReadOnlyDataLoadFinished);
        PlayFabReadClient.GetStatistics(StatisticsLoadFinished);
        
        //AccountCharsSet.LoadTutorial();
        PlayFabReadClient.GetMailCatalogItems(PlayFabSetting._MailCatalog, MailCatalogFinished);
        PlayFabReadClient.GetMailCatalogItems(PlayFabSetting._UnitCatalog, UnitCatalogFinished);
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        PlayFabReadClient.LoadTeamSet("arcade", ArcadeTFinished);
        
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "mailCatalogFinished","unitCatalogFinished","itemsLoadFinished", "statisticsFinished", 
                "userReadOnlyDataLoadLoaded", "arcadeTFinished"
            },
            () =>
            {
                ProgressLayer.Close();
                if (PlayerAccountInfo.Me.TitleDisplayName == null)
                {
                    BackGroundPS.target.ChangeBGByElement(Element.lightMagic);
                    SettingPage.SetNickName((_) => EnterProcess(), false);
                }
                else
                {
                    EnterProcess();
                }
            }
        );
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<FrontLayer>();
        UILayerLoader.Remove<UpperInfoBar>();
    }
}
