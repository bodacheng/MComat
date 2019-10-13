using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsRunner : MonoBehaviour
{
    #region 自定义携程
    private List<customCoroutine> mysubmissions = new List<customCoroutine>();
    private List<customCoroutine> endedcustomCoroutines = new List<customCoroutine>();
    #endregion

    public void runSubCoroutineOfState(customCoroutine _Coroutine)
    {
        _Coroutine.customCoroutineTrigger();
        mysubmissions.Add(_Coroutine);
    }

    public void endSubCoroutineOfState(customCoroutine _Coroutine)
    {
        if (_Coroutine.ifProcessing())
            _Coroutine.endCustomCoroutine();
        if (mysubmissions.Contains(_Coroutine))
        {
            mysubmissions.Remove(_Coroutine);
        }
    }
    
    public void endAllCoroutines()
    {
        foreach (customCoroutine customCoroutine in mysubmissions)
        {
            customCoroutine.endCustomCoroutine();
        }
        mysubmissions.Clear();
    }
    
    // Update is called once per frame
    public void BuffsRunnerFixedUpdate()
    {
        if (mysubmissions.Count > 0)
        {
            endedcustomCoroutines.Clear();
            foreach (customCoroutine customCoroutine in mysubmissions)
            {
                customCoroutine.customCoroutineProcess();
                if (!customCoroutine.ifProcessing())
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

public class customCoroutine
{
    bool processing = false;
    UnityEngine.Events.UnityAction startaction;
    UnityEngine.Events.UnityAction endaction;
    float processtime,timecounter;

    public customCoroutine(UnityEngine.Events.UnityAction startaction, 
                           float processtime, 
                           UnityEngine.Events.UnityAction endaction)
    {
        this.startaction = startaction;
        this.processtime = processtime;
        this.endaction = endaction;
        processing = false;
        timecounter = 0;
    }

    public void customCoroutineTrigger()
    {
        processing = true;
        timecounter = 0;
        startaction.Invoke();
    }

    public void endCustomCoroutine()
    {
        endaction.Invoke();
        processing = false;
    }

    public void customCoroutineProcess()
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
    public bool ifProcessing()
    {
        return processing;
    }
}