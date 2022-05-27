using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartUpPresentation : MonoBehaviour
{
    public Starter Starter;
    public RectTransform T;
    [FormerlySerializedAs("ResourceDownLoad")] public Cach cach;
    
    void Start()
    {
        StartCoroutine(cach.GetWholeDownLoadSize(DownLoadConfirm));
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
        popupLayer.ArrangeConfirmWindow(()=>
        {
            progressBar.gameObject.SetActive(true);
            StartCoroutine(cach.ResourcePrepareProcess(EnterGame, ProgressUIStateRefresh));
        }, msg);
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
