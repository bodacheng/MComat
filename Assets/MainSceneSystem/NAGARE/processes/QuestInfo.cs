using System.Collections;
using mainMenu;
using dataAccess;

public class QuestInfo : MainSceneProcess
{
    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowMyModel(null);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
        yield return GetReadyForQuestInfoPage();
    }
    
    public QuestInfo()
    {
        Step = MainSceneStep.QuestInfo;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(false);
    }
    
    // 这个函数目前是固定使用“默认队伍配置”
    public IEnumerator GetReadyForQuestInfoPage()
    {
        FightPreparePage.target.QuestName.text = FightLoad.ToBeLoad.battleNameJPG;
        switch(FightLoad.ToBeLoadMode)
        {
            case TeamSetGameMode.arena3V3:
                void GoToTeamEdit_Arena()
                {
                    TeamSet.SwitchTargetTeam(TeamSetGameMode.arena3V3);
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront,true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                yield return FightLoad.Arena();
                break;
            case TeamSetGameMode.story:
                void GoToTeamEdit_Arcade()
                {
                    TeamSet.SwitchTargetTeam(TeamSetGameMode.story);
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                yield return FightLoad.Arcade();
                break;
        }
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
        FightPreparePage.target.StageMembersInfoShow(FightLoad.ToBeLoad);
        FightPreparePage.target.BeginFight.onClick.RemoveAllListeners();
        FightPreparePage.target.BeginFight.onClick.AddListener(FightLoad.GoTo);
    }
}
