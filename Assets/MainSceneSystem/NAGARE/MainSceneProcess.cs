using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public abstract class MainSceneProcess
    {
        public ProcessesRunner processesRunner;//可能一般不需要。
        
        public MainSceneStep thisProcessStep;
        public MainSceneStep nextProcessStep = MainSceneStep.none;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

        public preparingScene _preparingScene;
        public MemberDetail _MemberDetail;
        public modelShower _modelShower;
        public MonsterBox _MonsterBox;
        public SkillStonesBox _SkillStonesBox;
        public CharsManager _CharsManager;
        public TheNineSlot _TheNineSlot;
        public TeamEditManager _TeamEditManager;
        public ReturnButtonManager _ReturnButtonManager;
        public SelfFightManager _SelfFightManager;
        public CameraManager _CameraManager;
        public LoadingCanvas _LoadingCanvas;
        public ChaptersManager _ChaptersManager;
        public QuestPreparePage _QuestPreparePage;
        public gotchaManager _gotchaManager;

        public void EelementsInherit(preparingScene _preparingScene)
        {
            this._preparingScene = _preparingScene;
            _MemberDetail = _preparingScene._MemberDetail;
            _SkillStonesBox = _preparingScene._SkillStonesBox;
            _CharsManager = _preparingScene._CharSetManager;
            _modelShower = _preparingScene._modelShower;
            _TheNineSlot = _preparingScene.TheNineSlot;
            _ReturnButtonManager = _preparingScene._ReturnButtonManager;
            _SelfFightManager = _preparingScene._SelfFightManager;
            _CameraManager = _preparingScene._CameraManager;
            _LoadingCanvas = _preparingScene._LoadingCanvas;
            _MonsterBox = _preparingScene._MonsterBox;
            _QuestPreparePage = _preparingScene._QuestPreparePage;
            _ChaptersManager = _preparingScene._ChaptersManager;
            _TeamEditManager = _preparingScene._TeamEditManager;
            _gotchaManager = _preparingScene._gotchaManager;
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
        none = 0,
        frontPage = 1,
        SelfFightFront = 4,
        TeamEditFront = 2,
        MemberDetail = 5,
        MemberDetail_edit = 16,
        MemberDetail_show = 17,
        SkillStones = 15,
        Gotcha = 7,
        QuestInfo = 8,
        Chapter = 9,
        ChaptersOfOneSeason = 10,
        SeasonsGamen = 11,

        JiNengRongLian_selectMaterialMonster = 12,
        JiNengRongLian_selectBaseMonster = 13,
        JiNengRongLian_waitForConfirm = 14,

        Tutorial_skillEdit = 18,
        Tutorial_Story = 19,
        
        Tutorial_skillEdit_sub1 = 20,
        Tutorial_skillEdit_sub2 = 21,
        // 前半 后半
        Tutorial_skillEdit_sub3 = 22,
        Tutorial_skillEdit_sub4 = 23,
    }
}