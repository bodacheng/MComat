using UnityEngine;

namespace Soul
{
    public class G_M_Attack_State : Behavior
    {

        readonly string clip_name;

        #region Constructor
        public G_M_Attack_State(string clip_name)
        {
            this.clip_name = clip_name;
        }
        #endregion

        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }

        #region Capacity Enter Exit
        public override void AI_State_enter()
        {
            base.AI_State_enter();
            _BasicPhysicSupport.OpenEnemyTouchingDrag(2);
            _Rigidbody.velocity = Vector3.zero;
            Animation_Manger.SetTrigger("face_reset");
            Animation_Manger.SetTrigger("confident");
            _Animator.SetFloat("speed", 0f);
            _SkillCancelFlag.turn_off_flag();
            _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
            pEvents.CloseAllPersonalityEffects();
            Sensor.GetEnemiesByDistance(true);
            if (Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (Sensor.GetEnemiesByDistance(false)[0] != null)
                    RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f);
            }
            _Animator.applyRootMotion = true;
            Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _BasicPhysicSupport.OpenEnemyTouchingDrag(0);
            _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(false);
        }
        #endregion

        #region Capacity Enter Exit
        public override bool Capacity_Exit_Condition()
        {
            return AnimationCasualFinishedFlag();
        }
        #endregion

        public override void _State_Update()
        {
            _BasicPhysicSupport.hiddenMethods.RecoverRootPosChange();
        }
    }
}