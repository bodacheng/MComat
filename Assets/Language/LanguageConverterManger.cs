using System.Collections.Generic;
using UnityEngine;

public class LanguageConverterManger : MonoBehaviour
{
    public List<LanguageConverter> preList = new();
    public static List<LanguageConverter> List = new();
    
    void Awake()
    {
        List = preList;
        ChangeLanguage();
    }
    
    public static void ChangeLanguage()
    {
        for (int i = 0; i < List.Count; i++)
        {
            List[i].Change();
        }
    }
}
