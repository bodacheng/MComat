using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Soul
{
    public abstract class AI_State
    {
        public GameObject gameObject;
        public Transform GeoCenterT;
        public Rigidbody _Rigidbody;
        public AIStateRunner _AIStateRunner;
        public Data_Center _DATA_CENTER;
        public BO_Ani_E _BO_Ani_E;
        public BO_Health BS_Main_Health;
        public ResistanceManager _ResistanceManager;
        public Pusher _Pusher;
        public Sensor Sensor;
        public inputManager _inputManager;
        public Animator _Animator;
        public SkillCancelFlag _SkillCancelFlag;
        public BO_Weapon_Animation_Events _Weapon_Animation_Events;
        public ShaderManager shaderManager;
        public stateType StateType;
        public bool nextAttackStateCanRushFirst = false;
        public Animation_Manger Animation_Manger;
        public BuffsRunner _BuffsRunner;
        public BlendShapeProxy blendShapeProxy;

        public float AT;//攻击力

        public string StateKey;
        public int splevel;
        public inputs_defined enterInput = inputs_defined.Null;
        public inputs_defined exitInput = inputs_defined.Null;
        public behaviorEnterRange[] behaviorEnterRanges;

        protected behaviorEnterRange[] InnerAndMidAndFarRanges = new behaviorEnterRange[3] { behaviorEnterRange.inner_range, behaviorEnterRange.mid_range, behaviorEnterRange.far_range };

        // Prepare for basic parameters here
        public virtual void pre_process_before_enter()
        {
            this._DATA_CENTER = GeoCenterT.GetComponent<Data_Center>();
            this.Sensor = _DATA_CENTER.Sensor;
            this.BS_Main_Health = _DATA_CENTER.BO_Health;
            this.shaderManager = _DATA_CENTER._ShaderManager;
            this._AIStateRunner = _DATA_CENTER.AIStateRunner;
            this.Animation_Manger = _DATA_CENTER.Animation_Manger;
            this._inputManager = this._AIStateRunner._inputManager;
            this._Animator = _DATA_CENTER.animator;
            this._SkillCancelFlag = _DATA_CENTER._SkillCancelFlag;
            this._BO_Ani_E = _DATA_CENTER._BO_Ani_E;
            this._Weapon_Animation_Events = _DATA_CENTER.bO_Weapon_Animation_Events;
            this._Rigidbody = _DATA_CENTER.Rigidbody;
            this._Pusher = _DATA_CENTER.pusher;
            this._ResistanceManager = _DATA_CENTER._ResistanceManager;
            this._BuffsRunner = _DATA_CENTER.buffsRunner;
            this.blendShapeProxy = _DATA_CENTER.blendShapeProxy;
        }

        // On what condition can we exit this state 
        public virtual bool capacity_exit_condition()
        {
            return true;
        }

        public virtual bool strategic_exit_condition()
        {
            return true;
        }

        public virtual bool Capacity_enter_condition()
        {
            return this.BS_Main_Health.hasPlentyGauge(this.splevel);
        }

        //一个状态的决策性进入条件与决策性退出条件如果没有形成一个真正意义上一正一反的关系，那么就会产生“无限进入进出循环”
        // 这个在移动状态和防御状态上我们都出现过。
        public virtual bool enter_condition_priority1()
        {
            return false;
        }

        public virtual bool enter_condition_priority2()
        {
            return false;
        }

        public virtual bool enter_condition_priority3()
        {
            return false;
        }

        // On what condition we have to enter this state
        public virtual bool force_enter_condition()
        {
            return false;
        }

        // Process when entering the state 
        public virtual void AI_State_enter()
        {
            Animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.unstarted);
            BS_Main_Health.AT = this.AT;
            this.BS_Main_Health.costCriticalGaugeBySPlevel(this.splevel);
        }

        public virtual void c_State_enter()
        {
            this.AI_State_enter();
        }

        // Process when exit the state 
        public virtual void AI_State_exit()
        {
            Animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.unstarted);
            this.Sensor.OneRoundDetectionStart(5);
        }
        
        // Local update of the state 
        public virtual void _State_FixedUpdate1()
        {
        }

        // Local update of the state 
        public virtual void _c_State_FixedUpdate1()
        {
            this._State_FixedUpdate1();
        }

        // Local fixedupdate of the state 
        public virtual void _State_FixedUpdate2()
        {

        }

        // Local fixedupdate of the state 
        public virtual void _c_State_FixedUpdate2()
        {
            this._State_FixedUpdate2();
        }

        #region state basic methods

        protected bool DetectApprovedEventAttack()
        {

            BO_Health BO_Health = gameObject.GetComponent<BO_Health>();
            if (BO_Health.returnApprovedEventAttackAttempts().Count > 0)
            {
                BO_Health.setManagingEventDamage(BO_Health.returnApprovedEventAttackAttempts()[0]);
                BO_Health.returnApprovedEventAttackAttempts().Clear();
                BO_Health.getManagingEventDamage().Position_set.run();
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void eventAttackEnderProcess()
        {

            BO_Health BO_Health = gameObject.GetComponent<BO_Health>();
            if (BO_Health.getManagingEventDamage() != null)
            {
                BO_Health.getManagingEventDamage().Position_set.end();
            }
            BO_Health.setManagingEventDamage(null);
        }

        // If the state is based on the distance from the nearest enemy, check if the character is at the proper distance to enter the state
        bool inner;
        bool mid;
        bool far;
        protected bool checkToEnemyDisEnterCondition(behaviorEnterRange[] behaviorEnterRanges)
        {
            if (behaviorEnterRanges != null)
            {
                if (behaviorEnterRanges.Length == 0)
                    return true;
                else
                {
                    inner = false;
                    mid = false;
                    far = false;
                    for (int i = 0; i < behaviorEnterRanges.Length; i++)
                    {
                        switch (behaviorEnterRanges[i])
                        {
                            case behaviorEnterRange.inner_range:
                                if (this.Sensor.getInnerEnemiesColliders().Count > 0)
                                    inner = true;
                                break;
                            case behaviorEnterRange.mid_range:
                                if (this.Sensor.getMidEnemiesColliders().Count > 0)
                                    mid = true;
                                break;
                            case behaviorEnterRange.far_range:
                                if (this.Sensor.getfarEnemiesColliders().Count > 0)
                                    far = true;
                                break;
                            case behaviorEnterRange.out_of_range:
                                if (this.Sensor.getInnerEnemiesColliders().Count == 0
                                    &&
                                    this.Sensor.getMidEnemiesColliders().Count == 0
                                    &&
                                    this.Sensor.getfarEnemiesColliders().Count == 0)
                                    return true;
                                break;
                            default:
                                break;
                        }
                    }
                    return inner || mid || far;
                }
            }
            return true;
        }

        List<behaviorEnterRange> temp;
        protected behaviorEnterRange[] RangePlusOne(behaviorEnterRange[] old)//这个东西的意思是，假设是连击情况下，那所有技能的触发范围可能有个修正，比如原本一个中程技能，连击情况下AI在近距离也可触发。
        {
            temp = old.ToList();
            if (temp.Contains(behaviorEnterRange.inner_range) && !temp.Contains(behaviorEnterRange.mid_range))
                temp.Add(behaviorEnterRange.mid_range);
            if (temp.Contains(behaviorEnterRange.mid_range) && !temp.Contains(behaviorEnterRange.inner_range))
                temp.Add(behaviorEnterRange.inner_range);
            if (temp.Contains(behaviorEnterRange.far_range) && !temp.Contains(behaviorEnterRange.mid_range))
                temp.Add(behaviorEnterRange.mid_range);
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
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, dirQ, turnSpeed * Quaternion.Angle(dirQ, gameObject.transform.rotation) * Time.fixedDeltaTime);
            return Vector3.SignedAngle(gameObject.transform.forward, look_dir, Vector3.up);
        }

        // Rotate to a direction
        protected bool RotateToDirection(Vector3 direction, float turnSpeed, bool ignoreY)
        {
            if (ignoreY)
            {
                direction.y = 0;
            }
            dirQ = Quaternion.LookRotation(direction);
            gameObject.transform.rotation = Quaternion.Slerp
                (gameObject.transform.rotation, dirQ, turnSpeed * Quaternion.Angle(dirQ, gameObject.transform.rotation) * Time.deltaTime);
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
            if (this._Rigidbody == null)
                return 0;
            if (ignoreY)
            {
                relativePos.y = 0;
            }
            v = relativePos.normalized * acceleration;
            //gameObject.GetComponent<Rigidbody>().AddForce(relativePos.normalized * acceleration * Time.deltaTime, ForceMode.VelocityChange);
            this._Rigidbody.velocity = v;
            return this._Rigidbody.velocity.magnitude;
        }

        public float use_acc = 0;
        public float Move_AddForce(Vector3 forcedirection, float acceleration, bool ignoreY)//废函数。
        {
            if (this._Rigidbody == null)
                return 0f;
            if (ignoreY)
                forcedirection.y = 0;

            use_acc += (acceleration - _Rigidbody.velocity.magnitude) * 2;
            _Rigidbody.AddForce(use_acc * forcedirection.normalized);

            return this._Rigidbody.velocity.magnitude;
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
        public void clampVelocity(float max_speed)
        {
            current_speed = this._Rigidbody.velocity.magnitude;
            if (current_speed > max_speed)
            {
                v = (max_speed / current_speed) * this._Rigidbody.velocity;
                this._Rigidbody.velocity = v;
            }
        }

        // Time.deltaTime / (0.5f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。
        // Quaternion.Angle(gameObject.transform.rotation, dirQ)
        // Rotate to velocity
        Vector3 dir;
        Quaternion slerp;
        public void RotateToVelocity(float turnSpeed, bool ignoreY)
        {
            dir = this._Rigidbody.velocity;
            if (ignoreY)
                dir.y = 0;

            if (dir.magnitude > 0.5f) //这个条件貌似是邪门了的起到一定的防抖动作用。
            {
                dirQ = Quaternion.LookRotation(dir);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, dirQ,
                                                                 turnSpeed *
                                                                 //Quaternion.Angle(gameObject.transform.rotation, dirQ) *
                                                                 (Time.fixedDeltaTime / (Time.fixedDeltaTime + 0.1f)));
                //gameObject.GetComponent<Rigidbody>().MoveRotation(slerp);
            }
        }

        // RotateToVelocity in reverse
        public void RotateToVelocityNegative(float turnSpeed, bool ignoreY)
        {
            if (ignoreY)
                dir = -new Vector3(this._Rigidbody.velocity.x, 0f, this._Rigidbody.velocity.z);
            else
                dir = -this._Rigidbody.velocity;

            if (dir.magnitude > 0.1)
            {
                dirQ = Quaternion.LookRotation(dir);
                slerp = Quaternion.Slerp(gameObject.transform.rotation, dirQ, dir.magnitude * turnSpeed * Time.deltaTime);
                this._Rigidbody.MoveRotation(slerp);
            }
        }

        // compare two Quaternions
        public bool compareQuaternionApproximately(Quaternion A, Quaternion B)
        {
            if (Mathf.Approximately(A.x, B.x)
                &&
                Mathf.Approximately(A.y, B.y)
                &&
                Mathf.Approximately(A.z, B.z)
                &&
                Mathf.Approximately(A.w, B.w)
               )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ifVectorClean(Vector3 rot)
        {
            if (rot == Vector3.zero)
                return false;

            if (float.IsNaN(rot.x) || float.IsNaN(rot.y) || float.IsNaN(rot.z))
            {
                return false;
            }
            if (float.IsInfinity(rot.x) || float.IsInfinity(rot.y) || float.IsInfinity(rot.z))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}

// The process state of the corotine of a AI_State
public enum AnimationPlaying_Step
{
    unstarted = 1,
    running = 2,
    over = 3
}