using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceManager : MonoBehaviour
{
    private int resistance = 0;
    public ShaderManager _ShaderManager;
    public int Resistance
    {
        get
        {
            return resistance;
        }
        set
        {
            resistance = Mathf.Clamp(value, 0, 10);//就是说角色最大ex槽最大100呗。
        }
    }
    
    public void resistanceUp(AnimationEvent R)
    {
        //defaultPools.Instance.GenerateEffect("resistanceUp", null, _healthCenterTransform.position, _healthCenterTransform.rotation, null);
        Resistance += R.intParameter;
        if (resistance > 0)
        {
            if (_ShaderManager != null)
                _ShaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), (float)Resistance / 5, 0.05f);
        }
        else
        {
            if (_ShaderManager != null)
                _ShaderManager.RimEffectsClear();
        }
        nextcountereventneeddamagecount = R.intParameter - 1;
        nextcounterevent = R.stringParameter;
    }
    
    public void resistanceClear()
    {
        Resistance = 0;
        if (_ShaderManager != null)
            _ShaderManager.RimEffectsClear();
    }
    
        //那么这个函数现在有一个作用在于为反击触发事件做准备。
    private string nextcounterevent;
    private int nextcountereventneeddamagecount;
    public string getNextCounterEventName()
    {
        return nextcounterevent;
    }
    public void setNextCounterEventName(string name)
    {
        nextcounterevent = name;
    }
    public int getNextCounterEventDamageTriggerAmount()
    {
        return nextcountereventneeddamagecount;
    }
}
