using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using mainMenu;
using UnityEngine;
using FightScene;

public static class FightLoad
{
    const int FightSceneBuildIndex = 2;
    public const float SceneLoadingProgressEnd = 0.12f;
    static bool sceneLoadInProgress;

    public static FightInfo Fight;

    public static void Go(FightInfo fightInfo, bool inSceneLoad = false)
    {
        if (!inSceneLoad && sceneLoadInProgress)
        {
            return;
        }

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
            LoadFightSceneAsync().Forget();
        }
        else
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        }
    }

    static async UniTaskVoid LoadFightSceneAsync()
    {
        if (sceneLoadInProgress)
        {
            return;
        }

        sceneLoadInProgress = true;
        var loadingBattleText = Translate.Get("LoadingBattle");
        ProgressLayer.Loading(loadingBattleText);
        ProgressLayer.LoadingPercent(loadingBattleText, 0f, false);

        try
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
            var operation = SceneManager.LoadSceneAsync(FightSceneBuildIndex);
            if (operation == null)
            {
                SceneManager.LoadScene(FightSceneBuildIndex);
                return;
            }

            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
                var sceneProgress = Mathf.Clamp01(operation.progress / 0.9f);
                ProgressLayer.LoadingPercent(loadingBattleText, Mathf.Lerp(0f, SceneLoadingProgressEnd, sceneProgress), false);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            ProgressLayer.LoadingPercent(loadingBattleText, SceneLoadingProgressEnd, false);
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        finally
        {
            sceneLoadInProgress = false;
        }
    }
}
