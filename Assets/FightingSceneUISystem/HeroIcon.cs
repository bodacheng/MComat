using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using System.Collections;
using dataAccess;

public class HeroIcon : MonoBehaviour {

    public Button iconButton;
    public Image Icon;
    public Image frame;
    public Image cooldownCurtain;
    
    public GetMonsterOfPlayerDetailModel _MonsterOfPlayerDetailModel;
    public CharConfig _CharConfig;

    static IDictionary<Zokusei, Sprite> frames = new Dictionary<Zokusei, Sprite>();
    
    public static void INIFrames()
    {
        if (!frames.ContainsKey(Zokusei.blueMagic))
            frames.Add(Zokusei.blueMagic, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
        if (!frames.ContainsKey(Zokusei.redMagic))
            frames.Add(Zokusei.redMagic, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
        if (!frames.ContainsKey(Zokusei.greenMagic))
            frames.Add(Zokusei.greenMagic, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
        if (!frames.ContainsKey(Zokusei.lightMagic))
            frames.Add(Zokusei.lightMagic, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
        if (!frames.ContainsKey(Zokusei.darkMagic))
            frames.Add(Zokusei.darkMagic, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
        if (!frames.ContainsKey(Zokusei.Null))
            frames.Add(Zokusei.Null, Resources.Load("essentialUIElements/iconframes/4") as Sprite);
    }
    
    public void CooldownCurtainUpdate(float proportion)
    {
        cooldownCurtain.fillAmount = proportion;
    }
        
    public void ChangeIcon(Sprite _Sprite,Zokusei zokusei)
    {
        if (frame == null || Icon == null)
        {
            Debug.Log("组件缺失");
            return;
        }

        frame.transform.localScale = Vector3.one;
        Icon.transform.localScale = Vector3.one * 0.8f;
        frame.transform.SetSiblingIndex(4);
        Icon.transform.SetSiblingIndex(4);
        if (cooldownCurtain != null)
        {
            cooldownCurtain.transform.SetSiblingIndex(3);
        }
        var colors = iconButton.colors;
        switch (zokusei)
        {
            case Zokusei.blueMagic:
                frame.color = new Color(0,0,1,1);
                colors.normalColor = new Color(0,0,1,1);
                colors.highlightedColor = new Color(0,0.2f,1,1);
            break;
            case Zokusei.redMagic:
                frame.color = new Color(1,0,0,1);
                colors.normalColor = new Color(1,0,0,1);
                colors.highlightedColor = new Color(1,0.2f,0,1);
            break;
            case Zokusei.greenMagic:
                frame.color = new Color(0,1,0,1);
                colors.normalColor = new Color(0,1,0,1);
                colors.highlightedColor = new Color(0,1,0.2f,1);
            break;
            case Zokusei.darkMagic:
                frame.color = new Color(1,0,1,1);
                colors.normalColor = new Color(1,0,1,1);
                colors.highlightedColor = new Color(1,0,0.8f,1);
            break;
            case Zokusei.lightMagic:
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
        
        if (frames.ContainsKey(zokusei))
            frame.sprite = frames[zokusei];                
    }
    
    public static IEnumerator ChangeHeroIconByMonsterOfPlayerId(string PosMonsterOfPlayerId, HeroIcon Icon)
    {
        if (PosMonsterOfPlayerId != null)
        {
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(PosMonsterOfPlayerId);
            yield return getchar;
            if (getchar.Current == null)
                yield break;
            GetMonsterOfPlayerDetailModel _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.monsterId);
            ChangeHeroIconByMonsterID(charConfig.RECORD_ID,Icon);
        }
        else
        {
            Icon.ChangeIcon(null, Zokusei.Null);
        }
    }
    
    public static void ChangeHeroIconByMonsterID(string monsterRecordID, HeroIcon Icon)
    {
        CharConfig charConfig = MonstersConfigTable.GetCharConfig(monsterRecordID);
        Icon.ChangeIcon(charConfig == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(charConfig.RECORD_ID), charConfig == null ? Zokusei.Null : charConfig._zokusei);
    }
    
    public void DecideIconSize(string mainMenuFocusing)
    {
        gameObject.GetComponent<RectTransform>().localScale = mainMenuFocusing != _MonsterOfPlayerDetailModel.monsterOfPlayerId ? new Vector3(1, 1, 1) : new Vector3(1.1f, 1.1f, 1.1f);
    }
    
    public static void Seletedfeature(HeroIcon _charIcon,GameObject selectedFrame, float size)
    {
        if (_charIcon == null)
        {
            selectedFrame.SetActive(false);
            return;
        }
        selectedFrame.transform.SetParent(_charIcon.transform);
        selectedFrame.transform.localPosition = Vector3.zero;
        selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        selectedFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(size,size);
        selectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        selectedFrame.gameObject.SetActive(true);
    }
    
    // 这个本身没问题但目前使用他的方式是有问题的。围绕SetParent(T);
    public static void ArrangeHeroIconToT(HeroIcon heroIconPretab, CharDataInfo CharDataInfo, RectTransform T)
    {
        HeroIcon MyMemberIcon = Instantiate(heroIconPretab);
        CharConfig charConfig = MonstersConfigTable.GetCharConfig(CharDataInfo.ResourceID);
        if (charConfig == null)
        {
            Debug.Log("?? : " + CharDataInfo.ResourceID);
            return;
        }
        MyMemberIcon.ChangeIcon(MonsterIconDic.Instance.GetMonsterIconSyn(charConfig.RECORD_ID), charConfig._zokusei);
        MyMemberIcon.transform.SetParent(T);
        MyMemberIcon.transform.localPosition = Vector3.one;
        MyMemberIcon.transform.localScale = Vector3.one;
        MyMemberIcon.gameObject.SetActive(true);
    }
}