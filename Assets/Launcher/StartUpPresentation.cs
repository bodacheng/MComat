using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter Starter;
    [SerializeField] RectTransform T;
    
    void Start()
    {
        AddressablesLogic.GetWholeDownLoadSize(
            DownLoadConfirm,
            () =>
            {
                var popupLayer = PopupLayer.Open(T.gameObject);
                popupLayer.ArrangeConfirmWindow((() =>
                {
                    SceneManager.LoadScene(0);
                }), " 下载错误。请检查网络 ");
            }
        ).Forget();
    }
    
    void DownLoadConfirm(string msg)
    {
        var popupLayer = PopupLayer.Open(T.gameObject);
        popupLayer.ArrangeConfirmWindow(
            async ()=>
            {
                await AddressablesLogic.ResourcePrepareProcess(Starter.EnterFrontScene, 
                    (x,f) => PopupLayer.LoadingPercent(x, T.gameObject, f));
            },
            () =>
            {
                Application.Quit();
            },
            msg
        );
    }
}
