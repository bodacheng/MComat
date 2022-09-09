using System.Collections.Generic;
using DummyLayerSystem;
using UnityEngine.Events;
using mainMenu;

public class ReturnLayer : UILayer
{
    public P3Button ReturnButton;
    static readonly UnityEvent UnityEvent = new ();
    public static readonly List<UnityAction> ReturnMissionList = new ();
    
    static ReturnLayer Get()
    {
        var l = UILayerLoader.Get("ReturnLayer");
        ReturnLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as ReturnLayer;
        }
        return returnValue;
    }
    
    static ReturnLayer Open()
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
        UnityEvent.RemoveAllListeners();
        UnityEvent.AddListener(ReturnMissionList[^1]);
        UnityEvent.Invoke();
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
}
