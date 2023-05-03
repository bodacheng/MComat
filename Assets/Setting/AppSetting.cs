using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Json;

public class AppSetting
{
    public static AppSetting Value = new AppSetting();
    float _bgmVolume = 0.5f, _effectsVolume = 0.5f, _cvVolume = 0.5f;
    
    public SystemLanguage Language { get; set; } = SystemLanguage.English;

    private string lobbyTheme ;
    
    public static async UniTask PlayBGM(string addressKey)
    {
        var clip = await AddressablesLogic.LoadT<AudioClip>(addressKey);
        BGMSource.clip = clip;
        BGMSource.loop = true;
        BGMSource.Play();
    }
    
    private static AudioSource bgmSource;
    public static AudioSource BGMSource
    {
        get => bgmSource;
        set
        {
            value.volume = AppSetting.Value._bgmVolume;
            bgmSource = value;
        }
    }

    public void Mute()
    {
        if (bgmSource != null)
            bgmSource.mute = true;
    }
    
    public void UnMute()
    {
        if (bgmSource != null)
            bgmSource.mute = false;
    }
    
    public float BgmVolume
    {
        get => _bgmVolume;
        set
        {
            _bgmVolume = Mathf.Clamp(value, 0, 1);
            if (BGMSource != null)
                BGMSource.volume = _bgmVolume;
        }
    }

    public float EffectsVolume
    {
        get => _effectsVolume;
        set => _effectsVolume = Mathf.Clamp(value, 0, 1);
    }
    
    public float CvVolume
    {
        get => _cvVolume;
        set => _cvVolume = Mathf.Clamp(value, 0, 1);
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
        }
        else
        {
            Value = new AppSetting
            {
                Language = (Application.systemLanguage == SystemLanguage.ChineseSimplified ||
                           Application.systemLanguage == SystemLanguage.ChineseTraditional) ? 
                    SystemLanguage.Chinese : Application.systemLanguage,
                _bgmVolume = 0.5f, _effectsVolume = 0.5f, _cvVolume = 0.5f
            };
            Save();
        }
    }
}
