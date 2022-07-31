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
        
        public MSceneProcess lastProcess;
        public MSceneProcess currentProcess;
        readonly IDictionary<MainSceneStep, MSceneProcess> Dic = new Dictionary<MainSceneStep, MSceneProcess>();

        public MSceneProcess GetProcess(MainSceneStep step)
        {
            return Dic[step];
        }

        public void Clear()
        {
            lastProcess = null;
            currentProcess = null;
            Dic.Clear();
        }

        public void Add(MainSceneStep step, MSceneProcess _process)
        {
            DicAdd<MainSceneStep, MSceneProcess>.Add(Dic, step, _process);
        }

        public void ProcessNagare()
        {
            if (currentProcess != null)
            {
                currentProcess.LocalUpdate();
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
                if (!currentProcess.CanEnterOtherProcess())
                {
                    return;
                }
                
                currentProcess.ProcessEnd();
                var Log_pre = new MainSceneLog()
                {
                    step = currentProcess.Step,
                    description = "end"
                };
                MainSceneLogger.Logs.Add(Log_pre);
            }
            
            lastProcess = currentProcess;
            Dic.TryGetValue(sceneStep, out currentProcess);
            if (currentProcess != null)
            {
                if (t != null)
                    currentProcess.ProcessEnter(t);
                else
                    currentProcess.ProcessEnter();
                var Log_new = new MainSceneLog()
                {
                    step = currentProcess.Step,
                    description = "start"
                };
                MainSceneLogger.Logs.Add(Log_new);
            }
            else
            {
                Debug.Log("empty state key:" + sceneStep);
            }
        }

        // 清空返回菜单，并进入step。将返回按钮设置为返回到FrontPage画面
        // 暂时没用。可能用来处理一些临时弹出的画面
        public void GrandNewChangeProcess(MainSceneStep sceneStep)
        {
            if (currentProcess != null)
                currentProcess.ProcessEnd();
            ReturnLayer.ReturnMissionList.Clear();
            var returnToStep = MainSceneStep.FrontPage;
            void returnTOCurrent()
            {
                PreScene.target.trySwitchToStep(returnToStep, false);
            }
            ReturnLayer.PUSH(returnTOCurrent);
            Dic.TryGetValue(sceneStep, out currentProcess);
            if (currentProcess != null)
            {
                currentProcess.ProcessEnter();
            }
        }
    }
}