using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight;

public partial class PopupLayer : UILayer
{
    [SerializeField] GameObject loadingIcon;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] Image bigCurtain;
    [SerializeField] Image empty;// 为了黑化整个屏幕
    
    static PopupLayer Get()
    {
        PopupLayer returnValue = null;
        UILayer l = UILayerLoader.Get("PopupLayer");
        if (l != null)
        {
            returnValue = l as PopupLayer;
        }
        return returnValue;
    }
    
    public static PopupLayer Open(GameObject T)
    {
        PopupLayer returnValue = Get();
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
