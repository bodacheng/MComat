using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    private GangbangInfo _controllingGangbangInfo;
    
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
                _controllingGangbangInfo = GangbangInfo.Copy((GangbangInfo)stage);
                _controllingGangbangInfo.FightMembers.HeroSets = TeamSet.GetTargetSet("gangbang").LoadTeamDic(); // 为了队员显示
                _layer.SetTeamEditFeature(
                    () =>
                    {
                        PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "gangbang", true);
                    }
                );
                
                _layer.SetGangbangFeature(
                    () => { PreScene.target.trySwitchToStep(MainSceneStep.GangBangFront, false); },
                    FightLoad.Fight.ID,
                    (x, y ,z, f)=>
                    {
                        var whole = _controllingGangbangInfo.SetTeamUnitCount(x, y, z, f);
                        if (x == 1) // 本地存储各个gangbang人数
                        {
                            PlayerPrefs.SetInt("gangbangPos"+ y, _controllingGangbangInfo.GetTeamUnitCount(x,y));
                            PlayerPrefs.Save();
                        }
                        
                        var canFight = CanFightCheck();
                        _layer.TeamEditIndicator.gameObject.SetActive(!canFight);
                        _layer.SetFightBeginEnableRender(canFight);
                        return whole;
                    },
                    (x,y)=> _controllingGangbangInfo.GetTeamUnitCount(x,y,x == 1));
                break;
        }
        
        if (stage is GangbangInfo)
        {
            _layer.GangbangStageMembersInfoShow(_controllingGangbangInfo, stage.Team1OneWord, stage.Team2OneWord);
        }
        else
        {
            _layer.StageMembersInfoShow(stage, stage.Team1OneWord, stage.Team2OneWord);
        }
        
        if (FightLoad.Fight.EventType == FightEventType.Gangbang)
        {
            _layer.SetFightMode(1);
            _layer.SetFightBeginFeature(()=> GoToFight(_controllingGangbangInfo));
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

        var canFight = CanFightCheck();
        _layer.TeamEditIndicator.gameObject.SetActive(!canFight);
        _layer.SetFightBeginEnableRender(canFight);
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
            case FightEventType.Quest:
                if (PlayerAccountInfo.Me.tutorialProgress == "SkillEditFinished2")
                {
                    if (FightLoad.Fight.FightMembers.HeroSets.GetValues().Count < 2)
                    {
                        return false;
                    }
                }
                else
                {
                    if (FightLoad.Fight.FightMembers.HeroSets.GetValues().Count == 0)
                    {
                        return false;
                    }
                }
                break;
            case FightEventType.Gangbang:
                return _controllingGangbangInfo.GetGroupWholeUnitCount(1) > 0
                    && _controllingGangbangInfo.GetGroupWholeUnitCount(2) > 0;
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
