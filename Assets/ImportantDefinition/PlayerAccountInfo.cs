using UnityEngine;
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
    public int arenaPoint = -1; // 依靠是否为-1来判断玩家的竞技场分数和防御队伍是否已经登陆。
    public int currentRank;
    
    public int ArenaCountToday;
    int _arcadeProcess;
    
    public int ArcadeProcess
    {
        get => _arcadeProcess;
        set => _arcadeProcess = Mathf.Clamp(value, 1, 100);
    }
    
    public string TutorialProgress = string.Empty;
    
    public PlayerAccountInfo()
    {
        TitleDisplayName = "helloKitty";
    }
}