using System.Collections;
using mainMenu;
using System.Collections.Generic;
using UnityEngine;

public class SkillStonesList : MainSceneProcess
{ 
    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess()
    {
        SkillStonesBox.target._skillStoneDetail.Clear();
        yield return CommonEnterProcess();
        SkillStonesBox.target.GenerateCells();
        SkillStonesBox.target.CellsFeatureLoad(1);
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess<T>(T t)
    {
        yield return CommonEnterProcess();
        SkillStonesBox.target.GenerateCells();
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
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
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

    public SkillStonesList()
    {
        Step = MainSceneStep.SkillStoneList;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        SkillStonesBox.target = PreScene.target._SkillStonesBox_Show;
        if (t != null)
            mainProcessRunner.Run(EnterProcess(t));
        else
            mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnter()
    {
        ProcessEnter<Any>(null);
    }

    public override void ProcessEnd()
    {
        SkillStonesBox.target._skillStoneDetail.Clear();
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
        SSLevelUpManager.target.CloseLevelUpPage();
    }
}