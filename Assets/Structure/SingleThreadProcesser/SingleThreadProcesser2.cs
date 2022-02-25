using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SingleThreadProcesser : MonoBehaviour
{
    readonly List<IEnumerator> coroutineQueue = new List<IEnumerator>();

    /// <summary>
    /// 执行一个コルーチン，严格按照前后顺序，前一个任务结束了后再执行下一个
    /// </summary>
    /// <param name="_process"></param>
    public void RunAsQueued(IEnumerator origin)
    {
        StartCoroutine(_RunAsQueued(origin));
    }
    
    IEnumerator _RunAsQueued(IEnumerator origin)
    {
        if (coroutineQueue.Contains(origin))
        {
            Debug.Log(":" + coroutineQueue.Count);
            yield break;
        }
        coroutineQueue.Add(origin);
        while (coroutineQueue.IndexOf(origin) > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        yield return coroutineQueue[0];
        coroutineQueue.RemoveAt(0);
    }
}
