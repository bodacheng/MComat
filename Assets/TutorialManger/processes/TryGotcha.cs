using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;

public class TryGotcha : TutorialProcess
{
    private GotchaLayer _gotchaLayer;
    private GotchaResultLayer _gotchaResultLayer;
    private ReturnLayer _returnLayer;
    
    public override void LocalUpdate()
    {
        if (_gotchaLayer == null)
        {
            _gotchaLayer = UILayerLoader.Get<GotchaLayer>();
            if (_gotchaLayer != null)
            {
                var gotchaFront  = (GotchaFront)ProcessesRunner.Main.GetProcess(MainSceneStep.GotchaFront);
                gotchaFront.SetExtraSuccessAction(
                    () =>
                    {
                        PlayFabReadClient.UpdateUserData(
                            new UpdateUserDataRequest()
                            {
                                Data = new Dictionary<string, string>()
                                {
                                    { "TutorialProgress", "GotchaFinished" }
                                }
                            },
                            () =>
                            {
                                PlayerAccountInfo.Me.tutorialProgress = "GotchaFinished";
                            },
                            PreScene.ReturnToLobby
                        );
                    }
                );
            }
        }
        
        if (_returnLayer == null)
            _returnLayer = UILayerLoader.Get<ReturnLayer>();
        if (_returnLayer != null)
            _returnLayer.gameObject.SetActive(false);
        
        if (_gotchaResultLayer == null)
        {
            _gotchaResultLayer = UILayerLoader.Get<GotchaResultLayer>();
            if (_gotchaResultLayer != null)
            {
                _returnLayer.gameObject.SetActive(true);
                _returnLayer.ForceBackMode(true);
            }
        }
    }

    public override bool CanEnterOtherProcess()
    {
        return PlayerAccountInfo.Me.tutorialProgress == "GotchaFinished" && _gotchaResultLayer != null && _gotchaResultLayer.ShowFinished;
    }
    
    public override void ProcessEnd()
    {
        var gotchaFront  = (GotchaFront)ProcessesRunner.Main.GetProcess(MainSceneStep.GotchaFront);
        gotchaFront.SetExtraSuccessAction(null);
    }
}
