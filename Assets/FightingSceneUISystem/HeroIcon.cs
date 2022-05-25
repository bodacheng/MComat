using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

public class HeroIcon : MonoBehaviour {

    public P3Button iconButton;
    public Image Icon;
    public Image frame;
    public Image cooldownCurtain;
    public UnitInfo unitInfo;
    public UnitConfig unitConfig;
    
    static readonly IDictionary<Element, Sprite> frames = new Dictionary<Element, Sprite>();
    
    public static void INIFrames()
    {
        if (!frames.ContainsKey(Element.blueMagic))
            frames.Add(Element.blueMagic, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
        if (!frames.ContainsKey(Element.redMagic))
            frames.Add(Element.redMagic, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
        if (!frames.ContainsKey(Element.greenMagic))
            frames.Add(Element.greenMagic, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
        if (!frames.ContainsKey(Element.lightMagic))
            frames.Add(Element.lightMagic, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
        if (!frames.ContainsKey(Element.darkMagic))
            frames.Add(Element.darkMagic, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
        if (!frames.ContainsKey(Element.Null))
            frames.Add(Element.Null, Resources.Load("essentialUIElements/iconframes/8") as Sprite);
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

    public void ChangeIcon(Sprite _Sprite, Element element)
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
        
        Icon.sprite = _Sprite;
        Icon.color = Icon.sprite == null ? new Color(1, 1, 1, 0f) : Color.white;
        
        if (frames.ContainsKey(element))
            frame.sprite = frames[element];                
    }
    
    public static void ChangeHeroIconByInstanceId(string instanceId, HeroIcon Icon)
    {
        var _one = MyMonsters.Get(instanceId);
        if (_one != null)
        {
            var unitConfig = Units.GetUnitConfig(_one.r_id);
            ChangeHeroIconByRID(unitConfig.RECORD_ID,Icon);
        }
        else
        {
            Icon.ChangeIcon(null, Element.Null);
        }
    }
    
    public static void ChangeHeroIconByRID(string rID, HeroIcon Icon)
    {
        UnitConfig unitConfig = Units.GetUnitConfig(rID);
        Icon.ChangeIcon(unitConfig == null ? null : UnitIconDic.Load(unitConfig.RECORD_ID), unitConfig == null ? Element.Null : unitConfig.element);
    }
        
    public static void SelectedFeature(HeroIcon _charIcon, GameObject selectedFrame, float localScale)
    {
        if (_charIcon == null)
        {
            selectedFrame.SetActive(false);
            return;
        }
        selectedFrame.transform.SetParent(_charIcon.transform);
        selectedFrame.transform.localPosition = Vector3.zero;
        selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(localScale, localScale, localScale);
        selectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        selectedFrame.gameObject.SetActive(true);
    }
    
    // 这个本身没问题但目前使用他的方式是有问题的。围绕SetParent(T);
    public static HeroIcon ArrangeHeroIconToT(HeroIcon prefab, UnitInfo unitInfo, RectTransform T)
    {
        var icon = Instantiate(prefab);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        if (unitConfig == null)
        {
            Debug.Log("?? : " + unitInfo.r_id);
            return null;
        }
        icon.unitInfo = unitInfo;
        icon.unitConfig = unitConfig;
        icon.ChangeIcon(UnitIconDic.Load(unitConfig.RECORD_ID), unitConfig.element);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
        return icon;
    }
}