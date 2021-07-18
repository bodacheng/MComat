using System.Collections.Generic;
using UnityEngine;

public class UILayerLoader
{
    private static readonly IDictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"ArenaFightOver", "DummyLayerSystem/ArenaFightOver"},
        {"CommonFightResult", "DummyLayerSystem/CommonFightResult"},
        {"TitleScreenLayer", "DummyLayerSystem/TitleScreenLayer"},
        {"ArcadeFightResult", "DummyLayerSystem/ArcadeFightResult"},
    };

    private static List<UILayer> Queues = new List<UILayer>();
    
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
        Queues.Add(t);
        return t;
    }

    public static void Pop()
    {
        if (Queues.Count > 0)
        {
            UILayer uiLayer = Queues[Queues.Count - 1];
            if (uiLayer != null)
            {
                GameObject.Destroy(uiLayer);
            }
            Queues.RemoveAt(Queues.Count - 1);
        }
    }
    
    public static void Remove(string index)
    {
        int toremoveindex = -1;
        for (int i = 0; i < Queues.Count; i++)
        {
            UILayer uiLayer = Queues[i];
            if (uiLayer.Index == index)
            {
                toremoveindex = i;
            }
        }

        if (toremoveindex >= 0)
        {
            UILayer layer = Queues[toremoveindex];
            GameObject.Destroy(layer);
            Queues.RemoveAt(toremoveindex);
        }
    }
}
