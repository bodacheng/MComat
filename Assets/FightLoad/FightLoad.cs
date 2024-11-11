using UnityEngine.SceneManagement;
using mainMenu;
using UnityEngine;
using FightScene;

public static class FightLoad
{
    public static FightInfo Fight;
    
    public static void Go(FightInfo fightInfo, bool inSceneLoad = false)
    {
        switch (fightInfo.EventType)
        {
            case FightEventType.Quest:
                switch (fightInfo.FightMode)
                {
                    case FightMode.Group:
                    case FightMode.Multi:
                        fightInfo.team1Mode = TeamMode.MultiRaid;
                        fightInfo.team2Mode = TeamMode.MultiRaid;
                        break;
                    case FightMode.Evolve:
                    case FightMode.Rotate:
                        fightInfo.team1Mode = TeamMode.Rotation;
                        fightInfo.team2Mode = TeamMode.Rotation;
                        break;
                }
                break;
            case FightEventType.Event:
                break;
            case FightEventType.Screensaver:
                fightInfo.team1Mode = TeamMode.Rotation;
                fightInfo.team2Mode = TeamMode.Rotation;
                break;
            case FightEventType.Arena:
            case FightEventType.Self:
                break;
            default:
                break;
        }
        
        if (fightInfo.FightMode == FightMode.Group)
        {
            fightInfo.Team1Auto = true;
            fightInfo.Team2Auto = true;
        }
        
        switch (fightInfo.EventType)
        {
            case FightEventType.Screensaver:
            case FightEventType.SkillTest:
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
        
        Fight =  FightInfo.Copy(fightInfo);

        if (!inSceneLoad)
        {
            PreScene.CashClear();
            SceneManager.LoadScene(2);
        }
        else
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        }
    }
    
    
}
