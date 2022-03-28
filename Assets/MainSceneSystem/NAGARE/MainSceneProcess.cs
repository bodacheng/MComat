
namespace mainMenu
{
    public abstract class MainSceneProcess : SceneProcess
    {
        public MainSceneStep Step;
        public MainSceneStep nextProcessStep = MainSceneStep.None;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

        protected SingleThreadProcesser mainProcessRunner;
        protected CameraManager _CameraManager;
        protected MissionWatcher missionWatcher;
        
        protected void Inherit(PreScene _preparingScene)
        {
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