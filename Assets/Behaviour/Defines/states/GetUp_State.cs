using UnityEngine;

namespace Soul
{
    public class GetUp : Behavior
    {
        readonly string clip_name;
        float counter;
        public GetUp(string _clip_name)
        {
            clip_name = _clip_name;
        }

        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }

        // On what condition can we exit this state 
        public override bool Capacity_Exit_Condition()
        {
            return counter > FightGlobalSetting._GetupTime;
        }

        public override void AI_State_enter()
        {
            base.AI_State_enter();
            _SkillCancelFlag.turn_on_flag();
            counter = 0f;
            _Animator.SetFloat("speed", 0f);
            Sensor.OneRoundDetectionStart(5);
            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
        }

        public override void C_State_enter()
        {
            AI_State_enter();
        }

        public override void _State_FixedUpdate1()
        {
            counter += Time.fixedDeltaTime;
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _SkillCancelFlag.turn_off_flag();
        }
    }
}