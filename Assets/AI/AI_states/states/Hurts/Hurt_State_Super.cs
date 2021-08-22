using UnityEngine;
using HittingDetection;
using UniRx;
namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void SuperDamgeStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._superhit_lastingtime;
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
            fixDesPos = CalFixPushPos(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
            EffectsManager.GenerateEffect("electric_s_e", FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), newValue.damageHappenPoint, newValue.CutRotation, _DATA_CENTER.geometryCenter);
        }
    }
}