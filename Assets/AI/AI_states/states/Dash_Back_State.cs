using UnityEngine;
using Soul;

public class Dash_Back_State : Behavior
{
    readonly string clip_name;
    readonly UnityEngine.Events.UnityAction breakfreestart;
    readonly UnityEngine.Events.UnityAction breakfreeend;
    readonly CustomCoroutine breakfreeCoroutine;

    public Dash_Back_State()
    {
        clip_name = "rushback";
        breakfreestart = () =>
        {
            _ResistanceManager.Resistance.Value +=10;
        };
        breakfreeend = () =>
        {
            _ResistanceManager.Resistance.Value -=10;
        };
        breakfreeCoroutine = new CustomCoroutine(breakfreestart, 0.6f, breakfreeend);
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override void _State_Update()
    {
        base._State_Update();
        if (BeheviourFrameCounter == 5f)
            _BuffsRunner.RunSubCoroutineOfState(breakfreeCoroutine);
    }

    public override void AI_State_enter()
    {
        base.AI_State_enter();
        _Animator.applyRootMotion = true;
        _Animator.SetFloat("speed", 0f);
        Sensor.ContinuousDetectionStart(2);
        _SkillCancelFlag.turn_off_flag();
        personality_Events.CloseAllPersonalityEffects();
        Vector3 threatsComingPosition = Vector3.zero;
        if (Sensor.GetEnemiesByDistance(true).Count > 0)
            threatsComingPosition = Sensor.GetEnemiesByDistance(false)[0].transform.position;

        if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0)
        {
            threatsComingPosition = Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position;
        }else{
            Collider temp = Sensor.GetClosestEnemyColliderInSensorRange();
            if (temp != null)
                threatsComingPosition = temp.transform.position;
        }
        RotateToTarget_Tween(threatsComingPosition, 0.01f, true);
        Animation_Manger.AnimationTrigger(clip_name,true,0.1f);
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
    
    //public override bool Capacity_enter_condition()
    //{
    //    return base.Capacity_enter_condition() && !_Animator.GetBool("in_transition");
    //}

    public override bool Capacity_Exit_Condition()
    {
        return AnimationCasualFinishedFlag();
    }

    public override void AI_State_exit()
    {
        _Animator.applyRootMotion = false;
        base.AI_State_exit();
    }
}
