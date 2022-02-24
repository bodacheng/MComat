using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public partial class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    List<UniTask> processQueue = new List<UniTask>();

    public async void RunAsQueued(UniTask origin)
    {
        await WaitForTurn(origin);
    }

    public async void RunAsQueued(UniTask origin, UnityAction afterToDo)
    {
        await WaitForTurn(origin);
        afterToDo();
    }

    
    /// <summary>
    /// 执行一个コルーチン，严格按照前后顺序，前一个任务结束了后再执行下一个
    /// 但函数本身可作为一个UniTask被创建，方便观察任务的执行情况。
    /// </summary>
    /// <param name="_process"></param>
    /// <returns></returns>
    public async UniTask RunAsQueued_UniTask(IEnumerator _process)
    {
        UniTask origin = _process.ToUniTask(this);
        await WaitForTurn(origin);
    }

    public async UniTask RunAsQueued_UniTask(List<IEnumerator> _processes, UnityAction afterToDo)
    {
        for (int i = 0; i < _processes.Count; i++)
        {
            await RunAsQueued_UniTask(_processes[i]);
        }
        afterToDo();
    }

    
    private async UniTask WaitForTurn(UniTask origin)
    {
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

    public void RunFreely(IEnumerator _process)
    {
        UniTask task = _process.ToUniTask(this);
        task.Forget();
    }
}
