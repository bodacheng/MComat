using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Singleton;

public class HeroIcon : MonoBehaviour {

    public P3Button iconButton;
    public Image Icon;
    public Image frame;
    public Image cooldownCurtain;
    public UnitInfo unitInfo;
    public UnitConfig unitConfig;
    
    static readonly IDictionary<Element, Sprite> Frames = new Dictionary<Element, Sprite>();
    
    public static async UniTask IniFrames()
    {
        var blue = await AddressablesLogic.LoadT<Sprite>("Icon_Frame/8.asset");
        
        if (!Frames.ContainsKey(Element.blueMagic))
            Frames.Add(Element.blueMagic, blue);
        if (!Frames.ContainsKey(Element.redMagic))
            Frames.Add(Element.redMagic, blue);
        if (!Frames.ContainsKey(Element.greenMagic))
            Frames.Add(Element.greenMagic, blue);
        if (!Frames.ContainsKey(Element.lightMagic))
            Frames.Add(Element.lightMagic, blue);
        if (!Frames.ContainsKey(Element.darkMagic))
            Frames.Add(Element.darkMagic, blue);
        if (!Frames.ContainsKey(Element.Null))
            Frames.Add(Element.Null, blue);
    }
    
    public void Grey()
    {
        frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, 0.3f);
        Icon.color = new Color(1,1,1,0.3f);
    }
    
    public void LightOn()
    {
        frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, 1f);
        Icon.color = new Color(1,1,1,1f);
    }
    
    public void CooldownCurtainUpdate(float proportion)
    {
        cooldownCurtain.fillAmount = proportion;
    }

    public void ChangeIcon(Sprite sprite, Element element)
    {
        if (frame == null || Icon == null)
        {
            Debug.Log("组件缺失");
            return;
        }

        //frame.transform.localScale = Vector3.one;
        //Icon.transform.localScale = Vector3.one;
        Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.GetComponent<RectTransform>().sizeDelta.x * 0.8f, frame.GetComponent<RectTransform>().sizeDelta.y * 0.8f);
        frame.transform.SetSiblingIndex(4);
        Icon.transform.SetSiblingIndex(4);
        if (cooldownCurtain != null)
        {
            cooldownCurtain.transform.SetSiblingIndex(3);
        }
        var colors = iconButton.colors;
        switch (element)
        {
            case Element.blueMagic:
                frame.color = new Color(0,0,1,1);
                colors.normalColor = new Color(0,0,1,1);
                colors.highlightedColor = new Color(0,0.2f,1,1);
            break;
            case Element.redMagic:
                frame.color = new Color(1,0,0,1);
                colors.normalColor = new Color(1,0,0,1);
                colors.highlightedColor = new Color(1,0.2f,0,1);
            break;
            case Element.greenMagic:
                frame.color = new Color(0,1,0,1);
                colors.normalColor = new Color(0,1,0,1);
                colors.highlightedColor = new Color(0,1,0.2f,1);
            break;
            case Element.darkMagic:
                frame.color = new Color(1,0,1,1);
                colors.normalColor = new Color(1,0,1,1);
                colors.highlightedColor = new Color(1,0,0.8f,1);
            break;
            case Element.lightMagic:
                frame.color = new Color(1,1,0,1);
                colors.normalColor = new Color(1,1,0,1);
                colors.highlightedColor = new Color(1f,1,0.2f,1);
            break;
            default:
                frame.color = new Color(1,1,1,0.8f);
                colors.normalColor = new Color(1,1,1,0.8f);
                colors.highlightedColor = new Color(1,1,1,1);
                break;
        }
        iconButton.colors = colors;
        
        Icon.sprite = sprite;
        Icon.color = Icon.sprite == null ? new Color(1, 1, 1, 0f) : Color.white;
        
        if (Frames.ContainsKey(element))
            frame.sprite = Frames[element];                
    }
    
    public static void ChangeHeroIconByInstanceId(string instanceId, HeroIcon Icon)
    {
        var info = dataAccess.Units.Get(instanceId);
        if (info != null)
        {
            var unitConfig = Units.GetUnitConfig(info.r_id);
            ChangeHeroIconByRid(unitConfig.RECORD_ID,Icon);
        }
        else
        {
            Icon.ChangeIcon(null, Element.Null);
        }
    }
    
    public static async void ChangeHeroIconByRid(string rID, HeroIcon Icon)
    {
        var unitConfig = Units.GetUnitConfig(rID);
        var pic = await UnitIconDic.Load(unitConfig.RECORD_ID);
        Icon.ChangeIcon(unitConfig == null ? null : pic, unitConfig == null ? Element.Null : unitConfig.element);
    }
        
    public static void SelectedFeature(HeroIcon unitIcon, GameObject selectedFrame, float localScale)
    {
        if (unitIcon == null)
        {
            selectedFrame.SetActive(false);
            return;
        }
        selectedFrame.transform.SetParent(unitIcon.transform);
        selectedFrame.transform.localPosition = Vector3.zero;
        selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(localScale, localScale, localScale);
        selectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        selectedFrame.gameObject.SetActive(true);
    }
    
    // 这个本身没问题但目前使用他的方式是有问题的。围绕SetParent(T);
    public static async UniTask<HeroIcon> ArrangeHeroIconToT(HeroIcon prefab, UnitInfo unitInfo, RectTransform T)
    {
        var icon = Instantiate(prefab);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        if (unitConfig == null)
        {
            Debug.Log("?? : " + unitInfo.r_id);
            return default;
        }
        icon.unitInfo = unitInfo;
        icon.unitConfig = unitConfig;
        var pic = await UnitIconDic.Load(unitConfig.RECORD_ID);
        icon.ChangeIcon(pic, unitConfig.element);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
        return icon;
    }
}