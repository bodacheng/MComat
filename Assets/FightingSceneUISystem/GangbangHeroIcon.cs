using System;
using UnityEngine;
using UnityEngine.UI;

public class GangbangHeroIcon : HeroIcon
{
    [SerializeField] BOButton minusBtn;
    [SerializeField] BOButton plusBtn;
    [SerializeField] Text count;
    
    void SetUp(Func<int, int> countSet, Func<int> countGet)
    {
        count.text = countGet().ToString();
        plusBtn.SetListener(
            () =>
            {
                var currentCount = countGet();
                var newWholeCount = countSet(currentCount + 1);
                count.text = countGet().ToString();
            }
        );
        
        minusBtn.SetListener(
            () =>
            {
                var currentCount = countGet();
                var newWholeCount = countSet(currentCount - 1);
                count.text = countGet().ToString();
            }
        );
    }
    
    public static GangbangHeroIcon ArrangeGangbangHeroIconToParent(
        Func<int, int> TeamCountSet, Func<int> TeamCountGet,
        GangbangHeroIcon prefab, UnitInfo unitInfo,
        RectTransform T, float iconSize = 100, bool withSkillCheck = false)
    {
        var icon = Instantiate(prefab);
        icon.SetUp(TeamCountSet, TeamCountGet);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        icon.unitInfo = unitInfo;
        icon.unitConfig = unitConfig;
        icon.ChangeIcon(unitInfo, withSkillCheck);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize,iconSize);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
        return icon;
    }
}
