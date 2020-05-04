
public abstract class NagareProcess
{
    public FightSceneProcessesRunner fightSceneProcessesRunner;
    public SceneStep thisProcessStep;
    public SceneStep nextProcessStep = SceneStep.none;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

    bool can_next;
    public bool AutoMoveToNext
    {
        set
        {
            can_next = value;
            if (can_next && nextProcessStep != SceneStep.none)
            {
                FightSceneProcessesRunner.ChangeProcess(nextProcessStep);
            }
        }
        get
        {
            return can_next;
        }
    }

    public NetFightScene FightScene;
    public RealTimeGameProcessManager _RealTimeGameProcessManager;
    public MobileInputsManager mobileInputsManager;
    public DebugManager debugManager;
    public CharsManager CharsManager;
    public CameraManager cameraManager;
    public FightLogger fightLogger;
    public FightOverControl fightOverControl;
    public SingleThreadProcesser mainProcessRunner;
    
    public void EelementsInherit(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.fightSceneProcessesRunner = fightSceneProcessesRunner;
        this.FightScene = _NetFightScene;
        this._RealTimeGameProcessManager = _NetFightScene._RealTimeGameProcessManager;
        this.mainProcessRunner = _NetFightScene.mainProcessRunner;
        this.mobileInputsManager = this._RealTimeGameProcessManager._mobileInputsManager;
        this.CharsManager = this.FightScene._CharSetManager;
        this.cameraManager = this._RealTimeGameProcessManager._CameraManager;
        this.fightOverControl = this.FightScene._FightOverControl;
        this.debugManager = this.FightScene._DebugManager;
        this.fightLogger = this.FightScene.fightLogger;
    }
    
    public virtual void ProcessEnter()
    {
    }
    
    public virtual void ProcessEnd()
    {        
    }
        
    public virtual void LocalUpdate()
    {
    }
}

public enum SceneStep
{
    none = 0,
    Preparing = 1,
    StoryBeforeFight = 6,
    CountDown = 4,

    Fighting = 2,
    BasicTryTutorial = 7,

    FightOver = 3,
    FightSummary = 5,
}