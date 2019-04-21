using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Playables;
using System;

public partial class NetFightScene : MonoBehaviour
{
    NagareProcess currentProcess;
    IDictionary<SceneStep, NagareProcess> SceneProcessDictionary;
    
    public void ProcessNagare()
    {
        if (currentProcess != null)
        {
            currentProcess.localUpdate();
            if (currentProcess.canEnterNextProcess())
            {
                changeProcess(currentProcess.nextProcessStep);
            }
        }
    }
    
    public void changeProcess(SceneStep sceneStep)
    {
        if (currentProcess != null)
            currentProcess.ProcessEnd();
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