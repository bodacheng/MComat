using DummyLayerSystem;
using mainMenu;

public class ArenaPage : MainSceneProcess
{
    private ArenaLayer arenaLayer;
    void EnterProcess()
    {
        mainProcessRunner.RunFreely(ModelShower.target.ShowMyModel(null));
        arenaLayer = UILayerLoader.Load(PreScene.target.T, "ArenaLayer") as ArenaLayer;
        arenaLayer.RefreshOpponent();
    }
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove("ArenaLayer");
    }
}