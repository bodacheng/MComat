using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    public void Run(IEnumerator _process)
    {
        UniTask t = EnumeratorAsyncExtensions.ToUniTask(_process, this);
        t.Forget();
    }

    public void Run(UniTask _process)
    {
        _process.Forget();
    }
}
