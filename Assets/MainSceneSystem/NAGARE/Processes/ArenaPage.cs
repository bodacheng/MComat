using System;
using System.Collections.Generic;
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
        arenaLayer.RefreshOpponent();
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