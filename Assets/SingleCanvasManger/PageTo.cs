using UnityEngine;
using mainMenu;
using System.Collections.Generic;

public class PageTo : MonoBehaviour
{
    public Canvas Main,
        Arcade,
        Arena3V3,
        SelfFight,
        QuestShow,
        TeamEdit,
        ShopTop,
        SK_Show,
        UnitOption,
        UnitSkillEdit,
        UnitSkillShow,
        Gotcha,
        StoneBoxExpansion,
        StoneMerge;

    static IDictionary<MainSceneStep, Canvas> Dic;
    
    void Awake()
    {
        Dic = new Dictionary<MainSceneStep, Canvas>
        {
            {MainSceneStep.FrontPage, Main},
            {MainSceneStep.ArcadeFront, Arcade},
            {MainSceneStep.Arena, Arena3V3},
            {MainSceneStep.SelfFightFront, SelfFight},
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
