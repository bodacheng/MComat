using System.Collections.Generic;
using UnityEngine;
using Soul;

public enum AIMoveStyle
{
    test = 0,
    normal = 1
}

public class Move_State : AI_State
{
    readonly float speed;
    readonly float time_limit;
    readonly AIMoveStyle _AIMoveStyle;
    float time_counter;
    Vector3 use_direction;
    AIMoveDirection moveDirection;
    Transform mainCam;
    Quaternion screenMovementSpace;
    Vector3 screenMovementForward, screenMovementRight;
    List<GameObject> EnemiesByDistance;

    enum AIMoveDirection
    {
        stay = 0,
        towardsEnemy = 1,
        backTowardsEnemy = 2,
        towardsEnemyRight = 6,
        towardsEnemyLeft = 7,
        RunAwayFromThreat = 5
    }

    public Move_State(AIMoveStyle aiMoveStyle, float speed, float time_limit)
	{
        _AIMoveStyle = aiMoveStyle;
        this.speed = speed;
		this.time_limit = time_limit;		
	}

	public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

    public override bool Enter_condition_priority3()
    {
        return true;
    }

    public override bool Strategic_exit_condition()
    {
        return Sensor.GetInnerEnemiesColliders().Count > 0 || Sensor.GetNearbyDamagingWeaponColliders().Count > 0 || Sensor.GetOutterDamagingWeaponColliders().Count > 0;
    }

    bool Timeup()
    {
        switch (moveDirection)
        {
            case AIMoveDirection.backTowardsEnemy:
                if (time_counter > time_limit / 2)
                    return true;
                break;
            case AIMoveDirection.RunAwayFromThreat:
                if (time_counter > time_limit / 3)
                    return true;
                break;
            case AIMoveDirection.stay:
                if (time_counter > time_limit / 2)
                    return true;
                break;
            case AIMoveDirection.towardsEnemy:
                if (time_counter > time_limit)
                    return true;
                break;
            case AIMoveDirection.towardsEnemyLeft:
                if (time_counter > time_limit / 3)
                    return true;
                break;
            case AIMoveDirection.towardsEnemyRight:
                if (time_counter > time_limit / 3)
                    return true;
                break;
        }
        return false;
    }

    public override void C_State_enter()
    {
        time_counter = 0f;
        this._Weapon_Animation_Events.ClearMarkerManagers();        
        this.mainCam = CameraManager._camera.transform;
        this.Animation_Manger.PlayLayerAnim(null);
        this.personality_Events.CloseAllPersonalityEffects();
    }

    public override void AI_State_enter()// 整个enter阶段与状态运行中有关的就是决定use_direction和moveDirection。前者状态运行中会调整。
    {
        this._Weapon_Animation_Events.ClearMarkerManagers();
        this.Sensor.ContinuousDetectionStart(-1);//movestate里希望对敌人的出现比较反应迅速。
        this.Animation_Manger.PlayLayerAnim(null);

        // 从这到底下那么也就是AI模式决定第一轮moveDirection和use_direction的
        // 而moveDirection是用来引导use_direction的
        DecideDirection();
        time_counter = 0f;
        this.mainCam = CameraManager._camera.transform;
        this.personality_Events.CloseAllPersonalityEffects();
    }

