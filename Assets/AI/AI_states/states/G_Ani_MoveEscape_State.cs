using UnityEngine;
using Soul;

public class G_Ani_MoveEscape_State : Behavior {

    readonly string clip_name;
    Transform mainCam;
    Quaternion screenMovementSpace;
    Vector3 screenMovementForward, screenMovementRight, use_direction;
    
    readonly UnityEngine.Events.UnityAction breakfreestart;
    readonly UnityEngine.Events.UnityAction breakfreeend;
    readonly CustomCoroutine breakfreeCoroutine;

    public G_Ani_MoveEscape_State(string _clip_name)
    {
        clip_name = _clip_name;
        breakfreestart = () =>
        {
            _ResistanceManager.Resistance.Value += 2;
        };
        breakfreeend = () =>
        {
            _ResistanceManager.Resistance.Value -= 2;
        };
        breakfreeCoroutine = new CustomCoroutine(breakfreestart, 0.6f, breakfreeend);
    }
    
    public override void _State_Update()
    {
        base._State_Update();
        if (BeheviourFrameCounter == 5)
            _BuffsRunner.RunSubCoroutineOfState(breakfreeCoroutine);
    }
    
    public override void Pre_process_before_enter()
    {
        base.Pre_process_before_enter ();
    }
    
    public override bool Capacity_enter_condition()
    {
        return _BasicPhysicSupport.hiddenMethods.Grounded && base.Capacity_enter_condition();
    }
    
    public override bool Capacity_Exit_Condition()
    {
        return AnimationCasualFinishedFlag();
    }
    
    Vector3 damagingWeaponComingDirection;
    Vector3 facedirection;
    Collider threat;
    Collider ECollider;
    public override void AI_State_enter()
	{
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        Sensor.ContinuousDetectionStart(2);
        _SkillCancelFlag.turn_off_flag();
        pEvents.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
        facedirection = gameObject.transform.forward;
        threat = Sensor.GetSuddenThreatInRange(0, 5);
        
        if (_BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
        {
            facedirection = Vector3.zero - gameObject.transform.position;
            facedirection.y = 0;
        }else{
            if (threat != null)
            {
                damagingWeaponComingDirection = gameObject.transform.position - threat.transform.position;
                switch (Random.Range(0, 2))
                {
                    case 0:
                        facedirection = Quaternion.Euler(0, -135, 0) * damagingWeaponComingDirection;
                        break;
                    case 1:
                        facedirection = Quaternion.Euler(0, 135, 0) * damagingWeaponComingDirection;
                        break;
                }
            }else{
                ECollider = Sensor.GetClosestEnemyColliderInSensorRange();
                if (ECollider != null)
                    facedirection = - gameObject.transform.position + ECollider.transform.position;
                switch (Random.Range(0, 2))
                {
                    case 0:
                        facedirection = Quaternion.Euler(0, -90, 0) * facedirection;
                        break;
                    case 1:
                        facedirection = Quaternion.Euler(0, 90, 0) * facedirection;
                        break;
                }
            }
        }
        
        RotateToTarget_Tween(gameObject.transform.position + facedirection, 0.01f, true);
	}

    float h;
    float v;
    public override void C_State_enter()
    {
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        pEvents.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name, true, 0.1f);
        mainCam = CameraManager._camera.transform;
        screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;

        h = UnityEngine.Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("joystick");
        v = UnityEngine.Input.GetAxis("Vertical") + UltimateJoystick.GetVerticalAxis("joystick");
        
        use_direction = (screenMovementForward * v) + (screenMovementRight * h);
        RotateToTarget_Tween(gameObject.transform.position + use_direction, 0.01f, true);
    }
    
    public override void AI_State_exit()
    {
        base.AI_State_exit();
        Sensor.OneRoundDetectionStart(5);
        _Animator.applyRootMotion = false;
    }
}