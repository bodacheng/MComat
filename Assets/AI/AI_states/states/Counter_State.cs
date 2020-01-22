using UnityEngine;
using Soul;

public class Counter_State : Behavior {
    readonly string clip_name;
    readonly bool keepRotationAdjustment;
    readonly float RotationAdjustmentTime;
    readonly float rotate_speed;
    readonly int skillEmergentLevel;

    public Counter_State(string _clip_name, float _RotationAdjustmentTime, float _rotate_speed)
    {
        clip_name = _clip_name;
        keepRotationAdjustment = false;
        rotate_speed = _rotate_speed;
        RotationAdjustmentTime = _RotationAdjustmentTime;
    }
    
    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
        nextAttackStateCanRushFirst = true;
	}
    
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        _SkillCancelFlag.turn_off_flag();
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
        personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.AnimationTrigger(clip_name);
        _Rigidbody.velocity = Vector3.zero;
        _Animator.applyRootMotion = true;
	}

	public override bool Capacity_Exit_Condition()
	{
        return Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over || Animation_Manger.GetIfOnNull();
    }

	public override void _State_FixedUpdate1() 
	{
	}
}
