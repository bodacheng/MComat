using mainMenu;
using dataAccess;
using System.Collections.Generic;

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
        mainProcessRunner.RunAsQueued(layer.WholeAnimProcess(Result));
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        StarsFall.target.gameObject.SetActive(false);
        GotchaResultLayer.Close();
    }
}
