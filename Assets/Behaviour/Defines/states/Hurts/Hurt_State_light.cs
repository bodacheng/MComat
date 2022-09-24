using UnityEngine;
using HittingDetection;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void NormalStart(V_Damage newValue)
        {
            _BasicPhysicSupport.OpenEnemyTouchingDrag(1);
            used_dizzy_time = FightGlobalSetting.LightHitLastingTime;
            if (_BasicPhysicSupport.hiddenMethods.Grounded)
            {
                _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                _Rigidbody.velocity = Vector3.zero;
            }
        }
    }
}