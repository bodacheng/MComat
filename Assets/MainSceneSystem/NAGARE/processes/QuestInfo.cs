using System.Collections;
using mainMenu;

public class QuestInfo : MainSceneProcess
{
    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowModel(null);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
        yield return FightPreparePage.target.GetReadyForStageASTeam();
    }
    
    public QuestInfo()
    {
        Step = MainSceneStep.QuestInfo;
        EelementsInherit(PreScene.target);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(false);
    }
}
