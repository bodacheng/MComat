using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOne : TutorialProcess
{
    private FrontLayer _frontLayer;
    public override void ProcessEnter()
    {
        
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return PlayerAccountInfo.Me.TutorialProgress == "StageOneFinished";
    }
}
