using FightScene;

namespace mainMenu
{
    public class TutorialProcess : SceneProcess
    {
        public TutorialStep Step;
        public SingleThreadProcesser mainProcessRunner;
        public CharsManager _CharsManager;
        public SelfFightManager _SelfFightManager;
        public CameraManager _CameraManager;
        public ProcessesRunner SubProcessesRunner;

        public void EelementsInherit(PreScene _preparingScene)
        {
            _CharsManager = _preparingScene._CharSetManager;
            _SelfFightManager = _preparingScene._SelfFightManager;
            _CameraManager = _preparingScene._CameraManager;
            mainProcessRunner = _preparingScene.mainProcessRunner;
        }
        
        public void EelementsInherit(NetFightScene NetFightScene)
        {
        }
    }

    public enum TutorialStep
    {
        //Tutorial (主要流程并行)
        GoToMemberDetail = 101,
        OpenSkillEdit = 102,

        SkillEditTry_A1Selected = 103,
        SkillEditTry_A2Selected = 104,
        SkillEditTry_A3Selected = 105,

        SkillEditTry_A1Filled = 106,
        SkillEditTry_A2Filled = 107,
        SkillEditTry_A3Filled = 108,

        ALineConfirm = 109,
        GoToStages = 111,
        GoToStage1 = 110,
        GoToTeamEdit = 113,
        ClickTeamEditSlot1 = 114,
        ChooseAdamToSlot1 = 115,
        ConfirmQuest1 = 116,
        TutorialReturn = 112,
        waitingForStage1Start = 117,        
        None = 0
    }
}