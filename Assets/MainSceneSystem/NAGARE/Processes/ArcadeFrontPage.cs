using Cysharp.Threading.Tasks;
using mainMenu;

public class ArcadeFrontPage : MainSceneProcess
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
        var stages = arcadeTop.NewStages(PlayerAccountInfo.Me.ArcadeProcess);
        arcadeTop.ShowStages(stages).Forget();
    }
    
    public override void ProcessEnd()
    {
        ArcadeTop.Close();
    }
}