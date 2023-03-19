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
                _layer.SetTeamEditFeature(GoToTeamEditArena);
                break;
            case FightEventType.Quest:
                FightScene.FightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
                void GoToTeamEditArcade()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                _layer.SetTeamEditFeature(GoToTeamEditArcade);
                break;
        }
        _layer.StageMembersInfoShow(FightScene.FightScene.Fight);
        
        void Go()
        {
            if (!FightScene.FightScene.Fight.FightMembers.CheckStonesLegal(FightScene.FightScene.Fight.EventType))
            {
                PopupLayer.ArrangeWarnWindow(Translate.Get("TeamUnitNotFull"));
                return;
            }
            switch (FightScene.FightScene.Fight.EventType)
            {
                case FightEventType.Arena:
                    if (FightScene.FightScene.Fight.FightMembers.HeroSets.GetValues().Count != 3)
                    {
                        PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                        return;
                    }
                    CloudScript.SubtractVirtualCurrency(
                        "TK",1,
                        () =>
                        {
                            FightLoad.Go(FightScene.FightScene.Fight, true);
                        }
                    );
                    break;
                default:
                    if (FightScene.FightScene.Fight.FightMembers.HeroSets.GetValues().Count == 0)
                    {
                        PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                        return;
                    }
                    FightLoad.Go(FightScene.FightScene.Fight, true);
                    break;
            }
        }
        _layer.SetFightBeginFeature(Go);
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
