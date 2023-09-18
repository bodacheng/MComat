using System;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class StageButton : MonoBehaviour
{
    public void LoadUnitIconsGangbang(List<UnitInfo> units, Func<string, int> TeamCountGet, Func<HeroIcon, UniTask> iconButtonFeature, bool clickBoss = false)
    {
        var heroIcons = GangbangUnitInfosShow(units, TeamCountGet, iconsT);
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
    
    List<GangbangHeroIcon> GangbangUnitInfosShow(List<UnitInfo> heroSets, Func<string, int> TeamCountGet, RectTransform showT)
    {
        foreach (Transform t in showT)
        {
            Destroy(t.gameObject);
        }
        var icons = new List<GangbangHeroIcon>();
        foreach (var unitInfo in heroSets)
        {
            void Load(UnitInfo unitInfo)
            {
                var v = GangbangHeroIcon.ArrangeGangbangHeroIconToParent
                    (null,()=>TeamCountGet(unitInfo.id), gangbangIconPrefab, unitInfo, showT, false, false);
                icons.Add(v);
            }
            Load(unitInfo);
        }
        for (var i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}
