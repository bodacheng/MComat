using System.Collections.Generic;
using UnityEngine;

namespace FightScene
{
    public class FSceneProcessesRunner
    {
        static FSceneProcessesRunner instance_main;
        public static FSceneProcessesRunner Main
        {
            get
            {
                if (instance_main == null)
                {
                    instance_main = new FSceneProcessesRunner();
                }
                return instance_main;
            }
        }
        
        public FSceneProcess lastProcess;
        public FSceneProcess currentProcess;
        readonly IDictionary<SceneStep, FSceneProcess> SceneProcessDictionary = new Dictionary<SceneStep, FSceneProcess>();
    
        public void Clear()
        {
            currentProcess = null;
            SceneProcessDictionary.Clear();
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
    
        public void AddNewProcess(SceneStep step, FSceneProcess _process)
        {
            if (!SceneProcessDictionary.ContainsKey(step))
                SceneProcessDictionary.Add(step, _process);
            else
                SceneProcessDictionary[step] = _process;
        }
        
        public FSceneProcess GetProcess(SceneStep step)
        {
            return SceneProcessDictionary[step];
        }
        
        public void ChangeProcess(SceneStep sceneStep)
        {
            if (currentProcess != null)
                currentProcess.ProcessEnd();
            lastProcess = currentProcess;
            currentProcess = SceneProcessDictionary.ContainsKey(sceneStep) ? SceneProcessDictionary[sceneStep] : null;
            if (currentProcess != null)
            {
                currentProcess.ProcessEnter();
                Debug.Log("主场景已经进入了：" + sceneStep);
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
    }
}