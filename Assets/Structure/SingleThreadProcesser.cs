using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    public async void Run(IEnumerator _process)
    {
        await _process;
    }

    public async void Run(UniTask _process)
    {
        _process.Forget();
    }
}
