using System.Collections;
using mainMenu;
using UnityEngine;

public class ArcadeFrontProcess : MainSceneProcess
{
    public bool loadFinished;
    
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowMyModel(null);
        yield return ArcadeManager.target.PageRefresh();
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(true);
        loadFinished = true;
    }
        
    public ArcadeFrontProcess()
    {
        Step = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.target);
    }
        
    public override void ProcessEnter()
    {
        loadFinished = false;
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.3f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}