using dataAccess;
using mainMenu;

public class QuestInfoPage : MainSceneProcess
{
    FightInfo loadFight;

    private FightPrepareLayer fightPrepareLayer;
    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    public void EnterProcess(FightInfo stage)
    {
        loadFight = stage;
        fightPrepareLayer =  UILayerLoader.Load(PreScene.target.T,"FightPrepareLayer") as FightPrepareLayer;
        fightPrepareLayer.QuestName.text = loadFight.battleNameJPG;
        
        switch (loadFight.GetEventType())
        {
            case FightEventType.Arena:
                loadFight.fightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEdit_Arena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                fightPrepareLayer.EditTeamButton.onClick.RemoveAllListeners();
                fightPrepareLayer.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                break;
            case FightEventType.Quest:
                void GoToTeamEdit_Arcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                fightPrepareLayer.EditTeamButton.onClick.RemoveAllListeners();
                fightPrepareLayer.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                break;
        }
        fightPrepareLayer.StageMembersInfoShow(loadFight);
        fightPrepareLayer.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(loadFight, true);
        }
        fightPrepareLayer.BeginFight.onClick.AddListener(Go);
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
        UILayerLoader.Remove("FightPrepareLayer");
    }
}
