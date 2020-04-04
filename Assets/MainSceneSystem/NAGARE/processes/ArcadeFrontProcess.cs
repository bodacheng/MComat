using System.Collections;
using UnityEngine;
using mainMenu;

public class ArcadeFrontProcess : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        ArcadeManager.Instance._ArcadeCanvas.gameObject.SetActive(true);
        ArcadeManager.Instance.LocalTest();
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public ArcadeFrontProcess(RectTransform T)
    {
        thisProcessStep = MainSceneStep.ArcadeFront;
        this.T = T;
        EelementsInherit(PreScene.Instance);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArcadeManager.Instance._ArcadeCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
