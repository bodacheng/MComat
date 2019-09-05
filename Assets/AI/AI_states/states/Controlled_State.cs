using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class Controlled_State : AI_State {

	private string clip_name;

    public Controlled_State(string clip_name)
	{
		this.clip_name = clip_name;
    }

	public override void pre_process_before_enter()
	{
		base.pre_process_before_enter ();
	}

	public override bool force_enter_condition()
	{
		return true;
	}

	public override void AI_State_enter()
	{		
        _DATA_CENTER.deActiveObjects();
		base.AI_State_enter();
		_Rigidbody.useGravity = false;
		Animation_Manger.PlayLayerAnim (clip_name);
    }

	public override bool capacity_exit_condition()//如果受伤动画播放完的话状态就退出，如果时间过了规定的晕眩时间，也退出。
	{
        return false;
	}
}
