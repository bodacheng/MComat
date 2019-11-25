using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class SkillStones : MainSceneProcess
{
    //enterProcess()绝不能出现triggerMainProcess
    public IEnumerator EnterProcess()
    {
        this._LoadingCanvas.DarkOff(1f);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        this._TheNineSlot.NineSlotT.gameObject.SetActive(false);
        
        IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
            yield return (loadMyStonesProcess);
                
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        yield return SkillStonesBox.Instance.EXTabsFeatureRefresh(true);//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        this._LoadingCanvas.LightUp();
    }
    
    public SkillStones(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.SkillStones;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        
    }

    public override void LocalUpdate()
    {
    }
}
