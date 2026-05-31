using UnityEngine.SceneManagement;
using mainMenu;
using UnityEngine;
using FightScene;

public static class FightLoad
{
    public static FightInfo Fight;
    
    public static void Go(FightInfo fightInfo, bool inSceneLoad = false)
    {
        if (fightInfo.ShouldForceAutoBattle)
        {
            fightInfo.Team1Auto = true;
            fightInfo.Team2Auto = true;
        }
        else
        {
            fightInfo.Team1Auto = PlayerPrefs.GetInt("auto", 0) == 1;
            fightInfo.Team2Auto = true;
        }
        
        if (fightInfo.ShouldRunFirstQuestTutorial)
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
