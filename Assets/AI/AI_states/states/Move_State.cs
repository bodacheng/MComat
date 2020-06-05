using System.Collections.Generic;
using UnityEngine;
using Soul;

public enum AIMoveMode
{
    test = 0,
    normal = 1
}

public class Move_State : Behavior
{
    readonly float speed;
    readonly float time_limit;
    readonly AIMoveMode _AIMoveStyle;
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
    
    public Move_State(AIMoveMode aiMoveStyle, float speed, float time_limit)
	{
        _AIMoveStyle = aiMoveStyle;
        this.speed = speed;
		this.time_limit = time_limit;		
	}
    
	public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}
    
    bool Finished()
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
                if (Vector3.Distance(gameObject.transform.position, targetPos) < 0.2f)
                {
                    return true;
                }
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
        _Weapon_Animation_Events.ClearMarkerManagers();
        mainCam = CameraManager._camera.transform;
        Animation_Manger.PlayLayerAnim(null,true,0.05f);
        pEvents.CloseAllPersonalityEffects();
    }

    public override void AI_State_enter()// 整个enter阶段与状态运行中有关的就是决定use_direction和moveDirection。前者状态运行中会调整。
    {
        _Weapon_Animation_Events.ClearMarkerManagers();
        Sensor.ContinuousDetectionStart(-1);//movestate里希望对敌人的出现比较反应迅速。
        Animation_Manger.PlayLayerAnim(null,true,0.05f);
        // AI模式决定第一轮moveDirection和use_direction的
        // moveDirection是用来引导use_direction的
        DecideDirection();
        time_counter = 0f;
        mainCam = CameraManager._camera.transform;
        pEvents.CloseAllPersonalityEffects();
    }

    void DecideDirection()
    {
        EnemiesByDistance = Sensor.GetEnemiesByDistance(true);
        switch (_AIMoveStyle)
        {
            case AIMoveMode.normal:
                if (EnemiesByDistance.Count > 0)
                {
                    Collider threat = Sensor.GetSuddenThreatInRange(0, 5);
                    whereToGo = threat != null ? 5 : Random.Range(0, 5);
                    switch (whereToGo)
                    {
                        case 0:
                            moveDirection = AIMoveDirection.towardsEnemyRight;
                            if (EnemiesByDistance[0] != null)
                                use_direction = GetVerticalDir(EnemiesByDistance[0].transform.position - gameObject.transform.position) + (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized;
                            break;
                        case 1:
                            moveDirection = AIMoveDirection.towardsEnemyLeft;
                            if (EnemiesByDistance[0] != null)
                                use_direction = -GetVerticalDir(EnemiesByDistance[0].transform.position - gameObject.transform.position) + (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized;
                            break;
                        case 2:
                            moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 3:
                            moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 4:
                            moveDirection = AIMoveDirection.towardsEnemy;
                            break;
                        case 5:
                            moveDirection = AIMoveDirection.RunAwayFromThreat;
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
                            if (threat != null)
                            {
                                Vector3 vertical = GetVerticalDir(threat.transform.position - gameObject.transform.position);
                                use_direction = Random.Range(0, 2) == 1 ? vertical : -vertical;
                            }
                            else
                            {
                                use_direction = Vector3.zero;
                            }
                            break;
                        default:
                            moveDirection = AIMoveDirection.stay;
                            use_direction = Vector3.zero;
                            break;
                    }
                }
                else
                {
                    moveDirection = AIMoveDirection.stay;
                    use_direction = Vector3.zero;
                }
                break;
            case AIMoveMode.test:
                moveDirection = AIMoveDirection.stay;
                use_direction = Vector3.zero;
                break;
        }
    }

    int whereToGo;
    Vector3 targetPos;
    Transform closetEnemyT;
    public void _f_State_Update_SP()
    {
        time_counter += Time.fixedDeltaTime;
        
        if (!Sensor.IFContinuousDetectionStarted())
        {
            Sensor.ContinuousDetectionStart(-1);//这个的真正目的是把检测关闭
        }
        if (_BasicPhysicSupport.hiddenMethods.onBattleGroundBundary) //这一段指的是AI模式下走位的问题。
        {
            use_direction = _BasicPhysicSupport.hiddenMethods.antiWallDirection;
            return;
        }
        
        if (Finished())
        {
            DecideDirection();
            time_counter = 0f;
        }

        if (EnemiesByDistance.Count > 0)
        {
            closetEnemyT = EnemiesByDistance[0].transform;
            if (Vector3.Distance(closetEnemyT.position, gameObject.transform.position) < 1f)
                moveDirection = AIMoveDirection.stay;
        }
        
        switch (moveDirection)
        {
            case AIMoveDirection.stay:
                use_direction = Vector3.zero;
                break;
            case AIMoveDirection.backTowardsEnemy:
                break;
            case AIMoveDirection.towardsEnemy:
                targetPos = closetEnemyT.position + (gameObject.transform.position - closetEnemyT.position).normalized * _AIStateRunner.FixedSkillTriggerDis();
                targetPos.y = 0;
                use_direction = targetPos - gameObject.transform.position;                
                // 其实use_direction的计算非常恶心，因为实时算朝向特定敌人的话会产生个抖动问题，上面的结果效果差强人意，但比底下这些强。
                // 底下这些是一些失败的例子
                //use_direction = Quaternion.Euler(0, angle * Time.fixedDeltaTime / (Time.fixedDeltaTime + 1f), 0) * use_direction;
                //use_direction = Vector3.Lerp(use_direction, newDir, (angle / 45) * Time.deltaTime / (Time.deltaTime + 1f));
                //use_direction = (EnemiesByDistance[0].transform.position - gameObject.transform.position).normalized;
                break;
            case AIMoveDirection.RunAwayFromThreat:
                break;
        }
        use_direction = use_direction.normalized;
        Collider[] EnemyAndTeammateBetweenMeAndEnemy = Sensor.EnemyAndTeammateBetweenMeAndEnemy();
        if (EnemyAndTeammateBetweenMeAndEnemy != null)
        {
            Vector3 temp = (EnemyAndTeammateBetweenMeAndEnemy[1].transform.position - this.gameObject.transform.position).normalized +
            (gameObject.transform.position - EnemyAndTeammateBetweenMeAndEnemy[0].transform.position).normalized ;
            temp.y = 0;
            use_direction = Vector3.RotateTowards(use_direction, temp, 10 * Time.fixedDeltaTime, 0).normalized;//里面的参数都是些很微妙的东西
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
            RotateToDirection(use_direction,20f, true);
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
            RotateToDirection(use_direction,20f, true);
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