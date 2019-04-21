using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class preparingScene : MonoBehaviour {

    public MainSceneProcess lastProcess;
    public MainSceneProcess currentProcess;
    IDictionary<MainSceneStep, MainSceneProcess> SceneProcessDictionary;
    
    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.localUpdate();
        }
    }
    
    public void changeProcess(MainSceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
        lastProcess = currentProcess;
         SceneProcessDictionary.TryGetValue(sceneStep,out currentProcess);
        if (currentProcess != null)
        {
            currentProcess.ProcessEnter();
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
