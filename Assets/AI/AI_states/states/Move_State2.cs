using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;
using System.Linq;

public class Move_State2 : AI_State
{
	private float speed,use_speed;
    private float x_direction_random_range;
	private float time_limit, time_counter;
    private Vector3 use_direction;
    private AIMoveDirection moveDirection;
    private AIMoveStyle _AIMoveStyle;
    private Transform mainCam;
    private Quaternion screenMovementSpace;
    private Vector3 direction, screenMovementForward, screenMovementRight;

    private List<GameObject> EnemiesByDistance;

    private enum AIMoveDirection : int
    {
        stay = 0,
        towardsEnemy = 1,
        backTowardsEnemy = 2,
        EnemynoLeft = 3,
        EnemynoRight = 4,
    }

    public Move_State2(AIMoveStyle aiMoveStyle, float speed, float x_direction_random_range,float time_limit)
	{
        this._AIMoveStyle = aiMoveStyle;
        this.speed = speed;
        this.x_direction_random_range = x_direction_random_range;
		this.time_limit = time_limit;		
	}

	public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

    public override bool Enter_condition_priority2()
    {
        //if (AI_DATA_CENTER.IsGrounded())
        return true;
        //return false;
    }

    public override bool Strategic_exit_condition()
    {
        if (Sensor.getMidEnemiesColliders().Count > 0 || this.time_counter >= this.time_limit)//|| Sensor.getInnerRangeWallColliders().Count > 0
        {
            return true;
        }
        return false;
    }

    public override void C_State_enter()
    {
        if (this._AIStateRunner.GetLastState() == this)
            return;

        personality_Events.CloseAllPersonalityEffects();
        //this.AI_DATA_CENTER.switchToMocaPhysicMaterial();
        this.time_counter = 0f;
        use_speed = speed;
        this.mainCam = CameraManager._camera.transform;
        _Rigidbody.useGravity = true;
        Animation_Manger.PlayLayerAnim(null);

        if (!_DATA_CENTER.IsGrounded())
            use_direction.y = -1;
    }

    private int whereToGo;
    public override void AI_State_enter()// 整个enter阶段与状态运行中有关的就是决定use_direction和moveDirection。前者状态运行中会调整。
    {
        this.personality_Events.CloseAllPersonalityEffects();
        //this.AI_DATA_CENTER.switchToSmoothPhysicMaterial();
        EnemiesByDistance = Sensor.getEnemiesByDistance(true);
        switch(_AIMoveStyle)
        {
            case AIMoveStyle.normal:
                if (EnemiesByDistance.Count > 0)
                {
                    if (Sensor.getInnerEnemiesColliders().Count > 0)
                    {
                        whereToGo = (int)Random.Range(0, 4);
                        if (whereToGo > 1)
                        {
                            this.moveDirection = AIMoveDirection.stay;
                            use_direction = Vector3.zero;
                        }else{
                            this.moveDirection = AIMoveDirection.backTowardsEnemy;
                            if (Sensor.getInnerEnemiesColliders()[0] != null)
                                use_direction = -Sensor.getInnerEnemiesColliders()[0].transform.position + gameObject.transform.position;
                            use_direction.y = 0;
                            if (x_direction_random_range > 0)
                            {
                                use_direction.x = Random.Range(use_direction.x - x_direction_random_range, use_direction.x + x_direction_random_range);
                            }
                        }
                    }
                    else if (Sensor.getMidEnemiesColliders().Count > 0 && Sensor.getInnerEnemiesColliders().Count == 0)
                    {
                        whereToGo = (int)Random.Range(0, 8);
                        switch (whereToGo)
                        {
                            case 1:
                                this.moveDirection = AIMoveDirection.EnemynoLeft;
                                if (Sensor.getMidEnemiesColliders()[0] != null)
                                    use_direction = -Sensor.getMidEnemiesColliders()[0].transform.right;
                                break;
                            case 2:
                                this.moveDirection = AIMoveDirection.EnemynoRight;
                                if (Sensor.getMidEnemiesColliders()[0] != null)
                                    use_direction = Sensor.getMidEnemiesColliders()[0].transform.right;
                                break;
                            case 0:
                                this.moveDirection = AIMoveDirection.towardsEnemy;
                                use_direction = Vector3.zero;
                                break;
                            case 3:
                                this.moveDirection = AIMoveDirection.towardsEnemy;
                                use_direction = Vector3.zero;
                                break;
                            case 4:
                                this.moveDirection = AIMoveDirection.towardsEnemy;
                                use_direction = Vector3.zero;
                                break;
                            case 5:
                                this.moveDirection = AIMoveDirection.towardsEnemy;
                                use_direction = Vector3.zero;
                                break;
                            default:
                                this.moveDirection = AIMoveDirection.stay;
                                use_direction = Vector3.zero;
                                break;
                        }
                    }
                    else if (Sensor.getMidEnemiesColliders().Count == 0)
                    {
                        this.moveDirection = AIMoveDirection.towardsEnemy;
                    }
                }
                else
                {
                    this.moveDirection = AIMoveDirection.stay;
                    use_direction = Vector3.zero;
                }
                break;
            case AIMoveStyle.test:
                this.moveDirection = AIMoveDirection.stay;
                use_direction = Vector3.zero;
                break;
            default:
                break;
        }
        use_direction = use_direction.normalized;

        if (!_DATA_CENTER.IsGrounded())
            use_direction.y = -1;

        if (this._AIStateRunner.GetLastState() == this)
            return;
        //this.AI_DATA_CENTER.switchToMocaPhysicMaterial();
        this.time_counter = 0f;
        use_speed = speed;
        this.mainCam = CameraManager._camera.transform;
        _Rigidbody.useGravity = true;
        Animation_Manger.PlayLayerAnim(null);
    }

