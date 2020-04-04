using System.Collections;
using mainMenu;
using dataAccess;

public class SkillStones : MainSceneProcess
{
    //EnterProcess()内绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        SkillStonesBox.target = PreScene.Instance._SkillStonesBox_Show;
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(true);

        IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
        yield return (loadMyStonesProcess);
        PreScene.Instance._SkillStonesBox_Show.BoxWholeT.gameObject.SetActive(true);
        
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.NormalTab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX1Tab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX2Tab.gameObject,5f),
        SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX3Tab.gameObject,5f),
        Zokusei.Null);
        
        LoadingCanvas.target.LightUp();
    }
    
    public SkillStones()
    {
        thisProcessStep = MainSceneStep.SkillStones;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        
    }

    public override void LocalUpdate()
    {
    }
}
