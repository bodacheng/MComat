using dataAccess;
using DummyLayerSystem;
using mainMenu;
public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    
    void EnterProcess(FightInfo stage)
    {
        FightScene.FightScene.Fight = stage;
        _layer = UILayerLoader.Load<FightPrepareLayer>();
        
        switch (FightScene.FightScene.Fight.EventType)
        {
            case FightEventType.Arena:
                FightScene.FightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEditArena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                _layer.EditTeamButton.onClick.RemoveAllListeners();
                _layer.EditTeamButton.onClick.AddListener(GoToTeamEditArena);
                break;
            case FightEventType.Quest:
                FightScene.FightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
                void GoToTeamEditArcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                _layer.EditTeamButton.onClick.RemoveAllListeners();
                _layer.EditTeamButton.onClick.AddListener(GoToTeamEditArcade);
                break;
        }
        _layer.StageMembersInfoShow(FightScene.FightScene.Fight);
        _layer.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            switch (FightScene.FightScene.Fight.EventType)
            {
                case FightEventType.Arena:
                    
                    CloudScript.SubtractVirtualCurrency(
                        "TK",1,
                        () =>
                        {
                            FightLoad.Go(FightScene.FightScene.Fight, true);
                        }
                    );
                    break;
                default:
                    FightLoad.Go(FightScene.FightScene.Fight, true);
                    break;
            }
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
        EnterProcess(FightScene.FightScene.Fight);
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
