using UnityEngine;
using Soul;

public class G_M_Attack_State : Behavior {
    readonly string clip_name;
    readonly bool keepRotationAdjustment;

    #region Constructor
    public G_M_Attack_State(string clip_name, bool keepRotationAdjustment)
	{
		this.clip_name = clip_name;
        this.keepRotationAdjustment = keepRotationAdjustment;
    }
    
    public G_M_Attack_State(string clip_name)
    {
        this.clip_name = clip_name;
        keepRotationAdjustment = false;
    }
    #endregion

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}
  
    #region Capacity Enter Exit
    Collider temp_C;  
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("confident");
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        personality_Events.CloseAllPersonalityEffects();
        _Rigidbody.velocity = Vector3.zero;
        temp_C = Sensor.GetClosestColliderInSensorRange(true,true,true);
        if (temp_C != null)
            RotateToTarget_Tween(temp_C.transform.position, 0.1f, true);
		_Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
	}
    
    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(false);
        _Animator.applyRootMotion = false;
    }
    #endregion

    #region Capacity Enter Exit
    public override bool Capacity_Exit_Condition()
	{
        return AnimationCasualFinishedFlag();
    }
    #endregion
}
