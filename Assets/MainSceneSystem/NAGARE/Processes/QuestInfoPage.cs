using dataAccess;
using DummyLayerSystem;
using mainMenu;
using FightScene;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    
    void EnterProcess(FightInfo stage)
    {
        NetFightScene.Fight = stage;
        _layer = UILayerLoader.Load<FightPrepareLayer>();
        
        switch (NetFightScene.Fight.EventType)
        {
            case FightEventType.Arena:
                NetFightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEditArena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                _layer.EditTeamButton.onClick.RemoveAllListeners();
                _layer.EditTeamButton.onClick.AddListener(GoToTeamEditArena);
                break;
            case FightEventType.Quest:
                NetFightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
                void GoToTeamEditArcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                _layer.EditTeamButton.onClick.RemoveAllListeners();
                _layer.EditTeamButton.onClick.AddListener(GoToTeamEditArcade);
                break;
        }
        _layer.StageMembersInfoShow(NetFightScene.Fight);
        _layer.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(NetFightScene.Fight, true);
        }
        _layer.BeginFight.onClick.AddListener(Go);
        
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
