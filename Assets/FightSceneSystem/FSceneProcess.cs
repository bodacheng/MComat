using mainMenu;

namespace FightScene
{
    public abstract class FSceneProcess: SceneProcess
    {
        public SceneStep Step;
        public SceneStep nextProcessStep = SceneStep.None;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。
        public NetFightScene FightScene;
        public MobileInputsManager mobileInputsManager;
        public CameraManager cameraManager;
        public FightLogger fightLogger;
        public SingleThreadProcesser mainProcessRunner;

        public void EelementsInherit(NetFightScene _NetFightScene)
        {
            this.FightScene = _NetFightScene;
            this.mainProcessRunner = _NetFightScene.mainProcessRunner;
            this.mobileInputsManager = RealTimeGameProcessManager.target._mobileInputsManager;
            this.cameraManager = RealTimeGameProcessManager.target._CameraManager;
            this.fightLogger = FightScene.fightLogger;
        }
    }

    public enum SceneStep
    {
        None = 0,
        Preparing = 1,
        StoryBeforeFight = 6,
        CountDown = 4,
        
        Fighting = 2,
        BasicTryTutorial = 7,
        
        FightOver = 3
    }
}