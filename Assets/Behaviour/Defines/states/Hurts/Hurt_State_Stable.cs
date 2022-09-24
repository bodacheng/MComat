using UnityEngine;
using HittingDetection;
using UniRx;
namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void StableDamgeStart(V_Damage newValue)
        {
            FightParamsRef.GetKnockOffCount().SetGauge(0f);
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