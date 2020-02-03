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
    Collider temp_C;  
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("confident");
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;
        personality_Events.CloseAllPersonalityEffects();
        _Rigidbody.velocity = Vector3.zero;
        temp_C = Sensor.GetClosestColliderInSensorRange(true,true,true);
        if (temp_C != null)
            RotateToTarget(temp_C.transform.position, 1f, true);
		_Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
	}
    
    public override void AI_State_exit()
    {
        base.AI_State_exit();
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

    Vector3 rotateTarget;
	public override void _State_FixedUpdate1() 
	{
        temp_C = Sensor.GetClosestColliderInSensorRange(true,true,true);
        if (temp_C!=null)
        {
            rotateTarget = temp_C.transform.position;
            SingleDirectionRotateProcess(rotateTarget);                  
        }
	}

    float lastFrameRotateAngle;
    float thisFrameRotateAngle;
    float ji;
    void SingleDirectionRotateProcess(Vector3 P)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.hiddenMethods.GetRotationAdjustmentStartFlag() || keepRotationAdjustment)
        {
            thisFrameRotateAngle = this.RotateToTarget(P, 1f, true);
            ji = thisFrameRotateAngle * lastFrameRotateAngle;
            if (ji > 0)//同向
            {
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
            else if (ji < 0)//反向
            {
                _SkillCancelFlag.TurnRotationAdjustmentStartFlag(0);
            }
            else //刚开始计
            {
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
        }
    }
}
