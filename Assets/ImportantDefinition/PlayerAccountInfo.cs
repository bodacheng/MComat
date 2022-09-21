using UnityEngine;
using Newtonsoft.Json;
using Json;
using System.IO;
using System;

[Serializable]
public class PlayerAccountInfo
{
    public static PlayerAccountInfo Me;
    
    public string PlayFabId;
    public string TitleDisplayName;
    public string PlayFabUserName;// for login
    public string Email;
    
    public string currentLinkedDeviceId;
    public int rankPont;
    public int currentRank;
    
    int _stoneBoxSize;
    int _arcadeProcess;
    
    public int ArcadeProcess
    {
        get => _arcadeProcess;
        set => _arcadeProcess = Mathf.Clamp(value, 1, 100);
    }
    
    public PlayerAccountProgressStep progress = PlayerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
        TitleDisplayName = "helloKitty";
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