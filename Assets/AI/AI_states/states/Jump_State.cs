using UnityEngine;
using Soul;

public class Jump_State : AI_State
{	
	private readonly string clip_name;
    private readonly float forward_force;
    private readonly float vertical_force;
    private readonly float state_time;
    private float time_counter;
    private Vector3 jumpDirection;
    private Transform mainCam;
    private Quaternion screenMovementSpace;
    private Vector3 screenMovementForward, screenMovementRight;
    bool jumpSuccessed;

    public Jump_State(string clip, float forward_force, float vertical_force, float least_state_time)
	{
        this.clip_name = clip;
        this.forward_force = forward_force;
        this.vertical_force = vertical_force;
        this.state_time = least_state_time;
	}

    public Jump_State(string clip, float forward_force, float vertical_force, float least_state_time,bool onEnemyBounce, BehaviorEnterRange[] behaviorEnterRanges)
    {
        this.clip_name = clip;
        this.forward_force = forward_force;
        this.vertical_force = vertical_force;
        this.state_time = least_state_time;
        this.behaviorEnterRanges = behaviorEnterRanges;
    }

    public Jump_State(string clip, float forward_force, float vertical_force, float least_state_time, bool onEnemyBounce)
    {
        this.clip_name = clip;
        this.forward_force = forward_force;
        this.vertical_force = vertical_force;
        this.state_time = least_state_time;
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override bool Capacity_enter_condition()
    {
        return _BasicPhysicSupport.hiddenMethods.Grounded;
    }

    public override bool Enter_condition_priority2()
	{
        return Sensor.GetNearbyDamagingWeaponColliders().Count > 0 && this.CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
    }

    public override bool Naturally_exit_condition()
    {
        return Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over || Animation_Manger.GetIfOnNull() || time_counter > this.state_time;
    }

    Vector3 damagingWeaponComingDirection;
    Vector3 enemy_to_me = Vector3.zero;
	public override void AI_State_enter()
	{
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);

        jumpSuccessed = false;
        this.personality_Events.CloseAllPersonalityEffects();
		_Rigidbody.useGravity = true;
        time_counter = 0;
        jumpDirection = Vector3.zero;
        enemy_to_me = Vector3.zero;
        damagingWeaponComingDirection = Vector3.zero;
        if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0)
        {
            damagingWeaponComingDirection = gameObject.transform.position - Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position;
            switch ((int)Random.Range(0, 2))
            {
                case 0:
                    jumpDirection = Quaternion.Euler(0, -50, 0) * damagingWeaponComingDirection;
                    break;
                case 1:
                    jumpDirection = Quaternion.Euler(0, 50, 0) * damagingWeaponComingDirection;
                    break;
                default:
                    break;
            }
        }else{

            if (Sensor.GetInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.GetInnerEnemiesColliders()[0] != null)
                    enemy_to_me = gameObject.transform.position - Sensor.GetInnerEnemiesColliders()[0].transform.position;
                enemy_to_me.y = 0;
            }

            switch ((int)Random.Range(0, 3))
            {
                case 0:
                    jumpDirection = Quaternion.Euler(0, -90, 0) * enemy_to_me;
                    break;
                case 1:
                    jumpDirection = Quaternion.Euler(0, 90, 0) * enemy_to_me;
                    break;
                default:
                    jumpDirection = enemy_to_me;
                    break;
            }
        }

        jumpDirection = jumpDirection.normalized * forward_force;
        this.RotateToDirection(jumpDirection, 10f, true);
        jumpDirection = jumpDirection + Vector3.up * vertical_force;
        if (IfVectorClean(jumpDirection))
            _Rigidbody.velocity = jumpDirection;
        Animation_Manger.AnimationTrigger(clip_name);
    }

    public override void C_State_enter()
    {
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);

        jumpSuccessed = false;
        this.personality_Events.CloseAllPersonalityEffects();
		_Rigidbody.useGravity = true;
        time_counter = 0;
        this.mainCam = CameraManager._camera.transform;
        screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;

        jumpDirection = new Vector3();

        float h = 0f;
        float v = 0f;
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            h = UnityEngine.Input.GetAxis("Horizontal");
            v = UnityEngine.Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
        }
        _Rigidbody.velocity = Vector3.zero;

        jumpDirection = (screenMovementForward * v) + (screenMovementRight * h);
        jumpDirection = jumpDirection.normalized * forward_force;
        this.RotateToDirection(jumpDirection, 10f, true);
        jumpDirection = jumpDirection + Vector3.up * vertical_force;
        if (IfVectorClean(jumpDirection))
		    _Rigidbody.velocity = jumpDirection;
        Animation_Manger.AnimationTrigger(clip_name);
    }

	public override void _State_FixedUpdate1()
	{
        if (!jumpSuccessed)
        {
            if (_BasicPhysicSupport.hiddenMethods.Grounded)
            {
                if (IfVectorClean(jumpDirection))
                    _Rigidbody.velocity = jumpDirection;
                jumpSuccessed = false;
            }else{
                jumpSuccessed = true;
            }
        }
        this.RotateToVelocity(1f,true);
        time_counter += Time.fixedDeltaTime;
	}

    public override void AI_State_exit()
    {
        jumpSuccessed = false;
        time_counter = 0;
        base.AI_State_exit();
    }

	public void Jump(Vector3 jumpVelocity)
	{
		//if (jumpSound)
		//{
		//    GetComponent<AudioSource>().volume = 1;
		//    GetComponent<AudioSource>().clip = jumpSound;
		//    GetComponent<AudioSource>().Play();
		//}
		_Rigidbody.velocity = jumpVelocity;
		//gameObject.GetComponent<Rigidbody>().AddRelativeForce(jumpVelocity, ForceMode.Impulse);
	}
}
