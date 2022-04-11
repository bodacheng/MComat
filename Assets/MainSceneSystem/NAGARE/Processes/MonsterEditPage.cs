using UnityEngine;
using mainMenu;
using System.Collections.Generic;

public class MonsterEditPage : MainSceneProcess
{
    private SkillEditLayer skillEditLayer;
    
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
        skillEditLayer = SkillEditLayer.Open();
        
        // 没这行的话从技能石升级画面返回的话角色模型加载不出来
        //yield return UnitOptionLayer.target.CharModelRender(UnitInfo.GetCharDataInfo(PreScene.target._focusing));
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
                    skillEditLayer = SkillEditLayer.Open();
                    skillEditLayer.SkillShowSpEnterProcess();
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
}
