using DummyLayerSystem;
using mainMenu;

public class ArenaPage : MSceneProcess
{
    private ArenaLayer arenaLayer;
    void EnterProcess()
    {
        arenaLayer = UILayerLoader.Load(PreScene.target.T, "ArenaLayer") as ArenaLayer;
        arenaLayer.RefreshOpponent(SetLoaded);
    }
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
        Inherit(PreScene.target);
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