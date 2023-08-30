using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    
    void EnterProcess(FightInfo stage)
    {
        if (stage is GangbangInfo)
        {
            var bangInfo = (GangbangInfo)stage;
            stage = bangInfo.ConvertToFightInfo();
        }
        
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
                _layer.SetArcadeFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.ArcadeFront, false);
                    },
                    FightScene.FightScene.Fight.ID
                );
                break;
            case FightEventType.Gangbang:
                FightScene.FightScene.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
                void GoToTeamEditGangbang()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                _layer.SetTeamEditFeature(GoToTeamEditGangbang);
                _layer.SetArcadeFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.GangBangFront, false);
                    },
                    FightScene.FightScene.Fight.ID
                );
                break;
        }
        _layer.StageMembersInfoShow(FightScene.FightScene.Fight, FightScene.FightScene.Fight.Team1OneWord, FightScene.FightScene.Fight.Team2OneWord);

        bool CanFightCheck()
        {
            if (!FightScene.FightScene.Fight.FightMembers.CheckStonesLegal(FightScene.FightScene.Fight.EventType))
            {
                return false;
            }
            
            switch (FightScene.FightScene.Fight.EventType)
            {
                case FightEventType.Arena:
                    if (FightScene.FightScene.Fight.FightMembers.HeroSets.GetValues().Count != 3)
                    {
                        return false;
                    }
                    break;
                default:
                    if (FightScene.FightScene.Fight.FightMembers.HeroSets.GetValues().Count == 0)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }
        
        int FightMode()
        {
            switch (FightScene.FightScene.Fight.EventType)
            {
                case FightEventType.Quest:
                    return FightScene.FightScene.Fight.ArcadeFightMode;
                default:
                    return 0;
            }
        }

        if (FightScene.FightScene.Fight.EventType == FightEventType.Gangbang)
        {
            
        }
        else
        {
            _layer.SetFightMode(FightMode());
            _layer.SetFightBeginFeature(()=> GoToFight(FightScene.FightScene.Fight));
        }
        
        _layer.SetFightBeginEnableRender(CanFightCheck());
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
        if (t is GangbangInfo)
        {
            EnterProcess(t as GangbangInfo);
        }
        else
        {
            EnterProcess(t as FightInfo);
        }
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<FightPrepareLayer>();
    }

    void GoToGangbang()
    {
        
    }
    
    void GoToFight(FightInfo fightInfo)
    {
        if (!fightInfo.FightMembers.CheckStonesLegal(fightInfo.EventType))
        {
            PopupLayer.ArrangeWarnWindow(Translate.Get("TeamUnitNotFull"));
            return;
        }
            
        fightInfo.team1Mode = _layer.GetSetFightMode();
        fightInfo.team2Mode = _layer.GetSetFightMode();
            
        switch (fightInfo.EventType)
        {
            case FightEventType.Arena:
                if (fightInfo.FightMembers.HeroSets.GetValues().Count != 3)
                {
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                CloudScript.SubtractVirtualCurrency(
                    "TK",1,
                    () =>
                    {
                        FightLoad.Go(fightInfo, true);
                    }
                );
                break;
            default:
                if (fightInfo.FightMembers.HeroSets.GetValues().Count == 0)
                {
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                FightLoad.Go(fightInfo, true);
                break;
        }
    }
}
