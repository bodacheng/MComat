using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Singleton;

public class HeroIcon : MonoBehaviour {

    public BOButton iconButton;
    [SerializeField] Image icon;
    [SerializeField] Image iconBg;
    [SerializeField] Image frame;
    [SerializeField] Image cooldownCurtain;
    [SerializeField] GameObject warnFlag;
    
    public UnitConfig unitConfig;
    public GameObject WarnFlag => warnFlag;
    
    protected void Grey()
    {
        frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, 0.3f);
        iconBg.color = new Color(iconBg.color.r, iconBg.color.g, iconBg.color.b, 0.3f);
        icon.color = new Color(1,1,1,0.3f);
    }
    
    protected void LightOn()
    {
        frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, 1f);
        iconBg.color = new Color(iconBg.color.r, iconBg.color.g, iconBg.color.b, 1f);
        icon.color = new Color(1,1,1,1f);
    }
    
    public void CooldownCurtainUpdate(float proportion)
    {
        cooldownCurtain.fillAmount = proportion;
    }

    public async void ChangeIcon(UnitInfo unitInfo, bool withSkillCheck = false, Func<int> teamCountGet = null)
    {
        var unitInfoFormal = UnitInfo.GetUnitInfo(unitInfo);
        if (unitInfo != null)
        {
            this.unitConfig = Units.GetUnitConfig(unitInfo.r_id);
            var pic = await UnitIconDic.Load(unitInfo.r_id, gameObject);
            if (this == null)
                return;
            ChangeIcon(pic, unitConfig.element);
            LightOn();
            if ((withSkillCheck && unitInfoFormal.set.CheckEdit() != SkillSet.SkillEditError.Perfect) 
                ||
                (teamCountGet != null && teamCountGet() == 0))
            {
                Grey();
            }
        }
        else
        {
            ChangeIcon(null, Element.Null);
        }
    }
    
    public async void ChangeIcon(string recordId)
    {
        this.unitConfig = Units.GetUnitConfig(recordId);
        var pic = await UnitIconDic.Load(recordId, gameObject);
        ChangeIcon(pic, unitConfig.element);
    }
    
    public void Clear()
    {
        ChangeIcon(null, Element.Null);
    }
    
    void AdjustSize(Image icon)
    {
        var sprite = icon.sprite;
        if (sprite == null)
            return;
        var iconRect = icon.GetComponent<RectTransform>();
        var wholeParentRect = transform.GetComponent<RectTransform>();
        float spriteAspectRatio = sprite.rect.width / sprite.rect.height;
        
        if (spriteAspectRatio < 1)
        {
            iconRect.sizeDelta = new Vector2(
                sprite.rect.width * wholeParentRect.rect.height / sprite.rect.height, 
                wholeParentRect.rect.height);
        }
        else
        {
            iconRect.sizeDelta = new Vector2(
                wholeParentRect.rect.width, 
                sprite.rect.height * wholeParentRect.rect.width / sprite.rect.width);
        }
    }
    
    async void ChangeIcon(Sprite sprite, Element element)
    {
        //Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.GetComponent<RectTransform>().sizeDelta.x * 0.8f, frame.GetComponent<RectTransform>().sizeDelta.y * 0.8f);
        // if (cooldownCurtain != null)
        // {
        //     cooldownCurtain.transform.SetSiblingIndex(icon.transform.GetSiblingIndex() - 1);
        // }

        Color color = Color.clear, frameColor = Color.clear;
        switch (element)
        {
            case Element.blueMagic:
                color = CommonSetting._blueBgColor;
                frameColor = CommonSetting._blueFrameColor;
                break;
            case Element.darkMagic:
                color = CommonSetting._darkBgColor;
                frameColor = CommonSetting._darkFrameColor;
                break;
            case Element.redMagic:
                color = CommonSetting._redBgColor;
                frameColor = CommonSetting._redFrameColor;
                break;
            case Element.lightMagic:
                color = CommonSetting._lightBgColor;
                frameColor = CommonSetting._lightFrameColor;
                break;
            case Element.greenMagic:
                color = CommonSetting._greenBgColor;
                frameColor = CommonSetting._greenFrameColor;
                break;
            default:
                color = CommonSetting._emptyBgColor;
                frameColor = CommonSetting._emptyFrameColor;
                break;
        }
        
        frame.color = frameColor;
        iconBg.color = color;
        icon.sprite = sprite;
        await UniTask.DelayFrame(1);
        AdjustSize(icon);
        icon.gameObject.SetActive(sprite != null);
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
        selectedFrame.transform.SetAsFirstSibling();
        selectedFrame.gameObject.SetActive(true);
    }
    
    public static HeroIcon ArrangeHeroIconToParent(HeroIcon prefab, UnitInfo unitInfo, Action<string> iconBehaviour, 
        RectTransform T, float iconSize = 100, bool withSkillCheck = false, bool showIllegalFlag = false)
    {
        var icon = Instantiate(prefab);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        icon.unitConfig = unitConfig;
        icon.ChangeIcon(unitInfo, withSkillCheck);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize,iconSize);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.warnFlag.SetActive(showIllegalFlag && unitInfo.set.CheckEdit() != SkillSet.SkillEditError.Perfect);
        icon.iconButton.interactable = true;
        icon.iconButton.SetListener(
            () =>
            {
                iconBehaviour(unitInfo.id);
            }
        );
        icon.gameObject.SetActive(true);
        return icon;
    }
}