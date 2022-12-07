using UnityEngine;
using mainMenu;
using DummyLayerSystem;

// 任何报酬的赋予都是服务端的工作，而反应在客户端上应该是一种根据远程结果进行刷新的机制

public class MailDetailProcess : MSceneProcess
{
    public MailDetailProcess()
    {
        Step = MainSceneStep.MailDetail;
    }
    
    MailDetailView _mailDetailViewLayer;
    public override void ProcessEnter<String>(String id)
    {
        _mailDetailViewLayer = UILayerLoader.Load<MailDetailView>();
        var mail = PlayFabReadClient.Get(id.ToString());
        _mailDetailViewLayer.Read(mail);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        if (_mailDetailViewLayer != null)
            GameObject.Destroy(_mailDetailViewLayer.gameObject);
    }
}
