using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SingleThreadProcesser : MonoBehaviour
{
    public static SingleThreadProcesser backup;
    
    readonly List<Task> Tasks = new List<Task>();
    
    class Task
    {
        public bool started = false;
        public IEnumerator process;
        public IEnumerator Processing(List<Task> Tasks)
        {
            yield return process;
            Tasks.Remove(Tasks[0]);
        }
    }
    
    void Update()
    {
        if (Tasks.Count > 0)
        {
            if (!Tasks[0].started)
            {
                Tasks[0].started = true;
                StartCoroutine(Tasks[0].Processing(Tasks));
            }
        }
    }
    
    public void Run(IEnumerator _process)
    {
        Tasks.Add(new Task{ process = _process });
    }
}
