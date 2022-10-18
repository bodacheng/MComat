using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryGotcha : TutorialProcess
{
    private FrontLayer _frontLayer;
    private GotchaLayer _gotchaLayer;
    private ReturnLayer _returnLayer;
    
    public override void LocalUpdate()
    {
        if (!_frontLayer)
        {
            _frontLayer = FrontLayer.Get();
            _frontLayer.PlsClickBtn("unit");
        }
        
        if (!_gotchaLayer)
        {
            _gotchaLayer = GotchaLayer.Get();
            GotchaLayer.SetExtraSuccessAction(
                () =>
                {
                    PlayerAccountInfo.Me.TutorialProgress = "GotchaFinished";
                }
            );
        }
        
        if (!_returnLayer)
        {
            _returnLayer = ReturnLayer.Get();
            _returnLayer.gameObject.SetActive(false);
        }
    }
}
