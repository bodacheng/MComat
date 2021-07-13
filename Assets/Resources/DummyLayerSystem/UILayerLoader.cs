using System.Collections.Generic;
using UnityEngine;

public class UILayerLoader
{
    private static IDictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"ArenaFightOver", "DummyLayerSystem/ArenaFightOver"}
    };
    
    public static UILayer Load(RectTransform T, string layerName)
    {
        string path = paths[layerName];
        UILayer UILayer = Resources.Load<UILayer>(path);
        UILayer.transform.SetParent(T);
        return UILayer;
    }
}
