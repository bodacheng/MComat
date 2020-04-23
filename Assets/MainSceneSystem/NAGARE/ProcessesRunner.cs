using System.Collections.Generic;
using UnityEngine;
using mainMenu;

// 这个进程处理器写成单例模式，并不代表除了那一个单例外就没发生成其他instance。可能在一些情况下单独建立它的instance比如在一个进程的内部。
public class ProcessesRunner
{
    static ProcessesRunner instance;
    public static ProcessesRunner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProcessesRunner();
            }
            return instance;
        }
    }
    
    public MainSceneProcess lastProcess;
    public MainSceneProcess currentProcess;
    readonly IDictionary<MainSceneStep, MainSceneProcess> SceneProcessDictionary = new Dictionary<MainSceneStep, MainSceneProcess>();

    public void Clear()
    {
        lastProcess = null;
        currentProcess = null;
        SceneProcessDictionary.Clear();
    }

    public void AddNewProcess(MainSceneStep step, MainSceneProcess _process)
    {
        SceneProcessDictionary.Add(step, _process);
    }
    
    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.LocalUpdate();
            if (currentProcess.CanEnterOtherProcess() && currentProcess.nextProcessStep != MainSceneStep.None)
            {
                ChangeProcess(currentProcess.nextProcessStep);
            }
        }
    }

    public void ChangeProcess(MainSceneStep sceneStep)
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
    
    public MainSceneProcess AccessCertainMainSceneProcessObject(MainSceneStep step)
    {
        return SceneProcessDictionary[step];
    }
}
