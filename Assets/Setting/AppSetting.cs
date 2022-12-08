using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Json;

public enum ApiLanguage
{
    EnUs,
    JaJp,
    ZhCn
}

public class AppSetting
{
    public static AppSetting value = new ();
    
    float _bgmVolume = 0.5f, _effectsVolume = 0.5f, _cvVolume = 0.5f;
    
    public static AudioSource bgmSource;
    
    public static ApiLanguage Language = ApiLanguage.JaJp;
    
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
        string json = JsonConvert.SerializeObject(value);
        LocalJson.SaveToJsonFile_persistentDataPath(null, "AppSetting.json", json);
    }
    
    public static void Load()
    {
        try
        {
            AppSetting info = new AppSetting();
            string wholePath = Application.persistentDataPath + "/AppSetting.json";
            if (File.Exists(wholePath))
            {
                string dataAsJson = File.ReadAllText(wholePath);
                info = JsonConvert.DeserializeObject<AppSetting>(dataAsJson);
                Debug.Log("基本程序设置读取成功");
            }
            value = info;
        }
        catch (Exception e)
        {
            Debug.Log("基本程序设置读取失败");
            Debug.Log(e.ToString());
            value = new AppSetting();
            Save();
        }
    }
}
