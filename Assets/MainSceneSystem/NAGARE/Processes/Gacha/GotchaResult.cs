using mainMenu;
using dataAccess;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;

public class GotchaResult : MSceneProcess
{
    public static List<StoneOfPlayerInfo> Result;
    private GotchaResultLayer layer;
    
    public GotchaResult()
    {
        Step = MainSceneStep.GotchaResult;
    }
    
    public override void ProcessEnter()
    {
        layer = UILayerLoader.Load<GotchaResultLayer>();
        layer.Setup();
        StarsFall.target.gameObject.SetActive(true);
        layer.NineForShow.AddOnClickToBtns(layer.ShowDetail);
        layer.WholeAnimProcess(Result).Forget();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        StarsFall.target.gameObject.SetActive(false);
        GotchaResultLayer.Close();
    }
}
