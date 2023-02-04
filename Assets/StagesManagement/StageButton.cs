using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageButton : MonoBehaviour
{
    public Button button;
    public RectTransform iconsT;
    public Text text;

    public int stageNo { get; set; }
    
    public List<HeroIcon> UnitIcons { get; set; }

    public void ChangeColorOfIcons(bool on)
    {
        Image buttonImage = GetComponent<Image>();
        //buttonImage.raycastTarget = on;
        buttonImage.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        text.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        for (int i = 0; i < UnitIcons.Count; i++)
        {
            if (on)
                UnitIcons[i].LightOn();
            else 
                UnitIcons[i].Grey();
            UnitIcons[i].iconButton.targetGraphic.raycastTarget = on;
            button.targetGraphic.raycastTarget = on;
        }
    }
}