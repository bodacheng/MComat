using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageConverterManger : MonoBehaviour
{
    public List<LanguageConverter> PreList = new List<LanguageConverter>();
    public static List<LanguageConverter> list = new List<LanguageConverter>();

    void Awake()
    {
        list = PreList;
        LanguageCodeTable.LoadLanguageCodes();
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
