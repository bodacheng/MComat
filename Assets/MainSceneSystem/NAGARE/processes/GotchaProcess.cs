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
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public GotchaProcess(PreScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Gotcha;
        this._PreScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
