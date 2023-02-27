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
    
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        UILayerLoader.SetHanger(t);
        AppSetting.Load();
        AppSetting.BGMSource = audioSource;
        AppSetting.BGMSource.volume = AppSetting.Value.BgmVolume;
        OnStart().Forget();
    }
    
    async UniTask OnStart()
    {
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            () =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    (() =>
                    {
                        SceneManager.LoadScene(0);
                    }), 
                " 下载错误。请检查网络 ");
            },
            starter.DownLoadLabels
        );
        
        if (bytes > 0)
        {
            DownLoadConfirm("Download size : " + bytes / 1048576 + "MB" + "\n\n" + "Start to download");
        }
        else
        {
            Go(); // no asset to download.begin directly 
        }
    }
    
    void DownLoadConfirm(string msg)
    {
        PopupLayer.ArrangeConfirmWindow(
            async ()=>
            {
                HighLightLayer.DarkOff(Color.white, 0);
                UILayerLoader.Load<ProgressLayer>();
                await AddressablesLogic.ResourcePrepareProcess(
                    () =>
                    {
                        UILayerLoader.Remove<ProgressLayer>();
                        Go();
                    },
                    (x,f) =>
                    {
                        if (f == 0)
                            ProgressLayer.LoadingPercent(x, f, false);
                        else
                            ProgressLayer.LoadingPercent(x, f, true);
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
            titleBgLayer.Setup(false);
            var titleScreenLayer = UILayerLoader.Load<TitleScreenLayer>();
            titleScreenLayer.Initialise();
        }
    }
}
