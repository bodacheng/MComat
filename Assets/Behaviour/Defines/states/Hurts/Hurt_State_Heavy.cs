using UnityEngine;
using HittingDetection;
using UniRx;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void HeavyStart(V_Damage newValue)
        {
            //gameObject.transform.DOMove(fixDesPos, 0.1f);
            _Rigidbody.linearVelocity = CalFixPushVector(newValue, gameObject.transform.position);
            
            _physicMissionDisposable = new SingleAssignmentDisposable();
            _physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (TimeCounter > FightGlobalSetting.NormalAttackPosFixingTime)
                    {
                        if (_BasicPhysicSupport.hiddenMethods.Grounded)
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        else
                        {
                            _Rigidbody.linearVelocity = Vector3.zero;
                        }
                        _physicMissionDisposable.Dispose();
                    }
                }
            ).AddTo(gameObject);
        }
    }
}
