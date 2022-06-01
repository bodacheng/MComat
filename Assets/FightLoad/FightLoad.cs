using UnityEngine.SceneManagement;
using dataAccess;
using FightScene;
using mainMenu;
using UnityEngine;

public static class FightLoad
{
    public static void Go(FightInfo stage, bool loadWithMyTeam = false)
    {
        if (stage.members == null)
            stage.members = new FightMembers();
        
        if (loadWithMyTeam)
        {
            stage.LoadMyTeam();
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
        }
        
        if (stage.members.HeroSets.GetValues().Count < 1 || stage.members.EnemySets.GetValues().Count < 1)
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
            PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeWarnWindow(error);
            return;
        }

        switch (stage.GetEventType())
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

        Debug.Log(stage.members.EnemySets._SerializableSets.Length + ":"+ stage.members.HeroSets._SerializableSets.Length);
        
        NetFightScene.Fight = stage;
        Stones.PreventStonesFromDestroy();
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        SceneManager.LoadScene(2);
    }
}
