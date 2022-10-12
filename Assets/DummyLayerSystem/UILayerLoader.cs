using System.Collections.Generic;
using UnityEngine;

namespace DummyLayerSystem
{
    internal static class UILayerLoader
    {
        static readonly IDictionary<string, string> paths = new Dictionary<string, string>()
        {
            {"NickNameLayer", "DummyLayerSystem/NickNameLayer"},
            {"UpperInfoBar", "DummyLayerSystem/UpperInfoBar"},
            {"FrontLayer", "DummyLayerSystem/FrontLayer"},
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
            {"PopupLayer", "DummyLayerSystem/PopupLayer"},
            {"HighLightLayer", "DummyLayerSystem/HighLightLayer"},
            {"ProgressLayer", "DummyLayerSystem/ProgressLayer"},
            {"SelfFightLayer", "DummyLayerSystem/SelfFightLayer"},
            {"FightPrepareLayer", "DummyLayerSystem/FightPrepareLayer"},
            {"TeamEditLayer", "DummyLayerSystem/TeamEditLayer"},
            {"SkillShowLayer", "DummyLayerSystem/SkillShowLayer"},
            {"GotchaLayer", "DummyLayerSystem/GotchaLayer"},
            {"GotchaResultLayer", "DummyLayerSystem/GotchaResultLayer"},
            {"StoneListLayer", "DummyLayerSystem/StoneListLayer"},
            {"SkillEditLayer", "DummyLayerSystem/SkillEditLayer"},
            {"UnitOptionLayer", "DummyLayerSystem/UnitOptionLayer"},
            {"StoneMergeLayer", "DummyLayerSystem/StoneMergeLayer"},
            {"ShopTopLayer", "DummyLayerSystem/ShopTopLayer"},
            {"BoxExpandHelperLayer", "DummyLayerSystem/BoxExpandHelperLayer"},
            {"BoxOverLoadFixLayer", "DummyLayerSystem/BoxOverLoadFixLayer"},
            {"ReturnLayer", "DummyLayerSystem/ReturnLayer"},
            {"LoginLayer", "DummyLayerSystem/LoginLayer"},
            {"FightScenePauseSupport", "DummyLayerSystem/FightScenePauseSupport"},
        };

        private static readonly List<UILayer> Queues = new List<UILayer>();
        
        public static void Clear(string except = null)
        {
            var toRemove = new List<UILayer>();
            foreach (var queue in Queues)
            {
                if (except != queue.Index)
                {
                    toRemove.Add(queue);
                }
            }
            
            foreach (var layer in toRemove)
            {
                Queues.Remove(layer);
                if (layer != null && layer.gameObject != null)
                    Remove(layer.Index);
            }
        }
    
        public static UILayer Get(string key)
        {
            return Queues.Find(x => x.Index == key);
        }
        
        public static UILayer Load(GameObject T, string layerName)
        {
            if (Get(layerName)!= null)
            {
                Debug.Log("冲突"+ layerName);
                return Get(layerName);
            }
            
            var path = paths[layerName];
            var UILayerPrefab = Resources.Load<UILayer>(path);
            var t = GameObject.Instantiate(UILayerPrefab);
            t.Index = layerName;
            t.transform.SetParent(T.transform);
            t.transform.localPosition = Vector3.zero;
            var rt = t.GetComponent<RectTransform>();
            rt.anchorMax = Vector2.one;
            rt.anchorMin = Vector2.zero;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            Queues.Add(t);
            return t;
        }

        // 为了把layer直接加入队列。
        public static void FixAdd(string layerName, UILayer t)
        {
            t.Index = layerName;
            Queues.Add(t);
        }

        public static void Pop()
        {
            if (Queues.Count > 0)
            {
                var uiLayer = Queues[Queues.Count - 1];
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
            var toRemoveIndex = -1;
            for (var i = 0; i < Queues.Count; i++)
            {
                var uiLayer = Queues[i];
                if (uiLayer.Index == index)
                {
                    toRemoveIndex = i;
                }
            }
            
            if (toRemoveIndex >= 0)
            {
                var layer = Queues[toRemoveIndex];
                if (layer.gameObject != null)
                    GameObject.Destroy(layer.gameObject);
                Queues.RemoveAt(toRemoveIndex);
            }
        }
    }
}
