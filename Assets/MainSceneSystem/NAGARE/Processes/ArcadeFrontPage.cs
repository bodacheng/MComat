using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;

public class ArcadeFrontPage : MSceneProcess
{
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
    }
    
    ArcadeTop _arcadeTop;
    StageModeTable stageModeTable;

    public override void ProcessEnter()
    {
        PlayFabReadClient.GetStageRewardInfo(Enter, PreScene.ReturnToLobby);
    }
    
    void Enter()
    {
        stageModeTable = new StageModeTable();
        _arcadeTop = UILayerLoader.Load<ArcadeTop>();
        _arcadeTop.Setup(stageModeTable);
        Load().Forget();
    }

    async UniTask Load()
    {
        await stageModeTable.LoadStageMode();
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