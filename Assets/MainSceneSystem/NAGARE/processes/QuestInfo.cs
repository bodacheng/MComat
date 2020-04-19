using System.Collections;
using mainMenu;

public class QuestInfo : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        yield return _modelShower.ShowModel(null);
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        QuestPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
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
        this.mainProcessRunner.Run(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        QuestPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}
