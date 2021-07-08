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
        MonsterBox,
        UnitSkillEdit,
        MemberDetail,
        Gotcha,
        StoneBoxExpansion,
        StoneMerge;

    static IDictionary<MainSceneStep, Canvas> Dic;
    
    void Awake()
    {
        Dic.Add(MainSceneStep.FrontPage, Main);
        Dic.Add(MainSceneStep.ArcadeFront, Arcade);
        Dic.Add(MainSceneStep.Arena, Arena3V3);
        Dic.Add(MainSceneStep.SelfFightFront, SelfFight);
        Dic.Add(MainSceneStep.QuestInfo, QuestShow);
        Dic.Add(MainSceneStep.TeamEditFront, TeamEdit);
        Dic.Add(MainSceneStep.ShopTop, ShopTop);
        Dic.Add(MainSceneStep.SkillStoneList, SK_Show);
        Dic.Add(MainSceneStep.MonsterList, MonsterBox);
        Dic.Add(MainSceneStep.MemberDetail_edit, UnitSkillEdit);
        Dic.Add(MainSceneStep.MemberDetail_show, MemberDetail);
        Dic.Add(MainSceneStep.GotchaFront, Gotcha);
        Dic.Add(MainSceneStep.GotchaAnim, Gotcha);
        Dic.Add(MainSceneStep.GotchaResult, Gotcha);
        Dic.Add(MainSceneStep.BoxExpansion, StoneBoxExpansion);
        Dic.Add(MainSceneStep.StoneMerge, StoneMerge);
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
