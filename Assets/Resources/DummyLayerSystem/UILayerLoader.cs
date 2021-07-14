using System.Collections.Generic;
using UnityEngine;

public class UILayerLoader
{
    private static readonly IDictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"ArenaFightOver", "DummyLayerSystem/ArenaFightOver/ArenaFightOver"},
        {"CommonFightResult", "DummyLayerSystem/CommonFightResult/CommonFightResult"}
    };
    
    public static UILayer Load(RectTransform T, string layerName)
    {
        string path = paths[layerName];
        UILayer UILayerPrefab = Resources.Load<UILayer>(path);
        UILayer t = GameObject.Instantiate(UILayerPrefab);
        t.transform.SetParent(T);
        RectTransform rt = t.GetComponent<RectTransform>();
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;
        return t;
    }
}
