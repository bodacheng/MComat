using UnityEngine;
using Soul;

public class Empty_State : Behavior
{
	public override void Pre_process_before_enter()
	{
        base.Pre_process_before_enter();
    }

	public override bool Capacity_Exit_Condition()
	{
        return false;
	}

	public override void AI_State_enter()
	{
		base.AI_State_enter();
        //if (Animation_Manger != null)
            Animation_Manger.PlayLayerAnim(null,false,0f);
        _DATA_CENTER.TurnShield(false);
        _Rigidbody.velocity = Vector3.zero;
        _BasicPhysicSupport.enabled = false;
        _DATA_CENTER.CleanClear();
        pEvents.CloseAllPersonalityEffects();
    }

	public override void _State_FixedUpdate1()
	{
        if (_BasicPhysicSupport.hiddenMethods.Grounded)
        {
            _Rigidbody.velocity = Vector3.zero;
            //_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

	public override void AI_State_exit()
	{
        _BasicPhysicSupport.enabled = true;
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}