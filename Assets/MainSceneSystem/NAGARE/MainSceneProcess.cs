
namespace mainMenu
{
    public abstract class MainSceneProcess : SceneProcess
    {
        public MainSceneStep Step;
        public MainSceneStep nextProcessStep = MainSceneStep.None;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

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
        SkillStones_Sell = 100,
        GotchaFront = 6,
        GotchaAnim = 7,
        GotchaResult = 24,
        
        ShopTop = 201,
        BoxOverLoadHelper = 203,
        BoxExpansion = 202,
        
        QuestInfo = 8,
        ArcadeFront = 9,
        Arena = 3,
        
        JiNengRongLian_selectMaterialMonster = 12,
        JiNengRongLian_selectBaseMonster = 13,
        JiNengRongLian_waitForConfirm = 14,
        
        Tutorial_skillEdit = 18,
        Tutorial_Story = 19
    }
}