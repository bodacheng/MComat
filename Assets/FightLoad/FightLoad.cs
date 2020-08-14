using UnityEngine.SceneManagement;
using dataAccess;
using mainMenu;

public static class FightLoad
{
    public static void Go(StageScriptableObject stage)
    {
        FightSceneNote.nextBattle = stage;
        SkillStonesBox.PreventCellsFromDestroy();
        MySkillStonesReader.PreventStonesFromDestroy();
        ArcadeManager.PreventStageButtonsFromDestroy();
        SceneManager.LoadScene(2);
    }
}