    private float angle;
    public override void _State_FixedUpdate1()
	{
        _Rigidbody.velocity = Vector3.zero;
        time_counter += Time.deltaTime;
        if (_DATA_CENTER.onBattleGroundBundary) //这一段指的是AI模式下走位的问题。
        {
            use_direction = _DATA_CENTER.antiWallDirection;
            return;
        }
        switch (this.moveDirection)
        {
            case AIMoveDirection.stay:
                use_direction = Vector3.zero;
                _Animator.SetFloat("speed", 0);
                break;
            case AIMoveDirection.backTowardsEnemy:
                _Animator.SetFloat("speed", 10);
                break;
            case AIMoveDirection.towardsEnemy:
                if (EnemiesByDistance.Count > 0)
                {
                    angle = Vector3.Angle(use_direction, use_direction += (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized);
                    use_direction = Vector3.Lerp(use_direction, use_direction += (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized, angle / 45 * Time.deltaTime);
                }
                _Animator.SetFloat("speed", 10);
                break;
            case AIMoveDirection.EnemynoLeft:
                _Animator.SetFloat("speed", 10);
                break;
            case AIMoveDirection.EnemynoRight:
                _Animator.SetFloat("speed", 10);
                break;
        }

        use_direction = use_direction.normalized;
        if (!_DATA_CENTER.IsGrounded())
            use_direction.y = -1;
        //if (AI_DATA_CENTER.getAlliesAndSelfByDistance(true).Count > 1)
        //{
        //    if (Vector3.Distance(AI_DATA_CENTER.getAlliesAndSelfByDistance(false)[0].transform.position, gameObject.transform.position) < Attack_And_Shield_Specification.Instance.SkillRange(behaviorEnterRange.near).far)
        //    {
        //        float angle = Vector3.Angle(use_direction, use_direction + (gameObject.transform.position - AI_DATA_CENTER.getAlliesAndSelfByDistance(false)[0].transform.position).normalized);
        //        use_direction = Vector3.Lerp(use_direction, use_direction + (gameObject.transform.position - AI_DATA_CENTER.getAlliesAndSelfByDistance(false)[0].transform.position).normalized, angle / 45 * Time.deltaTime);
        //        use_direction = use_direction.normalized;
        //    }
        //}

        PositionUpdate();
    }

    private float h = 0f;
    private float v = 0f;
    private Vector3 vel;
    public override void _c_State_FixedUpdate1()
    {
        _Rigidbody.velocity = Vector3.zero;
        time_counter += Time.deltaTime;
        if (this.mainCam == null)
            this.mainCam = CameraManager._camera.transform;
        if (mainCam != null)
		{
			//get movement axis relative to camera
			screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
			screenMovementForward = screenMovementSpace * Vector3.forward;
			screenMovementRight = screenMovementSpace * Vector3.right;
			//get movement input, set direction to move in
			h = 0f;
			v = 0f;
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				h = Input.GetAxis("Horizontal");
				v = Input.GetAxis("Vertical");
				//h = ETCInput.GetAxis("Horizontal");
				//v = ETCInput.GetAxis("Vertical");
			}
			else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				h = ETCInput.GetAxis("Horizontal");
				v = ETCInput.GetAxis("Vertical");
			}

			use_direction = (screenMovementForward * v) + (screenMovementRight * h);
            if (System.Math.Abs(h) > 0f || System.Math.Abs(v) > 0f)
            {
                _Animator.SetFloat("speed", 10f);
            }
            else
            {
                _Animator.SetFloat("speed", 0f);
            }
        }else{
            _Animator.SetFloat("speed", 0f);
            Debug.Log("错误：角色处于控制模式却没有被适配相机。");
        }
        if (!_DATA_CENTER.IsGrounded())
            use_direction.y = -1;

        PositionUpdate();
    }

