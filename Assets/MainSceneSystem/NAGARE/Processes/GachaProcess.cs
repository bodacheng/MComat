using System.Collections;
using mainMenu;

public class GachaProcess : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        BackGroundPS.target.Off();
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        PreScene.Instance.MainMenuCanvas.gameObject.SetActive(false);
        yield return GachaManager.target.Process();
    }
    
    public GachaProcess()
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
