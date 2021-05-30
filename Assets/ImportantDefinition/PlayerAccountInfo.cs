using UnityEngine;

[System.Serializable]
public class PlayerAccountInfo
{
    public string playerID;
    public string PlayerName;

    int stoneboxsize;
    int arcadeProcess;

    public int ArcadeProcess
    {
        get => arcadeProcess;
        set
        {
            arcadeProcess = Mathf.Clamp(value, 1, 100);
        }
    }

    public int Stoneboxsize
    {
        get => stoneboxsize;
        set
        {
            stoneboxsize = Mathf.Clamp(value, 0, value);
        }
    }

    public PlayerAccountProgressStep accountprogress = PlayerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
        PlayerName = "helloKitty";
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