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
        if (BeheviourFrameCounter == 5)
        {
            _BuffsRunner.RunSubCoroutineOfState(breakfreeCoroutine);
        }
    }

    public override void AI_State_enter()
    {
        base.AI_State_enter();
        _Animator.applyRootMotion = true;
        _Animator.SetFloat("speed", 0f);
        Sensor.ContinuousDetectionStart(2);
        _SkillCancelFlag.turn_off_flag();
        pEvents.CloseAllPersonalityEffects();
        Vector3 threatsComingPosition = Vector3.zero;
        if (Sensor.GetEnemiesByDistance(true).Count > 0)
            threatsComingPosition = Sensor.GetEnemiesByDistance(false)[0].transform.position;

        Collider threat = Sensor.GetSuddenThreatInRange(0, 5);
        if (threat != null)
        {
            threatsComingPosition = threat.transform.position;
        }else{
            Collider temp = Sensor.GetClosestEnemyColliderInSensorRange();
            if (temp != null)
                threatsComingPosition = temp.transform.position;
        }
        RotateToTarget_Tween(threatsComingPosition, 0.01f, true);
        Animation_Manger.AnimationTrigger(clip_name,true,0.1f);
    }
    
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
