using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Back_State : AI_State
{
    private string clip_name;
    private UnityEngine.Events.UnityAction breakfreestart;
    private UnityEngine.Events.UnityAction breakfreeend;
    private customCoroutine breakfreeCoroutine;

    public Dash_Back_State()
    {
        this.clip_name = "rushback";
        this.behaviorEnterRanges = null;
    }

    public Dash_Back_State(string clip_name, behaviorEnterRange[] behaviorEnterRanges)
    {
        this.clip_name = clip_name;
        this.behaviorEnterRanges = behaviorEnterRanges;
    }

    public override void pre_process_before_enter()
    {
		base.pre_process_before_enter ();
        breakfreestart = () =>
        {
            this.BS_Main_Health.Resistance +=10;
        };
        breakfreeend = () =>
        {
            this.BS_Main_Health.Resistance -=10;
        };
        breakfreeCoroutine = new customCoroutine(breakfreestart, 1f, breakfreeend);
    }

    public override bool capacity_enter_condition()
    {
        if (!AI_DATA_CENTER.IsGrounded())
            return false;
        if (this._AIStateRunner.getNowState().StateType == stateType.Hit_early)
        {
            if (this.BS_Main_Health.CriticalGauge < 60)
                return false;
        }
        return true;
    }

    public override bool enter_condition_priority2()
    {
        if (this.BS_Main_Health.Resistance > 0)
            return false;
        if ((this.BS_Main_Health.IFgettingDamage() || Sensor.getNearbyDamagingWeaponColliders().Count > 0) && this.BS_Main_Health.CriticalGauge > 90)
            return true;
        else
            return false;
    }

    //public override bool enter_condition_priority3()
    //{
    //    if (Sensor.getInnerEnemiesColliders().Count > 0)
    //    {
    //        return true; 
    //    }
    //    else
    //        return false;
    //}

    public override void AI_State_enter()
    {
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);
        _SkillCancelFlag.turn_off_flag();
        AI_DATA_CENTER.deActiveObjects();
        Vector3 threatsComingDirection = Vector3.zero;
        if (Sensor.getEnemiesByDistance(true).Count > 0)
            threatsComingDirection = Sensor.getEnemiesByDistance(false)[0].transform.position - gameObject.transform.position;

        if (Sensor.getNearbyDamagingWeaponColliders().Count > 0)
        {
            threatsComingDirection = - gameObject.transform.position + Sensor.getNearbyDamagingWeaponColliders()[0].transform.position;
        }else{
            if (Sensor.getInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.getInnerEnemiesColliders()[0] != null)
                    threatsComingDirection = - gameObject.transform.position + Sensor.getInnerEnemiesColliders()[0].transform.position;
            }
        }

        this.RotateToDirection(threatsComingDirection, 100000f, true);
        this.Animation_Manger.animationCustomCoroutineTrigger(animator_layer_index.Full_Body, clip_name);
		_Animator.applyRootMotion = true;

        if (this._AIStateRunner.getLastState().StateType == stateType.Hit_early)
        {
            this.BS_Main_Health.plusCriticalGauge(-60);
            BS_Main_Health.clearDamageLists();
            defaultPools.Instance.GenerateEffect("break_free", null,
                                                 this.AI_DATA_CENTER.geometryCenter.position, Quaternion.identity, this.AI_DATA_CENTER.geometryCenter);
            this._AIStateRunner.runSubCoroutineOfState(breakfreeCoroutine);
        }
    }

    public override void _f_State_Update()
    {
        _Rigidbody.velocity = Vector3.zero;
    }

    public override bool capacity_exit_condition()
    {
        if (this.Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
            return true;
        else
            return false;
    }

    public override void AI_State_exit()
    {
        _Animator.applyRootMotion = false;
        base.AI_State_exit();
    }
}
