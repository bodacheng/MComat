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
        var disposable4 = new SingleAssignmentDisposable();
        disposable4.Disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (TimeCounter > 2 * used_dizzy_time / 3)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    disposable4.Dispose();
                }
            }
        );
        fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
        _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
        _FightAttriCalReference.GetKnockOffCount().PlusGauge(6f);
        _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}
