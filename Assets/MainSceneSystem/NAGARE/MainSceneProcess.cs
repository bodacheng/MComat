
namespace mainMenu
{
    public abstract class MainSceneProcess : SceneProcess
    {
        public MainSceneStep Step;
        public MainSceneStep nextProcessStep = MainSceneStep.None;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

        public SingleThreadProcesser mainProcessRunner;
        public SelfFightManager _SelfFightManager;
        public CameraManager _CameraManager;
        public ProcessesRunner SubProcessesRunner;
        public MissionWatcher missionWatcher;

        public void EelementsInherit(PreScene _preparingScene)
        {
            _SelfFightManager = _preparingScene._SelfFightManager;
            _CameraManager = _preparingScene._CameraManager;
            mainProcessRunner = _preparingScene.mainProcessRunner;
        }
    }

    public enum MainSceneStep
    {
        None = 0,
        Setting = -1,
        MailBox = 10,
        MailDetail = 11,
        FrontPage = 1,
        SelfFightFront = 4,
        TeamEditFront = 2,
        MonsterList = 5,
        UnitSkillEdit = 16,
        UnitSkillShow = 17,
        SkillStoneList = 15,
        SkillStones_Sell = 100,
        GotchaFront = 6,
        GotchaAnim = 7,
        GotchaResult = 24,
        StoneMerge = 25,
        
        ShopTop = 201,
        BoxOverLoadHelper = 203,
        BoxExpansion = 202,
        
        QuestInfo = 8,
        ArcadeFront = 9,
        Arena = 3
    }
}