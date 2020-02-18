using UnityEngine;
using Soul;
using System.Collections.Generic;

public class Counter_State : Behavior {
    readonly string clip_name;
    public Counter_State(string _clip_name)
    {
        clip_name = _clip_name;
    }
    
    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
        nextAttackStateCanRushFirst = true;
	}

    List<Collider> near;
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        _SkillCancelFlag.turn_off_flag();
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
        personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.AnimationTrigger(clip_name,true,0.08f);
        _Rigidbody.velocity = Vector3.zero;
        _Animator.applyRootMotion = true;

        near = Sensor.GetNearbyDamagingWeaponColliders();

        if (near != null && near.Count > 0)
        {
            if (near[0] != null)
                RotateToTarget_Tween(near[0].transform.position, 0.5f, true);
        }
    }
        
	public override bool Capacity_Exit_Condition()
	{
        return AnimationCasualFinishedFlag();
    }
}
