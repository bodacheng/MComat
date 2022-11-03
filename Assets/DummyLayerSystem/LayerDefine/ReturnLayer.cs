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
    
    static readonly UnityEvent unityEvent = new ();
    public static readonly List<UnityAction> ReturnMissionList = new ();
    
    void Setup()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(POP);
    }
    
    public static void POP()
    {
        Debug.Log("her:"+ ReturnMissionList.Count);
        if (ReturnMissionList.Count == 0)
            return;
        unityEvent.RemoveAllListeners();
        unityEvent.AddListener(ReturnMissionList[^1]);
        unityEvent.Invoke();
        ReturnMissionList.RemoveAt(ReturnMissionList.Count - 1);
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
    
    public static void PUSH(UnityAction returnAction)
    {
        Debug.Log("WE ARE:"+returnAction);
        ReturnMissionList.Add(returnAction);
        var returnLayer = UILayerLoader.Load<ReturnLayer>();
        returnLayer.Setup();
    }

    public static void AddUniTaskCancel(CancellationTokenSource cts)
    {
        var layer = UILayerLoader.Get<ReturnLayer>();
        if (layer != null)
        {
            void triggerCts()
            {
                Debug.Log("cancel");
                cts.Cancel();
            }
            
            layer.ReturnButton.onClick.AddListener(() =>
            {
                triggerCts();
                layer.ReturnButton.onClick.RemoveListener(triggerCts);
            });
        }
    }

    public void ForceBackMode(bool on)
    {
        maskBg.gameObject.SetActive(on);
        ToTop();
    }
}
