using mainMenu;
using UnityEngine;

public class BoxOverLoadFix : MainSceneProcess
{
    public BoxOverLoadFix()
    {
        Step = MainSceneStep.BoxOverLoadHelper;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PageTo.Go(MainSceneStep.BoxOverLoadHelper);
        BoxOverLoadFixManager.target.ArrangeButtonsFeature();
    }
    
    public override void ProcessEnd()
    {
        BoxOverLoadFixManager.target.T.gameObject.SetActive(false);
    }
}