using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;

public partial class PopupLayer : UILayer
{
    [SerializeField] Image bigCurtain;
    
    public static PopupLayer Open(GameObject T)
    {
        var returnValue = UILayerLoader.Get<PopupLayer>();
        if (returnValue != null)
        {
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
