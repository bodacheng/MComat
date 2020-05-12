using System.Collections;
using mainMenu;
using dataAccess;
using UnityEngine;

// 这里，应该启动技能编辑画面。而且应该是技能编辑画面的一个特殊模式。
// 剧情人物亚当的模型应该会在这里启动，并且技能背包中应该是有备用的测试技能石。
// 点击了技能石编辑的确定按钮后，会进入战斗场景，而且是特殊的剧情式教学战斗。

public class Tutorial_skillEdit : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        SubProcessesRunner = new ProcessesRunner();
        if (AccountSet._AccInfo.accountprogress == PlayerAccountProgressStep.justCreated)
        {
            TryOneStoneAdd tryOneStoneAdd = new TryOneStoneAdd();
            TryEditALines tryEditALines = new TryEditALines();
            //SubProcessesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub1,tryOneStoneAdd);
            //SubProcessesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub2,tryEditALines);
            //SubProcessesRunner.ChangeProcess(MainSceneStep.Tutorial_skillEdit_sub1);
        }
        if (AccountSet._AccInfo.accountprogress == PlayerAccountProgressStep.Tutorial)
        {
            TryEditNineSlot tryEditNineSlot = new TryEditNineSlot(SubProcessesRunner);
            TryChangeStonePos _TryChangeStonePos = new TryChangeStonePos();
            //SubProcessesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub3,tryEditNineSlot);
            //SubProcessesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit_sub4,_TryChangeStonePos);
            //SubProcessesRunner.ChangeProcess(MainSceneStep.Tutorial_skillEdit_sub3);
        }
        yield break;
    }
    
    public Tutorial_skillEdit()
    {
        Step = MainSceneStep.Tutorial_skillEdit;
        nextProcessStep = MainSceneStep.Tutorial_Story;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        AccountSet._AccInfo.accountprogress = PlayerAccountProgressStep.Freedom;
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
        if (SubProcessesRunner != null)
            SubProcessesRunner.ProcessNagare();       
    }
}