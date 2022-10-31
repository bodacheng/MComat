using UnityEngine;
using mainMenu;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;

public class SkillEditPage : MSceneProcess
{
    private SkillEditLayer layer;
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    async UniTask EnterProcess()
    {
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }

        var layer = UILayerLoader.Load<SkillEditLayer>();
        await layer.Setup((x) =>
        {
            x._connector.ShowMyModel(PreScene.target.Focusing != null ? PreScene.target.Focusing.id : null);
        });
        
        SetLoaded(true);
    }
    
    public SkillEditPage()
    {
        Step = MainSceneStep.UnitSkillEdit;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        
        missionWatcher = new MissionWatcher(
            new List<string>() {"itemsLoadFinished"},
            ()=>EnterProcess().Forget(),
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        UILayerLoader.Remove<SkillEditLayer>();
    }
}
