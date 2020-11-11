using UnityEngine;

[System.Serializable]
public class PlayerAccountInfo
{
    public string playerID;
    public string PlayerName;
    
    int coin;//智慧果实
    int diamond;
    int stoneboxsize;
    
    int arcadeProcess;

    float bgmVolumn = 0.5f, effectsVolumn = 0.5f;
    
    public PlayerAccountProgressStep accountprogress = PlayerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
        PlayerName = "helloKitty";
        coinCount = 999999999;
        diamondCount = 0;
        Stoneboxsize = 500;
    }
    
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
    
    public int diamondCount
    {
        get => diamond;
        set
        {
            diamond = Mathf.Clamp(value, 0, value);
        }
    }

    public int coinCount
    {
        get => coin;
        set
        {
            coin = Mathf.Clamp(value, 0, value);
        }
    }

    public void PlusCoin(int plus)
    {
        coinCount = coinCount + plus;
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