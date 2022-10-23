using System.Collections.Generic;
using DummyLayerSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using mainMenu;
using UnityEngine;

public class ReturnLayer : UILayer
{
    [SerializeField] Button ReturnButton;
    [SerializeField] GameObject maskBg;
    
    static readonly UnityEvent unityEvent = new ();
    public static readonly List<UnityAction> ReturnMissionList = new ();
    
    public void Setup()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(POP);
    }
    
    public static void POP()
    {
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
        ReturnMissionList.Add(returnAction);
        var returnLayer = UILayerLoader.Load<ReturnLayer>();
        returnLayer.Setup();
    }

    public void ForceBackMode(bool on)
    {
        maskBg.gameObject.SetActive(on);
        ToTop();
    }
}
