using System.Collections;
using mainMenu;
using dataAccess;

public class SkillStones : MainSceneProcess
{
    //EnterProcess()内绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        yield return _modelShower.ShowModel(null);
        SkillStonesBox.target = PreScene.target._SkillStonesBox_Show;
        SSLevelUpManager.target.SetFocusingSSD(SkillStonesBox.target._skillStoneDetail);
        
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 1);
        
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(false);
        
        IEnumerator loadMyStonesProcess = MySkillStonesReader.LoadAll();
        yield return (loadMyStonesProcess);
        PreScene.target._SkillStonesBox_Show.BoxWholeT.gameObject.SetActive(true);
        
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.NormalTab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX1Tab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX2Tab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX3Tab.gameObject,5f),
        Zokusei.Null);
        
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        LoadingCanvas.target.LightUp();
    }
    
    public SkillStones()
    {
        thisProcessStep = MainSceneStep.SkillStones;
        EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }

    public override void LocalUpdate()
    {
    }
}
