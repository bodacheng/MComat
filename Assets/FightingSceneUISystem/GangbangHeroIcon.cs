using System;
using UnityEngine;
using UnityEngine.UI;

public class GangbangHeroIcon : HeroIcon
{
    [SerializeField] BOButton minusBtn;
    [SerializeField] BOButton plusBtn;
    [SerializeField] Text count;
    
    void SetUp(Func<int, int> countSet, Func<int> countGet, bool enableCountSet = true)
    {
        count.text = countGet().ToString();
        if (enableCountSet)
        {
            void Plus()
            {
                var currentCount = countGet();
                countSet(currentCount + 1);
                count.text = countGet().ToString();
            }
            plusBtn.SetListener(Plus);
            plusBtn.onHold.AddListener(Plus);

            void Minus()
            {
                var currentCount = countGet();
                countSet(currentCount - 1);
                count.text = countGet().ToString();
            }
            
            minusBtn.SetListener(Minus);
            minusBtn.onHold.AddListener(Minus);
        }
        else
        {
            plusBtn.gameObject.SetActive(false);
            minusBtn.gameObject.SetActive(false);
        }
    }
    
    public static void ArrangeGangbangHeroIconToParent(
        Func<int, int> TeamCountSet, Func<int> TeamCountGet,
        GangbangHeroIcon prefab, UnitInfo unitInfo,
        RectTransform T, float iconSize = 100, bool withSkillCheck = false, bool enableCountSet = true)
    {
        var icon = Instantiate(prefab);
        icon.SetUp(TeamCountSet, TeamCountGet, enableCountSet);
        var unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        icon.unitInfo = unitInfo;
        icon.unitConfig = unitConfig;
        icon.ChangeIcon(unitInfo, withSkillCheck);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize,iconSize);
        icon.transform.SetParent(T);
        icon.transform.localPosition = Vector3.one;
        icon.transform.localScale = Vector3.one;
        icon.gameObject.SetActive(true);
    }
}
