using System.Collections;
using mainMenu;
using dataAccess;
using UnityEngine;

// 这里，应该启动技能编辑画面。而且应该是技能编辑画面的一个特殊模式。
// 剧情人物亚当的模型应该会在这里启动，并且技能背包中应该是有备用的测试技能石。
// 点击了技能石编辑的确定按钮后，会进入战斗场景，而且是特殊的剧情式教学战斗。
public class Tutorial_skillEdit : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        this.processesRunner = new ProcessesRunner();
        if (AccountSet.instance._PlayerAccountInfo.accountprogress == playerAccountProgressStep.justCreated)
        {
            TryOneStoneAdd tryOneStoneAdd = new TryOneStoneAdd(this._preparingScene);
            TryEditALines tryEditALines = new TryEditALines(this._preparingScene);
            processesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub1,tryOneStoneAdd);
            processesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub2,tryEditALines);
            processesRunner.changeProcess(MainSceneStep.Tutorial_skillEdit_sub1);
        }
        if (AccountSet.instance._PlayerAccountInfo.accountprogress == playerAccountProgressStep.Tutorial)
        {
            TryEditNineSlot tryEditNineSlot = new TryEditNineSlot(this._preparingScene,processesRunner);
            TryChangeStonePos _TryChangeStonePos = new TryChangeStonePos(this._preparingScene);
            processesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub3,tryEditNineSlot);
            processesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub4,_TryChangeStonePos);
            processesRunner.changeProcess(MainSceneStep.Tutorial_skillEdit_sub3);
        }
        yield break;
    }
    
    public Tutorial_skillEdit(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit;
        this.nextProcessStep = MainSceneStep.Tutorial_Story;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()//这个应该是有条件的，玩家应该给亚当装配多少个技能石才能进入战斗环节？  
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        AccountSet.Instance._PlayerAccountInfo.accountprogress = playerAccountProgressStep.Freedom;
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
        if (processesRunner != null)
            processesRunner.ProcessNagare();       
    }
}