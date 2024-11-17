using Cysharp.Threading.Tasks;
using dataAccess;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class QuestInfoPage : MSceneProcess
{
    private FightPrepareLayer _layer;
    
    async UniTask EnterProcess(FightInfo stage)
    {
        var represent = stage.GetRepresentUnitInfo();
        UnitConfig unitConfig = Units.GetUnitConfig(represent.r_id);
        BackGroundPS.target.ChangeBGByElement(unitConfig.element);
        
        FightLoad.Fight = stage;
        if (FightLoad.Fight.FightMode != FightMode.Group)
        {
            _layer = UILayerLoader.Load<FightPrepareLayer>();
        }
        else
        {
            _layer = UILayerLoader.Load<FightPrepareLayer>(false, "FightPrepareLayer_gb");
        }
        
        _layer.SetFightModeFlg(FightLoad.Fight.FightMode);
        
        string teamKey;
        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                teamKey = "arena";
                break;
            case FightEventType.Quest:
                if (FightLoad.Fight.FightMode == FightMode.Group)
                {
                    teamKey = "gangbang";
                }
                else
                {
                    switch (FightLoad.Fight.FightMode)
                    {
                        case FightMode.Evolve:
                            teamKey = "arcade";
                            break;
                        default:
                            teamKey = "origin";
                            break;
                    }
                }
                break;
            default:
                teamKey = "origin";
                break;
        }
        
        FightLoad.Fight.FightMembers.HeroSets = TeamSet.GetTargetSet(teamKey).LoadTeamDic();
        _layer.SetTeamEditFeature(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, teamKey, true);
        });
        
        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                _layer.SetLayerAnimatorTrigger("normal");
                await _layer.BattleGroundSwitch.INI();
                _layer.BattleGroundSwitch.gameObject.SetActive(true);
                _layer.SetFightMode(FightMode.Evolve);// 不是rotate或multi的任意值
                break;
            case FightEventType.Quest:
                if (FightLoad.Fight.FightMode == FightMode.Group)
                {
                    _layer.SetGangbangFeature(FightLoad.Fight,
                        () => { PreScene.target.trySwitchToStep(MainSceneStep.ArcadeFront, false); },
                        FightLoad.Fight.ID,
                        (x, y ,z, maxCount) =>
                        {
                            var whole = FightLoad.Fight.SetTeamUnitCount(x, y, z, maxCount);
                            if (x == 1) // 本地存储各个gangbang人数
                            {
                                PlayerPrefs.SetInt("gangbangPos"+ y, FightLoad.Fight.GetTeamUnitCount(x,y));
                                PlayerPrefs.Save();
                            }
                            var canFight = CanFightCheck(FightLoad.Fight);
                            //_layer.TeamEditIndicator.gameObject.SetActive(!canFight);
                            _layer.SetFightBeginEnableRender(canFight);
                            return whole;
                        },
                        (x,y)=> FightLoad.Fight.GetTeamUnitCount(x,y,x == 1));
                }
                else
                {
                    _layer.SetLayerAnimatorTrigger("evolution");
                    if (FightLoad.Fight.FightMode == FightMode.Evolve)
                    {
                        var arcadeTeam = TeamSet.GetTargetSet(teamKey);
                        // 处理以前的旧逻辑
                        if (arcadeTeam.PosNumsWithLocalKeys.Length > 1)
                        {
                            for (var index = 0; index < arcadeTeam.PosNumsWithLocalKeys.Length; index++)
                            {
                                if (index > 0)
                                {
                                    arcadeTeam.SetPosUnitByInstanceID(index, null);
                                }
                            }
                        }
                    }
                    
                    _layer.SetArcadeFeature(
                        () =>
                        {
                            PreScene.target.trySwitchToStep(MainSceneStep.ArcadeFront, false);
                        },
                        FightLoad.Fight.ID
                    );
                }
                break;
            case FightEventType.Event:
                _layer.SetLayerAnimatorTrigger("evolution");
                _layer.SetEventFeature(FightLoad.Fight.ID);
                break;
        }
        
        if (stage.FightMode is FightMode.Group)
        {
            _layer.GangbangStageUnitsDisplay(FightLoad.Fight);
            _layer.SetFightBeginFeature(()=> GoToFight(FightLoad.Fight, _layer.SelectedMaxTeamCount));
        }
        else
        {
            _layer.StageMembersInfoShow(stage);
            _layer.SetFightBeginFeature(()=> GoToFight(FightLoad.Fight));
        }
        
        var canFight = CanFightCheck(FightLoad.Fight);
        //_layer.TeamEditIndicator.gameObject.SetActive(!canFight);
        _layer.SetFightBeginEnableRender(canFight, PlayerAccountInfo.Me.tutorialProgress != "Finished");
        SetLoaded(true);
    }
    
    public QuestInfoPage()
    {
        Step = MainSceneStep.QuestInfo;
    }
    
    public override void ProcessEnter()
    {
        EnterProcess(FightLoad.Fight).Forget();
    }
    
    public override void ProcessEnter<T>(T t)
    {
        EnterProcess(t as FightInfo).Forget();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<FightPrepareLayer>();
    }
    
    public static bool CanFightCheck(FightInfo fight)
    {
        switch (fight.EventType)
        {
            case FightEventType.Arena:
                if (fight.FightMembers.HeroSets.GetValues().Count != 3)
                {
                    return false;
                }
                break;
            case FightEventType.Quest:
                if (fight.FightMode == FightMode.Group)
                {
                    var unitCountFit = fight.GetGroupWholeUnitCount(1) > 0
                                       && fight.GetGroupWholeUnitCount(2) > 0;
                    if (!unitCountFit)
                        return false;
                }
                else
                {
                    if (fight.FightMode == FightMode.Evolve)
                    {
                        if (fight.FightMembers.HeroSets.GetValues().Count != 1)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (fight.FightMembers.HeroSets.GetValues().Count == 0)
                        {
                            return false;
                        }
                    }
                }
                break;
            case FightEventType.Event:
                if (PlayerAccountInfo.Me.tutorialProgress == "SkillEditFinished2")
                {
                    if (fight.FightMembers.HeroSets.GetValues().Count < 2)
                    {
                        return false;
                    }
                }
                else
                {
                    if (fight.FightMembers.HeroSets.GetValues().Count == 0)
                    {
                        return false;
                    }
                }
                break;
            default:
                if (fight.FightMembers.HeroSets.GetValues().Count == 0)
                {
                    return false;
                }
                break;
        }

        if (fight.FightMode == FightMode.Group)
        {
            var instanceIds = fight.GetNonZeroInstanceIds(1);
            if (!fight.FightMembers.CheckStonesLegal(fight.EventType, instanceIds))
            {
                return false;
            }
        }
        else
        {
            if (!fight.FightMembers.CheckStonesLegal(fight.EventType))
            {
                return false;
            }
        }
        
        return true;
    }
    
    void GoToFight(FightInfo fightInfo, int maxTeamUnitCount = -1)
    {
        // if (!fightInfo.FightMembers.CheckStonesLegal(fightInfo.EventType))
        // {
        //     PopupLayer.ArrangeWarnWindow(Translate.Get("TeamUnitNotFull"));
        //     return;
        // }

        switch (fightInfo.EventType)
        {
            case FightEventType.Arena:
            case FightEventType.Self:
                fightInfo.FightMode = _layer.GetSettingFightMode();
                break;
            default:
                break;
        }
        
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
                        fightInfo.battleGroundID = _layer.BattleGroundSwitch.BattleFieldIndex;
                        FightLoad.Go(fightInfo);
                    }
                );
                break;
            case FightEventType.Quest:
                if (fightInfo.FightMode == FightMode.Group)
                {
                    if (fightInfo.FightMembers.HeroSets.GetValues().Count < 1 || fightInfo.FightMembers.EnemySets.GetValues().Count < 1)
                    {
                        PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                        return;
                    }
                
                    void RealFight()
                    {
                        fightInfo.ConvertTeamToGangbang();
                        FightScene.FightScene.team1GroupSet = fightInfo.Team1GroupSet;
                        fightInfo.Team1ID = PlayerAccountInfo.Me.PlayFabId;
                        FightLoad.Go(fightInfo);
                    }
                
                    if (fightInfo.GetGroupWholeUnitCount(1) < maxTeamUnitCount)
                    {
                        PopupLayer.ArrangeConfirmWindow(
                            RealFight,
                            Translate.Get("HasExtraSeatForGangbangButFight"));
                        return;
                    }

                    RealFight();
                }
                else
                {
                    fightInfo.LoadMyTeam();
                    FightLoad.Go(fightInfo);
                }
                break;
            default:
                fightInfo.LoadMyTeam();
                if (fightInfo.FightMembers.HeroSets.GetValues().Count < 1 || fightInfo.FightMembers.EnemySets.GetValues().Count < 1)
                {
                    PopupLayer.ArrangeWarnWindow(Translate.Get("TeamNotFull"));
                    return;
                }
                if (dataAccess.Units.Dic.Count >= 3 && fightInfo.FightMembers.HeroSets.GetValues().Count < 3)
                {
                    PopupLayer.ArrangeConfirmWindow(
                        () => { FightLoad.Go(fightInfo);},
                        Translate.Get("HasExtraSeatButFight"));
                    return;
                }
                
                FightLoad.Go(fightInfo);
                break;
        }
    }
}