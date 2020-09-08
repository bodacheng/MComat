using System.Collections;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using UnityEngine;

public class SkillStones : MainSceneProcess
{
    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOffDirectly(1f);
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        yield return ModelShower.target.ShowMyModel(null);
        SSLevelUpManager.target.SetFocusingSSD(SkillStonesBox.target._skillStoneDetail);
        SkillStonesBox.target._skillStoneDetail.Clear();
        SSLevelUpManager.target.RefreshSkillLevelUpModule();
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 1);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        
        LoadingCanvas.target.LightUp();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(),5f), 
            Zokusei.blueMagic
        );
    }

    public SkillStones()
    {
        Step = MainSceneStep.SkillStones;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        SkillStonesBox.target = PreScene.target._SkillStonesBox_Show;
        if (ProcessesRunner.Main.lastProcess.Step != MainSceneStep.SkillStones_Sell)
        {
            mainProcessRunner.Run(EnterProcess());
        } else {
            SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        }
    }

    public override void ProcessEnd()
    {
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
        SSLevelUpManager.target.CloseLevelUpPage();
    }
}