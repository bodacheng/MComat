using System.Collections;
using mainMenu;
using UnityEngine;

public class StoneMerge : MainSceneProcess
{
    private StoneMergeLayer stoneMergeLayer;
    public StoneMerge()
    {
        Step = MainSceneStep.StoneMerge;
        Inherit(PreScene.target);
    }
    
    IEnumerator EnterProcess()
    {
        stoneMergeLayer = StoneMergeLayer.Open();
        
        yield return ModelShower.target.ShowMyModel(null);
        
        stoneMergeLayer.stoneBox.AddFeatureToCells(stoneMergeLayer.CellFeature_MergeMode);
        stoneMergeLayer.stoneBox.RestFilter();
        stoneMergeLayer.stoneBox.EXTabsFeatureRefresh(false);
        stoneMergeLayer._skillStoneDetail.Clear();
        stoneMergeLayer.stoneBox.IniExTabs(stoneMergeLayer.fxCamera);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        stoneMergeLayer.ReturnAllMaterialsToBox();
        stoneMergeLayer.stoneBox._tabEffects.CloseShowingZokuseiTagEffects();
        StoneMergeLayer.Close();
    }
}
