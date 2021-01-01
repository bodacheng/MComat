using UnityEngine;

namespace Soul
{
    public class Empty_State : Behavior
    {
        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }

        public override void AI_State_enter()
        {
            base.AI_State_enter();
            Animation_Manger.PlayLayerAnim(null, false, 0f);
            _Rigidbody.velocity = Vector3.zero;
            _BasicPhysicSupport.SetUsingGravity(false);
            _DATA_CENTER.CleanClear();
            pEvents.CloseAllPersonalityEffects();
        }

        public override void AI_State_exit()
        {
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _BasicPhysicSupport.SetUsingGravity(true);
        }
    }
}