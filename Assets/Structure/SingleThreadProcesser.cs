using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;

    readonly IEnumerator MenuProcess;
    readonly List<Task> Tasks = new List<Task>();
    
    class Task
    {
        public int phase = 0;
        public string description;
        public IEnumerator process;
        void SetPhase(int a)
        {
            phase = a;
        }
    
        public IEnumerator GiveProcessStartEndFlag()
        {
            SetPhase(1);
            yield return process;
            SetPhase(2);
        }
    }
    
    void Update()
    {
        if (Tasks.Count > 0)
        {
            switch (Tasks[0].phase)
            {
                case 0:
                    StartCoroutine(Tasks[0].GiveProcessStartEndFlag());
                break;
                case 1:
                break;
                case 2:
                    Tasks.Remove(Tasks[0]);
                break;
            }
        }
    }
    
    public void Run(IEnumerator _process)
    {
        Tasks.Add(new Task{ process = _process });
    }
    
    public void Run(IEnumerator _process, string _description)
    {
        Tasks.Add(new Task { process = _process, description = _description });
    }

    SingleAssignmentDisposable SingleAssignment;
    void AddRender()
    {
        SingleAssignment = new SingleAssignmentDisposable
        {
            Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (Tasks.Count > 0)
                    {
                        
                    }
                }
            )
        };
    }
}
