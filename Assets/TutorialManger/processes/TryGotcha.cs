using System.Collections.Generic;
using mainMenu;
using PlayFab.ClientModels;

public class TryGotcha : TutorialProcess
{
    private FrontLayer _frontLayer;
    private GotchaLayer _gotchaLayer;
    private GotchaResultLayer _gotchaResultLayer;
    private ReturnLayer _returnLayer;
    
    public override void LocalUpdate()
    {
        if (_frontLayer == null)
        {
            _frontLayer = FrontLayer.Get();
            if (_frontLayer != null)
                _frontLayer.PlsClickBtn("gotcha");
        }
        
        if (_gotchaLayer == null)
        {
            _gotchaLayer = GotchaLayer.Get();
            if (_gotchaLayer != null)
            {
                _gotchaLayer.TutorialMode();
                GotchaLayer.SetExtraSuccessAction(
                    () =>
                    {
                        PlayerAccountInfo.Me.TutorialProgress = "GotchaFinished";
                        PlayFabReadClient.UpdateUserData(
                            new UpdateUserDataRequest()
                            {
                                Data = new Dictionary<string, string>()
                                {
                                    { "TutorialProgress", "GotchaFinished" }
                                }
                            },
                            (x) =>
                            { }
                        );
                    }
                );
            }
        }
        
        if (_returnLayer == null)
        {
            _returnLayer = ReturnLayer.Get();
            if (_returnLayer != null)
                _returnLayer.gameObject.SetActive(false);
        }
        
        if (_gotchaResultLayer == null)
        {
            _gotchaResultLayer = GotchaResultLayer.Get();
            if (_gotchaResultLayer != null)
            {
                _returnLayer.gameObject.SetActive(true);
                _returnLayer.ForceBackMode(true);
            }
        }
    }

    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.FrontPage && 
               PlayerAccountInfo.Me.TutorialProgress == "GotchaFinished";
    }
}
