using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUpPresentation : MonoBehaviour
{
    [SerializeField] Starter Starter;
    [SerializeField] RectTransform T;
    
    void Start()
    {
        AddressablesLogic.GetWholeDownLoadSize(DownLoadConfirm).Forget();
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
                await AddressablesLogic.ResourcePrepareProcess(EnterGame, ProgressUIStateRefresh);
            },
            () =>
            {
                Application.Quit();
            },
            msg
        );
    }

    void EnterGame()
    {
        if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
        {
            Starter.ToSkillShowerMode();
        }
        else
        {
            Starter.EnterFrontScene();
        }
    }
}
