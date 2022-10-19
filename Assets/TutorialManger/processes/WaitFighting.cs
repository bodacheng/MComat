using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFighting : TutorialProcess
{
    private FightingStepLayer _fightingStepLayer;
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

    public override void LocalUpdate()
    {
        if (_fightingStepLayer == null)
        {
            _fightingStepLayer = FightingStepLayer.Get();
        }
    }
}
