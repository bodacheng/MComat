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
    [SerializeField] Text id;
    [SerializeField] Text rewardDM;
    [SerializeField] Text rewardGD;
    [SerializeField] Image rewardDMIcon;
    [SerializeField] Image rewardGDIcon;

    public Button Button => button;

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

    public void AwardRender(bool got)
    {
        rewardDM.color = new Color(rewardDM.color.r, rewardDM.color.g, rewardDM.color.b, got ? 0.3f : 1f);
        rewardGD.color = new Color(rewardGD.color.r, rewardGD.color.g, rewardGD.color.b, got ? 0.3f : 1f);
        rewardDMIcon.color = new Color(rewardDMIcon.color.r, rewardDMIcon.color.g, rewardDMIcon.color.b, got ? 0.3f : 1f);
        rewardGDIcon.color = new Color(rewardGDIcon.color.r, rewardGDIcon.color.g, rewardGDIcon.color.b, got ? 0.3f : 1f);
    }

    List<HeroIcon> UnitIcons { get; set; }

    public void ShowRewards(int awardDM, int awardGD)
    {
        rewardDM.text = awardDM.ToString();
        rewardGD.text = awardGD.ToString();
    }
    
    public void ChangeColorOfIcons(bool on)
    {
        var buttonImage = GetComponent<Image>();
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, on ? 1 : 0.3f);
        id.color = new Color(id.color.r, id.color.g, id.color.b, on ? 1 : 0.3f);
        button.targetGraphic.raycastTarget = on;
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