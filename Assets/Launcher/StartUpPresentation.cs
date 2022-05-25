using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUpPresentation : MonoBehaviour
{
    public Starter Starter;
    public RectTransform T;
    public ResourceDownLoad ResourceDownLoad;
    
    void Start()
    {
        StartCoroutine(ResourceDownLoad.GetWholeDownLoadSize(DownLoadConfirm));
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
        popupLayer.ArrangeConfirmWindow(()=> StartCoroutine(ResourceDownLoad.ResourcePrepareProcess(EnterGame, ProgressUIStateRefresh)), msg);
    }

    void EnterGame()
    {
        if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
        {
            Starter.ToSkillShowerMode();
        }
        else
        {
            Starter.BeginNetMode();
        }
    }
}
