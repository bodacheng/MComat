using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;

public class TryGotcha : TutorialProcess
{
    private GotchaLayer gotchaLayer;
    private GotchaResultLayer gotchaResultLayer;
    private ReturnLayer returnLayer;
    
    public override void LocalUpdate()
    {
        if (gotchaLayer == null)
        {
            gotchaLayer = UILayerLoader.Get<GotchaLayer>();
            if (gotchaLayer != null)
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
        
        if (returnLayer == null)
            returnLayer = UILayerLoader.Get<ReturnLayer>();
        if (returnLayer != null)
            returnLayer.gameObject.SetActive(false);
        
        if (gotchaResultLayer == null)
        {
            gotchaResultLayer = UILayerLoader.Get<GotchaResultLayer>();
            if (gotchaResultLayer != null)
            {
                returnLayer.gameObject.SetActive(true);
                returnLayer.ForceBackMode(true);
            }
        }
    }

    public override bool CanEnterOtherProcess()
    {
        return PlayerAccountInfo.Me.tutorialProgress == "GotchaFinished" && gotchaResultLayer != null && gotchaResultLayer.ShowFinished;
    }
    
    public override void ProcessEnd()
    {
        var gotchaFront  = (GotchaFront)ProcessesRunner.Main.GetProcess(MainSceneStep.GotchaFront);
        gotchaFront.SetExtraSuccessAction(null);
    }
}
