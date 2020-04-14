using System.Collections;
using mainMenu;

public class GotchaProcess : MainSceneProcess
{
    //enterProcess()绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        yield break;
    }
    
    public GotchaProcess()
    {
        thisProcessStep = MainSceneStep.Gotcha;
        EelementsInherit(PreScene.Instance);
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
    }

    public override void LocalUpdate()
    {
    }
}
