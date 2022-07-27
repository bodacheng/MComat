using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    [SerializeField] private TextMeshProUGUI ProgressMsg;
    [SerializeField] private Slider progressBar;
    
    void ProgressUIStateRefresh(string msg, float progress)
    {
        ProgressMsg.text = msg;
        progressBar.value = progress;
    }
    
    void DownLoadConfirm(string msg)
    {
        var popupLayer = PopupLayer.Open(T.gameObject);
        popupLayer.ArrangeConfirmWindow(
            async ()=>
            {
                progressBar.gameObject.SetActive(true);
                await AddressablesLogic.ResourcePrepareProcess(Starter.EnterFrontScene, ProgressUIStateRefresh);
            },
            () =>
            {
                Application.Quit();
            },
            msg
        );
    }
}
