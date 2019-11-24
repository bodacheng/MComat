using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class GetUp : AI_State {
	
    private string clip_name;
    private float length, counter;

    public GetUp(string _clip_name,float length)
	{
		this.clip_name = _clip_name;
        this.length = length;
	}

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

    public override bool Capacity_enter_condition()
    {
        if (!_DATA_CENTER.Grounded)
            return false;
        return true;
    }

    public override bool Capacity_exit_condition()
    {
        if (this.counter >= length)
            return true;
        else
            return false;
    }

    public override void AI_State_enter()
	{
        base.AI_State_enter();
        this.counter = 0f;
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);
        this.Animation_Manger.AnimationTrigger(clip_name);
	}

    public override void C_State_enter()
    {
        this.AI_State_enter();
    }

	public override void _State_FixedUpdate1() 
	{
        this.counter += Time.fixedDeltaTime;
		_Rigidbody.velocity = Vector3.zero;
	}

    public override void AI_State_exit()
    {
        base.AI_State_exit();
    }
}
