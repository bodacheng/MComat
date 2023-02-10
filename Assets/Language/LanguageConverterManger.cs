using System.Collections.Generic;
using UnityEngine;

public class LanguageConverterManger : MonoBehaviour
{
    public List<LanguageConverter> PreList = new();
    public static List<LanguageConverter> list = new();
    
    void Awake()
    {
        list = PreList;
        ChangeLanguage();
    }
    
    public static void ChangeLanguage()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Change();
        }
    }
}
