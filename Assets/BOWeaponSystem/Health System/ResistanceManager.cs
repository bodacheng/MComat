using UniRx;
using UnityEngine;

public class ResistanceManager : MonoBehaviour
{
    public HiddenMethods hiddenMethods;

    public ShaderManager _ShaderManager;
    public ReactiveProperty<int> Resistance { get; set; } = new ReactiveProperty<int>(0);

    int nextcountereventneeddamagecount;
    string nextcounterevent;

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
    }
    
    public void ResistanceUp(AnimationEvent R)
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
            
    public void ResistanceClear()
    {
        Resistance.Value = 0;
        if (_ShaderManager != null)
            _ShaderManager.RimEffectsClear();
    }
    
    public class HiddenMethods
    {
        readonly ResistanceManager resistanceManager;
        public HiddenMethods(ResistanceManager _ResistanceManager)
        {
            resistanceManager = _ResistanceManager;
        }

        //那么这个函数现在有一个作用在于为反击触发事件做准备。
        public string GetNextCounterEventName()
        {
            return resistanceManager.nextcounterevent;
        }
        public void SetNextCounterEventName(string name)
        {
            resistanceManager.nextcounterevent = name;
        }
        public int GetNextCounterEventDamageTriggerAmount()
        {
            return resistanceManager.nextcountereventneeddamagecount;
        }
    }
}
