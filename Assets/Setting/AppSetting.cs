using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Json;

public class AppSetting
{
    public static AppSetting value = new AppSetting();

    float bgmVolumn = 0.5f, effectsVolumn = 0.5f;
    public float BgmVolumn
    {
        get => bgmVolumn;
        set
        {
            bgmVolumn = Mathf.Clamp(value, 0, 1);
        }
    }

    public float EffectsVolumn
    {
        get => effectsVolumn;
        set
        {
            effectsVolumn = Mathf.Clamp(value, 0, 1);
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
            string wholepath = Application.persistentDataPath + "/AppSetting.json";
            if (File.Exists(wholepath))
            {
                string dataAsJson = File.ReadAllText(wholepath);
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
