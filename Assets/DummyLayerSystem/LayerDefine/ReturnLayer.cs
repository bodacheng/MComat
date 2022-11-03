using System;
using System.Collections.Generic;
using System.Threading;
using DummyLayerSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class ReturnLayer : UILayer
{
    [SerializeField] Button ReturnButton;
    [SerializeField] GameObject maskBg;
    
    public static readonly List<Func<bool>> ReturnMissionList = new ();
    
    void Setup()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(POP);
    }
    
    public static void POP()
    {
        if (ReturnMissionList.Count == 0)
            return;

        var targetMission = ReturnMissionList[^1];
        ReturnMissionList.RemoveAt(ReturnMissionList.Count - 1);
        var success = targetMission.Invoke();
        
        if (success)
        {
            if (ReturnMissionList.Count == 0)
            {
                UILayerLoader.Remove<ReturnLayer>();
            }
            else
            {
                var returnLayer = UILayerLoader.Load<ReturnLayer>();
                returnLayer.Setup();
            }
        }
    }
    
    public static void PUSH(Func<bool> returnAction)
    {
        ReturnMissionList.Add(returnAction);
        var returnLayer = UILayerLoader.Load<ReturnLayer>();
        returnLayer.Setup();
    }
    
    public void ForceBackMode(bool on)
    {
        maskBg.gameObject.SetActive(on);
        ToTop();
    }

    public static void AddUniTaskCancel(CancellationTokenSource cts)
    {
        var layer = UILayerLoader.Get<ReturnLayer>();
        if (layer != null)
        {
            void triggerCts()
            {
                cts.Cancel();
            }
            
            layer.ReturnButton.onClick.AddListener(() =>
            {
                triggerCts();
                layer.ReturnButton.onClick.RemoveListener(triggerCts);
            });
        }
    }
}
