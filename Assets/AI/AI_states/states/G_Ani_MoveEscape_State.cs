using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class G_Ani_MoveEscape_State : AI_State {
	private string clip_name;
    private Transform mainCam;
    private Quaternion screenMovementSpace;
    private Vector3 screenMovementForward, screenMovementRight, use_direction;
    private UnityEngine.Events.UnityAction breakfreestart;
    private UnityEngine.Events.UnityAction breakfreeend;
    private customCoroutine breakfreeCoroutine;
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

    public override void pre_process_before_enter()
	{
		base.pre_process_before_enter ();
	}

    public override bool Capacity_enter_condition()
    {
        if (!_DATA_CENTER.IsGrounded())
            return false;
        return true;
    }

    public override bool enter_condition_priority2()
    {
        if ((this.BS_Main_Health.IFgettingDamage() || Sensor.getNearbyDamagingWeaponColliders().Count > 0) 
            && this.BS_Main_Health.CriticalGauge > 90)
            return true;
        else
            return false;
    }

    public override bool capacity_exit_condition()
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
        Animation_Manger.animationTrigger(clip_name);        
        facedirection = gameObject.transform.forward;
        if (Sensor.getNearbyDamagingWeaponColliders().Count > 0)
        {
            damagingWeaponComingDirection = gameObject.transform.position - Sensor.getNearbyDamagingWeaponColliders()[0].transform.position;
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
            if (Sensor.getInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.getInnerEnemiesColliders()[0] != null)
                    facedirection = - gameObject.transform.position + Sensor.getInnerEnemiesColliders()[0].transform.position;
            }
        }
        this.RotateToDirection(-facedirection, 100f, true);
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

    float h = 0f;
    float v = 0f;
    public override void c_State_enter()
    {
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        this.Sensor.OneRoundDetectionStart(5);
        this.personality_Events.CloseAllPersonalityEffects();
        _Animator.applyRootMotion = true;
        Animation_Manger.animationTrigger(clip_name);                      
        this.mainCam = CameraManager._camera.transform;
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
        this.RotateToDirection(use_direction, 100f, true);
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Animator.applyRootMotion = false;
    }
}
