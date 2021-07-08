using System.Collections;
using mainMenu;
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
        PageTo.Go(MainSceneStep.StoneMerge);
        SkillStonesBox.target = PreScene.target._SkillStonesBox_NineSlot;
        StoneMergeManger.target._Canvas.gameObject.SetActive(true);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
        SkillStonesBox.target.CellsFeatureLoad(-1);
        SkillStonesBox.target.RestFilter();
        SkillStonesBox.target.EXTabsFeatureRefresh(false);
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
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        StoneMergeManger.target.ReturnAllMaterialsToBox();
        StoneMergeManger.target._Canvas.gameObject.SetActive(false);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }
}
