using dataAccess;
using UniRx;

namespace FightScene
{
    // 这个环节大体来说应该是某种。。。点击确认然后进入下一个菜单的感觉。。。
    public class FightSummaryProcess : FSceneProcess
    {
        public ReactiveProperty<bool> enternext { get; set; } = new ReactiveProperty<bool>(false);
        public FightSummaryProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.FightSummary;
            //this.nextProcessStep = 这个环节结束后应该是直接的产生条件判断分歧。
            EelementsInherit(_NetFightScene);
            enternext.Subscribe(x => { if (x) AfterSummary(FightSceneNote.nextBattle._fightEventType); });
        }

        public override void ProcessEnter()
        {
            mainProcessRunner.Run(FightOverControl.target.ShowRewards(999, 999));
        }

        public override void ProcessEnd()
        {
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
        }

        public void AfterSummary(FightEventType _fightEventType)
        {
            switch (_fightEventType)
            {
                case FightEventType.Arena:
                    break;
                case FightEventType.Quest:
                    break;
            }
        }
    }
}