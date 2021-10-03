using UnityEngine.SceneManagement;
using dataAccess;
using FightScene;

public static class FightLoad
{
    public static void Go(FightInfo stage, bool loadWithMyTeam = false)
    {
        if (stage.fightMembers == null)
            stage.fightMembers = new FightMembers();

        if (loadWithMyTeam)
        {
            stage.LoadMyTeam();
            stage.team1ID = Account._AccInfo.playerID;
        }
        
        if (stage.fightMembers.HeroSets.GetValues().Count < 1 || stage.fightMembers.EnemySets.GetValues().Count < 1)
        {
            string error;
            switch (Setting.Language)
            {
                case ApiLanguage.JaJp:
                    error = "チームメンバーは3人未満でステージに入場出来ません。";
                    break;
                default:
                    error = "队伍人员不够。";
                    break;
            }
            LoadingCanvas.target.ArrangeWarnWindow(error);
            return;
        }

        NetFightScene.Fight = stage;

        Stones.PreventStonesFromDestroy();
        ArcadeTop.ArcadeStages.Clear();
        GeneralModelPool.Clear();
        SceneManager.LoadScene(2);
    }
}
