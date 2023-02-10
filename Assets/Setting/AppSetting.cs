using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Json;

public class AppSetting
{
    public static AppSetting Value = new ();
    float _bgmVolume = 0.5f, _effectsVolume = 0.5f, _cvVolume = 0.5f;
    public static AudioSource bgmSource;

    public SystemLanguage Language { get; set; } = SystemLanguage.English;
    
    public float BgmVolume
    {
        get => _bgmVolume;
        set
        {
            _bgmVolume = Mathf.Clamp(value, 0, 1);
            bgmSource.volume = _bgmVolume;
        }
    }

    public float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = Mathf.Clamp(value, 0, 1);
        }
    }
    
    public float CvVolume
    {
        get => _cvVolume;
        set
        {
            _cvVolume = Mathf.Clamp(value, 0, 1);
        }
    }

    public static void Save()
    {
        string json = JsonConvert.SerializeObject(Value);
        LocalJson.SaveToJsonFile_persistentDataPath(null, "AppSetting.json", json);
    }
    
    public static void Load()
    {
        var wholePath = Application.persistentDataPath + "/AppSetting.json";
        if (File.Exists(wholePath))
        {
            var dataAsJson = File.ReadAllText(wholePath);
            Value = JsonConvert.DeserializeObject<AppSetting>(dataAsJson);
            Debug.Log("基本程序设置读取成功");
        }
        else
        {
            Value = new AppSetting
            {
                Language = Application.systemLanguage is SystemLanguage.ChineseSimplified or SystemLanguage.ChineseTraditional ? 
                    SystemLanguage.Chinese : Application.systemLanguage,
                _bgmVolume = 0.5f, _effectsVolume = 0.5f, _cvVolume = 0.5f
            };
            Save();
        }
    }
}
