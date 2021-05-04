using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class ProcessesRunner
    {
        static ProcessesRunner instance_main;
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
        
        public MainSceneProcess lastProcess;
        public MainSceneProcess currentProcess;
        readonly IDictionary<MainSceneStep, MainSceneProcess> Dic = new Dictionary<MainSceneStep, MainSceneProcess>();

        public MainSceneProcess GetProcess(MainSceneStep step)
        {
            return Dic[step];
        }

        public void Clear()
        {
            lastProcess = null;
            currentProcess = null;
            Dic.Clear();
        }

        public void Add(MainSceneStep step, MainSceneProcess _process)
        {
            DicAdd<MainSceneStep, MainSceneProcess>.Add(Dic, step, _process);
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

        public void ChangeProcess(MainSceneStep sceneStep)
        {
            ChangeProcess<Any>(sceneStep, null);
        }
        
        public void ChangeProcess<T>(MainSceneStep sceneStep, T t)
        {
            if (currentProcess != null)
            {
                currentProcess.ProcessEnd();
                MainSceneLog Log = new MainSceneLog()
                {
                    step = currentProcess.Step,
                    description = "end"
                };
                MainSceneLogger.Logs.Add(Log);
            }

            lastProcess = currentProcess;
            Dic.TryGetValue(sceneStep, out currentProcess);
            if (currentProcess != null)
            {
                if (t != null)
                    currentProcess.ProcessEnter(t);
                else
                    currentProcess.ProcessEnter();
                MainSceneLog Log = new MainSceneLog()
                {
                    step = currentProcess.Step,
                    description = "start"
                };
                MainSceneLogger.Logs.Add(Log);
            }
            else
            {
                if (Dic.ContainsKey(sceneStep))
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
            Dic.TryGetValue(sceneStep, out currentProcess);
            if (currentProcess != null)
            {
                currentProcess.ProcessEnter();
            }
        }
    }
}