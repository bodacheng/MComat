using DummyLayerSystem;
using mainMenu;

public class ArenaPage : MSceneProcess
{
    private ArenaLayer arenaLayer;
    readonly ArenaDummiesTable table = new ();
    void EnterProcess()
    {
        arenaLayer = UILayerLoader.Load(PreScene.target.T, "ArenaLayer") as ArenaLayer;
        arenaLayer.SetUp(SetLoaded, PreScene.ReturnToLobby, table.GetDummiesAroundPoint);
        arenaLayer.ShowMyTeam();
        
        if (PlayerAccountInfo.Me.arenaPoint != -1)
        {
            arenaLayer.RefreshOpponent();
        }
        else // 说明玩家的防御队伍没有登陆，因为arenaPoint是首次登陆防御队伍时候顺便登陆的
        {
            // 强制玩家登陆防御队伍
            SetLoaded(true);
        }
    }
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        table.Load();
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove("ArenaLayer");
    }
}