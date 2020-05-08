using System.Collections.Generic;
using UnityEngine;
using mainMenu;

// 这个进程处理器写成单例模式，并不代表除了那一个单例外就没发生成其他instance。可能在一些情况下单独建立它的instance比如在一个进程的内部。
public class ProcessesRunner
{
    static ProcessesRunner instance_main;
    static ProcessesRunner instance_sub;
    
    public static ProcessesRunner Main
    {
        get
        {
            if (instance_main == null)
            {
                instance_main = new ProcessesRunner();
            }
            return instance_main;
        }
    }
    
    public static ProcessesRunner Tutorial
    {
        get
        {
            if (instance_sub == null)
            {
                instance_sub = new ProcessesRunner();
            }
            return instance_sub;
        }
    }
    
    public MainSceneProcess lastProcess;
    public MainSceneProcess currentProcess;
    readonly IDictionary<MainSceneStep, MainSceneProcess> SceneProcessDictionary = new Dictionary<MainSceneStep, MainSceneProcess>();
    
    public MainSceneProcess GetProcess(MainSceneStep step)
    {
        return SceneProcessDictionary[step];
    }
    
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
    
    // 清空返回菜单，并进入step。将返回按钮设置为返回到FrontPage画面
    // 暂时没用。可能用来处理一些临时弹出的画面
    public void GrandNewChangeProcess(MainSceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
        ReturnButtonManager.ReturnMissionList.Clear();
        MainSceneStep returnToStep = MainSceneStep.FrontPage;
        void returnTOCurrent()
        {
            PreScene.target.trySwitchToStep(returnToStep, false);
        }
        ReturnButtonManager.PUSH(returnTOCurrent);
        SceneProcessDictionary.TryGetValue(sceneStep, out currentProcess);
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
        }
    }
}
