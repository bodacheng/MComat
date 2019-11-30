using System.Collections;
using UnityEngine;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser target;
    IEnumerator MenuProcess;
    bool processEnded;
    float processTime;

    void Start()
    {
        target = this;
    }

    void SetProcessStartEnd(bool a)
    {
        processEnded = a;
    }
    public void TriggerMainProcess(IEnumerator _process)
    {
        StartCoroutine(this.MainProcess(_process));
    }
    IEnumerator GiveProcessStartEndFlag(IEnumerator _process)
    {
        SetProcessStartEnd(false);
        yield return _process;
        SetProcessStartEnd(true);
    }
    IEnumerator MainProcess(IEnumerator _process)//这个函数是供外界调用的。
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
            }
        }
        processTime = 0;
        MenuProcess = GiveProcessStartEndFlag(_process);
        yield return MenuProcess;
    }
}
