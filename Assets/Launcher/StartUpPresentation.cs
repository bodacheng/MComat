using System;
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
    [SerializeField] AudioSource uiAudioSource;
    [SerializeField] Canvas canvas;
    
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
        PosCal.Canvas = this.canvas;
        UILayerLoader.SetHanger(t);
        AppSetting.Load();
        AppSetting.BGMSource = audioSource;
        AppSetting.BGMSource.volume = AppSetting.Value.BgmVolume;
        AppSetting.UiAudioSource = uiAudioSource;
        AppSetting.UiAudioSource.volume = AppSetting.Value.EffectsVolume;
        OnStart().Forget();
    }
    
    async UniTask OnStart()
    {
        var wText = String.Empty;
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.English:
                wText = "Checking program version...";
                break;
            case SystemLanguage.Japanese:
                wText = "プログラムのバージョンを確認中...";
                break;
            case SystemLanguage.Chinese:
                wText = "正在检测程序版本";
                break;
        }
        
        ProgressLayer.Loading(wText);
        bool needToUpdate = await AddressablesLogic.VersionConfirm();
        ProgressLayer.Close();
        
        if (needToUpdate)
        {
            var text = string.Empty;
            switch (AppSetting.Value.Language)
            {
                case SystemLanguage.English:
                    text = "New version detected, please update the program";
                    break;
                case SystemLanguage.Japanese:
                    text = "新しいバージョンが検出されました。プログラムをアップデートしてください";
                    break;
                case SystemLanguage.Chinese:
                    text = "监测到新版本，请更新程序";
                    break;
                default:
                    text = "New version detected, please update the program";
                    break;
            }
            PopupLayer.ArrangeWarnWindow(() =>
            {
                #if UNITY_ANDROID
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.MCombat.BO");
                #endif
                
                #if UNITY_IOS
                Application.OpenURL("https://apps.apple.com/app/%E9%AD%94%E6%99%B6%E9%97%98%E5%A3%AB/id1640836926");
                #endif
            }, text);
            return;
        }
        
        var rText = string.Empty;
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.English:
                rText = "Inspecting resources";
                break;
            case SystemLanguage.Japanese:
                rText = "リソースを検査中";
                break;
            case SystemLanguage.Chinese:
                rText = "检查资源中";
                break;
        }
        ProgressLayer.Loading(rText);
        
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            () =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    (
                        () =>
                        {
                            SceneManager.LoadScene(0);
                        }
                    ),
                "Download Failed"
                );
            },
            starter.DownLoadLabels
        );
        
        ProgressLayer.Close();
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
                var imageBg = UILayerLoader.Load<ImageBg>();
                imageBg.Setup();
                UILayerLoader.Load<ProgressLayer>();
                await AddressablesLogic.ResourcePrepareProcess(
                    () =>
                    {
                        UILayerLoader.Remove<ProgressLayer>();
                        UILayerLoader.Remove<ImageBg>();
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
