using UniRx;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ResistanceManager : MonoBehaviour
{
    public Data_Center data_Center;
    
    int temp;
    readonly List<SingleAssignmentDisposable> disposableTasks = new List<SingleAssignmentDisposable>();
    Color resistColor;
    SingleAssignmentDisposable ResistColorChange;
    
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#0046BC00", out resistColor);
        OpenResistRender();
    }
    
    void OpenResistRender()
    {
        data_Center.FightDataRef.Resistance.Subscribe(
            x => 
            {
                if (x > 0)
                {
                    if (data_Center._ShaderManager != null)
                    {
                        data_Center._ShaderManager.RimEffectsUp(resistColor, 0.2f);
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

    readonly Color speedBuff = new Color(0.2f, 0.2f, 1f);
    public void ResistanceUp(AnimationEvent R)
    {
        data_Center.FightDataRef.Resistance.Value += R.intParameter;                      
        switch (R.stringParameter)
        {
            case "resistup":
                UnityEngine.Events.UnityAction eventStart = () =>
                {
                    data_Center.FightDataRef.Resistance.Value += 1;
                    data_Center._SkillCancelFlag.turn_on_flag();
                    EffectsManager.GenerateEffect(CommonSetting.BreakFreeEffectCode, 
                        FightGlobalSetting.EffectPathDefine(), data_Center.geometryCenter.position, data_Center.geometryCenter.rotation, data_Center.geometryCenter).Forget();
                };
                UnityEngine.Events.UnityAction eventEnd = () =>
                {
                    data_Center.FightDataRef.Resistance.Value -= 1;
                };
                CustomCoroutine eventCoroutine = new CustomCoroutine(eventStart, 0.8f, 
                () =>
                {
                    return data_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit;
                }, eventEnd);
                temp = data_Center.FightDataRef.Resistance.Value;
                var disposable = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable);
                disposable.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (data_Center.FightDataRef.Resistance.Value < temp)
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
                    data_Center.FightDataRef.Resistance.Value += 1;
                    data_Center._ShaderManager.RimEffectsUp(speedBuff, 0.5f);
                    data_Center.AnimationManger.AddSpeedBuff("speedup", 2);
                    EffectsManager.GenerateEffect("speedupbuff", FightGlobalSetting.EffectPathDefine(), data_Center.WholeT.position, data_Center.WholeT.rotation, data_Center.WholeT).Forget();
                };
                UnityEngine.Events.UnityAction eventEnd3 = () =>
                {
                    data_Center.AnimationManger.RemoveSpeedBuff("speedup");
                    data_Center.FightDataRef.Resistance.Value -= 1;
                    data_Center._ShaderManager.RimEffectsClear(0.2f);
                };
                
                CustomCoroutine eventCoroutine3 = new CustomCoroutine(
                    eventStart3, 0.8f,
                     () => data_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit, eventEnd3);
                temp = data_Center.FightDataRef.Resistance.Value;
                var disposable3 = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable3);
                disposable3.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (data_Center.FightDataRef.Resistance.Value < temp)
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
                    data_Center.FightDataRef.Resistance.Value = 0;
                };
                var eventCoroutine2 = new CustomCoroutine(eventStart2, 0.2f, eventEnd2);
                temp = data_Center.FightDataRef.Resistance.Value;
                var disposable2 = new SingleAssignmentDisposable();
                disposableTasks.Add(disposable2);
                disposable2.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (data_Center.FightDataRef.Resistance.Value < temp)
                    {
                        data_Center.buffsRunner.RunSubCoroutineOfState(eventCoroutine2);
                        disposable2.Dispose();
                    }
                });
                break;
        }
    }

    public void DreamComboStart()
    {
        UnityEngine.Events.UnityAction eventStart = () =>
        {
            data_Center.FightDataRef.Resistance.Value += 2;
        };
        UnityEngine.Events.UnityAction eventEnd = () =>
        {
            data_Center.FightDataRef.Resistance.Value -= 2;
        };
        CustomCoroutine eventCoroutine = new CustomCoroutine(eventStart, FightGlobalSetting._dreamComboResistTime,
            () => data_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit, eventEnd);
        data_Center.buffsRunner.RunSubCoroutineOfState(eventCoroutine);
    }
    
    public void ResistanceClear()
    {
        if (disposableTasks.Count > 0)
        {
            foreach (var _d in disposableTasks)
            {
                if (!_d.IsDisposed)
                {
                    _d.Dispose();
                }
            }
            disposableTasks.Clear();
        }
        data_Center.FightDataRef.Resistance.Value = 0;
    }
}
