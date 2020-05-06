
namespace mainMenu
{
    public abstract class MainSceneProcess
    {
        public MainSceneStep thisProcessStep;
        public MainSceneStep nextProcessStep = MainSceneStep.None;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

        public SingleThreadProcesser mainProcessRunner;
        public ModelShower _modelShower;
        public CharsManager _CharsManager;
        public SelfFightManager _SelfFightManager;
        public CameraManager _CameraManager;
        public ProcessesRunner subProcessesRunner;

        public void EelementsInherit(PreScene _preparingScene)
        {
            _CharsManager = _preparingScene._CharSetManager;
            _modelShower = _preparingScene._modelShower;
            _SelfFightManager = _preparingScene._SelfFightManager;
            _CameraManager = _preparingScene._CameraManager;
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
        None = 0,
        Setting = -1,
        FrontPage = 1,
        SelfFightFront = 4,
        TeamEditFront = 2,
        MemberDetail = 5,
        MemberDetail_edit = 16,
        MemberDetail_show = 17,
        SkillStones = 15,
        GotchaFront = 6,
        GotchaAnim = 7,
        GotchaResult = 24,
        
        ShopTop = 201,
        BoxExpansion = 202,
        
        QuestInfo = 8,
        ArcadeFront = 9,
        Arena = 3,
        Tutorial_skillEdit = 18,
        Tutorial_Story = 19,

        Tutorial_skillEdit_sub1 = 20,
        Tutorial_skillEdit_sub2 = 21,
        // 前半 后半
        Tutorial_skillEdit_sub3 = 22,
        Tutorial_skillEdit_sub4 = 23,

        JiNengRongLian_selectMaterialMonster = 12,
        JiNengRongLian_selectBaseMonster = 13,
        JiNengRongLian_waitForConfirm = 14,
    }
}