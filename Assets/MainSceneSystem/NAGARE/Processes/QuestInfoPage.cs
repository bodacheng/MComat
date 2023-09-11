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
        FightLoad.Fight = stage;
        _layer = UILayerLoader.Load<FightPrepareLayer>();
        
        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                FightLoad.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arena").LoadTeamDic();
                void GoToTeamEditArena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                _layer.SetTeamEditFeature(GoToTeamEditArena);
                break;
            case FightEventType.Quest:
                FightLoad.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet("arcade").LoadTeamDic();
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
                    FightLoad.Fight.ID
                );
                break;
            case FightEventType.Gangbang:
                controllingGangbangInfo = GangbangInfo.Copy((GangbangInfo)stage);
                controllingGangbangInfo.FightMembers.HeroSets = TeamSet.GetTargetSet("gangbang").LoadTeamDic(); // 为了队员显示
                _layer.SetTeamEditFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "gangbang", true);
                    }
                );
                
                _layer.SetGangbangFeature(
                    () => { PreScene.target.trySwitchToStep(MainSceneStep.GangBangFront, false); },
                    FightLoad.Fight.ID,
                    (x, y ,z)=>
                    {
                        var whole = controllingGangbangInfo.SetTeamUnitCount(x, y, z);
                        if (x == 1) // 本地存储各个gangbang人数
                        {
                            PlayerPrefs.SetInt("gangbangPos"+ y, controllingGangbangInfo.GetTeamUnitCount(x,y));
                            PlayerPrefs.Save();
                        }
                        return whole;
                    },
                    (x,y)=> controllingGangbangInfo.GetTeamUnitCount(x,y,x == 1));
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
        
        if (FightLoad.Fight.EventType == FightEventType.Gangbang)
        {
            _layer.SetFightMode(1);
            _layer.SetFightBeginFeature(()=> GoToFight(controllingGangbangInfo));
        }
        else
        {
            int FightMode()
            {
                switch (FightLoad.Fight.EventType)
                {
                    case FightEventType.Quest:
                        return FightLoad.Fight.ArcadeFightMode;
                    default:
                        return 0;
                }
            }
            _layer.SetFightMode(FightMode());
            _layer.SetFightBeginFeature(()=> GoToFight(FightLoad.Fight));
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
        EnterProcess(FightLoad.Fight);
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
        if (!FightLoad.Fight.FightMembers.CheckStonesLegal(FightLoad.Fight.EventType))
        {
            return false;
        }
            
        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                if (FightLoad.Fight.FightMembers.HeroSets.GetValues().Count != 3)
                {
                    return false;
                }
                break;
            default:
                if (FightLoad.Fight.FightMembers.HeroSets.GetValues().Count == 0)
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
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                var bangBangInfo = ((GangbangInfo)fightInfo);
                bangBangInfo.ConvertTeamToGangbang();
                FightScene.FightScene.team1GroupSet = bangBangInfo.Team1GroupSet;
                fightInfo.Team1ID = PlayerAccountInfo.Me.PlayFabId;
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
