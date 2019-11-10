using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;

public class monsterboxFilter : MonoBehaviour
{
    //public LoadingCanvas loadingCanvas;
    public Toggle byzokusei;
    public Toggle bylevel;
    public Dropdown typeDropDown;
    public InputField searchtag;
    
    // 等级升序降序？
    int order = 0;//0:升序 1:降序 
    //是否按type排序
    
    //关键词，貌似只是索引玩家对角色的自定义tag
    string keyword;

    List<charIcon> Red = new List<charIcon>(),
                   Blue = new List<charIcon>(),
                   Green = new List<charIcon>(),
                   Light = new List<charIcon>(),
                   Dark = new List<charIcon>();

    public List<charIcon> orderIcons(List<charIcon> origin_mainMenuIcons)
    {
        origin_mainMenuIcons = typeFilter(origin_mainMenuIcons);    
        if (bylevel.isOn)
            origin_mainMenuIcons = orderIconsByLevel(origin_mainMenuIcons,order);

        if (byzokusei.isOn)
            origin_mainMenuIcons = orderIconsByZokusei(origin_mainMenuIcons);

        //origin_mainMenuIcons = putFavourateFirst(origin_mainMenuIcons);
        origin_mainMenuIcons = searchByKeyword(origin_mainMenuIcons);
        return origin_mainMenuIcons;
    }

    List<charIcon> typeFilter(List<charIcon> origin_mainMenuIcons)
    {
        List<charIcon> new_mainMenuIcons = new List<charIcon>();
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            if (origin_mainMenuIcons[i]._CharacterResourceInfo.type == typeDropDown.options[typeDropDown.value].text)
                new_mainMenuIcons.Add(origin_mainMenuIcons[i]);
        }
        return new_mainMenuIcons;
    }

    List<charIcon> orderIconsByLevel(List<charIcon> origin_mainMenuIcons,int order)
    {
        for (int i = 0; i < origin_mainMenuIcons.Count - 1; i++)
        {
            for (int j = 0; j< origin_mainMenuIcons.Count-1-i; j++)
            {
                int expj = origin_mainMenuIcons[j].AccountCharacterInfo.experience;
                int expj1 = origin_mainMenuIcons[j + 1].AccountCharacterInfo.experience;
                if (order == 1 ? expj > expj1 : expj < expj1)
                {
                    charIcon temp = origin_mainMenuIcons[j];
                    origin_mainMenuIcons[j]=origin_mainMenuIcons[j+1];
                    origin_mainMenuIcons[j + 1] = temp;
                }
            }
        }
        return origin_mainMenuIcons;
    }
    
    List<charIcon> orderIconsByZokusei(List<charIcon> origin_mainMenuIcons)
    {
        Red.Clear();Blue.Clear();Green.Clear();Light.Clear();Dark.Clear();
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            switch (origin_mainMenuIcons[i]._CharacterResourceInfo._zokusei)
            {
                case Zokusei.redMagic:
                    Red.Add(origin_mainMenuIcons[i]);
                    break;
                case Zokusei.blueMagic:
                    Blue.Add(origin_mainMenuIcons[i]);
                    break;
                case Zokusei.greenMagic:
                    Green.Add(origin_mainMenuIcons[i]);
                    break;
                case Zokusei.lightMagic:
                    Light.Add(origin_mainMenuIcons[i]);
                    break;
                case Zokusei.darkMagic:
                    Dark.Add(origin_mainMenuIcons[i]);
                    break;
                default:
                    Debug.Log("角色属性信息错误："+origin_mainMenuIcons[i]._CharacterResourceInfo.REAL_NAME);
                    Red.Add(origin_mainMenuIcons[i]);
                break;
            }
        }
        List<charIcon> newList = new List<charIcon>();
        
        newList.AddRange(Red);
        newList.AddRange(Blue);
        newList.AddRange(Green);
        newList.AddRange(Light);
        newList.AddRange(Dark);
        return newList;
    }
    
    List<charIcon> putFavourateFirst(List<charIcon> origin_mainMenuIcons)
    {
        List<charIcon> favorites = new List<charIcon>(); 
        for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        {
            //if (origin_mainMenuIcons[i]._CharacterDataInfo.favorite)
                //favorites.Add(origin_mainMenuIcons[i]);
        }
        List<charIcon> new_mainMenuIcons = new List<charIcon>();
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
    
    List<charIcon> searchByKeyword(List<charIcon> origin_mainMenuIcons)
    {
        List<charIcon> targets = new List<charIcon>(); 
        //for (int i = 0; i < origin_mainMenuIcons.Count; i++)
        //{
        //    if (origin_mainMenuIcons[i].AccountCharacterInfo.userd_efined_name != null 
        //        &&
        //        origin_mainMenuIcons[i].AccountCharacterInfo.userd_efined_name.Contains(searchtag.text))
        //        targets.Add(origin_mainMenuIcons[i]);
        //}
        List<charIcon> new_mainMenuIcons = new List<charIcon>();
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
