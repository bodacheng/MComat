using System.Collections;
using UnityEngine;
using mainMenu;

public class QuestInfo : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        T.gameObject.SetActive(true);
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(false);
        //_QuestPreparePage.QuestName.text = _QuestPreparePage._Stage.battleNameENG;
        yield break;
    }
    
    public QuestInfo(RectTransform T)
    {
        thisProcessStep = MainSceneStep.QuestInfo;
        this.T = T;
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
        this.T.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
