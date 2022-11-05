using UnityEngine;
using HittingDetection;
using UniRx;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void ExplosionDamageStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting.HeavyHitLastingTime;
            physicMissionDisposable = new SingleAssignmentDisposable();
            physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (TimeCounter > FightGlobalSetting.NormalAttackPosFixingTime)
                    {
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        physicMissionDisposable.Dispose();
                    }
                }
            ).AddTo(gameObject);

            _Rigidbody.velocity = CalFixPushVector(newValue.DamageEffectPoint, newValue.attacker.Center.WholeT.position,
                gameObject.transform.position,
                newValue.from_weapon.damage_type, newValue.from_weapon._WeaponMode);
        }
    }
}