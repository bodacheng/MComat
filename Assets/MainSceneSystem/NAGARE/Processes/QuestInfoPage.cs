using dataAccess;
using mainMenu;

public class QuestInfoPage : MainSceneProcess
{
    FightInfo loadFight;

    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    public void EnterProcess(FightInfo stage)
    {
        loadFight = stage;
        PageTo.Go(MainSceneStep.QuestInfo);
        GetReadyForQuestInfoPage();
    }
    
    public QuestInfoPage()
    {
        Step = MainSceneStep.QuestInfo;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        EnterProcess(t as FightInfo);
    }
    
    public override void ProcessEnd()
    {
    }

    // 这个函数目前是固定使用“默认队伍配置”
    public void GetReadyForQuestInfoPage()
    {
        FightPreparePage.target.QuestName.text = loadFight.battleNameJPG;
        switch (loadFight.GetEventType())
        {
            case FightEventType.Arena:
                loadFight.fightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEdit_Arena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                break;
            case FightEventType.Quest:
                void GoToTeamEdit_Arcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                break;
        }
        FightPreparePage.target.StageMembersInfoShow(loadFight);
        FightPreparePage.target.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(loadFight, true);
        }
        FightPreparePage.target.BeginFight.onClick.AddListener(Go);
    }
}
