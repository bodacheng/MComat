using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class Dash_Back_State : AI_State
{
    private readonly string clip_name;
    private readonly UnityEngine.Events.UnityAction breakfreestart;
    private readonly UnityEngine.Events.UnityAction breakfreeend;
    private readonly customCoroutine breakfreeCoroutine;
    
    public Dash_Back_State()
    {
        this.clip_name = "rushback";
        this.behaviorEnterRanges = null;
        breakfreestart = () =>
        {
            this._ResistanceManager.Resistance.Value +=10;
        };
        breakfreeend = () =>
        {
            this._ResistanceManager.Resistance.Value -=10;
        };
        breakfreeCoroutine = new customCoroutine(breakfreestart, 1f, breakfreeend);
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override bool Capacity_enter_condition()
    {
        return _DATA_CENTER.Grounded;
    }

    public override bool Enter_condition_priority2()
    {
        return Sensor.GetNearbyDamagingWeaponColliders().Count > 0;
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
        _Animator.SetFloat("speed", 0f);
        Sensor.OneRoundDetectionStart(2);
        _SkillCancelFlag.turn_off_flag();
        personality_Events.CloseAllPersonalityEffects();
        Vector3 threatsComingDirection = Vector3.zero;
        if (Sensor.GetEnemiesByDistance(true).Count > 0)
            threatsComingDirection = Sensor.GetEnemiesByDistance(false)[0].transform.position - gameObject.transform.position;

        if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0)
        {
            threatsComingDirection = - gameObject.transform.position + Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position;
        }else{
            if (Sensor.GetInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.GetInnerEnemiesColliders()[0] != null)
                    threatsComingDirection = - gameObject.transform.position + Sensor.GetInnerEnemiesColliders()[0].transform.position;
            }
        }
        RotateToDirection(threatsComingDirection, 100000f, true);
        Animation_Manger.AnimationTrigger(clip_name);
		
        //if (_AIStateRunner.getLastState().StateType == stateType.Def)
        //{
        //    Defend_State df = (Defend_State)this._AIStateRunner.getLastState();
        //    if (df.block_time_counter > 0)
        //    {
        //        df.block_time_counter = 0;
        //        this._FightAttriCalReference.costCriticalGaugeBySPlevel(3);
        //        _FightAttriCalReference.ClearDamageLists();
        //        EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", null,this._DATA_CENTER.geometryCenter.position, Quaternion.identity, this._DATA_CENTER.geometryCenter);
        //        this._BuffsRunner.runSubCoroutineOfState(breakfreeCoroutine);
        //    }
        //}
    }

    public override void _State_FixedUpdate1()
    {
        //_Rigidbody.velocity = Vector3.zero;
    }

    public override bool Capacity_exit_condition()
    {
        return this.Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over ? true : false;
    }

    public override void AI_State_exit()
    {
        _Animator.applyRootMotion = false;
        base.AI_State_exit();
    }
}
