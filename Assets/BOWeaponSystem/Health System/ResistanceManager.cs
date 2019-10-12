using UniRx;
using UnityEngine;

public class ResistanceManager : MonoBehaviour
{
    public ShaderManager _ShaderManager;
    public ReactiveProperty<int> Resistance { get; set; } = new ReactiveProperty<int>(0);
    
    public void resistanceUp(AnimationEvent R)
    {
        //defaultPools.Instance.GenerateEffect("resistanceUp", null, _healthCenterTransform.position, _healthCenterTransform.rotation, null);
        Resistance.Value += R.intParameter;
        if (Resistance.Value > 0)
        {
            if (_ShaderManager != null)
                _ShaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), (float)Resistance.Value / 5, 0.05f);
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
        Resistance.Value = 0;
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
