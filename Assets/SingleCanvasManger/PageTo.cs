using UnityEngine;
using mainMenu;
using System.Collections.Generic;

public class PageTo : MonoBehaviour
{
    public Canvas
        QuestShow,
        TeamEdit,
        ShopTop,
        SK_Show,
        UnitOption,
        UnitSkillEdit,
        UnitSkillShow,
        Gotcha,
        BoxExceedFixer,
        StoneBoxExpansion,
        StoneMerge;

    static IDictionary<MainSceneStep, Canvas> Dic;
    
    void Awake()
    {
        Dic = new Dictionary<MainSceneStep, Canvas>
        {
            {MainSceneStep.QuestInfo, QuestShow},
            {MainSceneStep.TeamEditFront, TeamEdit},
            {MainSceneStep.ShopTop, ShopTop},
            {MainSceneStep.SkillStoneList, SK_Show},
            {MainSceneStep.MonsterList, UnitOption},
            {MainSceneStep.UnitSkillEdit, UnitSkillEdit},
            {MainSceneStep.UnitSkillShow, UnitSkillShow},
            {MainSceneStep.GotchaFront, Gotcha},
            {MainSceneStep.GotchaAnim, Gotcha},
            {MainSceneStep.GotchaResult, Gotcha},
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
