using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneProcessesRunner
{
    public NagareProcess lastProcess;
    public NagareProcess currentProcess;
    IDictionary<SceneStep, NagareProcess> SceneProcessDictionary = new Dictionary<SceneStep, NagareProcess>();
    
    public void AddNewProcess(SceneStep step,NagareProcess _process)
    {
        SceneProcessDictionary.Add(step, _process);
    }
    
    public NagareProcess accessCertainFightSceneProcessObject(SceneStep step)
    {
        return SceneProcessDictionary[step];
    }
    
    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.localUpdate();
            if (currentProcess.canEnterNextProcess() && currentProcess.nextProcessStep != SceneStep.none)
            {
                changeProcess(currentProcess.nextProcessStep);
            }
        }
    }
    
    public void changeProcess(SceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
        
        lastProcess = currentProcess;        
        SceneProcessDictionary.TryGetValue(sceneStep,out currentProcess);
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
