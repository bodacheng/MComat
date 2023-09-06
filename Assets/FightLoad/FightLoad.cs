using UnityEngine.SceneManagement;
using mainMenu;
using UnityEngine;

public static class FightLoad
{
    public static void Go(GangbangInfo stage,  bool loadWithMyTeam = false)
    {
        // 
        Go(stage, loadWithMyTeam);
    }
    
    public static void Go(FightInfo fightInfo)
    {
        switch (fightInfo.EventType)
        {
            case FightEventType.Screensaver:
            case FightEventType.SkillTest:
            case FightEventType.Gangbang:
                fightInfo.Team1Auto = true;
                fightInfo.Team2Auto = true;
                break;
            default:
                fightInfo.Team1Auto = PlayerPrefs.GetInt("auto", 0) == 1;
                fightInfo.Team2Auto = true;
                break;
        }
        
        if (fightInfo.ID == "1" && fightInfo.EventType == FightEventType.Quest)
        {
            fightInfo.RunTutorial = true;
            fightInfo.Team1Auto = false;
            fightInfo.Team2Auto = false;
        }
        
        FightScene.FightScene.Fight =  FightInfo.Copy(fightInfo);
        PreScene.CashClear();
        SceneManager.LoadScene(2);
    }
}
