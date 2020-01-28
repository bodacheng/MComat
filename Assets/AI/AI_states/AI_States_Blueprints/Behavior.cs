using HittingDetection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Soul
{
    public abstract partial class Behavior
    {
        public GameObject gameObject;
        public Transform GeoCenterT;
        public Rigidbody _Rigidbody;
        public BehaviorRunner _AIStateRunner;
        public Data_Center _DATA_CENTER;
        public BO_Ani_E _BO_Ani_E;
        public FightAttriCalReference _FightAttriCalReference;
        public ResistanceManager _ResistanceManager;
        public BasicPhysicSupport _BasicPhysicSupport;
        public Sensor Sensor;
        public Controller controller;
        public Animator _Animator;
        public SkillCancelFlag _SkillCancelFlag;
        public BO_Weapon_Animation_Events _Weapon_Animation_Events;
        public ShaderManager shaderManager;
        public BehaviorType StateType;
        public bool nextAttackStateCanRushFirst;
        public Animation_Manger Animation_Manger;
        public BuffsRunner _BuffsRunner;
        public BlendShapeProxy blendShapeProxy;
        public Personality_events personality_Events;

        public float AT; //攻击力,或者说攻击力权重。这个设计的目的在于让所有技能的伤害可以在技能表里以一种形式直接设置。
        public string StateKey;
        public int splevel;
        public Inputs_defined enterInput = Inputs_defined.Null;
        public Inputs_defined exitInput = Inputs_defined.Null;
        public BehaviorEnterRange[] behaviorEnterRanges;

        protected BehaviorEnterRange[] InnerAndMidAndFarRanges = { BehaviorEnterRange.inner_range, BehaviorEnterRange.mid_range, BehaviorEnterRange.far_range };

        // Prepare for basic parameters here
        public virtual void Pre_process_before_enter()
        {
            this.gameObject = _DATA_CENTER.WholeT.gameObject;
            this.GeoCenterT = _DATA_CENTER.geometryCenter;
            this.Sensor = _DATA_CENTER.Sensor;
            this._FightAttriCalReference = _DATA_CENTER._FightAttriCalReference;
            this.shaderManager = _DATA_CENTER._ShaderManager;
            this._AIStateRunner = _DATA_CENTER._MyBehaviorRunner;
            this.Animation_Manger = _DATA_CENTER.Animation_Manger;
            this.controller = _DATA_CENTER.controller;
            this._SkillCancelFlag = _DATA_CENTER._SkillCancelFlag;
            this._BO_Ani_E = _DATA_CENTER._BO_Ani_E;
            this._Weapon_Animation_Events = _DATA_CENTER.bO_Weapon_Animation_Events;
            this._BasicPhysicSupport = _DATA_CENTER._BasicPhysicSupport;
            this._Animator = _BasicPhysicSupport.animator;
            this._Rigidbody = _BasicPhysicSupport.Rigidbody;
            this._ResistanceManager = _DATA_CENTER._ResistanceManager;
            this._BuffsRunner = _DATA_CENTER.buffsRunner;
            this.blendShapeProxy = _DATA_CENTER.blendShapeProxy;
            this.personality_Events = _DATA_CENTER.Personality_events;
        }

        // On what condition can we exit this state 
        public virtual bool Capacity_Exit_Condition()
        {
            return true;
        }

        public virtual bool Strategic_exit_condition()
        {
            return CheckExitCondition(strategic_exit_condition_code);
        }

        public virtual bool Capacity_enter_condition()
        {
            return this._FightAttriCalReference.HasPlentyGauge(splevel);
        }

        // On what condition we have to enter this state
        public virtual bool Force_enter_condition()
        {
            return false;
        }

        // Process when entering the state 
        public virtual void AI_State_enter()
        {
            Animation_Manger.SetAnimationPlayingStep(AnimationPlaying_Step.unstarted);
            _FightAttriCalReference.AT = this.AT;
            _FightAttriCalReference.CostCriticalGaugeBySPlevel(this.splevel);
        }
        
        // Process when entering the state 
        public virtual void AI_State_enter(V_Damage newValue)
        {
            Animation_Manger.SetAnimationPlayingStep(AnimationPlaying_Step.unstarted);
            _FightAttriCalReference.AT = this.AT;
            _FightAttriCalReference.CostCriticalGaugeBySPlevel(this.splevel);
        }

        public virtual void C_State_enter()
        {
            AI_State_enter();
        }
        
        public virtual void C_State_enter(V_Damage newValue)
        {
            AI_State_enter(newValue);
        }

        // Process when exit the state 
        public virtual void AI_State_exit()
        {
            Animation_Manger.SetAnimationPlayingStep(AnimationPlaying_Step.unstarted);
            Sensor.OneRoundDetectionStart(5);
        }
        
        // Local update of the state 
        public virtual void _State_FixedUpdate1()
        {
        }

        // Local update of the state 
        public virtual void _c_State_FixedUpdate1()
        {
            _State_FixedUpdate1();
        }

        // Local fixedupdate of the state 
        public virtual void _State_FixedUpdate2()
        {

        }

        // Local fixedupdate of the state 
        public virtual void _c_State_FixedUpdate2()
        {
            _State_FixedUpdate2();
        }
        
        #region state basic methods

        protected bool DetectApprovedEventAttack()
        {

            FightAttriCalReference BO_Health = gameObject.GetComponent<FightAttriCalReference>();
            if (BO_Health.ReturnApprovedEventAttackAttempts().Count > 0)
            {
                BO_Health.SetManagingEventDamage(BO_Health.ReturnApprovedEventAttackAttempts()[0]);
                BO_Health.ReturnApprovedEventAttackAttempts().Clear();
                BO_Health.GetManagingEventDamage().Position_set.run();
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void EventAttackEnderProcess()
        {
            FightAttriCalReference BO_Health = gameObject.GetComponent<FightAttriCalReference>();
            if (BO_Health.GetManagingEventDamage() != null)
            {
                BO_Health.GetManagingEventDamage().Position_set.end();
            }
            BO_Health.SetManagingEventDamage(null);
        }

        // If the state is based on the distance from the nearest enemy, check if the character is at the proper distance to enter the state
        bool inner;
        bool mid;
        bool far;
        protected bool CheckToEnemyDisEnterCondition(BehaviorEnterRange[] behaviorEnterRanges)
        {
            if (behaviorEnterRanges != null)
            {
                if (behaviorEnterRanges.Length == 0)
                    return true;
                inner = false;
                mid = false;
                far = false;
                for (int i = 0; i < behaviorEnterRanges.Length; i++)
                {
                    switch (behaviorEnterRanges[i])
                    {
                        case BehaviorEnterRange.inner_range:
                            inner |= this.Sensor.GetInnerEnemiesColliders().Count > 0;
                            break;
                        case BehaviorEnterRange.mid_range:
                            mid |= this.Sensor.GetMidEnemiesColliders().Count > 0;
                            break;
                        case BehaviorEnterRange.far_range:
                            far |= this.Sensor.GetFarEnemiesColliders().Count > 0;
                            break;
                        case BehaviorEnterRange.out_of_range:
                            if (this.Sensor.GetInnerEnemiesColliders().Count == 0
                                &&
                                this.Sensor.GetMidEnemiesColliders().Count == 0
                                &&
                                this.Sensor.GetFarEnemiesColliders().Count == 0)
                                return true;
                            break;
                        default:
                            break;
                    }
                }
                return inner || mid || far;
            }
            return true;
        }

        List<BehaviorEnterRange> temp;
        protected BehaviorEnterRange[] RangePlusOne(BehaviorEnterRange[] old)//这个东西的意思是，假设是连击情况下，那所有技能的触发范围可能有个修正，比如原本一个中程技能，连击情况下AI在近距离也可触发。
        {
            temp = old.ToList();
            if (temp.Contains(BehaviorEnterRange.inner_range) && !temp.Contains(BehaviorEnterRange.mid_range))
                temp.Add(BehaviorEnterRange.mid_range);
            if (temp.Contains(BehaviorEnterRange.mid_range) && !temp.Contains(BehaviorEnterRange.inner_range))
                temp.Add(BehaviorEnterRange.inner_range);
            if (temp.Contains(BehaviorEnterRange.far_range) && !temp.Contains(BehaviorEnterRange.mid_range))
                temp.Add(BehaviorEnterRange.mid_range);
            return temp.ToArray();
        }

        // Rotate to a target
        Vector3 look_dir;
        Quaternion dirQ;
        //返回带符号角度，用以判断往哪个方向摆头。
        protected float RotateToTarget(Vector3 target, float turnSpeed, bool ignoreY)
        {
            look_dir = target - gameObject.transform.position;
            if (ignoreY)
            {
                look_dir.y = 0;
            }
            dirQ = Quaternion.LookRotation(look_dir);
            dirQ = Quaternion.Slerp(gameObject.transform.rotation, dirQ, turnSpeed * Quaternion.Angle(dirQ, gameObject.transform.rotation) * Time.fixedDeltaTime);
            _Rigidbody.MoveRotation(dirQ);
            //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, dirQ, turnSpeed * Quaternion.Angle(dirQ, gameObject.transform.rotation) * Time.fixedDeltaTime);
            return Vector3.SignedAngle(_Rigidbody.transform.forward, look_dir, Vector3.up);
        }
        float angle;
        // Rotate to a direction
        protected bool RotateToDirection(Vector3 direction, float turnSpeed, bool ignoreY)
        {
            if (ignoreY)
            {
                direction.y = 0;
            }
            dirQ = Quaternion.LookRotation(direction);
            angle = Quaternion.Angle(dirQ, gameObject.transform.rotation);
            dirQ = Quaternion.Slerp(gameObject.transform.rotation, dirQ, angle*(360-angle)/(180*180/turnSpeed) * Time.fixedDeltaTime);
            _Rigidbody.MoveRotation(dirQ);
            return Mathf.Approximately(Quaternion.Angle(dirQ, gameObject.transform.rotation), 0f);
        }

        // Move to direction
        //public float Move(Vector3 relativePos, float acceleration, bool ignoreY) {
        //    if (ignoreY)
        //        relativePos.y = 0;
        //    gameObject.GetComponent<Rigidbody>().AddForce(relativePos.normalized * acceleration * Time.deltaTime, ForceMode.VelocityChange);
        //    return gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //}

        Vector3 v;
        public float Move(Vector3 relativePos, float acceleration, bool ignoreY)
        {
            if (_Rigidbody == null)
                return 0;
            if (ignoreY)
            {
                relativePos.y = 0;
            }
            v = relativePos.normalized * acceleration;
            //_Rigidbody.AddForce(v, ForceMode.VelocityChange);
            _Rigidbody.velocity = v;
            return _Rigidbody.velocity.magnitude;
        }

        public float use_acc;
        public float Move_AddForce(Vector3 forcedirection, float acceleration, bool ignoreY)//废函数。
        {
            if (this._Rigidbody == null)
                return 0f;
            if (ignoreY)
                forcedirection.y = 0;

            use_acc += (acceleration - _Rigidbody.velocity.magnitude) * 2;
            _Rigidbody.AddForce(use_acc * forcedirection.normalized);
            return _Rigidbody.velocity.magnitude;
        }

        public void MoveByChangePosition(Vector3 relativePos, float acceleration, bool ignoreY)
        {
            if (ignoreY)
            {
                relativePos.y = 0;
            }
            v = relativePos;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                                         gameObject.transform.position + v,
                                                         Time.deltaTime * acceleration);
        }

        // apply friction to rigidbody, and make sure it doesn't exceed its max speed
        public void ManageSpeed(Rigidbody rigidbody, float maxSpeed, bool ignoreY)
        {
            if (rigidbody == null)
                return;
            Vector3 currentSpeed = rigidbody.velocity;
            if (ignoreY)
            {
                currentSpeed.y = 0;
            }
            if (currentSpeed.magnitude > maxSpeed)
            {
                rigidbody.AddForce((currentSpeed.magnitude / maxSpeed * -1) * currentSpeed.normalized * maxSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        float current_speed;
        public void ClampVelocity(float max_speed)
        {
            current_speed = _Rigidbody.velocity.magnitude;
            if (current_speed > max_speed)
            {
                v = (max_speed / current_speed) * _Rigidbody.velocity;
                _Rigidbody.velocity = v;
            }
        }

        // Time.deltaTime / (0.5f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。
        // Quaternion.Angle(gameObject.transform.rotation, dirQ)
        // Rotate to velocity
        Vector3 dir;
        Quaternion slerp;
        public void RotateToVelocity(float turnSpeed, bool ignoreY)
        {
            dir = _Rigidbody.velocity;
            if (ignoreY)
                dir.y = 0;
            dirQ = Quaternion.LookRotation(dir);
            dirQ = Quaternion.Slerp(gameObject.transform.rotation, dirQ, turnSpeed * Quaternion.Angle(dirQ, gameObject.transform.rotation) * Time.fixedDeltaTime);
            _Rigidbody.MoveRotation(dirQ);
        }

        // RotateToVelocity in reverse
        public void RotateToVelocityNegative(float turnSpeed, bool ignoreY)
        {
            dir = ignoreY ? -new Vector3(this._Rigidbody.velocity.x, 0f, this._Rigidbody.velocity.z) : -this._Rigidbody.velocity;

            if (dir.magnitude > 0.1)
            {
                dirQ = Quaternion.LookRotation(dir);
                slerp = Quaternion.Slerp(gameObject.transform.rotation, dirQ, dir.magnitude * turnSpeed * Time.deltaTime);
                _Rigidbody.MoveRotation(slerp);
            }
        }

        float f_temp;
        Vector3 v_temp;
        protected Vector3 CalFixPosDestination(Vector3 damageHappenPoint, 
                                                Vector3 attackerTransform_foward,
                                                 Vector3 attackerTransform_pos, 
                                                  Vector3 victimT_pos, WeaponPosAdjustMode weaponPosAdjustMode)
        {
            switch (weaponPosAdjustMode)
            {
                case WeaponPosAdjustMode.draw:
                    return damageHappenPoint;
                case WeaponPosAdjustMode.pushToMidForward:
                    damageHappenPoint.y = 0;
                    f_temp = Vector3.Dot(damageHappenPoint - attackerTransform_pos, attackerTransform_foward);
                    if (f_temp > 0 && Vector3.Distance(attackerTransform_pos,victimT_pos) < FightGlobalSetting._attackDrawingDistance)
                    {
                        v_temp = f_temp * attackerTransform_foward + attackerTransform_pos;//+ (touchingEnemyBody ? attackerTransform_foward : Vector3.zero);
                        return v_temp;
                    }
                    else
                    {
                        return CalFixPosDestination(damageHappenPoint,attackerTransform_foward,attackerTransform_pos,victimT_pos, WeaponPosAdjustMode.explosion);
                    }
                case WeaponPosAdjustMode.explosion:
                    v_temp = (victimT_pos - damageHappenPoint).normalized;
                    v_temp.y = 0;
                    v_temp = v_temp + victimT_pos;
                    return v_temp;
                default:
                    return victimT_pos;
            }
        }

        // compare two Quaternions
        public bool CompareQuaternionApproximately(Quaternion A, Quaternion B)
        {
            return Mathf.Approximately(A.x, B.x)
                &&
                Mathf.Approximately(A.y, B.y)
                &&
                Mathf.Approximately(A.z, B.z)
                &&
                Mathf.Approximately(A.w, B.w)
                ? true
                : false;
        }

        public bool IfVectorClean(Vector3 rot)
        {
            return rot != Vector3.zero && !float.IsNaN(rot.x) && !float.IsNaN(rot.y) && !float.IsNaN(rot.z) && !float.IsInfinity(rot.x) && !float.IsInfinity(rot.y) && !float.IsInfinity(rot.z);
        }
        #endregion
    }
}