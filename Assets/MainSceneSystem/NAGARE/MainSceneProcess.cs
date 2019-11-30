using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public abstract class MainSceneProcess
    {        
        public MainSceneStep thisProcessStep;
        public MainSceneStep nextProcessStep = MainSceneStep.none;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

        public preparingScene _preparingScene;
        public SingleThreadProcesser mainProcessRunner;
        public MemberDetail _MemberDetail;
        public ModelShower _modelShower;
        public CharsManager _CharsManager;
        public TeamEditManager _TeamEditManager;
        public ReturnButtonManager _ReturnButtonManager;
        public SelfFightManager _SelfFightManager;
        public CameraManager _CameraManager;
        public ChaptersManager _ChaptersManager;
        public QuestPreparePage _QuestPreparePage;
        public gotchaManager _gotchaManager;

        public ProcessesRunner subProcessesRunner;

        public void EelementsInherit(preparingScene _preparingScene)
        {
            this._preparingScene = _preparingScene;
            _MemberDetail = _preparingScene._MemberDetail;
            _CharsManager = _preparingScene._CharSetManager;
            _modelShower = _preparingScene._modelShower;
            _ReturnButtonManager = _preparingScene._ReturnButtonManager;
            _SelfFightManager = _preparingScene._SelfFightManager;
            _CameraManager = _preparingScene._CameraManager;
            _QuestPreparePage = _preparingScene._QuestPreparePage;
            _ChaptersManager = _preparingScene._ChaptersManager;
            _TeamEditManager = _preparingScene._TeamEditManager;
            _gotchaManager = _preparingScene._gotchaManager;
            mainProcessRunner = _preparingScene.mainProcessRunner;
        }

        public virtual void ProcessEnter()
        {
        }

        public virtual void ProcessEnd()
        {
        }

        public virtual bool CanEnterOtherProcess()
        {
            return true;
        }

        public virtual void LocalUpdate()
        {
        }
    }

    public enum MainSceneStep
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