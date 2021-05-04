using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using System;
using UnityEngine;

public class MissionWatcher
{
    private CompositeDisposable Disposable { get; } = new CompositeDisposable();

    List<ReactiveProperty<int>> MissionFlags;

    ReactiveProperty<int> MissionCompletedCount = new ReactiveProperty<int>();

    void DisposeAll()
    {
        Disposable.Dispose();
        MissionFlags.Clear();
    }

    async void RunUniTask(UniTask task)
    {
        await task;
    }

    public MissionWatcher(List<ReactiveProperty<int>> missionFlags, UniTask success, UniTask fail)
    {
        MissionFlags = missionFlags;
        MissionCompletedCount.Value = 0;
        for (int i = 0; i < MissionFlags.Count; i++)
        {
            MissionFlags[i].Subscribe(x =>
            {
                switch (x)
                {
                    case -1:
                        RunUniTask(fail);
                        DisposeAll();
                        break;
                    case 1:
                        MissionCompletedCount.Value += 1;
                        MissionFlags[i].Dispose();
                        break;
                }
            }).AddTo(Disposable);
        }

        MissionCompletedCount.Subscribe(x =>
        {
            if (x == MissionFlags.Count)
            {
                Debug.Log("her" + x);
                RunUniTask(success);
                DisposeAll();
            }
        }).AddTo(Disposable);
    }

    public MissionWatcher(List<ReactiveProperty<int>> missionFlags, Action success, Action fail)
    {
        MissionFlags = missionFlags;
        MissionCompletedCount.Value = 0;
        for (int i = 0; i < MissionFlags.Count; i++)
        {
            MissionFlags[i].Subscribe(x =>
            {
                switch (x)
                {
                    case -1:
                        fail.Invoke();
                        DisposeAll();
                        break;
                    case 1:
                        MissionCompletedCount.Value += 1;
                        MissionFlags[i].Dispose();
                        break;
                }
            }).AddTo(Disposable);
        }

        MissionCompletedCount.Subscribe(x =>
        {
            if (x == MissionFlags.Count)
            {
                success.Invoke();
                DisposeAll();
            }
        }).AddTo(Disposable);
    }
}
