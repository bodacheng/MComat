using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

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
        breakfreestart = () =>
        {
            this._ResistanceManager.Resistance +=10;
        };
        breakfreeend = () =>
        {
            this._ResistanceManager.Resistance -=10;
        };
        breakfreeCoroutine = new customCoroutine(breakfreestart, 1f, breakfreeend);
    }

    public override void pre_process_before_enter()
    {
		base.pre_process_before_enter ();
    }

    public override bool capacity_enter_condition()
    {
        if (!_DATA_CENTER.IsGrounded())
            return false;
        return true;
    }

    public override bool enter_condition_priority2()
    {
        if ((this.BS_Main_Health.IFgettingDamage() && this.BS_Main_Health.CriticalGauge > 95) || Sensor.getNearbyDamagingWeaponColliders().Count > 0)
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
        _Animator.applyRootMotion = true;
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(2);
        _SkillCancelFlag.turn_off_flag();
        _DATA_CENTER.deActiveObjects();
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
        this.Animation_Manger.animationTrigger(clip_name);
		
        if (this._AIStateRunner.getLastState().StateType == stateType.Def)
        {
            Defend_State df = (Defend_State)this._AIStateRunner.getLastState();
            if (df.block_time_counter > 0)
            {
                df.block_time_counter = 0;
                this.BS_Main_Health.costCriticalGaugeBySPlevel(3);
                BS_Main_Health.clearDamageLists();
                EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", null,this._DATA_CENTER.geometryCenter.position, Quaternion.identity, this._DATA_CENTER.geometryCenter);
                this._BuffsRunner.runSubCoroutineOfState(breakfreeCoroutine);
            }
        }
    }

    public override void _State_FixedUpdate1()
    {
        //_Rigidbody.velocity = Vector3.zero;
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
