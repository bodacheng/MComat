using System.Collections.Generic;
using UnityEngine;
using mainMenu;

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
    readonly IDictionary<TutorialStep, TutorialProcess> TutorialProcessesDic = new Dictionary<TutorialStep, TutorialProcess>();

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

        TutorialRunner.Main.AddTutorialProcess(TutorialStep.GoToMemberDetail, goToMemberDetail);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.OpenSkillEdit, openSkillEdit);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A1Selected, skillEditA1Try);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A2Selected, skillEditA2Try);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A3Selected, skillEditA3Try);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A1Filled, skillEditTry_A1Filled);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A2Filled, skillEditTry_A2Filled);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.SkillEditTry_A3Filled, skillEditTry_A3Filled);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.ALineConfirm, aLineConfirm);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.TutorialReturn, returnOne);
        TutorialRunner.Main.AddTutorialProcess(TutorialStep.GoToStages, goToStages);

        TutorialProcesses.Add(goToMemberDetail);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(skillEditA1Try);
        TutorialProcesses.Add(skillEditA2Try);
        TutorialProcesses.Add(skillEditA3Try);
        TutorialProcesses.Add(skillEditTry_A1Filled);
        TutorialProcesses.Add(skillEditTry_A2Filled);
        TutorialProcesses.Add(skillEditTry_A3Filled);
        TutorialProcesses.Add(aLineConfirm);
        TutorialProcesses.Add(returnOne);
        TutorialProcesses.Add(returnOne);
        TutorialProcesses.Add(goToStages);
    }

    public void AddTutorialProcess(TutorialStep step, TutorialProcess _process)
    {
        TutorialProcessesDic.Add(step, _process);
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
        ChangeProcess(TutorialProcesses[0].Step);
    }

    public void MoveToNext()
    {
        // 假设当前process在0位置
        if (TutorialProcesses.Count > 1)
        {
            ChangeProcess(TutorialProcesses[1].Step);
        }
        else
        {
            ChangeProcess(TutorialStep.None);
        }
        TutorialProcesses.RemoveAt(0);
    }

    void ChangeProcess(TutorialStep sceneStep)
    {
        if (currentProcess != null)
        {
            currentProcess.ProcessEnd();
            TutorialLog Log = new TutorialLog()
            {
                step = currentProcess.Step,
                description = "end"
            };
            TutorialLogger.Logs.Add(Log);
        }

        lastProcess = currentProcess;
        TutorialProcessesDic.TryGetValue(sceneStep, out currentProcess);
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
            TutorialLog Log = new TutorialLog()
            {
                step = currentProcess.Step,
                description = "start"
            };
            TutorialLogger.Logs.Add(Log);
        }
        else
        {
            if (TutorialProcessesDic.ContainsKey(sceneStep))
            {
                Debug.Log(sceneStep + "倒是在字典里");
                Debug.Log(currentProcess);
            }
            Debug.Log("这个场景进程没定义：" + sceneStep);
        }
    }
}
