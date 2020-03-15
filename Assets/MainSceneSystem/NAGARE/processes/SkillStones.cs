using System.Collections;
using mainMenu;
using dataAccess;

public class SkillStones : MainSceneProcess
{
    //enterProcess()绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        
        IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
        yield return (loadMyStonesProcess);
                
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        yield return SkillStonesBox.Instance.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        LoadingCanvas.target.LightUp();
    }
    
    public SkillStones(PreScene _preparingScene)
    {
        thisProcessStep = MainSceneStep.SkillStones;
        this._PreScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        
    }

    public override void LocalUpdate()
    {
    }
}
