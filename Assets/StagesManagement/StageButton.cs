using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class StageButton : MonoBehaviour
{
    [SerializeField] BOButton button;
    [SerializeField] HeroIcon unitIconPrefab;
    [SerializeField] GangbangHeroIcon gangbangIconPrefab;
    [SerializeField] RectTransform iconsT;
    [SerializeField] Text id;
    [SerializeField] RewardUI rewardUI;
    
    public Button Button => button;
    public RewardUI RewardUI => rewardUI;

    public CriticalGaugeMode CriticalGaugeMode {
        set;
        get;
    }

    private int stageNo;
    public int StageNo
    {
        get=> stageNo;
        set
        {
            stageNo = value;
            id.text = value.ToString();
        }
    }
    
    public void ChangeColorOfIcons(bool on)
    {
        var buttonImage = GetComponent<Image>();
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, on ? 1 : 0.3f);
        id.color = new Color(id.color.r, id.color.g, id.color.b, on ? 1 : 0.3f);
        button.interactable = on;
    }
    
    public void LoadUnitIcons(List<UnitInfo> units, Func<HeroIcon, UniTask> iconButtonFeature, bool clickBoss = false)
    {
        var heroIcons = UnitInfosShow(units, iconsT);
        for (var i = 0; i < heroIcons.Count; i++)
        {
            var heroIcon = heroIcons[i];
            heroIcon.iconButton.onClick.RemoveAllListeners();
            heroIcon.iconButton.onClick.AddListener(
                async () =>
                {
                    ProgressLayer.Loading(string.Empty);
                    await iconButtonFeature(heroIcon);
                    ProgressLayer.Close();
                }
            );
            if (clickBoss && i == 0)
            {
                heroIcon.iconButton.onClick.Invoke();
            }
        }
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