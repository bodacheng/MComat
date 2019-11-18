using System.Collections.Generic;
using UnityEngine;

public class FightSceneProcessesRunner
{
    public static NagareProcess lastProcess;
    public static NagareProcess currentProcess;
    private static readonly IDictionary<SceneStep, NagareProcess> SceneProcessDictionary = new Dictionary<SceneStep, NagareProcess>();

    public static void Clear()
    {
        SceneProcessDictionary.Clear();
    }

    public void LocalUpdate()
    {
        if (currentProcess != null)
        {
            currentProcess.LocalUpdate();
        }
    }

    public void AddNewProcess(SceneStep step, NagareProcess _process)
    {
        if (!SceneProcessDictionary.ContainsKey(step))
            SceneProcessDictionary.Add(step, _process);
        else {
            SceneProcessDictionary[step] = _process;
        }            
    }
    
    public NagareProcess AccessCertainFightSceneProcessObject(SceneStep step)
    {
        return SceneProcessDictionary[step];
    }
        
    public static void ChangeProcess(SceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
        lastProcess = currentProcess;
        currentProcess = SceneProcessDictionary[sceneStep];
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
            Debug.Log("主场景已经进入了："+sceneStep);
        }
        else{
            if (SceneProcessDictionary.ContainsKey(sceneStep))
            {
                Debug.Log(sceneStep +"倒是在字典里");
                Debug.Log(currentProcess);
            }
            Debug.Log("这个场景进程没定义：" + sceneStep);
        }
    }
}
