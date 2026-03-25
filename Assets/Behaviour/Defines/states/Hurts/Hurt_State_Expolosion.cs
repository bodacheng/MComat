using UnityEngine;
using HittingDetection;
using UniRx;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void ExplosionDamageStart(V_Damage newValue)
        {
            _usedDizzyTime = FightGlobalSetting.HeavyHitLastingTime;
            _physicMissionDisposable = new SingleAssignmentDisposable();
            _physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (TimeCounter > FightGlobalSetting.NormalAttackPosFixingTime)
                    {
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        _physicMissionDisposable.Dispose();
                    }
                }
            ).AddTo(gameObject);

            _Rigidbody.linearVelocity = CalFixPushVector(newValue, gameObject.transform.position);
        }
    }
}
