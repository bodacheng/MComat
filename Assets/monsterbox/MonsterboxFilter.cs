using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterboxFilter : MonoBehaviour
{
    public Toggle byzokusei;
    public Dropdown typeDropDown;
    public InputField searchtag;

    // 等级升序降序？
    readonly int order;//0:升序 1:降序 //是否按type排序

    //关键词，貌似只是索引玩家对角色的自定义tag
    readonly string keyword;
    List<HeroIcon> Red = new List<HeroIcon>(),
                   Blue = new List<HeroIcon>(),
                   Green = new List<HeroIcon>(),
                   Light = new List<HeroIcon>(),
                   Dark = new List<HeroIcon>();

    public List<HeroIcon> OrderIcons(List<HeroIcon> origin_mainMenuIcons)
    {
        origin_mainMenuIcons = TypeFilter(origin_mainMenuIcons);    
        if (byzokusei.isOn)
        {
            origin_mainMenuIcons = OrderIconsByZokusei(origin_mainMenuIcons);
        }
        //origin_mainMenuIcons = putFavourateFirst(origin_mainMenuIcons);
        origin_mainMenuIcons = SearchByKeyword(origin_mainMenuIcons);
        return origin_mainMenuIcons;
    }

    public void RefreshTypeDropDown(List<string> types)
    {
        typeDropDown.options.Clear();
        for (int i = 0; i < types.Count; i++)
        {
            Dropdown.OptionData m_NewData = new Dropdown.OptionData
            {
                text = types[i]
            };
            typeDropDown.options.Add(m_NewData);
        }
    }

    List<HeroIcon> TypeFilter(List<HeroIcon> origin_mainMenuIcons)
    {
        List<HeroIcon> new_mainMenuIcons = new List<HeroIcon>();
        if (typeDropDown.options.Count > 0 && typeDropDown.options[typeDropDown.value] != null)
        {
            for (int i = 0; i < origin_mainMenuIcons.Count; i++)
            {
                if (origin_mainMenuIcons[i] != null)
                {
                    if (origin_mainMenuIcons[i].unitConfig.TYPE == typeDropDown.options[typeDropDown.value].text)
                    {
                        new_mainMenuIcons.Add(origin_mainMenuIcons[i]);
                    }
                }
                else
                {
                    Debug.Log("丢失角色头像");
                }
            }
            return new_mainMenuIcons;
        }
        Debug.Log("typeDropDown错误。当前type 有：" + typeDropDown.options.Count + "个值");
        return origin_mainMenuIcons;
    }
    
    List<HeroIcon> OrderIconsByZokusei(List<HeroIcon> origin_mainMenuIcons)
    {
        Red.Clear();Blue.Clear();Green.Clear();Light.Clear();Dark.Clear();
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            switch (origin_mainMenuIcons[i].unitConfig.element)
            {
                case Element.redMagic:
                    Red.Add(origin_mainMenuIcons[i]);
                    break;
                case Element.blueMagic:
                    Blue.Add(origin_mainMenuIcons[i]);
                    break;
                case Element.greenMagic:
                    Green.Add(origin_mainMenuIcons[i]);
                    break;
                case Element.lightMagic:
                    Light.Add(origin_mainMenuIcons[i]);
                    break;
                case Element.darkMagic:
                    Dark.Add(origin_mainMenuIcons[i]);
                    break;
                default:
                    Debug.Log("角色属性信息错误："+origin_mainMenuIcons[i].unitConfig.REAL_NAME);
                    Red.Add(origin_mainMenuIcons[i]);
                break;
            }
        }
        List<HeroIcon> newList = new List<HeroIcon>();
        
        newList.AddRange(Red);
        newList.AddRange(Blue);
        newList.AddRange(Green);
        newList.AddRange(Light);
        newList.AddRange(Dark);
        return newList;
    }

    List<HeroIcon> PutFavourateFirst(List<HeroIcon> origin_mainMenuIcons)
    {
        List<HeroIcon> favorites = new List<HeroIcon>(); 
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            //if (origin_mainMenuIcons[i]._CharacterDataInfo.favorite)
                //favorites.Add(origin_mainMenuIcons[i]);
        }
        List<HeroIcon> new_mainMenuIcons = new List<HeroIcon>();
        for (int i = 0; i < favorites.Count;i++)
        {
            new_mainMenuIcons.Add(favorites[i]);
        }        
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            if (!favorites.Contains(origin_mainMenuIcons[i]))
                new_mainMenuIcons.Add(origin_mainMenuIcons[i]);
        }
        return new_mainMenuIcons;
    }
    
    List<HeroIcon> SearchByKeyword(List<HeroIcon> origin_mainMenuIcons)
    {
        List<HeroIcon> targets = new List<HeroIcon>(); 
        //for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        //{
        //    if (origin_mainMenuIcons[i].AccountCharacterInfo.userd_efined_name != null 
        //        &&
        //        origin_mainMenuIcons[i].AccountCharacterInfo.userd_efined_name.Contains(searchtag.text))
        //        targets.Add(origin_mainMenuIcons[i]);
        //}
        List<HeroIcon> new_mainMenuIcons = new List<HeroIcon>();
        for (int i = 0; i < targets.Count;i++)
        {
            new_mainMenuIcons.Add(targets[i]);
        }        
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            if (!targets.Contains(origin_mainMenuIcons[i]))
                new_mainMenuIcons.Add(origin_mainMenuIcons[i]);
        }
        return new_mainMenuIcons;
    } 
}
