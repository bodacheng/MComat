using mainMenu;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StonesPage : MainSceneProcess
{
    public StonesPage()
    {
        Step = MainSceneStep.SkillStoneList;
        EelementsInherit(PreScene.target);
    }
    
    private StoneListLayer stoneListLayer;
    
    readonly ReactiveProperty<int> userReadOnlyDataLoadFinished = new ReactiveProperty<int>(0);
    void UserReadOnlyDataLoadFinished(int value)
    {
        userReadOnlyDataLoadFinished.Value = value;
    }
    
    readonly ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }
    
    public override void ProcessEnter()
    {
        ProcessEnter<Any>(null);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        PlayFabReadClient.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        
        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished, userReadOnlyDataLoadFinished
            },
            () =>
            {
                if (t != null)
                    EnterProcess(t);
                else
                    EnterProcess();
            },
            () => { Debug.Log("错误，怎么办？"); }
        );
    }

    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess()
    {
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        CommonEnterProcess();
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess<T>(T t)
    {
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        CommonEnterProcess();
        stoneListLayer.levelManager.OpenLevelUpPage(t as string);
    }
    
    void CommonEnterProcess()
    {
        stoneListLayer = StoneListLayer.Open();
    }
    
    public override void ProcessEnd()
    {
        missionWatcher.DisposeAll();
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);
        StoneListLayer.Close();
    }
}