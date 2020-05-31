using FightScene;
using mainMenu;

public class TutorialProcess : SceneProcess
{
    public SingleThreadProcesser mainProcessRunner;
    public SelfFightManager _SelfFightManager;
    public CameraManager _CameraManager;
    public ProcessesRunner SubProcessesRunner;

    public void EelementsInherit(PreScene _preparingScene)
    {
        _SelfFightManager = _preparingScene._SelfFightManager;
        _CameraManager = _preparingScene._CameraManager;
        mainProcessRunner = _preparingScene.mainProcessRunner;
    }
    
    public void EelementsInherit(NetFightScene NetFightScene)
    {
    }
}