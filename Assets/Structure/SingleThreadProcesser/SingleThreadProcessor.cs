using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SingleThreadProcessor
{
    List<UniTask> processQueue;
    
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
        if (processQueue == null)
            processQueue = new List<UniTask>();
        processQueue.Add(origin);
        await UniTask.WaitUntil(() => processQueue.IndexOf(origin) == 0).ContinueWith(
            async () =>
            {
                await origin.ContinueWith(() =>
                {
                    Debug.Log(processQueue.IndexOf(origin) + ":"+ processQueue.Count);
                    processQueue.Remove(origin);
                });
            });
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
