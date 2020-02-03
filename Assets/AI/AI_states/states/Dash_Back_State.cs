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
        behaviorEnterRanges = null;
        breakfreestart = () =>
        {
            _ResistanceManager.Resistance.Value +=10;
        };
        breakfreeend = () =>
        {
            _ResistanceManager.Resistance.Value -=10;
        };
        breakfreeCoroutine = new CustomCoroutine(breakfreestart, 1f, breakfreeend);
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }
    
    public override void AI_State_enter()
    {
        base.AI_State_enter();
        _Animator.applyRootMotion = true;
        
        _Animator.SetFloat("speed", 0f);
        Sensor.ContinuousDetectionStart(2);
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
        RotateToDirection(threatsComingDirection, 10f, true);
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
