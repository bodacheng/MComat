using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Singleton;

public class HeroIcon : MonoBehaviour {

    public BOButton iconButton;
    public Image Icon;
    public Image frame;
    public Image cooldownCurtain;
    
    public UnitInfo unitInfo;
    public UnitConfig unitConfig;
    
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

    public async void ChangeIcon(UnitInfo unitInfo)
    {
        this.unitInfo = unitInfo;
        if (unitInfo != null)
        {
            this.unitConfig = Units.GetUnitConfig(unitInfo.r_id);
            var pic = await UnitIconDic.Load(unitInfo.r_id, gameObject);
            ChangeIcon(pic, unitConfig.element);
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
    
    void ChangeIcon(Sprite sprite, Element element)
    {
        //Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.GetComponent<RectTransform>().sizeDelta.x * 0.8f, frame.GetComponent<RectTransform>().sizeDelta.y * 0.8f);
        Icon.transform.SetSiblingIndex(frame.transform.GetSiblingIndex()- 1);
        if (cooldownCurtain != null)
        {
            cooldownCurtain.transform.SetSiblingIndex(Icon.transform.GetSiblingIndex() - 1);
        }

        var htmlString = "";
        switch (element)
        {
            case Element.blueMagic:
                htmlString = "00ABFFFF";
                break;
            case Element.darkMagic:
                htmlString = "8D00FFFF";
                break;
            case Element.redMagic:
                htmlString = "FF5367FF";
                break;
            case Element.lightMagic:
                htmlString = "FFE300FF";
                break;
            case Element.greenMagic:
                htmlString = "0FE500FF";
                break;
            case Element.Null:
                htmlString = "FFFFFFFF";
                break;
        }

        ColorUtility.TryParseHtmlString("#"+htmlString, out var color);
        frame.color = color;
        Icon.sprite = sprite;
        if (Icon.sprite != null)
        {
            //LightOn();
        }
        else
        {
            //Grey();
        }
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
        icon.unitInfo = unitInfo;
        if (unitConfig == null)
        {
            return icon;
        }
        icon.unitConfig = unitConfig;
        var pic = await UnitIconDic.Load(unitConfig.RECORD_ID, icon.gameObject);
        icon.ChangeIcon(pic, unitConfig.element);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
        return icon;
    }
}