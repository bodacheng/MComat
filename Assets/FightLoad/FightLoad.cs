using UnityEngine.SceneManagement;
using dataAccess;
using FightScene;
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
            string error;
            switch (AppSetting.Language)
            {
                case ApiLanguage.JaJp:
                    error = "チームメンバーは3人未満でステージに入場出来ません。";
                    break;
                default:
                    error = "队伍人员不够。";
                    break;
            }
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
                stage.Team1Auto = PlayerPrefs.GetInt("auto") == 1;
                stage.Team2Auto = true;
                break;
        }
        
        NetFightScene.Fight =  FightInfo.Copy(stage);
        Stones.Clear();
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        SceneManager.LoadScene(2);
    }
}
