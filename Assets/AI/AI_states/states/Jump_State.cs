using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_State : AI_State
{	
	private string clip_name;
    private float forward_force;
    private float vertical_force;
    private float state_time;
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

    public Jump_State(string clip, float forward_force, float vertical_force, float least_state_time,bool onEnemyBounce, behaviorEnterRange[] behaviorEnterRanges)
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

    public override void pre_process_before_enter()
    {
		base.pre_process_before_enter ();
    }

    public override bool capacity_enter_condition()
    {
        if (!AI_DATA_CENTER.IsGrounded())
        {
            return false;
        }return true;
    }

    public override bool enter_condition_priority2()
	{   
        if (Sensor.getNearbyDamagingWeaponColliders().Count > 0  && this.checkToEnemyDisEnterCondition(this.behaviorEnterRanges))
        {
            return true;
        }
        return false;
	}

    public override bool capacity_exit_condition()
    {
        if (Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
            return true;
        if (time_counter > this.state_time)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 damagingWeaponComingDirection;
    Vector3 enemy_to_me = Vector3.zero;
	public override void AI_State_enter()
	{
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);

        jumpSuccessed = false;
        AI_DATA_CENTER.deActiveObjects();
		_Rigidbody.useGravity = true;
        time_counter = 0;
        jumpDirection = Vector3.zero;
        enemy_to_me = Vector3.zero;
        damagingWeaponComingDirection = Vector3.zero;
        if (Sensor.getNearbyDamagingWeaponColliders().Count > 0)
        {
            damagingWeaponComingDirection = gameObject.transform.position - Sensor.getNearbyDamagingWeaponColliders()[0].transform.position;
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

            if (Sensor.getInnerEnemiesColliders().Count > 0)
            {
                if (Sensor.getInnerEnemiesColliders()[0] != null)
                    enemy_to_me = gameObject.transform.position - Sensor.getInnerEnemiesColliders()[0].transform.position;
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
        this.RotateToDirection(jumpDirection, 100000f, true);
        jumpDirection = jumpDirection + Vector3.up * vertical_force;
        if (ifVectorClean(jumpDirection))
            _Rigidbody.velocity = jumpDirection;
        Animation_Manger.animationCustomCoroutineTrigger(animator_layer_index.Full_Body, clip_name);
    }

    public override void c_State_enter()
    {
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Sensor.OneRoundDetectionStart(5);

        jumpSuccessed = false;
        AI_DATA_CENTER.deActiveObjects();
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
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
        }
        _Rigidbody.velocity = Vector3.zero;

        jumpDirection = (screenMovementForward * v) + (screenMovementRight * h);
        jumpDirection = jumpDirection.normalized * forward_force;
        this.RotateToDirection(jumpDirection, 9999f, true);
        jumpDirection = jumpDirection + Vector3.up * vertical_force;
        if (ifVectorClean(jumpDirection))
		    _Rigidbody.velocity = jumpDirection;
        Animation_Manger.animationCustomCoroutineTrigger(animator_layer_index.Full_Body, clip_name);
    }

	public override void _f_State_Update()
	{     
        if (!jumpSuccessed)
        {
            if (AI_DATA_CENTER.IsGrounded())
            {
                if (ifVectorClean(jumpDirection))
                    _Rigidbody.velocity = jumpDirection;
                jumpSuccessed = false;
            }else{
                jumpSuccessed = true;
            }
        }
        this.RotateToVelocity(1f,true);
	}

    public override void _State_FixedUpdate()
    {
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
