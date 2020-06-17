using UnityEngine;
using HittingDetection;
using Soul;
using UniRx;

public partial class Hurt_State : Behavior
{
    void SuperDamgeStart(V_Damage newValue)
    {
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim(),true,0.05f);
        used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
        physicMissionDisposable = new SingleAssignmentDisposable();
        physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (TimeCounter > 2 * FightGlobalSetting._normalattackpositionfixingtime)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    physicMissionDisposable.Dispose();
                }
            }
        );
        fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
        _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(6f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}
