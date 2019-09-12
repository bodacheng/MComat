using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleThreadProcesser : MonoBehaviour
{
        // 主进程
    private IEnumerator MenuProcess;
    private bool processEnded = false;
    private float processTime = 0;
    private void setProcessStartEnd(bool a)
    {
        processEnded = a;
    }
    public void triggerMainProcess(IEnumerator _process)
    {
        StartCoroutine(this.MainProcess(_process));
    }
    private IEnumerator giveProcessStartEndFlag(IEnumerator _process)
    {
        setProcessStartEnd(false);
        yield return _process;
        setProcessStartEnd(true);
    }
    private IEnumerator MainProcess(IEnumerator _process)//这个函数是供外界调用的。
    {
        if (MenuProcess != null)
        {
            while (!processEnded)
            {
                processTime += 0.01f;
                if (processTime > 5f)
                {
                    Debug.Log("进程超时.");
                    StopCoroutine(MenuProcess);
                    break;
                }
                yield return null;
            };
        }
        processTime = 0;
        MenuProcess = giveProcessStartEndFlag(_process);
        yield return MenuProcess;
    }
}
