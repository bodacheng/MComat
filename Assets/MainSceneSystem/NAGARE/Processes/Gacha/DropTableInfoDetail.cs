using DummyLayerSystem;
using mainMenu;

public class DropTableInfoDetail : MSceneProcess
{
    private DropTableInfoLayer _layer;
    
    public DropTableInfoDetail()
    {
        Step = MainSceneStep.DropTableInfo;
    }

    public override void ProcessEnter<T>(T tableId)
    {
        _layer = UILayerLoader.Load<DropTableInfoLayer>();
        CloudScript.GetDropTableInfo(_layer.ShowDropTableInfo, tableId as string);
    }

    public override void ProcessEnd()
    {
        UILayerLoader.Remove<DropTableInfoLayer>();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
}
