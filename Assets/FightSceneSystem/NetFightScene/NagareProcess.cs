using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NagareProcess
{
    public FightSceneProcessesRunner fightSceneProcessesRunner;
    public SceneStep thisProcessStep;
    public SceneStep nextProcessStep = SceneStep.none;//有的话代表本process存在一个注定会自然迁移到的下一个process。没的话代表本process不一定迁移到哪。

    public NetFightScene _NetFightScene;
    public RealTimeGameProcessManager _RealTimeGameProcessManager;
    public mobileInputsManager mobileInputsManager;
    public FightTalksRunner fightTalksRunner;
    public BoundaryControllByGod BoundaryControllByGod;
    public DebugManager debugManager;
    public CharsManager CharsManager;
    public CameraManager cameraManager;
    public LoadingCanvas loadingCanvas;
    public FightLogger fightLogger;
    public FightOverControl fightOverControl;
    public SingleThreadProcesser mainProcessRunner;
    
    public void EelementsInherit(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.fightSceneProcessesRunner = fightSceneProcessesRunner;
        this._NetFightScene = _NetFightScene;
        this._RealTimeGameProcessManager = _NetFightScene._RealTimeGameProcessManager;
        this.mainProcessRunner = _NetFightScene.mainProcessRunner;
        this.mobileInputsManager = this._RealTimeGameProcessManager._mobileInputsManager;
        this.CharsManager = this._NetFightScene._CharSetManager;
        this.cameraManager = this._RealTimeGameProcessManager._CameraManager;
        this.loadingCanvas = this._NetFightScene._LoadingCanvas;
        this.fightOverControl = this._NetFightScene._FightOverControl;
        this.fightTalksRunner = this._NetFightScene._FightTalksRunner;
        this.debugManager = this._NetFightScene._DebugManager;
        this.fightLogger = this._NetFightScene.fightLogger;
        this.BoundaryControllByGod = this._NetFightScene._BoundaryControllByGod;
    }
    public virtual void ProcessEnter()
    {
    }
    
    public virtual void ProcessEnd()
    {
    }
    
    public virtual bool canEnterNextProcess()
    {
        return false;
    }
    
    public virtual void localUpdate()
    {
    }
}

public enum SceneStep : int
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