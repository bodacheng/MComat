using UnityEngine.SceneManagement;
using dataAccess;
using mainMenu;
using System.Collections;
using UnityEngine;

public static class FightLoad
{
    #region 待加载战斗信息
    public static StageScriptableObject ToBeLoad;
    public static TeamSetGameMode ToBeLoadMode;
    #endregion
    
    // 加载战斗信息
    public static void PreLoad(StageScriptableObject stageScriptableObject, TeamSetGameMode teamSetGameMode)
    {
        ToBeLoad = stageScriptableObject;
        ToBeLoadMode = teamSetGameMode;
    }
    
    public static void Arcade()
    {
        ToBeLoad.LoadLocalFightFromScript();
        PosKeySet set = TeamSet.Default;
        ToBeLoad.localFight.HeroSets = TeamSet.ToDic(set);
    }
    
    public static void Arena()
    {
        PosKeySet set = TeamSet.Arena3V3;
        ToBeLoad.localFight.HeroSets = TeamSet.ToDic(set);
    }
    
    #region 进入战斗
    public static void GoTo()
    {
        switch (ToBeLoadMode)
        {
            case TeamSetGameMode.story:
                Arcade();
                break;
            case TeamSetGameMode.arena3V3:
                Arena();
                break;
        }
        if (ToBeLoad.localFight.HeroSets.values.Count < 1 || ToBeLoad.localFight.EnemySets.values.Count < 1)
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
        Go(ToBeLoad);
    }
    
    public static void Go(StageScriptableObject stage)
    {
        FightSceneNote.nextBattle = stage;
        SkillStonesBox.PreventCellsFromDestroy();
        Stones.PreventStonesFromDestroy();
        ArcadeManager.ArcadeStages.Clear();
        GeneralModelPool.Clear();
        SceneManager.LoadScene(2);
    }
    #endregion
}
