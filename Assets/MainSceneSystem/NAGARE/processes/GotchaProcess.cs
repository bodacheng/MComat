using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class GotchaProcess : MainSceneProcess
{
    //enterProcess()绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(true);
        yield break;
    }
    
    public GotchaProcess()
    {
        this.thisProcessStep = MainSceneStep.Gotcha;
        this.EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._gotchaManager.gotchaCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
