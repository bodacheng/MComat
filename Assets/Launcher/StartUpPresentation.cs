using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter starter;
    [SerializeField] bool frontSceneFight;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource uiAudioSource;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform safeAreaRect;

    bool startupInProgress;
    bool startupChecksInProgress;
    bool resourceDownloadInProgress;
    
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
        PosCal.SafeAreaRect = safeAreaRect;
        PosCal.TestIni();
        UILayerLoader.SetHanger(safeAreaRect, canvas.transform);
        AppSetting.Load();
        AppSetting.BGMSource = audioSource;
        AppSetting.BGMSource.volume = AppSetting.Value.BgmVolume;
        AppSetting.UiAudioSource = uiAudioSource;
        AppSetting.UiAudioSource.volume = AppSetting.Value.EffectsVolume;
        OnStart().Forget();
    }
    
    async UniTask OnStart()
    {
        if (startupChecksInProgress || resourceDownloadInProgress || startupInProgress)
        {
            return;
        }

        startupChecksInProgress = true;
        var destroyed = this.GetCancellationTokenOnDestroy();
        // This budget covers metadata checks, not the user-approved bulk download.
        using (var checks = CancellationTokenSource.CreateLinkedTokenSource(destroyed))
        {
            checks.CancelAfter(TimeSpan.FromSeconds(45));
            try
            {
                await CheckStartupResources(checks.Token);
            }
            catch (OperationCanceledException) when (destroyed.IsCancellationRequested)
            {
                // The scene is gone; do not create retry UI on its replacement.
            }
            catch (OperationCanceledException exception)
            {
                ShowStartupResourceFailure(new TimeoutException("Startup resource checks exceeded 45 seconds.", exception));
            }
            catch (Exception exception)
            {
                if (!destroyed.IsCancellationRequested)
                    ShowStartupResourceFailure(exception);
            }
            finally
            {
                startupChecksInProgress = false;
            }
        }
    }

    async UniTask CheckStartupResources(CancellationToken cancellationToken)
    {
        string text = String.Empty;
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.English:
                text = "Checking program version...";
                break;
            case SystemLanguage.Japanese:
                text = "プログラムのバージョンを確認中...";
                break;
            case SystemLanguage.Chinese:
                text = "正在检测程序版本";
                break;
        }
        
        ProgressLayer.Loading(text);
        bool needToUpdate = await AddressablesLogic.VersionConfirm(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        ProgressLayer.Close();
        
        if (needToUpdate)
        {
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
        
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.English:
                text = "Inspecting resources";
                break;
            case SystemLanguage.Japanese:
                text = "リソースを検査中";
                break;
            case SystemLanguage.Chinese:
                text = "检查资源中";
                break;
        }
        ProgressLayer.Loading(text);
        
        // 告诉用户检查资源中，其实也把config文件下载了，合起来几十kb而已。
        if (!await AddressablesLogic.DownLoadConfig(cancellationToken))
        {
            throw new InvalidOperationException("The game configuration could not be downloaded.");
        }
        CommonSetting commonSetting = await AddressablesLogic.GetCommonSetting(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        if (commonSetting == null)
        {
            throw new InvalidOperationException("The game configuration could not be loaded.");
        }
        commonSetting.Initialise();

        bool downloadSizeFailed = false;
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            _ => downloadSizeFailed = true,
            commonSetting.DownLoadLabels,
            cancellationToken
        );
        cancellationToken.ThrowIfCancellationRequested();
        if (downloadSizeFailed)
        {
            throw new InvalidOperationException("The required game resources could not be inspected.");
        }
        
        ProgressLayer.Close();
        if (bytes > 0)
        {
            DownLoadConfirm("Download Size :" + Math.Round((double)bytes / 1048576, 3) + "MB" + "\n\n" + "Start to download", 
                bytes, commonSetting.DownLoadLabels);
        }
        else
        {
            Go().Forget(); // no asset to download.begin directly 
        }
    }
    
    void DownLoadConfirm(string msg, float wholeBytes, List<string> downLoadLabels)
    {
        PopupLayer.ArrangeConfirmWindow(
            () => DownloadResources(wholeBytes, downLoadLabels).Forget(),
            Application.Quit,
            msg
        );
    }

    async UniTask DownloadResources(float wholeBytes, List<string> downLoadLabels)
    {
        if (resourceDownloadInProgress || startupInProgress)
        {
            return;
        }

        resourceDownloadInProgress = true;
        var destroyed = this.GetCancellationTokenOnDestroy();
        try
        {
            HighLightLayer.DarkOff(Color.white, 0);
            var imageBg = UILayerLoader.Load<ImageBg>();
            imageBg.Setup();
            UILayerLoader.Load<ProgressLayer>();
            await AddressablesLogic.ResourcePrepareProcess(
                null,
                text =>
                {
                    if (!destroyed.IsCancellationRequested)
                        ProgressLayer.LoadingPercent(text, wholeBytes > 0 ? AddressablesLogic.DownloadedBytes / wholeBytes : 0);
                },
                downLoadLabels,
                destroyed,
                TimeSpan.FromSeconds(60));
            destroyed.ThrowIfCancellationRequested();
            ProgressLayer.Close();
            UILayerLoader.Remove<ImageBg>();
            await Go();
        }
        catch (OperationCanceledException) when (destroyed.IsCancellationRequested)
        {
        }
        catch (Exception exception)
        {
            if (!destroyed.IsCancellationRequested)
                ShowStartupResourceFailure(exception);
        }
        finally
        {
            resourceDownloadInProgress = false;
        }
    }

    void ShowStartupResourceFailure(Exception exception)
    {
        Debug.LogException(exception);
        ProgressLayer.Close();
        UILayerLoader.Remove<ImageBg>();

        string text;
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.Japanese:
                text = "ゲームデータを読み込めませんでした。接続を確認して再試行してください。";
                break;
            case SystemLanguage.Chinese:
                text = "游戏数据加载失败，请检查网络后重试。";
                break;
            default:
                text = "Game data could not be loaded. Check your connection and retry.";
                break;
        }

        PopupLayer.ArrangeConfirmWindow(
            () => OnStart().Forget(),
            Application.Quit,
            text);
    }

    async UniTask Go()
    {
        if (startupInProgress)
        {
            return;
        }

        startupInProgress = true;
        HighLightLayer.Close();
        Application.targetFrameRate = 70;
        FightGlobalSetting.SceneStep = 1;

        ProgressLayer.Loading(GetStartupInitializationText());
        // Let the loading layer render before SteamAPI.Init performs synchronous
        // native work on Unity's main thread.
        await UniTask.NextFrame();

        try
        {
            if (!await starter.Initialise())
            {
                ProgressLayer.Close();
                PopupLayer.ArrangeWarnWindow(
                    Application.Quit,
                    GetSteamInitializationFailureText());
                return;
            }

            if (frontSceneFight && PlayFabReadClient.DontShowFrontFight == "False")
            {
                ProgressLayer.Close();
                starter.EnterFrontScene();
            }
            else
            {
                await AppSetting.PlayBGM(CommonSetting.StartThemeAddressKey);
                var titleBgLayer= UILayerLoader.Load<TitleBgLayer>(true, null, true);
                titleBgLayer.Setup(1);
                titleBgLayer.Rotate(false);
                var titleScreenLayer = UILayerLoader.Load<TitleScreenLayer>(true, null, true);
                titleScreenLayer.Initialise();
                ProgressLayer.Close();
            }
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            ProgressLayer.Close();
            PopupLayer.ArrangeConfirmWindow(
                () => Go().Forget(),
                Application.Quit,
                GetStartupFailureText(exception.Message));
        }
        finally
        {
            startupInProgress = false;
        }
    }

    string GetStartupInitializationText()
    {
        if (!CommonSetting.PcMode)
        {
            switch (AppSetting.Value.Language)
            {
                case SystemLanguage.Japanese:
                    return "ゲームデータを初期化中...";
                case SystemLanguage.Chinese:
                    return "正在初始化游戏数据...";
                default:
                    return "Initializing game data...";
            }
        }

        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.Japanese:
                return "Steamとゲームデータを初期化中...";
            case SystemLanguage.Chinese:
                return "正在初始化Steam和游戏数据...";
            default:
                return "Initializing Steam and game data...";
        }
    }

    string GetSteamInitializationFailureText()
    {
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.Japanese:
                return "Steamの初期化に失敗しました。Steamクライアントを起動し、ゲームを再起動してください。";
            case SystemLanguage.Chinese:
                return "Steam初始化失败。请启动Steam客户端后重新启动游戏。";
            default:
                return "Steam initialization failed. Start the Steam client and restart the game.";
        }
    }

    string GetStartupFailureText(string detail)
    {
        switch (AppSetting.Value.Language)
        {
            case SystemLanguage.Japanese:
                return "ゲームの初期化に失敗しました。再試行してください。\n\n" + detail;
            case SystemLanguage.Chinese:
                return "游戏初始化失败，请重试。\n\n" + detail;
            default:
                return "Game initialization failed. Please try again.\n\n" + detail;
        }
    }
}
