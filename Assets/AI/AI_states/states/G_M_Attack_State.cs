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
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        _FightAttriCalRef.ScaleAllMyCollider(0.7f);
        _Rigidbody.velocity = Vector3.zero;
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("confident");
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        pEvents.CloseAllPersonalityEffects();
        Sensor.GetEnemiesByDistance(true);
        if (Sensor.GetEnemiesByDistance(false)[0] != null)
            RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
		_Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
	}
    
    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _FightAttriCalRef.ScaleAllMyCollider(1f);
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
