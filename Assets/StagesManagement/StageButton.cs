using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageButton : MonoBehaviour
{
    public Button button;
    public RectTransform IconsT;
    public Text text;
    public int ID;
}

public class StageInfo
{
    public FightInfo stageConfig;
    public StageButton stageButton;
    public List<HeroIcon> MemberIcons;
    
    // 即 关卡是否已经解锁
    public void ChangeColorOfIcons(bool on)
    {
        Image buttonImage = stageButton.GetComponent<Image>();
        if (on)
        {
            //buttonImage.raycastTarget = true;
            buttonImage.color = new Color(1, 1, 1, 1);
            stageButton.text.color = new Color(1, 1, 1, 1);
            for (int i = 0; i < MemberIcons.Count; i++)
            {
                MemberIcons[i].LightOn();
                MemberIcons[i].iconButton.targetGraphic.raycastTarget = true;
            }
        }else{
            //buttonImage.raycastTarget = false;
            buttonImage.color = new Color(1, 1, 1, 0.3f);
            stageButton.text.color = new Color(1, 1, 1, 0.3f);
            for (int i = 0; i < MemberIcons.Count; i++)
            {
                MemberIcons[i].Grey();
                MemberIcons[i].iconButton.targetGraphic.raycastTarget = true; // 可以浏览未解锁关卡
            }
        }
    }
}