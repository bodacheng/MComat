using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public enum AIMoveMode
    {
        test = 0,
        normal = 1
    }

    public class Move_State : Behavior
    {
        readonly float speed;
        readonly float time_limit;
        public AIMoveMode _AIMoveStyle;
        float time_counter;
        Vector3 use_direction;
        AIMoveDirection moveDirection;
        Transform mainCam;
        Quaternion screenMovementSpace;
        Vector3 screenMovementForward, screenMovementRight;
        List<GameObject> EnemiesByDistance = new List<GameObject>();

        enum AIMoveDirection
        {
            stay,
            towardsEnemy,
            backTowardsEnemy,
            towardsEnemyRight,
            towardsEnemyLeft,
            RunAwayFromThreat,
            RunToBattleGroundCenter
        }

        public Move_State(AIMoveMode aiMoveStyle, float _speed, float _time_limit)
        {
            _AIMoveStyle = aiMoveStyle;
            speed = _speed;
            time_limit = _time_limit;
        }

        public override bool Capacity_enter_condition()
        {
            return true;
        }

        void CommonEnter()
        {
            time_counter = 0f;
            _Animator.applyRootMotion = false;
            _Weapon_Animation_Events.ClearMarkerManagers();
            Animation_Manger.PlayLayerAnim(null, true, 0.05f);
            pEvents.CloseAllPersonalityEffects();
            mainCam = CameraManager._camera.transform;
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _BasicPhysicSupport.Rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        public override void C_State_enter()
        {
            CommonEnter();
        }

        // 整个enter阶段与状态运行中有关的就是决定use_direction和moveDirection。前者状态运行中会调整。
        public override void AI_State_enter()
        {
            CommonEnter();
            Sensor.ContinuousDetectionStart(-1);
            DecideDirection();
        }

        // Process when exit the state 
        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _BasicPhysicSupport.Rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
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
                case AIMoveDirection.RunToBattleGroundCenter:
                    if (time_counter > time_limit / 3)
                        return true;
                    break;
            }
            return false;
        }

        void DecideDirection()
        {
            if (_BasicPhysicSupport.atRing && moveDirection == AIMoveDirection.RunToBattleGroundCenter)
            {
                // 非常粗糙的逻辑。意思是只要在边界并且已经是在往边界移动了，就不用再重新决策往哪跑了。和213行的逻辑相匹配
                return;
            }
            EnemiesByDistance = Sensor.GetEnemiesByDistance(true);
            switch (_AIMoveStyle)
            {
                case AIMoveMode.normal:
                    if (_BasicPhysicSupport.atRing)
                    {
                        moveDirection = AIMoveDirection.RunToBattleGroundCenter;
                        use_direction = Vector3.zero - gameObject.transform.position;
                        use_direction.y = 0;
                    }
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
                            case 3:
                            case 4:
                                moveDirection = AIMoveDirection.towardsEnemy;
                                if (closetEnemyT != null)
                                {
                                    targetPos = closetEnemyT.position + (gameObject.transform.position - closetEnemyT.position).normalized * _AIStateRunner.FixedSkillTriggerDis();
                                    targetPos.y = 0;
                                }
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
            if (_BasicPhysicSupport.atRing)
            {
                DecideDirection();
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
                    use_direction = closetEnemyT.position - gameObject.transform.position;
                    break;
                case AIMoveDirection.towardsEnemy:
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
                (gameObject.transform.position - EnemyAndTeammateBetweenMeAndEnemy[0].transform.position).normalized;
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

                h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("joystick");
                v = Input.GetAxis("Vertical") + UltimateJoystick.GetVerticalAxis("joystick");

                use_direction = (screenMovementForward * v) + (screenMovementRight * h);
            }
            else
            {
                Debug.Log("错误：角色处于控制模式却没有被适配相机。");
            }
        }

        public override void _c_State_FixedUpdate1()
        {
            _c_State_Update_SP();
            use_direction = use_direction.normalized;
            if (!MobileInputsManager.target.BeingControl(_AIStateRunner))
            {
                use_direction = Vector3.zero;
            }
            if (use_direction.magnitude > 0.1f)
            {
                _Animator.SetFloat("speed", 10f);
                Move(use_direction, speed, true);
                RotateToDirection(use_direction, 20f, true);
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
                RotateToDirection(use_direction, 20f, true);
            }
            else
            {
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
}