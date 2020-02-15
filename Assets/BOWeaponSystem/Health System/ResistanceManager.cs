using UniRx;
using UnityEngine;
using System.Collections.Generic;

public class ResistanceManager : MonoBehaviour
{
    public ReactiveProperty<int> Resistance { get; set; } = new ReactiveProperty<int>(0);
    public Data_Center data_Center;
    
    int temp;
    readonly List<SingleAssignmentDisposable> disposabletasks = new List<SingleAssignmentDisposable>();

    Color ResistColor;
    
    void Awake()
    {
        ColorUtility.TryParseHtmlString("70D1FF",out ResistColor);
        Resistance.Subscribe(
            x => 
            {
                if (x > 0)
                {
                    if (data_Center._ShaderManager != null)
                    {
                        data_Center._ShaderManager.RimEffectsUp(ResistColor, 1 , 0.2f);
                    }
                }
                else
                {
                    if (data_Center._ShaderManager != null)
                    {
                        data_Center._ShaderManager.RimEffectsClear(0.3f);
                    }
                }
            }
        );
    }

    public void ResistanceUp(AnimationEvent R)
    {
        Resistance.Value += R.intParameter;                      
        switch (R.stringParameter)
        {
            case "resistup":
                UnityEngine.Events.UnityAction eventStart = () =>
                {
                    Resistance.Value += 1;
                    data_Center._SkillCancelFlag.turn_on_flag();
                    EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", "defaultmagic", data_Center.geometryCenter.position, data_Center.geometryCenter.rotation, data_Center.geometryCenter);
                };
                UnityEngine.Events.UnityAction eventEnd = () =>
                {
                    Resistance.Value = 0;
                };
                CustomCoroutine eventCoroutine = new CustomCoroutine(eventStart, 0.2f, eventEnd);
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
            case "magic_release":
                UnityEngine.Events.UnityAction eventStart2 = () =>
                {
                    data_Center._BO_Ani_E.ReleasePreparedMagicToAir("T");
                };
                UnityEngine.Events.UnityAction eventEnd2 = () =>
                {
                    data_Center._SkillCancelFlag.turn_on_flag();
                    Resistance.Value = 0;
                };
                CustomCoroutine eventCoroutine2 = new CustomCoroutine(eventStart2, 0.2f, eventEnd2);
                temp = Resistance.Value;
                var disposable2 = new SingleAssignmentDisposable();
                disposabletasks.Add(disposable2);
                disposable2.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Resistance.Value < temp)
                    {
                        data_Center.buffsRunner.RunSubCoroutineOfState(eventCoroutine2);
                        disposable2.Dispose();
                    }
                });
                break;
        }
    }
    
    public void ResistanceClear()
    {
        if (disposabletasks.Count > 0)
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
