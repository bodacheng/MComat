using UnityEngine;
using HittingDetection;
using Soul;
using UniRx;
using DG.Tweening;

public partial class Hurt_State : Behavior
{
    void PushToMidStart(V_Damage newValue, float dis, bool Grounded)
    {
        used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
        Vector3 MidDistanceFromMe = newValue.attacker._Center.geometryCenter.transform.position + newValue.attacker._Center.WholeT.transform.forward * dis;
        if (Grounded)
        {
            PlayHurtAnim(newValue);
            MidDistanceFromMe.y = 0;
        }
        else
        {
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(), true, 0.05f);
            _BasicPhysicSupport.SetUsingGravity(false);
        }

        physicMissionDisposable = new SingleAssignmentDisposable();
        physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Vector3.Distance(MidDistanceFromMe,gameObject.transform.position) < 0.3f || _BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    physicMissionDisposable.Dispose();
                }
            }
        );
        gameObject.transform.DOMove(MidDistanceFromMe, 0.3f).OnComplete(() => {
            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            _BasicPhysicSupport.SetUsingGravity(true);
        });
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}