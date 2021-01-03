using UnityEngine;

namespace Soul
{
    public class Counter_State : Behavior
    {
        readonly string clip_name;
        public Counter_State(string _clip_name)
        {
            clip_name = _clip_name;
        }

        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
            nextAttackStateCanRushFirst = true;
        }

        Collider threat;
        public override void AI_State_enter()
        {
            base.AI_State_enter();
            _SkillCancelFlag.turn_off_flag();
            _Animator.SetFloat("speed", 0f);
            _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
            pEvents.CloseAllPersonalityEffects();
            Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
            _Rigidbody.velocity = Vector3.zero;
            _Animator.applyRootMotion = true;
            if (threat != null)
            {
                RotateToTarget_Tween(threat.transform.position, 0.02f);
            }
        }

        public override bool Capacity_Exit_Condition()
        {
            return AnimationCasualFinishedFlag();
        }
    }
}