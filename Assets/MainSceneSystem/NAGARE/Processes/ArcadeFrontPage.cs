using System.Threading;
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
    CancellationTokenSource _cts;
    public override void ProcessEnter()
    {
        _arcadeTop = UILayerLoader.Load<ArcadeTop>();
        _cts = new CancellationTokenSource();
        _arcadeTop.Setup(_cts);
        ReturnLayer.AddUniTaskCancel(_cts);
        Load().Forget();
    }

    async UniTask Load()
    {
        var stages = _arcadeTop.NewStages(PlayerAccountInfo.Me.arcadeProcess);
        await _arcadeTop.ShowStages(stages, _cts.Token);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArcadeTop>();
        _cts.Dispose();
    }
}