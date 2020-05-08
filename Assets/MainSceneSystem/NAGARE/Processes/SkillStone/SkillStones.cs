using System.Collections;
using mainMenu;
using dataAccess;
using System.Collections.Generic;

public class SkillStones : MainSceneProcess
{
    //EnterProcess()内绝不能出现triggerMainProcess
    public static IEnumerator EnterProcess()
    {
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        LoadingCanvas.target.DarkOff(1f);
        yield return ModelShower.target.ShowModel(null);
        SSLevelUpManager.target.SetFocusingSSD(SkillStonesBox.target._skillStoneDetail);        
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 1);        
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        
        IEnumerator loadMyStonesProcess = MySkillStonesReader.LoadAll();
        yield return (loadMyStonesProcess);        
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        LoadingCanvas.target.LightUp();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
    }

    public SkillStones()
    {
        Step = MainSceneStep.SkillStones;
        EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
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
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.NormalTab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX1Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX2Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX3Tab.gameObject,5f), 
            Zokusei.Null
        );
    }

    public override void ProcessEnd()
    {
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
    }
}