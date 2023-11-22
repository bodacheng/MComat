using DummyLayerSystem;

public partial class ArenaFightOver : UILayer
{
    public async void LoadNextGangbangStage(int stageNo)
    {
        var nextFight = await PlayerAccountInfo.Me.GangbangModeManager.LoadStage(stageNo);
        if (nextFight != null)
        {
            nextStageTitle.text = "Stage " + stageNo;
            nextBtn.gameObject.SetActive(true);
            nextFor1v1Btn.gameObject.SetActive(false);
            nextForMultiBtn.gameObject.SetActive(false);
            nextBtn.SetListener(() =>
            {
                var newFightInstance = GangbangInfo.Copy(nextFight);
                newFightInstance.LoadMyTeam();
                newFightInstance.Team1GroupSet = FightScene.FightScene.team1GroupSet;
                newFightInstance.ConvertTeamToGangbang();
                UILayerLoader.Remove<ArenaFightOver>();
                FightLoad.Go(newFightInstance, true);
            });
        }
    }
}
