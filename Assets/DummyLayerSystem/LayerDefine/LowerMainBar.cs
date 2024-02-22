using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class LowerMainBar : UILayer
{
    [SerializeField] private BOButton playTab;
    [SerializeField] private BOButton fighterTab;
    [SerializeField] private BOButton stoneTab;
    [SerializeField] private BOButton gotchaTab;
    
    [SerializeField] private BOButton backBtn;
    
    public void Initialise(PreScene pre)
    {
        playTab.SetListener(() => pre.trySwitchToStep(MainSceneStep.FrontPage));
        fighterTab.SetListener(() => pre.trySwitchToStep(MainSceneStep.UnitList));
        stoneTab.SetListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList));
        gotchaTab.SetListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront));
    }
}
