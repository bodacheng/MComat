using System;
using System.Collections.Generic;
using System.Threading;
using DummyLayerSystem;
using UnityEngine.UI;
using UnityEngine;

public class ReturnLayer : UILayer
{
    [SerializeField] Button returnButton;
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject curtain;
    
    public static readonly List<Func<bool>> ReturnMissionList = new List<Func<bool>>();
    
    
    void Setup()
    {
        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(POP);
    }
    
    public static void POP()
    {
        if (ReturnMissionList.Count == 0)
            return;

        var targetMission = ReturnMissionList[ReturnMissionList.Count - 1];
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
        curtain.gameObject.SetActive(on);
        indicator.gameObject.SetActive(on);
        ToTop();
    }

    public static void AddUniTaskCancel(CancellationTokenSource cts)
    {
        var layer = UILayerLoader.Get<ReturnLayer>();
        if (layer != null)
        {
            void triggerCts()
            {
                if (cts != null && !cts.IsCancellationRequested)
                    cts.Cancel();
            }
            
            layer.returnButton.onClick.AddListener(() =>
            {
                triggerCts();
                layer.returnButton.onClick.RemoveListener(triggerCts);
            });
        }
    }

    public static void MoveBack()
    {
        var layer = UILayerLoader.Get<ReturnLayer>();
        if (layer != null)
        {
            layer.transform.SetAsFirstSibling();
        }
    }
    
    public static void MoveFront()
    {
        var layer = UILayerLoader.Get<ReturnLayer>();
        if (layer != null)
        {
            layer.transform.SetAsFirstSibling();
        }
    }
}
