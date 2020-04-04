using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SingleThreadProcesser : MonoBehaviour
{
    readonly IEnumerator MenuProcess;
    readonly List<Task> Tasks = new List<Task>();

    class Task
    {
        public int phase = 0;
        public IEnumerator process;
        public int test = 0;
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
                    Tasks[0].test += 1;
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
}
