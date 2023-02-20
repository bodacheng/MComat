using UnityEngine.SceneManagement;
using dataAccess;
using UnityEngine;

public static class FightLoad
{
    public static void Go(FightInfo stage, bool loadWithMyTeam = false)
    {
        if (loadWithMyTeam)
        {
            stage.LoadMyTeam();
            stage.Team1ID = PlayerAccountInfo.Me.PlayFabId;
        }
        
        if (stage.FightMembers.HeroSets.GetValues().Count < 1 || stage.FightMembers.EnemySets.GetValues().Count < 1)
        {
            string error = Translate.Get("TeamNotFull");
            PopupLayer.ArrangeWarnWindow(error);
            return;
        }

        switch (stage.EventType)
        {
            case FightEventType.Screensaver:
                stage.Team1Auto = true;
                stage.Team2Auto = true;
                break;
            case FightEventType.SkillTest:
                stage.Team1Auto = true;
                stage.Team2Auto = true;
                break;
            default:
                stage.Team1Auto = PlayerPrefs.GetInt("auto", 0) == 1;
                stage.Team2Auto = true;
                break;
        }
        
        if (stage.ID == "1")
        {
            stage.RunTutorial = true;
            stage.Team1Auto = false;
            stage.Team2Auto = false;
            Debug.Log("AI1模式："+stage.team1AIMode);
        }
        
        FightScene.FightScene.Fight =  FightInfo.Copy(stage);
        
        Stones.ClearRender();
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        SceneManager.LoadScene(2);
    }
}
