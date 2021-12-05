using System.Collections;
using mainMenu;
using UnityEngine;

public class StoneMerge : MainSceneProcess
{
    private StoneMergeLayer stoneMergeLayer;
    public StoneMerge()
    {
        Step = MainSceneStep.StoneMerge;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        stoneMergeLayer = StoneMergeLayer.Open();
        
        yield return PreScene.target.modelShower.ShowMyModel(null);
        
        stoneMergeLayer.stoneBox.AddFeatureToCells(stoneMergeLayer.CellFeature_MergeMode);
        stoneMergeLayer.stoneBox.RestFilter();
        stoneMergeLayer.stoneBox.EXTabsFeatureRefresh(false);
        stoneMergeLayer._skillStoneDetail.Clear();
        stoneMergeLayer.stoneBox.IniExTabs(stoneMergeLayer.fxCamera);
        stoneMergeLayer.stoneBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, stoneMergeLayer.fxCamera, stoneMergeLayer.stoneBox.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, stoneMergeLayer.fxCamera, stoneMergeLayer.stoneBox.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, stoneMergeLayer.fxCamera, stoneMergeLayer.stoneBox.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, stoneMergeLayer.fxCamera, stoneMergeLayer.stoneBox.EX3Tab.GetComponent<RectTransform>(),5f), 
            Zokusei.redMagic
        );
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        stoneMergeLayer.ReturnAllMaterialsToBox();
        stoneMergeLayer.stoneBox._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        StoneMergeLayer.Close();
    }
}
