using System;
using DummyLayerSystem;
using FightScene;

public partial class ArenaFightOver : UILayer
{
    public async void LoadNextGangbangStage()
    {
        Int32.TryParse(FightScene.FightScene.Fight.ID, out var nowStageNo);
        var nextStageNo = nowStageNo + 1;
        var nextFight = await PlayerAccountInfo.Me.GangbangModeManager.LoadStage(nextStageNo);
        if (nextFight != null)
        {
            nextStageTitle.text = "Stage " + nextStageNo;
            nextBtn.gameObject.SetActive(true);
            nextFor1v1Btn.gameObject.SetActive(false);
            nextForMultiBtn.gameObject.SetActive(false);
            nextBtn.SetListener(() =>
            {
                var newFightInstance = GangbangInfo.Copy(nextFight);
                newFightInstance.LoadMyTeam();
                newFightInstance.Team1GroupSet = FightScene.FightScene.team1GroupSet;
                newFightInstance.ConvertTeamToGangbang();
                FightScene.FightScene.Fight = newFightInstance;
                FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
                UILayerLoader.Remove<ArenaFightOver>();
            });
        }
    }
}
