namespace mainMenu
{
    public abstract class MSceneProcess : SceneProcess
    {
        public MainSceneStep Step;
        protected CameraManager _CameraManager;
        protected MissionWatcher missionWatcher;
        private bool _loaded = false;
        
        protected void SetLoaded(bool value)
        {
            _loaded = value;
        }
        public bool GetLoaded()
        {
            return _loaded;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return _loaded;
        }
        
        protected void Inherit(PreScene _preparingScene)
        {
            _CameraManager = _preparingScene._CameraManager;
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
        UnitList = 5,
        UnitSkillEdit = 16,
        UnitSkillShow = 17,
        SkillStoneList = 15,
        SkillStones_Sell = 100,
        GotchaFront = 6,
        GotchaResult = 24,
        Ranking = 25,
        
        ShopTop = 201,
        BoxOverLoadHelper = 203,
        
        QuestInfo = 8,
        ArcadeFront = 9,
        Arena = 3
    }
}