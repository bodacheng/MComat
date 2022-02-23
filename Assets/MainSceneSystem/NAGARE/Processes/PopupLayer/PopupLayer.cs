using DummyLayerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight;

public partial class PopupLayer : UILayer
{
    [SerializeField] GameObject loadingIcon;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] Image bigCurtain;
    
    void Start()
    {
        UILayerLoader.FixAdd("PopupLayer", this);
    }

    public override void OnDestroy()
    {
        Debug.Log("why?");
        base.OnDestroy();
    }

    static PopupLayer Get()
    {
        PopupLayer returnValue = null;
        var l = UILayerLoader.Get("PopupLayer");
        if (l != null)
        {
            returnValue = l as PopupLayer;
        }
        return returnValue;
    }
    
    public static PopupLayer Open(GameObject T)
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(T,"PopupLayer") as PopupLayer;
        return returnValue;
    }
    
    public static void Close()
    {
        HighlightUI.Dismiss();
        UILayerLoader.Remove("PopupLayer");
    }
}
