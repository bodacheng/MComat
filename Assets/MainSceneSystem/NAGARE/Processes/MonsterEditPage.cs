using UnityEngine;
using mainMenu;
using System.Collections.Generic;

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
        layer = SkillEditLayer.Open();
        
        mainProcessRunner.RunAsQueued(layer._connector.ShowMyModel(
            PreScene.target._focusing != null ?
                PreScene.target._focusing.id: null));
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
            () => {
                if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                {
                    layer = SkillEditLayer.Open();
                    layer.SkillShowSpEnterProcess();
                }
                else
                {
                    EnterProcess();
                }
            },
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        SkillEditLayer.Close();
    }
    
    public override void LocalUpdate()
    {
        if (SkillShowSupporter.focusingC != null)
        {
            layer._connector.CameraPositionCal();
        }
        if (SkillShowSupporter.IfShowingSkill)
        {
            SkillShowSupporter.SkillsPrintOutLateUpdate();
        }
    }
}
