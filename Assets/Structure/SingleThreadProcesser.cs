using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    List<UniTask> processQueue = new List<UniTask>();

    public async void RunAsQueued(UniTask origin)
    {
        await QueueRun(origin);
    }

    public async void RunAsQueued(UniTask origin, UnityEngine.Events.UnityAction afterToDo)
    {
        await QueueRun(origin);
        afterToDo();
    }

    /// <summary>
    /// 执行一个コルーチン，严格按照前后顺序，前一个任务结束了后再执行下一个
    /// </summary>
    /// <param name="_process"></param>
    public async void RunAsQueued(IEnumerator _process)
    {
        UniTask origin = EnumeratorAsyncExtensions.ToUniTask(_process, this);
        await QueueRun(origin);
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
        await QueueRun(origin);
    }

    public async UniTask RunAsQueued_UniTask(List<IEnumerator> _processes, UnityEngine.Events.UnityAction afterToDo)
    {
        for (int i = 0; i < _processes.Count; i++)
        {
            await RunAsQueued_UniTask(_processes[i]);
        }
        afterToDo();
    }

    public async UniTask QueueRun(UniTask origin)
    {
        UniTask whole = AsQueue(origin);
        processQueue.Add(origin);
        await whole;
        processQueue.Remove(origin);
    }

    private async UniTask AsQueue(UniTask origin)
    {
        await WaitForTurn(origin);
        await origin;
    }

    private async UniTask WaitForTurn(UniTask origin)
    {
        await UniTask.WaitUntil(() => processQueue.IndexOf(origin) == 0);
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
