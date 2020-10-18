using UnityEngine;
using UnityEngine.UI;

public class LanguageConverter : MonoBehaviour
{
    public string languageCode;
    public Text target;
    
    void Awake()
    {
        if (!LanguageConverterManger.list.Contains(this))
        {
            LanguageConverterManger.list.Add(this);
        }
        if (target == null)
            target = transform.GetComponent<Text>();
    }
    
    public void Change()
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
