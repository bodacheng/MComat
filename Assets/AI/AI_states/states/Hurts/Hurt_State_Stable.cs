using UnityEngine;
using HittingDetection;
using UniRx;
namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void StableDamgeStart(V_Damage newValue)
        {
            _FightAttriCalRef.GetKnockOffCount().SetGauge(0f);
            used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
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
        }
    }
}