using System.Collections.Concurrent;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class SingleThreadProcessor
{
    private readonly ConcurrentQueue<UniTask> processQueue = new ConcurrentQueue<UniTask>();
    public int TaskRunningCount => processQueue.Count;
    
    public async UniTask RunAsQueued(UniTask origin)
    {
        await WaitForTurn(origin);
    }

    public async void RunAsQueued(UniTask origin, UnityAction afterToDo)
    {
        await WaitForTurn(origin);
        afterToDo();
    }
    
    private async UniTask WaitForTurn(UniTask origin)
    {
        await UniTask.WaitUntil(()=> processQueue.Count == 0);
        processQueue.Enqueue(origin);
        await UniTask.WaitUntil(() =>
        {
            var tempQueue = new List<UniTask>(processQueue);
            return tempQueue.Contains(origin) && tempQueue.IndexOf(origin) == 0;
        });
        processQueue.TryDequeue(out _);
    }

    /// <summary>
    /// 不考虑与其他任务执行顺序的执行一个UniTask
    /// </summary>
    /// <param name="_process"></param>
    public void RunFreely(UniTask _process)
    {
        _process.Forget();
    }
}
