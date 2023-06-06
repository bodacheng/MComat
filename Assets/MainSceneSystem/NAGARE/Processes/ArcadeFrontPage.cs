using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;

public class ArcadeFrontPage : MSceneProcess
{
    readonly ArcadeModeManager arcadeModeManager;
    ArcadeTop _arcadeTop;
    
    public ArcadeFrontPage(ArcadeModeManager arcadeModeManager)
    {
        Step = MainSceneStep.ArcadeFront;
        this.arcadeModeManager = arcadeModeManager;
    }
    
    public override void ProcessEnter()
    {
        PlayFabReadClient.GetStageRewardInfo(Enter);
    }
    
    void Enter()
    {
        _arcadeTop = UILayerLoader.Load<ArcadeTop>();
        Load().Forget();
    }

    async UniTask Load()
    {
        await arcadeModeManager.Initialize();
        _arcadeTop.Setup(arcadeModeManager);
        var stages = _arcadeTop.NewStages(PlayerAccountInfo.Me.arcadeProcess);
        await _arcadeTop.ShowStages(stages);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArcadeTop>();
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