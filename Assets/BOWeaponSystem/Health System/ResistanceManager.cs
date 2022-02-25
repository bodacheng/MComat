using UniRx;
using UnityEngine;
using System.Collections.Generic;

public class ResistanceManager : MonoBehaviour
{
    public ReactiveProperty<int> Resistance { get; set; } = new ReactiveProperty<int>(0);
    public Data_Center data_Center;
    
    int temp;
    readonly List<SingleAssignmentDisposable> disposableTasks = new List<SingleAssignmentDisposable>();

    Color ResistColor;
    
    SingleAssignmentDisposable ResistColorChange;
    
    void Awake()
    {
        ColorUtility.TryParseHtmlString("70D1FF", out ResistColor);
        OpenResistRender();
    }
    
    public void OpenResistRender()
    {
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
        ).AddTo(this.gameObject);
    }
    
    Color speedBuff = new Color(0.2f, 0.2f, 1f);
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
                    EffectsManager.GenerateEffect("break_free", "defaultmagic", data_Center.geometryCenter.position, data_Center.geometryCenter.rotation, data_Center.geometryCenter);
                };
                UnityEngine.Events.UnityAction eventEnd = () =>
                {
                    Resistance.Value = 0;
                };
                CustomCoroutine eventCoroutine = new CustomCoroutine(eventStart, 0.8f, 
                () =>
                {
                    return data_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit;
                }, eventEnd);
                temp = Resistance.Value;
                var disposable = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable);
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
            case "speedup":
                UnityEngine.Events.UnityAction eventStart3 = () =>
                {
                    data_Center._SkillCancelFlag.turn_on_flag();
                    Resistance.Value += 1;
                    data_Center._ShaderManager.FlatColor(0.5f, speedBuff);
                    data_Center.Animation_Manger.Speed = 2f;
                    EffectsManager.GenerateEffect("speedupbuff", "defaultmagic", data_Center.WholeT.position, data_Center.WholeT.rotation, data_Center.WholeT);
                };
                UnityEngine.Events.UnityAction eventEnd3 = () =>
                {
                    data_Center.Animation_Manger.Speed = 1f;
                    Resistance.Value = 0;
                    data_Center._ShaderManager.FlatColor(0f, Color.clear);
                };
                
                CustomCoroutine eventCoroutine3 = new CustomCoroutine(
                    eventStart3, 0.8f,
                     () =>
                     {
                         return data_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit;
                     }, eventEnd3);
                temp = Resistance.Value;
                var disposable3 = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable3);
                disposable3.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Resistance.Value < temp)
                    {
                        data_Center.buffsRunner.RunSubCoroutineOfState(eventCoroutine3);
                        disposable3.Dispose();
                    }
                });
                break;
            case "magic_release":
                UnityEngine.Events.UnityAction eventStart2 = () =>
                {
                    data_Center._BO_Ani_E.hiddenMethods.ReleasePreparedMagic_core(transform.position,transform.rotation, null, 1, data_Center._MyBehaviorRunner.GetNowState().StateKey);
                };
                UnityEngine.Events.UnityAction eventEnd2 = () =>
                {
                    data_Center._SkillCancelFlag.turn_on_flag();
                    Resistance.Value = 0;
                };
                var eventCoroutine2 = new CustomCoroutine(eventStart2, 0.2f, eventEnd2);
                temp = Resistance.Value;
                var disposable2 = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable2);
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
        if (disposableTasks.Count > 0)
        {
            foreach (SingleAssignmentDisposable _d in disposableTasks)
            {
                if (!_d.IsDisposed)
                {
                    _d.Dispose();
                }
            }
            disposableTasks.Clear();
        }
        Resistance.Value = 0;
    }
}
