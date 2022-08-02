using System.Collections.Generic;
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
    readonly List<TutorialProcess> TutorialProcesses = new ();
    
    public void GenerateTutorial()
    {
        var goToMemberDetail = new GoToMemberDetail();
        var openSkillEdit = new OpenSkillEdit();
        var skillEditA1Try = new SkillEditA1Try();
        var skillEditA2Try = new SkillEditA2Try();
        var skillEditA3Try = new SkillEditA3Try();
        var skillEditTry_A1Filled = new SkillEditTry_A1Filled();
        var skillEditTry_A2Filled = new SkillEditTry_A2Filled();
        var skillEditTry_A3Filled = new SkillEditTry_A3Filled();
        var aLineConfirm = new ALineConfirm();
        var returnOne = new ReturnOne();
        var goToStages = new GoToStages();
        var goToStageOne = new GoToStageOne();
        var goToTeamEdit = new GoToTeamEdit();
        var clickTeamEditSlotOne = new ClickTeamEditSlotOne();
        var ChooseAdamToSlot1 = new ChooseAdamToSlot1();
        var confirmQuest1 = new ConfirmQuest1();
        
        bool StartedFighting()
        {
            return FSceneProcessesRunner.Main.currentProcess.Step == SceneStep.Fighting; 
        }
        var waitForStage1Loaded = new WaitProcess(StartedFighting);
        
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
