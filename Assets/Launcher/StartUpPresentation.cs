using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter Starter;
    [SerializeField] RectTransform T;
    [SerializeField] bool FrontSceneFight;
    
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        UILayerLoader.SetHanger(T);
        OnStart().Forget();
    }

    async UniTask OnStart()
    {
        await HighLightLayer.LoadBg();
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            () =>
            {
                PopupLayer.ArrangeConfirmWindow(
                    (() =>
                    {
                        SceneManager.LoadScene(0);
                    }), 
                " 下载错误。请检查网络 ");
            }
        );
        
        if (bytes > 0)
        {
            DownLoadConfirm("Download size : " + bytes / 1048576 + "MB" + "\n\n" + "Start to download");
        }
        else
        {
            Go();
        }
    }
    
    void DownLoadConfirm(string msg)
    {
        PopupLayer.ArrangeConfirmWindow(
            T.gameObject,
            async ()=>
            {
                HighLightLayer.DarkOff(Color.white, 0, true);
                UILayerLoader.Load<ProgressLayer>();
                await AddressablesLogic.ResourcePrepareProcess(
                    Go,
                    (x,f) =>
                    {
                        if (f == 0)
                            ProgressLayer.LoadingPercent(x, f, false);
                        else
                            ProgressLayer.LoadingPercent(x, f, true);
                    }
                );
            },
            () =>
            {
                Application.Quit();
            },
            msg
        );
    }

    void Go()
    {
        HighLightLayer.Close();
        Starter.Initialise();
        if (FrontSceneFight)
        {
            Starter.EnterFrontScene();
        }
        else
        {
            var TitleScreenLayer = UILayerLoader.Load<TitleScreenLayer>();
            TitleScreenLayer.Initialise();
        }
    }
}
