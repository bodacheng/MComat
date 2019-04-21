using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public override void _f_State_Update()
	{
        if (BS_Main_Health.returnDamageList(damageType.heavy_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0)
            {
                BS_Main_Health._health -= BS_Main_Health.returnDamageList(damageType.heavy_damage)[0]._damage;
                BS_Main_Health.returnDamageList(damageType.heavy_damage).RemoveAt(0);
            }
        }
        if (BS_Main_Health.returnDamageList(damageType.light_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0)
            {
                BS_Main_Health._health -= -BS_Main_Health.returnDamageList(damageType.light_damage)[0]._damage;
                BS_Main_Health.returnDamageList(damageType.light_damage).RemoveAt(0);
            }
        }
		if (BS_Main_Health.returnDamageList(damageType.supper_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0)
            {
                BS_Main_Health._health -=(-BS_Main_Health.returnDamageList(damageType.supper_damage)[0]._damage);
                BS_Main_Health.returnDamageList(damageType.supper_damage).RemoveAt(0);
            }
        }
		if (BS_Main_Health.returnDamageList(damageType.knockOff_damage) != null)
        {
			if (BS_Main_Health.returnDamageList(damageType.knockOff_damage).Count > 0)
            {
				BS_Main_Health._health -=(BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0]._damage);
				BS_Main_Health.returnDamageList(damageType.knockOff_damage).RemoveAt(0);
            }
        }

    }

	public override void AI_State_enter()
	{		
        AI_DATA_CENTER.deActiveObjects();
		base.AI_State_enter();
		_Rigidbody.useGravity = false;
		Animation_Manger.PlayLayerAnim (animator_layer_index.Full_Body,clip_name);

        if (BS_Main_Health.returnDamageList(damageType.heavy_damage) != null)
		{
            if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0)
			{
                BS_Main_Health._health -=(BS_Main_Health.returnDamageList(damageType.heavy_damage)[0]._damage);
                BS_Main_Health.returnDamageList(damageType.heavy_damage).RemoveAt(0);
			}
		}
        if (BS_Main_Health.returnDamageList(damageType.light_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0) {
                BS_Main_Health._health-=(BS_Main_Health.returnDamageList(damageType.light_damage)[0]._damage);
                BS_Main_Health.returnDamageList(damageType.light_damage).RemoveAt(0);
            }
        }
		if (BS_Main_Health.returnDamageList(damageType.supper_damage) != null)
        {
			if (BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0)
            {
				BS_Main_Health._health -=(BS_Main_Health.returnDamageList(damageType.supper_damage)[0]._damage);
				BS_Main_Health.returnDamageList(damageType.supper_damage).RemoveAt(0);
            }
        }
		if (BS_Main_Health.returnDamageList(damageType.knockOff_damage) != null)
        {
			if (BS_Main_Health.returnDamageList(damageType.knockOff_damage).Count > 0)
            {
				BS_Main_Health._health -=(BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0]._damage);
				BS_Main_Health.returnDamageList(damageType.knockOff_damage).RemoveAt(0);
            }
        }
    }

	public override bool capacity_exit_condition()//如果受伤动画播放完的话状态就退出，如果时间过了规定的晕眩时间，也退出。
	{
        return false;
	}
}
