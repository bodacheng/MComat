using UnityEngine;
using Soul;

public class G_Ani_MoveEscape_State : AI_State {
    readonly string clip_name;
    Transform mainCam;
    Quaternion screenMovementSpace;
    Vector3 screenMovementForward, screenMovementRight, use_direction;

    public G_Ani_MoveEscape_State(string _clip_name)
	{
        clip_name = _clip_name;
        behaviorEnterRanges = null;
	}

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

    public override bool Capacity_enter_condition()
    {
        return _BasicPhysicSupport.hiddenMethods.Grounded;
    }
    
    public override bool Enter_condition_priority1()
    {
        return _AIStateRunner.GetNowState().StateKey == "Defend" && _ResistanceManager.Resistance.Value < 2;
    }

    public override bool Enter_condition_priority2()
    {
        return (_FightAttriCalReference.IFgettingDamage() || Sensor.GetNearbyDamagingWeaponColliders().Count > 0) && _ResistanceManager.Resistance.Value == 0;
    }

    public override bool Naturally_exit_condition()
    {
        return Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over || Animation_Manger.GetIfOnNull();
    }
    Vector3 damagingWeaponComingDirection;
    Vector3 facedirection;
    public override void AI_State_enter()
	{
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        Sensor.OneRoundDetectionStart(5);
        _SkillCancelFlag.turn_off_flag();
        personality_Events.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name);        
        facedirection = gameObject.transform.forward;
        if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0)
        {
            damagingWeaponComingDirection = gameObject.transform.position - Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position;
            switch (Random.Range(0, 2))
            {
                case 0:
                    facedirection = Quaternion.Euler(0, -90, 0) * damagingWeaponComingDirection;
                    break;
                case 1:
                    facedirection = Quaternion.Euler(0, 90, 0) * damagingWeaponComingDirection;
                    break;
            }
        }else{
            if (Sensor.GetInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.GetInnerEnemiesColliders()[0] != null)
                    facedirection = - gameObject.transform.position + Sensor.GetInnerEnemiesColliders()[0].transform.position;
            }
        }
        RotateToDirection(-facedirection, 10f, true);
	}

    float h;
    float v;
    public override void C_State_enter()
    {
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        Sensor.OneRoundDetectionStart(5);
        personality_Events.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name);
        mainCam = CameraManager._camera.transform;
        screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
            || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
        }
        use_direction = (screenMovementForward * v) + (screenMovementRight * h);
        RotateToDirection(use_direction, 10f, true);
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Animator.applyRootMotion = false;
    }
}
