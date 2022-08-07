using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Singleton;

public class HeroIcon : MonoBehaviour {

    public P3Button iconButton;
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

    public void ChangeIcon(Sprite sprite, Element element)
    {
        if (frame == null || Icon == null)
        {
            Debug.Log("组件缺失");
            return;
        }
        
        //Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(frame.GetComponent<RectTransform>().sizeDelta.x * 0.8f, frame.GetComponent<RectTransform>().sizeDelta.y * 0.8f);
        Icon.transform.SetSiblingIndex(frame.transform.GetSiblingIndex()- 1);
        if (cooldownCurtain != null)
        {
            cooldownCurtain.transform.SetSiblingIndex(Icon.transform.GetSiblingIndex() - 1);
        }
        
        Icon.sprite = sprite;
        Icon.color = Icon.sprite == null ? new Color(1, 1, 1, 0f) : Color.white;
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