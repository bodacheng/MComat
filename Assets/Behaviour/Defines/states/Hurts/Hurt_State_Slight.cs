using UnityEngine;
using HittingDetection;
using UniRx;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void SlightDamgeStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._slighthit_lastingtime;
            fixDesPos = CalFixPushPos(newValue.impactComingPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            //gameObject.transform.DOMove(fixDesPos, 0.1f);
            _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
            
            physicMissionDisposable = new SingleAssignmentDisposable();
            physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (TimeCounter > used_dizzy_time)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    physicMissionDisposable.Dispose();
                }
            });
        }
    }
}