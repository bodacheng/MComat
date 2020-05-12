using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class TutorialRunner
{
    static TutorialRunner instance_sub;
    public static TutorialRunner Main
    {
        get
        {
            if (instance_sub == null)
            {
                instance_sub = new TutorialRunner();
            }
            return instance_sub;
        }
    }

    public TutorialProcess lastProcess;
    public TutorialProcess currentProcess;
    readonly IDictionary<TutorialStep, TutorialProcess> TutorialProcessesDic = new Dictionary<TutorialStep, TutorialProcess>();

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
                ChangeProcess(currentProcess.nextProcessStep);
            }
        }
    }

    public void ChangeProcess(TutorialStep sceneStep)
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
