using System.Collections;
using UnityEngine;

public class StartUpPresentation : MonoBehaviour
{
    public Starter Starter;
    public RectTransform T;
    public ResourceDownLoad ResourceDownLoad;
    
    void Start()
    {
        StartCoroutine(ResourceDownLoad.GetWholeDownLoadSize(DownLoadConfirm));
    }
    
    void DownLoadConfirm(string msg)
    {
        var popupLayer = PopupLayer.Open(T.gameObject);
        popupLayer.ArrangeConfirmWindow(()=> StartCoroutine(ResourceDownLoad.ResourcePrepareProcess(EnterGame)), msg);
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
