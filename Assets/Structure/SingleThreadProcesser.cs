using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;
using System;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    List<UniTask> processQueue = new List<UniTask>();

    public async void RunAsQueued(UniTask origin)
    {
        UniTask whole = AsQueue(origin);
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner" + this + "  process count:" + processQueue.Count);
    }

    public async void RunAsQueued(UniTask origin, UnityEngine.Events.UnityAction afterToDo)
    {
        UniTask whole = AsQueue(origin);
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner" + this + "  process count:" + processQueue.Count);
        afterToDo();
    }

    /// <summary>
    /// 执行一个コルーチン，严格按照前后顺序，前一个任务结束了后再执行下一个
    /// </summary>
    /// <param name="_process"></param>
    public async void RunAsQueued(IEnumerator _process)
    {
        UniTask origin = EnumeratorAsyncExtensions.ToUniTask(_process, this);
        UniTask whole = AsQueue(origin);
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner"+ this + "  process count:" + processQueue.Count) ;
    }

    /// <summary>
    /// 执行一个コルーチン，严格按照前后顺序，前一个任务结束了后再执行下一个
    /// 但函数本身可作为一个UniTask被创建，方便观察任务的执行情况。
    /// </summary>
    /// <param name="_process"></param>
    /// <returns></returns>
    public async UniTask RunAsQueued_UniTask(IEnumerator _process)
    {
        UniTask origin = EnumeratorAsyncExtensions.ToUniTask(_process, this);
        UniTask whole = AsQueue(origin);
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner" + this + "  process count:" + processQueue.Count);
    }

    /// <summary>
    /// 同时执行多个コルーチン，转换为UniTask以多线程执行任务。
    /// 往往执行此操作需要观察其执行，因此只有UniTask版本
    /// </summary>
    /// <param name="_processes"></param>
    /// <returns></returns>
    public async UniTask RunAsQueued_UniTask(List<IEnumerator> _processes)
    {
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0; i < _processes.Count; i++)
        {
            UniTask task = EnumeratorAsyncExtensions.ToUniTask(_processes[i], this);
            tasks.Add(task);
        }

        UniTask whole = AsQueue(UniTask.WhenAll(tasks.ToArray()));
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner" + this + "  process count:" + processQueue.Count);
    }

    public async UniTask RunAsQueued_UniTask(List<IEnumerator> _processes, UnityEngine.Events.UnityAction afterToDo)
    {
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0; i < _processes.Count; i++)
        {
            UniTask task = EnumeratorAsyncExtensions.ToUniTask(_processes[i], this);
            tasks.Add(task);
        }

        UniTask whole = AsQueue(UniTask.WhenAll(tasks.ToArray()));
        processQueue.Add(whole);
        await whole;
        processQueue.Remove(whole);
        Debug.Log("runner" + this + "  process count:" + processQueue.Count);
        afterToDo();
    }

    private async UniTask AsQueue(UniTask origin)
    {
        await temp(origin);
        await origin;
    }

    private async UniTask<bool> temp(UniTask origin)
    {
        return (processQueue.IndexOf(origin) == 0);
    }

    /// <summary>
    /// 不考虑与其他任务执行顺序的执行一个UniTask
    /// </summary>
    /// <param name="_process"></param>
    public void RunFreely(UniTask _process)
    {
        _process.Forget();
    }

    public void RunFreely(IEnumerator _process)
    {
        UniTask task = EnumeratorAsyncExtensions.ToUniTask(_process, this);
        task.Forget();
    }
}
