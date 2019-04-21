using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charIcon : MonoBehaviour {

    public int localId;
    public Button iconButton;
    public Image Icon;
    public Image frame;

    public CharacterDataInfo _CharacterDataInfo;
    public CharacterResourceInfo _CharacterResourceInfo;

    static IDictionary<zokusei, Sprite> frames;

    public static void iniFrames()
    {
        frames = new Dictionary<zokusei, Sprite>();

        Sprite frameobject_dark = Resources.Load("iconframes/1") as Sprite;
        Sprite frameobject_blue = Resources.Load("iconframes/1") as Sprite;
        Sprite frameobject_red = Resources.Load("iconframes/2") as Sprite;
        Sprite frameobject_light = Resources.Load("iconframes/2") as Sprite;
        Sprite frameobject_green = Resources.Load("iconframes/1") as Sprite;
        Sprite frameobject_null = Resources.Load("iconframes/0") as Sprite;

        frames.Add(zokusei.blueMagic,frameobject_blue);
        frames.Add(zokusei.redMagic,frameobject_red);
        frames.Add(zokusei.greenMagic,frameobject_green);
        frames.Add(zokusei.lightMagic,frameobject_light);
        frames.Add(zokusei.darkMagic,frameobject_dark);
        frames.Add(zokusei.Null,frameobject_null);
    }

    public void changeIcon(Sprite _Sprite,zokusei zokusei)
    {
        frame.sprite = frames[zokusei];
        
        var colors = iconButton.colors;
        switch (zokusei)
        {
            case zokusei.blueMagic:
                frame.color = new Color(0,0,1,1);
                colors.normalColor = new Color(0,0,1,1);
                colors.highlightedColor = new Color(0,0.2f,1,1);
            break;
            case zokusei.redMagic:
                frame.color = new Color(1,0,0,1);
                colors.normalColor = new Color(1,0,0,1);
                colors.highlightedColor = new Color(1,0.2f,0,1);
            break;
            case zokusei.greenMagic:
                frame.color = new Color(0,1,0,1);
                colors.normalColor = new Color(0,1,0,1);
                colors.highlightedColor = new Color(0,1,0.2f,1);
            break;
            case zokusei.darkMagic:
                frame.color = new Color(1,0,1,1);
                colors.normalColor = new Color(1,0,1,1);
                colors.highlightedColor = new Color(1,0,0.8f,1);
            break;
            case zokusei.lightMagic:
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
            Debug.Log("角色头像没找到？？");
            Icon.color = new Color(1,1,1,0f);
        }
        else
            Icon.color = Color.white;
            
        frame.transform.localScale = Vector3.one;
        Icon.transform.localScale = Vector3.one * 0.75f;
        frame.transform.SetSiblingIndex(3);
        Icon.transform.SetSiblingIndex(4);
    }

    public void decideIconSize(int mainMenuFocusing)
    {
        if (mainMenuFocusing != localId)
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        else
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
            
    }
}
