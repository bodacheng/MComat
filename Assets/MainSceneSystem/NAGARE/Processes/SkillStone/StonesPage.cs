using System.Collections;
using mainMenu;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using dataAccess;

public class StonesPage : MainSceneProcess
{
    ReactiveProperty<int> userReadOnlyDataLoadFinished = new ReactiveProperty<int>(0);
    void UserReadOnlyDataLoadFinished(int value)
    {
        userReadOnlyDataLoadFinished.Value = value;
    }

    ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess()
    {
        SkillStonesBox.target._skillStoneDetail.Clear();
        yield return CommonEnterProcess();
        SkillStonesBox.target.CellsFeatureLoad(1);
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess<T>(T t)
    {
        yield return CommonEnterProcess();
        SSLevelUpManager.target.OpenLevelUpPage(t as string);
    }
    
    static IEnumerator CommonEnterProcess()
    {
        LoadingCanvas.target.DarkOffDirectly(1f);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        yield return ModelShower.target.ShowMyModel(null);
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(true);
        SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(), 5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(), 5f), 
            Zokusei.blueMagic
        );
        LoadingCanvas.target.LightUp();
    }

    public StonesPage()
    {
        Step = MainSceneStep.SkillStoneList;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        Account.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        ItemLoader.LoadAll(ItemsLoadFinished);

        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished, userReadOnlyDataLoadFinished
            },
            () =>
            {
                SkillStonesBox.target = PreScene.target._SkillStonesBox_Show;
                if (t != null)
                    mainProcessRunner.RunAsQueued(EnterProcess(t));
                else
                    mainProcessRunner.RunAsQueued(EnterProcess());
            },
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnter()
    {
        ProcessEnter<Any>(null);
    }

    public override void ProcessEnd()
    {
        missionWatcher.DisposeAll();
        ItemsLoadFinished(0);
        UserReadOnlyDataLoadFinished(0);

        SkillStonesBox.target._skillStoneDetail.Clear();
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);

        // 下面这句相当意义不明
        //SSLevelUpManager.target.CloseLevelUpPage();
    }
}