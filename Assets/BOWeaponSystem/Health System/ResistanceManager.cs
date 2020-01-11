using UniRx;
using UnityEngine;
using System.Collections.Generic;

public class ResistanceManager : MonoBehaviour
{
    public ReactiveProperty<int> Resistance { get; set; } = new ReactiveProperty<int>(0);
    public Data_Center data_Center;
    
    int temp;
    readonly List<SingleAssignmentDisposable> disposabletasks = new List<SingleAssignmentDisposable>();
    
    public void ResistanceUp(AnimationEvent R)
    {
        Resistance.Value += R.intParameter;
        if (Resistance.Value > 0)
        {
            if (data_Center._ShaderManager != null)
                data_Center._ShaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), (float)Resistance.Value / 5, 0.05f);
        }
        else
        {
            if (data_Center._ShaderManager != null)
                data_Center._ShaderManager.RimEffectsClear();
        }
                      
        switch (R.stringParameter)
        {
            case "resistup":
                UnityEngine.Events.UnityAction eventStart = () =>
                {
                    Resistance.Value += 50;
                    data_Center._SkillCancelFlag.turn_on_flag();
                    EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", "defaultmagic", data_Center.geometryCenter.position, data_Center.geometryCenter.rotation, data_Center.geometryCenter);
                };
                UnityEngine.Events.UnityAction eventEnd = () =>
                {
                    Resistance.Value = 0;
                };
                CustomCoroutine eventCoroutine = new CustomCoroutine(eventStart, 0.5f, eventEnd);
                temp = Resistance.Value;
                var disposable = new SingleAssignmentDisposable();
                disposabletasks.Add(disposable);
                disposable.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Resistance.Value < temp)
                    {
                        data_Center.buffsRunner.RunSubCoroutineOfState(eventCoroutine);
                        disposable.Dispose();
                    }
                });
                break;
        }
    }
    
    public void ResistanceClear()
    {
        if (data_Center._ShaderManager != null)
            data_Center._ShaderManager.RimEffectsClear();
        if (disposabletasks.Count>0)
        {
            foreach (SingleAssignmentDisposable _d in disposabletasks)
            {
                if (!_d.IsDisposed)
                {
                    _d.Dispose();
                }
            }
            disposabletasks.Clear();
        }
        Resistance.Value = 0;
    }
}
