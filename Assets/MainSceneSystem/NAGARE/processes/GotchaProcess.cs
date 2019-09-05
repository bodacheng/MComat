using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class GotchaProcess : MainSceneProcess
{
    //enterProcess()绝不能出现triggerMainProcess
    public IEnumerator enterProcess()
    {
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineSlotT.gameObject.SetActive(false);
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public GotchaProcess(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Gotcha;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this._preparingScene.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
