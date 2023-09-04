using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangbangHeroIcon : HeroIcon
{
    [SerializeField] BOButton minusBtn;
    [SerializeField] BOButton plusBtn;
    [SerializeField] Text count;

    private Func<int, int> TeamCountSet;

    void SetUp()
    {
        plusBtn.SetListener(
            () =>
            {
                count.text = TeamCountSet(1).ToString();
            }
        );
        
        minusBtn.SetListener(
            () =>
            {
                count.text = TeamCountSet(0).ToString();
            }
        );
    }
    
    public static GangbangHeroIcon ArrangeGangbangHeroIconToParent(
        GangbangHeroIcon prefab, UnitInfo unitInfo, RectTransform T, 
        float iconSize = 100, bool withSkillCheck = false)
    {
        var icon = Instantiate(prefab);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        icon.unitInfo = unitInfo;
        icon.unitConfig = unitConfig;
        icon.ChangeIcon(unitInfo, withSkillCheck);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize,iconSize);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
        
        icon.SetUp();
        return icon;
    }
}
