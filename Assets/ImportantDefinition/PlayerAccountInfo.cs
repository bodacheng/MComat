using UnityEngine;
using Newtonsoft.Json;
using Json;
using System.IO;
using System;

[Serializable]
public class PlayerAccountInfo
{
    public static PlayerAccountInfo Me;
    
    public string PlayFabUsername;
    public string PlayerName;
    
    int _stoneBoxSize;
    int _arcadeProcess;
    
    public int ArcadeProcess
    {
        get => _arcadeProcess;
        set => _arcadeProcess = Mathf.Clamp(value, 1, 100);
    }
    
    public int StoneBoxSize
    {
        get => _stoneBoxSize;
        set => _stoneBoxSize = Mathf.Clamp(value, 0, value);
    }
    
    public PlayerAccountProgressStep progress = PlayerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
        PlayerName = "helloKitty";
    }
    
    public static void Save()
    {
        string json = JsonConvert.SerializeObject(Me);
        LocalJson.SaveToJsonFile_persistentDataPath(null, "account.json", json);
    }
    
    /// <summary>
    /// 查找本地是否有保存的账号信息，有的话就从中读取登陆用id，并且登陆playfab
    /// 如果没有，则用设备代码登陆，并且登陆成功后，直接将账号信息保存至本地
    /// </summary>
    public static void Load()
    {
        try
        {
            var path = Application.persistentDataPath + "/account.json";
            Debug.Log("从这里寻找存储于本地的账户信息："+ path);
            if (File.Exists(path))
            {
                var dataAsJson = File.ReadAllText(path);
                Me = JsonConvert.DeserializeObject<PlayerAccountInfo>(dataAsJson);
            }
            else
            {
                Me = null;
            }
        }
        catch (Exception e)
        {
            
        }
    }
}

public enum PlayerAccountProgressStep
{
    justCreated = 1,
    Tutorial = 2,
    justNamed = 3,
    MenuTuorial = 4,
    Freedom = 5,
    Season1_Completed = 6,
}