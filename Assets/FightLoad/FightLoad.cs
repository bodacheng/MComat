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
    
    public static IEnumerator Arcade()
    {
        ToBeLoad.LoadLocalFightFromScript();
        PosKeySet set = TeamSet.Default;
        IEnumerator getDefaultTeamSet = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
        yield return getDefaultTeamSet;
        if (getDefaultTeamSet.Current == null)
        {
            Debug.Log("获取我方人员错误");
            yield break;
        }
        ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getDefaultTeamSet.Current;
    }
    
    public static IEnumerator Arena()
    {
        PosKeySet set = TeamSet.Arena3V3;
        IEnumerator getArenaTeam = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
        yield return getArenaTeam;
        if (getArenaTeam.Current == null)
        {
            Debug.Log("获取我方人员错误");
            yield break;
        }
        ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getArenaTeam.Current;
    }
    
    #region 进入战斗
    public static void GoTo()
    {
        IEnumerator LoadAndGo()
        {
            switch(ToBeLoadMode)
            {
                case TeamSetGameMode.story:
                    yield return Arcade();
                    break;
                case TeamSetGameMode.arena3V3:
                    yield return Arena();
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
                yield break;
            }
            Go(ToBeLoad);
        }
        PreScene.target.mainProcessRunner.RunAsQueued(LoadAndGo());
    }
    
    public static void Go(StageScriptableObject stage)
    {
        FightSceneNote.nextBattle = stage;
        SkillStonesBox.PreventCellsFromDestroy();
        MySkillStones.PreventStonesFromDestroy();
        ArcadeManager.ArcadeStages.Clear();
        GeneralModelPool.Clear();
        SceneManager.LoadScene(2);
    }
    #endregion
}
