using System;
using mainMenu;
using dataAccess;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GotchaLayer : UILayer
{
    [SerializeField] private Button Gotcha9;
    [SerializeField] private Button GetAllSKBtn;
    [SerializeField] private Button GetAllMBtn;
    [SerializeField] private Button Remove25StonesBtn;

    [SerializeField] private Button openDropTableInfo;

    private static Action extraSuccessAction;

    public static void SetExtraSuccessAction(Action _extraSuccessAction)
    {
        extraSuccessAction = _extraSuccessAction;
    }
    
    public void Setup(Action<Action<List<StoneOfPlayerInfo>>> nine, Action getAllSK, Action getAllM, Action remove25Stones, Action dropTableInfo)
    {
        Gotcha9.onClick.AddListener(() => { nine(temp);});
        openDropTableInfo.onClick.AddListener(dropTableInfo.Invoke);
        
        GetAllSKBtn.gameObject.SetActive(true);
        GetAllMBtn.gameObject.SetActive(true);
        Remove25StonesBtn.gameObject.SetActive(true);

        GetAllSKBtn.onClick.AddListener(()=>getAllSK());
        GetAllMBtn.onClick.AddListener(()=>getAllM());
        Remove25StonesBtn.onClick.AddListener(()=> remove25Stones());
    }

    void temp(List<StoneOfPlayerInfo> stones)
    {
        GotchaResult.Result = stones;
        PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, true);
        extraSuccessAction?.Invoke();
    }
}