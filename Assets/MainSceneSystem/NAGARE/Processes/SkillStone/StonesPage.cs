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
        CommonEnterProcess();
        stoneListLayer.box.AddFeatureToCells(stoneListLayer.CellFeature_StoneShow);
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess<T>(T t)
    {
        CommonEnterProcess();
        stoneListLayer.ssLevelUper.OpenLevelUpPage(t as string);
    }
    
    void CommonEnterProcess()
    {
        stoneListLayer = StoneListLayer.Open();
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.DarkOffDirectly(1f);
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        //stoneListLayer.fxCamera.transform.SetParent(null);
    //     stoneListLayer.box._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
    //     (
    // ScreenPositionCal.Cal2(2, stoneListLayer.fxCamera, stoneListLayer.box.NormalTab.GetComponent<RectTransform>(), 5f),
    // ScreenPositionCal.Cal2(2, stoneListLayer.fxCamera, stoneListLayer.box.EX1Tab.GetComponent<RectTransform>(), 5f),
    // ScreenPositionCal.Cal2(2, stoneListLayer.fxCamera, stoneListLayer.box.EX2Tab.GetComponent<RectTransform>(), 5f),
    // ScreenPositionCal.Cal2(2, stoneListLayer.fxCamera, stoneListLayer.box.EX3Tab.GetComponent<RectTransform>(), 5f), 
    //         Zokusei.blueMagic
    //     );
        popupLayer.LightUp();
    }
    
    public override void ProcessEnd()
    {
        missionWatcher.DisposeAll();
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);
        StoneListLayer.Close();
    }
}