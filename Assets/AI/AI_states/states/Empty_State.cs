using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class Empty_State : AI_State
{
	public Empty_State()
	{
	}

	public override void Pre_process_before_enter()
	{
        base.Pre_process_before_enter();
    }

	public override bool Naturally_exit_condition()
	{
        return false;
	}

	public override void AI_State_enter()
	{
		base.AI_State_enter();
        //if (Animation_Manger != null)
            Animation_Manger.PlayLayerAnim(null);
        this._DATA_CENTER.TurnShield(false);
        this._Rigidbody.velocity = Vector3.zero;
        this._DATA_CENTER.CleanClear();
        this.personality_Events.CloseAllPersonalityEffects();
    }

	public override void _State_FixedUpdate1()
	{
        if (_DATA_CENTER.Grounded && this.Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.unstarted)
        {
            this._Rigidbody.velocity = Vector3.zero;
            //_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

	public override void AI_State_exit()
	{
        this._Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}