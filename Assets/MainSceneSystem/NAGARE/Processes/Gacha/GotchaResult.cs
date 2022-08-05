using mainMenu;
using dataAccess;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GotchaResult : MSceneProcess
{
    public static List<StoneOfPlayerInfo> Result;
    private GotchaResultLayer layer;
    
    public GotchaResult()
    {
        Step = MainSceneStep.GotchaResult;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        layer = GotchaResultLayer.Open();
        StarsFall.target.gameObject.SetActive(true);
        layer.NineForShow.LoadShowDetailFeature(layer.ShowDetail);
        layer.WholeAnimProcess(Result).Forget();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        StarsFall.target.gameObject.SetActive(false);
        GotchaResultLayer.Close();
    }
}