	public void PositionUpdate()
	{
        this.MoveByChangePosition(use_direction, use_speed, false);
        switch (this.moveDirection)
        {
            case AIMoveDirection.stay:
                this.RotateToDirection(use_direction, 20f, true);
                break;
            case AIMoveDirection.backTowardsEnemy:
                this.RotateToDirection(use_direction,20f, true);
                break;
            case AIMoveDirection.towardsEnemy:
                this.RotateToDirection(use_direction, 20f, true);
                break;
            case AIMoveDirection.EnemynoLeft:
                this.RotateToDirection(use_direction, 20f, true);
                break;
            case AIMoveDirection.EnemynoRight:
                this.RotateToDirection(use_direction, 20f, true);
                break;
            default:
                break;
        }
	}

	public override void AI_State_exit()
	{
        if (this._AIStateRunner.GetNowState() == this)
            return;
        use_speed = speed;
		time_counter = 0f;
    }
}

//关于受力移动，我们的逻辑是这样的：我们所设定的speed这个数值具体来说就是所期望的rigibody.velcoity.magitube，
// 这个speed我们称之为期望速度，而物体从静止所需要的期望速度越大，则一上来所需要的力则越大（更大加速度）
//因此在MoveTo函数里，我们直接把speed作为accelration参数给带入了其中。其实也许根据情况应该乘以个参数？但这里我们没有多想，总之表现出这个等比关系就点到为止了。
//而一旦物体达到了期望速度，我们做了件什么事情呢。。。那就是立刻给物体一个相反的，同样大小的力，与所施加的目标方向力达到个平衡，
//并且这个反向力中有个currentSpeed/maxSpeed参数，就是说越是超过期望速度，给的方向力就越大那么一点，从而确保两个方向的力完全一样，这就符合了初中时候所学习过的力学原理。
//所以总的来说，我们现在整套逻辑符合了“提速至期望速度，达到期望速度后施加反向力来与以平衡，期望速度越大，开始给的加速度就越大”这些我们所需要的逻辑，
//但可能在施予力大小等方面，需要根据整个场景尺寸比例等情况乘以一些固定参数，这个只要我们的游戏运行看着没大的问题也就不需要过多考虑
