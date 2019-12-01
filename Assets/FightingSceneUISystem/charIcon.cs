using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;

public class charIcon : MonoBehaviour {

    public Button iconButton;
    public Image Icon;
    public Image frame;

    public GetMonsterOfPlayerDetailModel _MonsterOfPlayerDetailModel;
    public CharacterResourceInfo _CharacterResourceInfo;

    static IDictionary<Zokusei, Sprite> frames;
    public static void iniFrames()
    {
        frames = new Dictionary<Zokusei, Sprite>();

        Sprite frameobject_dark = Resources.Load("essentialUIElements/iconframes/1") as Sprite;
        Sprite frameobject_blue = Resources.Load("essentialUIElements/iconframes/1") as Sprite;
        Sprite frameobject_red = Resources.Load("essentialUIElements/iconframes/2") as Sprite;
        Sprite frameobject_light = Resources.Load("essentialUIElements/iconframes/2") as Sprite;
        Sprite frameobject_green = Resources.Load("essentialUIElements/iconframes/1") as Sprite;
        Sprite frameobject_null = Resources.Load("essentialUIElements/iconframes/0") as Sprite;

        if(frameobject_blue)
            frames.Add(Zokusei.blueMagic,frameobject_blue);
        if(frameobject_red)
            frames.Add(Zokusei.redMagic,frameobject_red);
        if(frameobject_green)
            frames.Add(Zokusei.greenMagic,frameobject_green);
        if(frameobject_light)
            frames.Add(Zokusei.lightMagic,frameobject_light);
        if(frameobject_dark)
            frames.Add(Zokusei.darkMagic,frameobject_dark);
        if(frameobject_null)
            frames.Add(Zokusei.Null,frameobject_null);
    }
    
    public static void Seletedfeature(charIcon _charIcon,GameObject selectedFrame)
    {
        selectedFrame.transform.SetParent(_charIcon.frame.transform);
        selectedFrame.transform.localPosition = Vector3.zero;
        selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        selectedFrame.gameObject.SetActive(true);  
    }
    
    public void changeIcon(Sprite _Sprite,Zokusei zokusei)
    {
        if (frame == null || Icon == null)
        {
            Debug.Log("组件缺失");
            return;
        }

        frame.transform.localScale = Vector3.one;
        Icon.transform.localScale = Vector3.one * 0.75f;
        frame.transform.SetSiblingIndex(3);
        Icon.transform.SetSiblingIndex(4);

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
        if (Icon.sprite == null)
        {
            Icon.color = new Color(1,1,1,0f);
        }
        else
            Icon.color = Color.white;
            
        if (frames.ContainsKey(zokusei))
            frame.sprite = frames[zokusei];                
    }

    public void decideIconSize(string mainMenuFocusing)
    {
        if (mainMenuFocusing != _MonsterOfPlayerDetailModel.monsterOfPlayerId)
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        else
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
    }
}
