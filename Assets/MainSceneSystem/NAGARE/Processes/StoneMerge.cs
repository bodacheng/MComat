using System.Collections;
using mainMenu;
using dataAccess;
using UnityEngine;

public class StoneMerge : MainSceneProcess
{
    public StoneMerge()
    {
        Step = MainSceneStep.StoneMerge;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowMyModel(null);
        PreScene.target.MainMenuCanvas.gameObject.SetActive(false);
        SkillStonesBox.target = PreScene.target._SkillStonesBox_NineSlot;
        StoneMergeManger.target._Canvas.gameObject.SetActive(true);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, -1);
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(false);
        SkillStonesBox.target._skillStoneDetail.Clear();
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(),5f), 
            Zokusei.redMagic
        );
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        StoneMergeManger.target.ReturnAllMaterialsToBox();
        StoneMergeManger.target._Canvas.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }
}
