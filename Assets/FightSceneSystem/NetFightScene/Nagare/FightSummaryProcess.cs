using mainMenu;
using dataAccess;

// 这个环节大体来说应该是某种。。。点击确认然后进入下一个菜单的感觉。。。
public class FightSummaryProcess : NagareProcess
{
    public bool enternext = false;
    public FightSummaryProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.FightSummary;
        //this.nextProcessStep = 这个环节结束后应该是直接的产生条件判断分歧。
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }

    public override bool canEnterNextProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        enternext = false;
        this.mainProcessRunner.triggerMainProcess(fightOverControl.showRewards(999,999,new System.Collections.Generic.List<int>()));
    }
    
    public override void ProcessEnd()
    {
        fightOverControl.FightOverCanvas.gameObject.SetActive(false);
    }
    
    public override void localUpdate()
    {
        if (enternext)
        {
            afterSummary(FightSceneNote.Instance.nextBattle._fightEventType);
        }
    }

    public void afterSummary(fightEventType _fightEventType)
    {
        switch (_fightEventType)
        {
            case fightEventType.Arena:
                break;
            case fightEventType.Quest:
                break;
            case fightEventType.Tutorial_Basic:
                AccountSet.instance._PlayerAccountInfo.accountprogress = playerAccountProgressStep.Tutorial;
                this._NetFightScene.returnToFront(MainSceneStep.frontPage);
                break;
            case fightEventType.Tutorial_Story_AdamVsGuards:
                AccountSet.instance._PlayerAccountInfo.accountprogress = playerAccountProgressStep.Freedom;
                this._NetFightScene.returnToFront(MainSceneStep.frontPage);
                break;
        }    
    }
}