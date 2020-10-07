using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LanguageConverter : MonoBehaviour
{
    static List<LanguageConverter> list = new List<LanguageConverter>();
    public string languageCode;
    Text target;
    
    void Awake()
    {
        if (!list.Contains(this))
        {
            list.Add(this);
        }
        target = transform.GetComponent<Text>();
    }
    
    public static void ChangeLanguage()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Change();
        }
    }

    void Change()
    {
        if (target != null && !string.IsNullOrEmpty(languageCode))
        {
            LanguageCodeTable.Row row = LanguageCodeTable.Find_RECORD_ID(languageCode);
            if (row != null)
            {
                switch (Setting.Language)
                {
                    case ApiLanguage.EnUs:
                        target.text = row.EN;
                        break;
                    case ApiLanguage.JaJp:
                        target.text = row.JP;
                        break;
                    case ApiLanguage.ZhCn:
                        target.text = row.CH;
                        break;
                }
            }
        }
    }
}
