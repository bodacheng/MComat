using UnityEngine;

namespace Soul
{
    public class Idle_State : Behavior
    {
        string clip_name;

        public Idle_State(string clip_name)
        {
            this.clip_name = clip_name;
        }

        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }

        public override void AI_State_enter()
        {
            base.AI_State_enter();
            this._Animator.SetFloat("speed", 0f);
            Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
            this._Rigidbody.velocity = Vector3.zero;
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }
    }
}