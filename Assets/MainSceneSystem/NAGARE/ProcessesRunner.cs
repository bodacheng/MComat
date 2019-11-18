using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class ProcessesRunner
{
    public MainSceneProcess lastProcess;
    public MainSceneProcess currentProcess;
    IDictionary<MainSceneStep, MainSceneProcess> SceneProcessDictionary = new Dictionary<MainSceneStep, MainSceneProcess>();
    
    public void AddNewProcess(MainSceneStep step,MainSceneProcess _process)
    {
        SceneProcessDictionary.Add(step, _process);
    }
    
    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.LocalUpdate();
            if (currentProcess.CanEnterOtherProcess() && currentProcess.nextProcessStep != MainSceneStep.none)
            {
                changeProcess(currentProcess.nextProcessStep);
            }
        }
    }

    public void changeProcess(MainSceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
        lastProcess = currentProcess;
        SceneProcessDictionary.TryGetValue(sceneStep, out currentProcess);
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
        }
        else
        {
            if (SceneProcessDictionary.ContainsKey(sceneStep))
            {
                Debug.Log(sceneStep + "倒是在字典里");
                Debug.Log(currentProcess);
            }
            Debug.Log("这个场景进程没定义：" + sceneStep);
        }
    }
    
    public MainSceneProcess accessCertainMainSceneProcessObject(MainSceneStep step)
    {
        return SceneProcessDictionary[step];
    }
}
