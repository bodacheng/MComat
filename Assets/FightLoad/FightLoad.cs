using UnityEngine.SceneManagement;
using dataAccess;
using FightScene;
using mainMenu;

public static class FightLoad
{
    public static void Go(FightInfo stage, bool loadWithMyTeam = false)
    {
        if (loadWithMyTeam)
        {
            stage.LoadMyTeam();
            stage.team1ID = PlayerAccountInfo.Me.PlayFabId;
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
            PopupLayer.ArrangeWarnWindow(PreScene.target.T, error);
            return;
        }

        switch (stage.EventType)
        {
            case FightEventType.Screensaver:
                stage.team1Auto = true;
                stage.team2Auto = true;
                break;
            case FightEventType.SkillTest:
                stage.team1Auto = true;
                stage.team2Auto = true;
                break;
            default:
                stage.team1Auto = true;
                stage.team2Auto = true;
                break;
        }
        
        NetFightScene.Fight = stage;
        Stones.PreventStonesFromDestroy();
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        SceneManager.LoadScene(2);
    }
}
