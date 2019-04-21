using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NagareProcess
{
    public NetFightScene _NetFightScene;
    public SceneStep thisProcessStep;
    public SceneStep nextProcessStep;
            
    public virtual void ProcessEnter()
    {
    }
    
    public virtual void ProcessEnd()
    {
    }
    
    public virtual bool canEnterNextProcess()
    {
        return false;
    }
    
    public virtual void localUpdate()
    {
    }
}

public enum SceneStep : int
{
    Preparing = 1,
    StoryBeforeFight = 6,
    CountDown = 4,
    Fighting = 2,
    FightOver = 3,
    FightSummary = 5
}