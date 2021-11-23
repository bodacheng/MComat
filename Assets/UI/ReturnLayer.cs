using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using mainMenu;

public class ReturnLayer : UILayer
{
    public Button ReturnButton;
    static UnityEvent UnityEvent = new UnityEvent();
    public static readonly List<UnityAction> ReturnMissionList = new List<UnityAction>();
    
    static ReturnLayer Open()
    {
        UILayer l = UILayerLoader.Get("ReturnLayer");
        ReturnLayer returnValue;
        if (l != null)
        {
            returnValue = l as ReturnLayer;
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"ReturnLayer") as ReturnLayer;
        returnValue = l as ReturnLayer;
        returnValue.ReturnButton.onClick.RemoveAllListeners();
        returnValue.ReturnButton.onClick.AddListener(POP);
        return returnValue;
    }

    static void Close()
    {
        UILayerLoader.Remove("ReturnLayer");
    }
    
    public static void POP()
    {
        if (ReturnMissionList.Count == 0)
            return;
        UnityEvent.RemoveAllListeners();
        UnityEvent.AddListener(ReturnMissionList[ReturnMissionList.Count - 1]);
        UnityEvent.Invoke();
        ReturnMissionList.RemoveAt(ReturnMissionList.Count - 1);
        if (ReturnMissionList.Count == 0)
        {
            Close();
        }
    }
    
    public static void PUSH(UnityAction onemission)
    {
        ReturnMissionList.Add(onemission);
        Open();
    }
}
