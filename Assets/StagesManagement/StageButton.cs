using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class StageButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] HeroIcon unitIconPrefab;
    [SerializeField] RectTransform iconsT;
    [SerializeField] Text text;
    
    public Button Button => button;

    private int stageNo;
    public int StageNo
    {
        get=> stageNo;
        set
        {
            stageNo = value;
            text.text = "Stage" + value;
        }
    }

    List<HeroIcon> UnitIcons { get; set; }

    public void ChangeColorOfIcons(bool on)
    {
        var buttonImage = GetComponent<Image>();
        buttonImage.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        text.color = new Color(1, 1, 1, on ? 1 : 0.3f);
        button.targetGraphic.raycastTarget = on;
    }
    
    public void LoadUnitIcons(List<UnitInfo> units, Func<HeroIcon, UniTask> IconButtonFeature, bool clickBoss = false)
    {
        var heroIcons = UnitInfosShow(units, iconsT);
        for (var i = 0; i < heroIcons.Count; i++)
        {
            var heroIcon = heroIcons[i];
            heroIcon.iconButton.onClick.RemoveAllListeners();
            heroIcon.iconButton.onClick.AddListener( 
                async () =>
                {
                    ProgressLayer.Loading(">");
                    await IconButtonFeature(heroIcon);
                    ProgressLayer.Close();
                }
            );
            if (clickBoss && i == 0)
            {
                heroIcon.iconButton.onClick.Invoke();
            }
        }
        UnitIcons = heroIcons;
    } 
    
    List<HeroIcon> UnitInfosShow(List<UnitInfo> heroSets, RectTransform showT)
    {
        foreach (Transform t in showT)
        {
            Destroy(t.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach (var unitInfo in heroSets)
        {
            void load(UnitInfo unitInfo)
            {
                var v = HeroIcon.ArrangeHeroIconToParent(unitIconPrefab, unitInfo, showT);
                icons.Add(v);
            }
            load(unitInfo);
        }
        for (var i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}