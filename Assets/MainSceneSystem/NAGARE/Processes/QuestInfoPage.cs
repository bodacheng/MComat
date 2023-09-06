using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    private GangbangInfo controllingGangbangInfo;
    
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
                _layer.SetArcadeFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.ArcadeFront, false);
                    },
                    FightScene.FightScene.Fight.ID
                );
                break;
            case FightEventType.Gangbang:
                controllingGangbangInfo = GangbangInfo.Copy((GangbangInfo)stage);
                controllingGangbangInfo.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic(); // 为了队员显示
                _layer.SetTeamEditFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                    }
                );
                _layer.SetGangbangFeature(
                    () => { PreScene.target.trySwitchToStep(MainSceneStep.GangBangFront, false); },
                    FightScene.FightScene.Fight.ID,
                    (x, y ,z)=>
                    {
                        int returnValue = controllingGangbangInfo.SetTeamUnitCount(x,y,z);
                        return returnValue;
                    },
                    controllingGangbangInfo.GetTeamUnitCount
                );
                break;
        }

        if (stage is GangbangInfo)
        {
            _layer.GangbangStageMembersInfoShow(controllingGangbangInfo, stage.Team1OneWord, stage.Team2OneWord);
        }
        else
        {
            _layer.StageMembersInfoShow(stage, stage.Team1OneWord, stage.Team2OneWord);
        }
        
        if (FightScene.FightScene.Fight.EventType == FightEventType.Gangbang)
        {
            _layer.SetFightMode(1);
            _layer.SetFightBeginFeature(()=> GoToFight(controllingGangbangInfo));
        }
        else
        {
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
                        fightInfo.LoadMyTeam();
                        FightLoad.Go(fightInfo);
                    }
                );
                break;
            case FightEventType.Gangbang:
                if (fightInfo.FightMembers.HeroSets.GetValues().Count < 1 || fightInfo.FightMembers.EnemySets.GetValues().Count < 1)
                {
                    Debug.Log(fightInfo.FightMembers.HeroSets.GetValues().Count + ":" + fightInfo.FightMembers.EnemySets.GetValues().Count);
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                ((GangbangInfo)fightInfo).ConvertTeamToGangbang();
                FightLoad.Go(fightInfo);
                break;
            default:
                fightInfo.LoadMyTeam();
                if (fightInfo.FightMembers.HeroSets.GetValues().Count < 1 || fightInfo.FightMembers.EnemySets.GetValues().Count < 1)
                {
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                
                FightLoad.Go(fightInfo);
                break;
        }
    }
}
