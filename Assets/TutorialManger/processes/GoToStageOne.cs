using mainMenu;
using UnityEngine;

public class GoToStageOne : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private ArcadeTop _arcadeTop;
    private ArcadeFrontPage _arcadeFrontPage;
    private FightingStepLayer fightingLayer;
    
    public override void ProcessEnter()
    {
    }
    
    public override void ProcessEnd()
    {
        HighLightLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return fightingLayer != null;
    }
    
    public override void LocalUpdate()
    {
        if (!Loaded)
        {
            if (_returnLayer == null)
                _returnLayer = ReturnLayer.Get();
            
            if (_arcadeTop == null)
                _arcadeTop = ArcadeTop.Get();
            
            if (_returnLayer != null && _arcadeTop != null)
            {
                _returnLayer.gameObject.SetActive(false);
                Loaded = true;
            }
        }

        if (fightingLayer == null)
        {
            fightingLayer = FightingStepLayer.Get();
        }
    }
}