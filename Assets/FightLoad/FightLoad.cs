using UnityEngine.SceneManagement;
using dataAccess;
using mainMenu;
using FightScene;

public static class FightLoad
{
    public static void Go(StageScriptableObject stage, bool loadWithMyTeam = false)
    {
        if (stage.localFight == null)
            stage.localFight = new LocalFight();

        if (loadWithMyTeam)
        {
            PosKeySet set = null;
            switch (stage._fightEventType)
            {
                case FightEventType.Quest:
                    set = TeamSet.Default;
                    break;
                case FightEventType.Arena:
                    set = TeamSet.Arena3V3;
                    break;
            }

            stage.localFight.HeroSets = TeamSet.ToDic(set);
        }

        if (stage.localFight.HeroSets.values.Count < 1 || stage.localFight.EnemySets.values.Count < 1)
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
        ArcadeManager.ArcadeStages.Clear();
        GeneralModelPool.Clear();
        SceneManager.LoadScene(2);
    }
}
