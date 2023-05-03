using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter starter;
    [SerializeField] RectTransform t;
    [SerializeField] bool frontSceneFight;
    [SerializeField] AudioSource audioSource;
    
    void OpenAppStoreLink()
    {
        string storeLink = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            storeLink = "https://apps.apple.com/app/idYOUR_APP_ID";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            storeLink = "https://play.google.com/store/apps/details?id=YOUR_PACKAGE_NAME";
        }

        if (!string.IsNullOrEmpty(storeLink))
        {
            Application.OpenURL(storeLink);
        }
        else
        {
            Debug.LogError("Unable to open App Store link.");
        }
    }
    
    void Start()
    {
        //Screen.SetResolution(1920, 1080, true);
        UILayerLoader.SetHanger(t);
        AppSetting.Load();
        AppSetting.BGMSource = audioSource;
        AppSetting.BGMSource.volume = AppSetting.Value.BgmVolume;
        OnStart().Forget();
    }
    
    async UniTask OnStart()
    {
        bool needToUpdate = await AddressablesLogic.VersionConfirm();
        if (needToUpdate)
        {
            PopupLayer.ArrangeWarnWindow(
                OpenAppStoreLink,
                Translate.Get("NeedToUpdate"));
            return;
        }
        
        Debug.Log(" 没有强制升级程序 ");
        
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            () =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    (() =>
                    {
                        SceneManager.LoadScene(0);
                    }), 
                "Download Failed");
            },
            starter.DownLoadLabels
        );
        
        if (bytes > 0)
        {
            DownLoadConfirm("Download Size :" + bytes / 1048576 + "MB" + "\n\n" + "Start to download", bytes);
        }
        else
        {
            Go(); // no asset to download.begin directly 
        }
    }
    
    void DownLoadConfirm(string msg, float wholeBytes)
    {
        PopupLayer.ArrangeConfirmWindow(
            async ()=>
            {
                HighLightLayer.DarkOff(Color.white, 0);
                var titleBgLayer= UILayerLoader.Load<TitleBgLayer>();
                titleBgLayer.Setup(0.67f);
                UILayerLoader.Load<ProgressLayer>();
                await AddressablesLogic.ResourcePrepareProcess(
                    () =>
                    {
                        UILayerLoader.Remove<ProgressLayer>();
                        UILayerLoader.Remove<TitleBgLayer>();
                        Go();
                    },
                    (x) =>
                    {
                        ProgressLayer.LoadingPercent(x, AddressablesLogic.DownloadedBytes / wholeBytes);
                    },
                    starter.DownLoadLabels
                );
            },
            Application.Quit,
            msg
        );
    }

    async void Go()
    {
        HighLightLayer.Close();
        await starter.Initialise();
        if (frontSceneFight && PlayFabReadClient.DontShowFrontFight != "true")
        {
            starter.EnterFrontScene();
        }
        else
        {
            await AppSetting.PlayBGM(CommonSetting.StartThemeAddressKey);
            var titleBgLayer= UILayerLoader.Load<TitleBgLayer>();
            titleBgLayer.Setup(1);
            titleBgLayer.Rotate(false);
            var titleScreenLayer = UILayerLoader.Load<TitleScreenLayer>();
            titleScreenLayer.Initialise();
        }
    }
}
