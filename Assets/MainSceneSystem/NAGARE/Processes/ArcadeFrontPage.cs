using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;

public delegate UniTask<FightInfo> LoadStageDelegate(int stageNo);
public class ArcadeFrontPage : MSceneProcess
{
    ArcadeTop _arcadeTop;
    int _loadVersion;
    
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
    }
    
    public override void ProcessEnter()
    {
        SetLoaded(false);
        var loadVersion = ++_loadVersion;
        _arcadeTop = UILayerLoader.Load<ArcadeTop>();
        Load(loadVersion).Forget();
    }
    
    bool IsActiveLoad(int loadVersion)
    {
        return loadVersion == _loadVersion && _arcadeTop != null;
    }
    
    async UniTask Load(int loadVersion)
    {
        await ArcadeModeManager.Instance.Initialize();
        if (!IsActiveLoad(loadVersion))
            return;
        
        _arcadeTop.SetupArcade(ArcadeModeManager.Instance.MaxStageNum, ArcadeModeManager.Instance.LoadStage, ArcadeModeManager.Instance.DirectToArcadeStage);
        var stages = _arcadeTop.NewStages(ArcadeModeManager.ClampQuestProgress(PlayerAccountInfo.Me.arcadeProcess));
        if (!IsActiveLoad(loadVersion))
            return;
        
        await _arcadeTop.ShowStages(stages);
        if (!IsActiveLoad(loadVersion))
            return;
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        _loadVersion++;
        SetLoaded(false);
        UILayerLoader.Remove<ArcadeTop>();
        _arcadeTop = null;
    }
}

public class StageAward
{
    public string stageKey;
    public Award award;
}

public class Award
{
    public int g;
    public int d;
}
