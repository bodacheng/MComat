using UnityEngine;
using HittingDetection;
using Soul;
using UniRx;
using DG.Tweening;

public partial class Hurt_State : Behavior
{
    void PushToMidStart(V_Damage newValue)
    {
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim(),true,0.05f);
        used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
        Vector3 MidDistanceFromMe = newValue.attacker._Center.WholeT.transform.position + newValue.attacker._Center.WholeT.transform.forward * 10f;
        MidDistanceFromMe.y = 0;
        var disposable6 = new SingleAssignmentDisposable();
        disposable6.Disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Vector3.Distance(MidDistanceFromMe,gameObject.transform.position) < 0.5f || _BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    disposable6.Dispose();
                }
            }
        );
        gameObject.transform.DOMove(MidDistanceFromMe,0.3f).OnComplete(() =>{_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;});
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}