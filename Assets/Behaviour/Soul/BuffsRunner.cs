using System.Collections.Generic;
using UnityEngine;

public class BuffsRunner
{
    #region 自定义携程

    public readonly List<CustomCoroutine> mySubMissions = new List<CustomCoroutine>();
    private readonly List<CustomCoroutine> endedCustomCoroutines = new List<CustomCoroutine>();
    
    public bool Freezing { get; set; } = false;
    #endregion
    
    public void RunSubCoroutineOfState(CustomCoroutine _Coroutine)
    {
        _Coroutine.CustomCoroutineTrigger();
        mySubMissions.Add(_Coroutine);
    }

    public void EndSubCoroutineOfState(CustomCoroutine _Coroutine)
    {
        if (_Coroutine.IfProcessing())
            _Coroutine.EndCustomCoroutine();
        if (mySubMissions.Contains(_Coroutine))
        {
            mySubMissions.Remove(_Coroutine);
        }
    }
    
    public void EndAllCoroutines()
    {
        foreach (CustomCoroutine customCoroutine in mySubMissions)
        {
            customCoroutine.EndCustomCoroutine();
        }
        mySubMissions.Clear();
    }
    
    // Update is called once per frame
    public void BuffsRunnerFixedUpdate()
    {
        if (mySubMissions.Count > 0)
        {
            endedCustomCoroutines.Clear();
            foreach (CustomCoroutine customCoroutine in mySubMissions)
            {
                customCoroutine.CustomCoroutineProcess();
                if (!customCoroutine.IfProcessing())
                {
                    endedCustomCoroutines.Add(customCoroutine);
                }
            }
            for (int i = 0; i < endedCustomCoroutines.Count; i++)
            {
                mySubMissions.Remove(endedCustomCoroutines[i]);
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