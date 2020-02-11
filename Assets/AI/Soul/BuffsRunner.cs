using System.Collections.Generic;
using UnityEngine;

public class BuffsRunner : MonoBehaviour
{
    #region 自定义携程
    readonly List<CustomCoroutine> mysubmissions = new List<CustomCoroutine>();
    readonly List<CustomCoroutine> endedcustomCoroutines = new List<CustomCoroutine>();
    #endregion

    public void RunSubCoroutineOfState(CustomCoroutine _Coroutine)
    {
        _Coroutine.CustomCoroutineTrigger();
        mysubmissions.Add(_Coroutine);
    }

    public void EndSubCoroutineOfState(CustomCoroutine _Coroutine)
    {
        if (_Coroutine.IfProcessing())
            _Coroutine.EndCustomCoroutine();
        if (mysubmissions.Contains(_Coroutine))
        {
            mysubmissions.Remove(_Coroutine);
        }
    }
    
    public void EndAllCoroutines()
    {
        foreach (CustomCoroutine customCoroutine in mysubmissions)
        {
            customCoroutine.EndCustomCoroutine();
        }
        mysubmissions.Clear();
    }
    
    // Update is called once per frame
    public void BuffsRunnerFixedUpdate()
    {
        if (mysubmissions.Count > 0)
        {
            endedcustomCoroutines.Clear();
            foreach (CustomCoroutine customCoroutine in mysubmissions)
            {
                customCoroutine.CustomCoroutineProcess();
                if (!customCoroutine.IfProcessing())
                {
                    endedcustomCoroutines.Add(customCoroutine);
                }
            }
            for (int i = 0; i < endedcustomCoroutines.Count; i++)
            {
                mysubmissions.Remove(endedcustomCoroutines[i]);
            }
        }
    }
}

public class CustomCoroutine
{
    bool processing;
    UnityEngine.Events.UnityAction startaction;
    UnityEngine.Events.UnityAction endaction;
    readonly float processtime;
    float timecounter;

    public CustomCoroutine(UnityEngine.Events.UnityAction startaction,float processtime,UnityEngine.Events.UnityAction endaction)
    {
        this.startaction = startaction;
        this.processtime = processtime;
        this.endaction = endaction;
        processing = false;
        timecounter = 0;
    }

    public void CustomCoroutineTrigger()
    {
        processing = true;
        timecounter = 0;
        startaction.Invoke();
    }

    public void EndCustomCoroutine()
    {
        endaction.Invoke();
        processing = false;
    }

    public void CustomCoroutineProcess()
    {
        if (processing && timecounter < processtime)
        {
            timecounter += Time.fixedDeltaTime;
            if (timecounter >= processtime)
            {
                processing = false;
                endaction.Invoke();
            }
        }
    }
    public bool IfProcessing()
    {
        return processing;
    }
}