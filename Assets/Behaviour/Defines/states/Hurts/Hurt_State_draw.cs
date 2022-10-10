using UnityEngine;
using HittingDetection;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void DrawDamageStart(V_Damage newValue)
        {
            _Rigidbody.mass = 200;
            used_dizzy_time = FightGlobalSetting.HeavyHitLastingTime;
        }

        void DrawDamageUpdate(V_Damage newValue)
        {
            Vector3 destination()
            {
                var vector3 = newValue.from_weapon_marker.transform.position;
                vector3.y = gameObject.transform.position.y;
                return vector3;
            }
            _Rigidbody.MovePosition(destination());
        }
    }
}