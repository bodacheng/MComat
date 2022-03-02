using UnityEngine;
using HittingDetection;
using UniRx;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void ExplosionDamgeStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
            physicMissionDisposable = new SingleAssignmentDisposable();
            physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (TimeCounter > used_dizzy_time)
                    {
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        physicMissionDisposable.Dispose();
                    }
                }
            );

            fixDesPos = CalFixPushPos(newValue.DamageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
        }
    }
}