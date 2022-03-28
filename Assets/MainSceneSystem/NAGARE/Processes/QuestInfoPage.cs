using dataAccess;
using DummyLayerSystem;
using mainMenu;
using FightScene;

public class QuestInfoPage : MainSceneProcess
{
    private FightPrepareLayer layer;
    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    void EnterProcess(FightInfo stage)
    {
        NetFightScene.Fight = stage;
        layer = FightPrepareLayer.Open();
        layer.QuestName.text = NetFightScene.Fight.battleNameJPG;
        
        switch (NetFightScene.Fight.GetEventType())
        {
            case FightEventType.Arena:
                NetFightScene.Fight.fightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEdit_Arena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                layer.EditTeamButton.onClick.RemoveAllListeners();
                layer.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                break;
            case FightEventType.Quest:
                void GoToTeamEdit_Arcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                layer.EditTeamButton.onClick.RemoveAllListeners();
                layer.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                break;
        }
        layer.StageMembersInfoShow(NetFightScene.Fight);
        layer.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(NetFightScene.Fight, true);
        }
        layer.BeginFight.onClick.AddListener(Go);
    }
    
    public QuestInfoPage()
    {
        Step = MainSceneStep.QuestInfo;
        Inherit(PreScene.target);
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
        UILayerLoader.Remove("FightPrepareLayer");
    }
}
