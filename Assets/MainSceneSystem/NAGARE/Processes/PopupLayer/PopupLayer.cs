using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class PopupLayer : UILayer
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject loadingIcon;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] Image bigCurtain;
    [SerializeField] Image empty;// 为了黑化整个屏幕
    
    [Header("Validation")]
    [SerializeField] RectTransform ValidationWindow;
    [SerializeField] Text ValidationIntro;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    
    public static PopupLayer Open(GameObject T)
    {
        PopupLayer returnValue;
        UILayer l = UILayerLoader.Get("PopupLayer");
        if (l != null)
        {
            returnValue = l as PopupLayer;
            return returnValue;
        }
        returnValue = UILayerLoader.Load(T,"PopupLayer") as PopupLayer;
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("PopupLayer");
    }
}
