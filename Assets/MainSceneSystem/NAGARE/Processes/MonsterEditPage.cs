using UnityEngine;
using mainMenu;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class MonsterEditPage : MainSceneProcess
{
    private SkillEditLayer layer;
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    void EnterProcess()
    {
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        SkillEditLayer.Open((x) =>
        {
            x._connector.ShowMyModel(PreScene.target._focusing != null ? PreScene.target._focusing.id : null);
        }).Forget();
    }
    
    public MonsterEditPage()
    {
        Step = MainSceneStep.UnitSkillEdit;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        
        missionWatcher = new MissionWatcher(
            new List<string>() {"itemsLoadFinished"},
            EnterProcess,
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        SkillEditLayer.Close();
    }
}
