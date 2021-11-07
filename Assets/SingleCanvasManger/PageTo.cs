using UnityEngine;
using mainMenu;
using System.Collections.Generic;

public class PageTo : MonoBehaviour
{
    public Canvas
        ShopTop,
        UnitOption,
        UnitSkillEdit,
        BoxExceedFixer,
        StoneBoxExpansion,
        StoneMerge;

    static IDictionary<MainSceneStep, Canvas> Dic;
    
    void Awake()
    {
        Dic = new Dictionary<MainSceneStep, Canvas>
        {
            {MainSceneStep.ShopTop, ShopTop},
            {MainSceneStep.MonsterList, UnitOption},
            {MainSceneStep.UnitSkillEdit, UnitSkillEdit},
            {MainSceneStep.BoxOverLoadHelper, BoxExceedFixer},
            {MainSceneStep.BoxExpansion, StoneBoxExpansion},
            {MainSceneStep.StoneMerge, StoneMerge}
        };
    }

    public static void Go(MainSceneStep step)
    {
        foreach (var kv in Dic)
        {
            if (kv.Key == step)
            {
                if (kv.Value != null)
                    kv.Value.gameObject.SetActive(true);
            }
            else
            {
                if (kv.Value != null)
                    kv.Value.gameObject.SetActive(false);
            }
        }
    }
}
