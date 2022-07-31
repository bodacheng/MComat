using UnityEngine;
using mainMenu;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class MonsterEditPage : MSceneProcess
{
    private SkillEditLayer layer;

    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    
    async UniTask EnterProcess()
    {
        PopupLayer.Loading(">", PreScene.target.T);
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        await SkillEditLayer.Open((x) =>
        {
            x._connector.ShowMyModel(PreScene.target._focusing != null ? PreScene.target._focusing.id : null);
        });
        PopupLayer.Close();
        SetLoaded(true);
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
            ()=>EnterProcess().Forget(),
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        SkillEditLayer.Close();
    }
}
