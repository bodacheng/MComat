using System.Collections.Generic;
using UnityEngine;

public class BuffsRunner
{    
    #region 自定义携程
    public readonly List<CustomCoroutine> mysubmissions = new List<CustomCoroutine>();
    readonly List<CustomCoroutine> endedcustomCoroutines = new List<CustomCoroutine>();
    
    public bool Freesing { get; set; } = false;
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

public delegate bool EndConditionDelegate();

public class CustomCoroutine
{
    bool processing;
    
    UnityEngine.Events.UnityAction startaction;
    UnityEngine.Events.UnityAction endaction;
    readonly EndConditionDelegate endCondition;
    readonly float processtime;
    float timecounter;
    
    public CustomCoroutine(UnityEngine.Events.UnityAction startaction, float processtime, UnityEngine.Events.UnityAction endaction)
    {
        this.startaction = startaction;
        this.processtime = processtime;
        this.endaction = endaction;
        endCondition = TimeOver;
        processing = false;
        timecounter = 0;
    }
    
    public CustomCoroutine(UnityEngine.Events.UnityAction startaction, float processtime, EndConditionDelegate c, UnityEngine.Events.UnityAction endaction)
    {
        this.startaction = startaction;
        this.processtime = processtime;
        this.endaction = endaction;
        endCondition = TimeOver;
        endCondition = EndConditionCombine(endCondition, c);
        processing = false;
        timecounter = 0;
    }
    
    EndConditionDelegate EndConditionCombine(EndConditionDelegate a, EndConditionDelegate b)
    {
        bool c()
        {
            return a() || b();
        }
        return c;
    }
    
    bool TimeOver()
    {
        return timecounter >= processtime;
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
        if (processing && !endCondition())
        {
            timecounter += Time.fixedDeltaTime;
        }
        if (endCondition())
        {
            processing = false;
            endaction.Invoke();
        }
    }
    
    public bool IfProcessing()
    {
        return processing;
    }
}