using mainMenu;
using dataAccess;

// Tutorial 1 
public class ChooseAdamToSlot1 : TutorialProcess
{
    public ChooseAdamToSlot1()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(MonsterBox.GetCharIcon("1").transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(0) == "1";
    }
}