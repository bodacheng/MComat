using UnityEngine;
using HittingDetection;
using UniRx;
namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void LightDamgeStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            //fixDesPos = CalFixPushPos(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            //gameObject.transform.DOMove(fixDesPos, 0.1f);
            //_Rigidbody.velocity = fixDesPos - gameObject.transform.position;
        }
    }
}