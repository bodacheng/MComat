using System.Collections.Generic;
using UnityEngine;

public class UILayerLoader
{
    static readonly IDictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"MainTop", "DummyLayerSystem/MainTop"},
        {"ArcadeTop", "DummyLayerSystem/ArcadeTop"},
        {"ArenaLayer", "DummyLayerSystem/ArenaLayer"},
        {"MailBox", "DummyLayerSystem/MailBox"},
        {"MailDetail", "DummyLayerSystem/MailDetail"},

        {"ArenaFightOver", "DummyLayerSystem/ArenaFightOver"},
        {"CommonFightResult", "DummyLayerSystem/CommonFightResult"},
        {"TitleScreenLayer", "DummyLayerSystem/TitleScreenLayer"},
        {"ArcadeFightResult", "DummyLayerSystem/ArcadeFightResult"},
        {"FightResultAnimLayer", "DummyLayerSystem/FightResultAnimLayer"},
        {"CountDownLayer", "DummyLayerSystem/CountDownLayer"},
        {"FightingStepLayer", "DummyLayerSystem/FightingStepLayer"},
        {"SettingLayer", "DummyLayerSystem/SettingLayer"},
        {"UnitsLayer", "DummyLayerSystem/UnitsLayer"},
        {"LogoLayer", "DummyLayerSystem/LogoLayer"},
        {"PopupLayer", "DummyLayerSystem/PopupLayer"},
    };

    private static List<UILayer> Queues = new List<UILayer>();

    public static void Clear()
    {
        foreach (var queue in Queues)
        {
            if (queue != null && queue.gameObject != null)
            GameObject.Destroy(queue.gameObject);
        }
        Queues.Clear();
    }
    
    public static UILayer Get(string key)
    {
        return Queues.Find(x => x.Index == key);
    }
    
    public static UILayer Load(GameObject T, string layerName)
    {
        string path = paths[layerName];
        UILayer UILayerPrefab = Resources.Load<UILayer>(path);
        UILayer t = GameObject.Instantiate(UILayerPrefab);
        t.Index = layerName;
        t.transform.SetParent(T.transform);
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
                uiLayer.OnDestroy();
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
            GameObject.Destroy(layer.gameObject);
            Queues.RemoveAt(toremoveindex);
            Debug.Log("Layer destroied :" + index);
        }
    }
}
