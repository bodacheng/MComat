using System;
using DummyLayerSystem;

public partial class ArenaFightOver : UILayer
{
    public async void LoadNextGangbangStage()
    {
        Int32.TryParse(FightLoad.Fight.ID, out var nowStageNo);
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
                newFightInstance.ConvertTeamToGangbang();
                newFightInstance.Team1GroupSet = FightScene.FightScene.team1GroupSet;
                UILayerLoader.Remove<ArenaFightOver>();
                FightLoad.Go(newFightInstance, true);
            });
        }
    }
}
