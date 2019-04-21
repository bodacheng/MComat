using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : AI_State
{
	string clip_name;

	public Idle_State(string clip_name)
	{
        this.clip_name = clip_name;
	}

	public override void pre_process_before_enter()
	{
        base.pre_process_before_enter();
    }

	public override void AI_State_enter()
	{
		base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, clip_name);
        this._Rigidbody.velocity = Vector3.zero;
	}

    public override bool capacity_exit_condition()
    {
        return false;
    }
}