    void DecideDirection()
    {
        EnemiesByDistance = Sensor.GetEnemiesByDistance(true);
        switch (_AIMoveStyle)
        {
            case AIMoveStyle.normal:
                if (EnemiesByDistance.Count > 0)
                {
                    whereToGo = Sensor.GetNearbyDamagingWeaponColliders().Count > 0 && Sensor.GetNearbyDamagingWeaponColliders()[0] != null ? 5 : Random.Range(0, 5);

                    switch (whereToGo)
                    {
                        case 0:
                            this.moveDirection = AIMoveDirection.towardsEnemyRight;
                            if (EnemiesByDistance[0] != null)
                                use_direction = GetVerticalDir(EnemiesByDistance[0].transform.position - this.gameObject.transform.position)
                                    + (EnemiesByDistance[0].transform.position - this.gameObject.transform.position).normalized;
                            break;
                        case 1:
                            this.moveDirection = AIMoveDirection.towardsEnemyLeft;
                            if (EnemiesByDistance[0] != null)
                                use_direction = -GetVerticalDir(EnemiesByDistance[0].transform.position - this.gameObject.transform.position)
                                     + (EnemiesByDistance[0].transform.position - this.gameObject.transform.position).normalized;
                            break;
                        case 2:
                            this.moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 3:
                            this.moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 4:
                            this.moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 5:
                            this.moveDirection = AIMoveDirection.RunAwayFromThreat;
                            //this.Sensor.SensorDetectMyTeam();
                            //List<Collider> myteamms = this.Sensor.getMyTeammatesNearby();
                            //if (myteamms.Count > 0)
                            //{
                            //    Vector3 vertical = GetVerticalDir(Sensor.getNearbyDamagingWeaponColliders()[0].transform.position - this.gameObject.transform.position);
                            //    Vector3 vertical_r = -vertical;
                            //    Vector3 toTeammates = myteamms[0].transform.position - this.gameObject.transform.position;
                            //    use_direction = Vector3.Angle(toTeammates, vertical) > Vector3.Angle(toTeammates, vertical_r) ? vertical : vertical_r;
                            //}
                            //else{
                            if (Sensor.GetNearbyDamagingWeaponColliders().Count > 0 && Sensor.GetNearbyDamagingWeaponColliders()[0] != null)
                            {
                                Vector3 vertical = GetVerticalDir(Sensor.GetNearbyDamagingWeaponColliders()[0].transform.position - this.gameObject.transform.position);
                                use_direction = (int)Random.Range(0, 2) == 1 ? vertical : -vertical;
                            }
                            else
                            {
                                use_direction = Vector3.zero;
                            }
                            break;
                        default:
                            this.moveDirection = AIMoveDirection.stay;
                            use_direction = Vector3.zero;
                            break;
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
    }

    int whereToGo;
    float angle;
    Vector3 newDir;
    public void _f_State_Update_SP()
    {
        time_counter += Time.fixedDeltaTime;
        
        if (!Sensor.IFContinuousDetectionStarted())
        {
            Sensor.ContinuousDetectionStart(-1);//这个的真正目的是把检测关闭
        }
        if (_DATA_CENTER.onBattleGroundBundary) //这一段指的是AI模式下走位的问题。
        {
            use_direction = _DATA_CENTER.antiWallDirection;
            return;
        }
        if (Timeup())
        {
            DecideDirection();
            time_counter = 0f;
        }
        switch (moveDirection)
        {
            case AIMoveDirection.stay:
                use_direction = Vector3.zero;
                break;
            case AIMoveDirection.backTowardsEnemy:
                break;
            case AIMoveDirection.towardsEnemy:
                if (EnemiesByDistance.Count > 0)
                {
                    newDir = use_direction += (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized;
                    angle = Vector3.Angle(use_direction, newDir);
                    use_direction = Vector3.Lerp(use_direction, newDir,angle / 45 * Time.deltaTime);
                    // 其实use_direction的计算非常恶心，因为实时算朝向特定敌人的话会产生个抖动问题，上面的结果效果差强人意，但比底下这些强。
                    // 底下这些是一些失败的例子
                    //use_direction = Quaternion.Euler(0, angle * Time.fixedDeltaTime / (Time.fixedDeltaTime + 1f), 0) * use_direction;
                    //use_direction = Vector3.Lerp(use_direction, newDir, (angle / 45) * Time.deltaTime / (Time.deltaTime + 1f));
                    //use_direction = (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized;
                }
                break;
            case AIMoveDirection.RunAwayFromThreat:
                break;
        }
        use_direction = use_direction.normalized;
        Collider[] EnemyAndTeammateBetweenMeAndEnemy = Sensor.EnemyAndTeammateBetweenMeAndEnemy();
        if (EnemyAndTeammateBetweenMeAndEnemy != null)
        {
            newDir = (EnemyAndTeammateBetweenMeAndEnemy[1].transform.position - this.gameObject.transform.position).normalized +
            (gameObject.transform.position - EnemyAndTeammateBetweenMeAndEnemy[0].transform.position).normalized ;
            newDir.y = 0;
            use_direction = Vector3.RotateTowards(use_direction, newDir, 10 * Time.fixedDeltaTime, 0).normalized;//里面的参数都是些很微妙的东西
        }
    }

    float h;
    float v;
    Vector3 vel;
    public void _c_State_Update_SP()
    {
        if (Sensor.IFContinuousDetectionStarted())
        {
            Sensor.OneRoundDetectionStart(1);
        }

        if (mainCam != null)
		{
			//get movement axis relative to camera
			screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
			screenMovementForward = screenMovementSpace * Vector3.forward;
			screenMovementRight = screenMovementSpace * Vector3.right;
			//get movement input, set direction to move in
			h = 0f;
			v = 0f;
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
            || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
                h = UnityEngine.Input.GetAxis("Horizontal");
                v = UnityEngine.Input.GetAxis("Vertical");
				//h = ETCInput.GetAxis("Horizontal");
				//v = ETCInput.GetAxis("Vertical");
			}
			else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				h = ETCInput.GetAxis("Horizontal");
				v = ETCInput.GetAxis("Vertical");
			}
			use_direction = (screenMovementForward * v) + (screenMovementRight * h);
        }else{
            Debug.Log("错误：角色处于控制模式却没有被适配相机。");
        }
    }

    public override void _c_State_FixedUpdate1()
    {
        _c_State_Update_SP();
        use_direction = use_direction.normalized;
        if (use_direction.magnitude > 0.1f)
        {
            _Animator.SetFloat("speed", 10f);
            Move(use_direction, speed, true);
            RotateToDirection(use_direction,10f, true);
        }
        else
        {
            _Animator.SetFloat("speed", 0f);
            _Rigidbody.velocity = Vector3.zero;
        }
    }

    public override void _State_FixedUpdate1()
	{
        _f_State_Update_SP();
        use_direction = use_direction.normalized;
        if (use_direction.magnitude > 0.1f)
        {
            _Animator.SetFloat("speed", 10f);
            Move(use_direction, speed, true);
            RotateToDirection(use_direction,10f, true);
        }else{
            _Animator.SetFloat("speed", 0f);
            _Rigidbody.velocity = Vector3.zero;
        }
	}

    /// <summary>
    /// 获取某向量的垂直向量
    /// </summary>
    Vector3 GetVerticalDir(Vector3 _dir)
    {
        //（_dir.x,_dir.z）与（？，1）垂直，则_dir.x * ？ + _dir.z * 1 = 0
        return Mathf.Approximately(_dir.z, 0) ? new Vector3(0, 0, -1) : new Vector3(-_dir.z / _dir.x, 0, 1).normalized;
    }
}

//以下评论所描述的企划我们18年12月已经放弃了。
//关于受力移动，我们的逻辑是这样的：我们所设定的speed这个数值具体来说就是所期望的rigibody.velcoity.magitube，
// 这个speed我们称之为期望速度，而物体从静止所需要的期望速度越大，则一上来所需要的力则越大（更大加速度）
//因此在MoveTo函数里，我们直接把speed作为accelration参数给带入了其中。其实也许根据情况应该乘以个参数？但这里我们没有多想，总之表现出这个等比关系就点到为止了。
//而一旦物体达到了期望速度，我们做了件什么事情呢。。。那就是立刻给物体一个相反的，同样大小的力，与所施加的目标方向力达到个平衡，
//并且这个反向力中有个currentSpeed/maxSpeed参数，就是说越是超过期望速度，给的方向力就越大那么一点，从而确保两个方向的力完全一样，这就符合了初中时候所学习过的力学原理。
//所以总的来说，我们现在整套逻辑符合了“提速至期望速度，达到期望速度后施加反向力来与以平衡，期望速度越大，开始给的加速度就越大”这些我们所需要的逻辑，
//但可能在施予力大小等方面，需要根据整个场景尺寸比例等情况乘以一些固定参数，这个只要我们的游戏运行看着没大的问题也就不需要过多考虑
