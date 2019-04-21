using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainSceneProcess
{
    public preparingScene _preparingScene;
    public MainSceneStep step;

    // 继承preparingScene里的部分信息。
    public MemberDetail _MemberDetail;
    public MonsterBox _MonsterBox;
    public SkillStonesBox _SkillStonesBox;
    public CharsManager _CharsManager;
    public TheNineSlot _TheNineSlot;
    public ReturnButtonManager _ReturnButtonManager;
    public SelfFightManager _SelfFightManager;
    public CameraManager _CameraManager;
    public LoadingCanvas _LoadingCanvas;
    
    public void EelementsInherit(preparingScene _preparingScene)
    {
        _MemberDetail = _preparingScene._MemberDetail;
        _SkillStonesBox = _preparingScene._SkillStonesBox;
        _CharsManager = _preparingScene._CharSetManager;
        _TheNineSlot = _preparingScene.TheNineSlot;
        _ReturnButtonManager = _preparingScene._ReturnButtonManager;
        _SelfFightManager = _preparingScene._SelfFightManager;
        _CameraManager = _preparingScene._CameraManager;
        _LoadingCanvas = _preparingScene._LoadingCanvas;
        _MonsterBox = _preparingScene._MonsterBox;
    }
    
    public virtual void ProcessEnter()
    {
    }
    
    public virtual void ProcessEnd()
    {
    }
    
    public virtual bool canEnterOtherProcess()
    {
        return true;
    }
    
    public virtual void localUpdate()
    {
    }
}

public enum MainSceneStep : int
{
    frontPage = 1,
    FightModeChoose = 6,
    SelfFightFront = 4,
    TeamEditFront = 2,
    TeamEditMonsterDetail = 3,
    MemberDetail = 5,
    MemberDetail_edit = 16,
    MemberDetail_show = 17,
    SkillStones = 15,
    Gotcha = 7,
    QuestInfo =8,
    Chapter = 9,
    Seasons = 10,
    SeasonsGamen = 11,
    
    JiNengRongLian_selectMaterialMonster = 12,
    JiNengRongLian_selectBaseMonster = 13,
    JiNengRongLian_waitForConfirm = 14,       
}