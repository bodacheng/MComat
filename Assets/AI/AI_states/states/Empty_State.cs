using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty_State : AI_State
{
	public Empty_State()
	{
	}

	public override void pre_process_before_enter()
	{
        base.pre_process_before_enter();
    }

	public override bool capacity_exit_condition()
	{
        return false;
	}

	public override void AI_State_enter()
	{
		base.AI_State_enter();
        //if (Animation_Manger != null)
            Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, null);
        this.AI_DATA_CENTER.deActiveObjects();
        this.AI_DATA_CENTER.turnShield(false);
        this.Sensor.Stop();
        this._Rigidbody.velocity = Vector3.zero;
    }

	public override void _f_State_Update()
	{
        if (AI_DATA_CENTER.IsGrounded() && this.Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.unstarted)
        {
            this._Rigidbody.velocity = Vector3.zero;
            //_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this._Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}