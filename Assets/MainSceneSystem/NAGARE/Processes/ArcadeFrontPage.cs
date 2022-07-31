using Cysharp.Threading.Tasks;
using mainMenu;

public class ArcadeFrontPage : MSceneProcess
{
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
        Inherit(PreScene.target);
    }
    
    ArcadeTop arcadeTop;
    public override void ProcessEnter()
    {
        arcadeTop = ArcadeTop.Open();
        Load().Forget();
    }

    async UniTask Load()
    {
        var stages = arcadeTop.NewStages(PlayerAccountInfo.Me.ArcadeProcess);
        await arcadeTop.ShowStages(stages);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        ArcadeTop.Close();
    }
}