using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter Starter;
    [SerializeField] RectTransform T;
    
    void Start()
    {
        OnStart().Forget();
    }

    async UniTask OnStart()
    {
        var bytes = await AddressablesLogic.GetWholeDownLoadSize(
            () =>
            {
                var popupLayer = PopupLayer.Open(T.gameObject);
                popupLayer.ArrangeConfirmWindow((() =>
                {
                    SceneManager.LoadScene(0);
                }), " 下载错误。请检查网络 ");
            }
        );
        
        if (bytes > 0)
        {
            DownLoadConfirm("Download size : " + bytes / 1048576 + "MB" + "\n\n" + "Start to download");
        }
        else
        {
            Starter.EnterFrontScene();
        }
    }
    
    void DownLoadConfirm(string msg)
    {
        var popupLayer = PopupLayer.Open(T.gameObject);
        popupLayer.ArrangeConfirmWindow(
            async ()=>
            {
                ProgressLayer.Open(T.gameObject);
                await AddressablesLogic.ResourcePrepareProcess(Starter.EnterFrontScene, 
                    (x,f) =>
                    {
                        if (f == 0)
                            ProgressLayer.LoadingPercent(x, f, false);
                        else
                            ProgressLayer.LoadingPercent(x, f, false);
                    });
            },
            () =>
            {
                Application.Quit();
            },
            msg
        );
    }
}
