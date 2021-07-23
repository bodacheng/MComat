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
}

public class StageInfo
{
    public FightInfo stageConfig;
    public StageButton stageButton;
    
    // 即 关卡是否已经解锁
    public void ChangeColorOfIcons(bool on)
    {
        Image buttonImage = stageButton.GetComponent<Image>();
        //buttonImage.raycastTarget = on;
        buttonImage.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        stageButton.text.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        for (int i = 0; i < stageButton.MemberIcons.Count; i++)
        {
            if (on)
                stageButton.MemberIcons[i].LightOn();
            else 
                stageButton.MemberIcons[i].Grey();
            stageButton.MemberIcons[i].iconButton.targetGraphic.raycastTarget = on;
            stageButton.button.targetGraphic.raycastTarget = on;
        }
    }
}