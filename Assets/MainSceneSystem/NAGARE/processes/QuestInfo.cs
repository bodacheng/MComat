using System.Collections;
using UnityEngine;
using mainMenu;

public class QuestInfo : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        QuestPreparePage.Instance.QuestPreparePageCanvas.gameObject.SetActive(true);
        //_QuestPreparePage.QuestName.text = _QuestPreparePage._Stage.battleNameENG;
        yield break;
    }
    
    public QuestInfo()
    {
        thisProcessStep = MainSceneStep.QuestInfo;
        EelementsInherit(PreScene.Instance);
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
        QuestPreparePage.Instance.QuestPreparePageCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
