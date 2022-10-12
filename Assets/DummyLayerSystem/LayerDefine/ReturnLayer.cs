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
    
    public static ReturnLayer Get()
    {
        var l = UILayerLoader.Get("ReturnLayer");
        ReturnLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as ReturnLayer;
        }
        return returnValue;
    }
    
    public static ReturnLayer Open()
    {
        ReturnLayer returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T,"ReturnLayer") as ReturnLayer;
        returnValue.ReturnButton.onClick.RemoveAllListeners();
        returnValue.ReturnButton.onClick.AddListener(POP);
        return returnValue;
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
            UILayerLoader.Remove("ReturnLayer");
        }
        else
        {
            Open();
        }
    }
    
    public static void PUSH(UnityAction returnAction)
    {
        ReturnMissionList.Add(returnAction);
        Open();
    }

    public void ForceBackMode(bool on)
    {
        maskBg.gameObject.SetActive(on);
        ToTop();
    }
}
