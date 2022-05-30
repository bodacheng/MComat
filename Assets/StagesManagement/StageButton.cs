using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageButton : MonoBehaviour
{
    public Button button;
    public RectTransform IconsT;
    public List<HeroIcon> MemberIcons;
    public Text text;
    public int ID;
    
    public void ChangeColorOfIcons(bool on)
    {
        Image buttonImage = GetComponent<Image>();
        //buttonImage.raycastTarget = on;
        buttonImage.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        text.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        for (int i = 0; i < MemberIcons.Count; i++)
        {
            if (on)
                MemberIcons[i].LightOn();
            else 
                MemberIcons[i].Grey();
            MemberIcons[i].iconButton.targetGraphic.raycastTarget = on;
            button.targetGraphic.raycastTarget = on;
        }
    }
}