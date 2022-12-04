using dataAccess;
using DummyLayerSystem;
using mainMenu;
using FightScene;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer layer;
    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    void EnterProcess(FightInfo stage)
    {
        NetFightScene.Fight = stage;
        layer = UILayerLoader.Load<FightPrepareLayer>();
        layer.QuestName.text = NetFightScene.Fight.battleNameJPG;
        
        switch (NetFightScene.Fight.EventType)
        {
            case FightEventType.Arena:
                NetFightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEditArena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                layer.EditTeamButton.onClick.RemoveAllListeners();
                layer.EditTeamButton.onClick.AddListener(GoToTeamEditArena);
                break;
            case FightEventType.Quest:
                NetFightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
                void GoToTeamEditArcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                layer.EditTeamButton.onClick.RemoveAllListeners();
                layer.EditTeamButton.onClick.AddListener(GoToTeamEditArcade);
                break;
        }
        layer.StageMembersInfoShow(NetFightScene.Fight);
        layer.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(NetFightScene.Fight, true);
        }
        layer.BeginFight.onClick.AddListener(Go);
        
        SetLoaded(true);
    }
    
    public QuestInfoPage()
    {
        Step = MainSceneStep.QuestInfo;
    }
    
    public override void ProcessEnter()
    {
        EnterProcess(NetFightScene.Fight);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        EnterProcess(t as FightInfo);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<FightPrepareLayer>();
    }
}
