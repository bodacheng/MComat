using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class G_Ani_MoveEscape_State : AI_State {
    readonly string clip_name;
    Transform mainCam;
    Quaternion screenMovementSpace;
    Vector3 screenMovementForward, screenMovementRight, use_direction;
    readonly UnityEngine.Events.UnityAction breakfreestart;
    readonly UnityEngine.Events.UnityAction breakfreeend;
    customCoroutine breakfreeCoroutine;
    // This skill script is based on animation that has forward motion

    public G_Ani_MoveEscape_State(string _clip_name)
	{
		this.clip_name = _clip_name;
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
        if (!_BasicPhysicSupport.hiddenMethods.Grounded)
            return false;
        return true;
    }

    public override bool Enter_condition_priority2()
    {
        if ((this._FightAttriCalReference.IFgettingDamage() || Sensor.GetNearbyDamagingWeaponColliders().Count > 0) 
            && this._FightAttriCalReference.CriticalGauge > 90)
            return true;
        else
            return false;
    }

    public override bool Naturally_exit_condition()
    {
        if (Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
            return true;
        else
            return false;
    }
    Vector3 damagingWeaponComingDirection;
    Vector3 facedirection;
    public override void AI_State_enter()
	{
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);
        _SkillCancelFlag.turn_off_flag();
        this.personality_Events.CloseAllPersonalityEffects();
        this._Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name);        
        facedirection = gameObject.transform.forward;
        if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0)
        {
            damagingWeaponComingDirection = gameObject.transform.position - Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position;
            switch ((int)Random.Range(0,2))
            {
                case 0:
                    facedirection = Quaternion.Euler(0, -90, 0) * damagingWeaponComingDirection;
                    break;
                case 1:
                    facedirection = Quaternion.Euler(0, 90, 0) * damagingWeaponComingDirection;
                    break;
                default:
                    break;
            }
        }else{
            if (Sensor.GetInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.GetInnerEnemiesColliders()[0] != null)
                    facedirection = - gameObject.transform.position + Sensor.GetInnerEnemiesColliders()[0].transform.position;
            }
        }
        this.RotateToDirection(-facedirection, 10f, true);
        if (this._AIStateRunner.GetLastState().StateType == stateType.Def)
        {
            Defend_State df = (Defend_State)this._AIStateRunner.GetLastState();
            if (df.time_counter > 0)
            {
                df.time_counter = 0;
                this._FightAttriCalReference.costCriticalGaugeBySPlevel(3);
                _FightAttriCalReference.ClearDamageLists();
                EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", null,this._DATA_CENTER.geometryCenter.position, Quaternion.identity, this._DATA_CENTER.geometryCenter);
                this._BuffsRunner.runSubCoroutineOfState(breakfreeCoroutine);
            }
        }
	}

    float h = 0f;
    float v = 0f;
    public override void C_State_enter()
    {
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        this.Sensor.OneRoundDetectionStart(5);
        this.personality_Events.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name);                      
        this.mainCam = CameraManager._camera.transform;
        screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
            || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            h = UnityEngine.Input.GetAxis("Horizontal");
            v = UnityEngine.Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
        }
        use_direction = (screenMovementForward * v) + (screenMovementRight * h);
        this.RotateToDirection(use_direction, 10f, true);
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Animator.applyRootMotion = false;
    }
}
