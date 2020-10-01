using System.Collections;
using mainMenu;
using dataAccess;

public class StoneMerge : MainSceneProcess
{
    public StoneMerge()
    {
        Step = MainSceneStep.StoneMerge;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        SkillStonesBox.target = PreScene.target._SkillStonesBox_NineSlot;
        SkillStonesBox.target.CellsFeatureLoad(AccountSet._AccInfo.Stoneboxsize, -1);
        StoneMergeManger.target._Canvas.gameObject.SetActive(true);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        StoneMergeManger.target.ReturnAllMaterialsToBox();
        StoneMergeManger.target._Canvas.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
    }
}
