using DummyLayerSystem;

public class ForceBack : TutorialProcess
{
    private ReturnLayer _returnLayer;
    
    public delegate bool WaitOverDelegate();
    readonly WaitOverDelegate _waitForThis;
    
    public ForceBack(WaitOverDelegate waitOverDelegate)
    {
        _waitForThis = waitOverDelegate;
    }
    
    public override void ProcessEnter()
    {
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return _waitForThis();
    }

    public override void LocalUpdate()
    {
        if (_returnLayer == null)
        {
            _returnLayer = UILayerLoader.Get<ReturnLayer>();
        }
        
        if (_returnLayer != null)
        {
            _returnLayer.gameObject.SetActive(true);
            _returnLayer.ForceBackMode(true);
        }
    }
}
