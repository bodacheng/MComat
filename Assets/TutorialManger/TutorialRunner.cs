using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using FightScene;

public class TutorialRunner
{
    static TutorialRunner instance;
    public static TutorialRunner Main
    {
        get
        {
            if (instance == null)
            {
                instance = new TutorialRunner();
            }
            return instance;
        }
    }

    public TutorialProcess lastProcess;
    public TutorialProcess currentProcess;

    // 这个结构代表了教程的顺序, 很大的特点在于可加入重复元素。典型的如后退菜单
    List<TutorialProcess> TutorialProcesses = new List<TutorialProcess>();

    public void GenerateTutorial()
    {
        GoToMemberDetail goToMemberDetail = new GoToMemberDetail();
        OpenSkillEdit openSkillEdit = new OpenSkillEdit();
        SkillEditA1Try skillEditA1Try = new SkillEditA1Try();
        SkillEditA2Try skillEditA2Try = new SkillEditA2Try();
        SkillEditA3Try skillEditA3Try = new SkillEditA3Try();
        SkillEditTry_A1Filled skillEditTry_A1Filled = new SkillEditTry_A1Filled();
        SkillEditTry_A2Filled skillEditTry_A2Filled = new SkillEditTry_A2Filled();
        SkillEditTry_A3Filled skillEditTry_A3Filled = new SkillEditTry_A3Filled();
        ALineConfirm aLineConfirm = new ALineConfirm();
        ReturnOne returnOne = new ReturnOne();
        GoToStages goToStages = new GoToStages();
        GoToStageOne goToStageOne = new GoToStageOne();
        GoToTeamEdit goToTeamEdit = new GoToTeamEdit();
        ClickTeamEditSlotOne clickTeamEditSlotOne = new ClickTeamEditSlotOne();
        ChooseAdamToSlot1 ChooseAdamToSlot1 = new ChooseAdamToSlot1();
        ConfirmQuest1 confirmQuest1 = new ConfirmQuest1();
        
        bool StartedFighting()
        { 
            return FSceneProcessesRunner.Main.currentProcess.Step == SceneStep.Fighting; 
        }
        WaitProcess waitForStage1Loaded = new WaitProcess(StartedFighting);

        TutorialProcesses.Add(goToMemberDetail);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(skillEditA1Try);
        TutorialProcesses.Add(skillEditTry_A1Filled);
        TutorialProcesses.Add(skillEditA2Try);
        TutorialProcesses.Add(skillEditTry_A2Filled);
        TutorialProcesses.Add(skillEditA3Try);
        TutorialProcesses.Add(skillEditTry_A3Filled);
        TutorialProcesses.Add(aLineConfirm);
        TutorialProcesses.Add(returnOne);
        TutorialProcesses.Add(returnOne);
        TutorialProcesses.Add(goToStages);
        TutorialProcesses.Add(goToStageOne);
        TutorialProcesses.Add(goToTeamEdit);
        TutorialProcesses.Add(clickTeamEditSlotOne);
        TutorialProcesses.Add(ChooseAdamToSlot1);
        TutorialProcesses.Add(returnOne);
        TutorialProcesses.Add(confirmQuest1);
        TutorialProcesses.Add(waitForStage1Loaded);
    }

    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.LocalUpdate();
            if (currentProcess.CanEnterOtherProcess()) // && currentProcess.nextProcessStep != MainSceneStep.None
            {
                MoveToNext();
            }
        }
    }

    public void StartToMove()
    {
        ChangeProcess(TutorialProcesses[0]);
    }

    public void MoveToNext()
    {
        // 假设当前process在0位置
        if (TutorialProcesses.Count > 1)
        {
            ChangeProcess(TutorialProcesses[1]);
        }
        else
        {
            ChangeProcess(null);
        }
        TutorialProcesses.RemoveAt(0);
    }
    
    void ChangeProcess(TutorialProcess nextProcess)
    {
        if (currentProcess != null)
        {
            currentProcess.ProcessEnd();
            TutorialLog Log = new TutorialLog()
            {
                description = "end"
            };
            TutorialLogger.Logs.Add(Log);
        }

        lastProcess = currentProcess;
        currentProcess = nextProcess;
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
            TutorialLog Log = new TutorialLog()
            {
                description = "start"
            };
            TutorialLogger.Logs.Add(Log);
        }
    }
}
