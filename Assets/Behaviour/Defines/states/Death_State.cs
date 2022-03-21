using UnityEngine;
using HittingDetection;
using Skill;

namespace Soul
{
    //死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
    public class Death_State : Behavior
    {
        float time_count;
        Vector3 _xz;
        bool touchedBoundary;
        bool dropped;

        public Death_State()
        {
            StateType = BehaviorType.KnockOff;
        }

        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }

        public override bool Force_enter_condition()
        {
            return false;
        }

        public override void AI_State_enter(V_Damage newValue)
        {
            base.AI_State_enter();
            time_count = 0f;
            pEvents.CloseAllPersonalityEffects();
            _DATA_CENTER.FightDataRef.IsDead.Value = true;
            time_counter = 0;
            FightParamsRef.ChangeLayerForLimbs(14);
            _Rigidbody.velocity = Vector3.zero;
            _Animator.SetFloat("speed", 0f);
            _Animator.applyRootMotion = false;
            //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(), true, 0.05f);
            EffectsManager.GenerateEffect("super_hit", FightGlobalSetting.EffectPathDefine(newValue.from_weapon.element), newValue.DamageEffectPoint, gameObject.transform.rotation, FightParamsRef.Center.geometryCenter);
            touchedBoundary = false;
            dropped = false;
            _xz = CalFixPushPos(newValue.impactComingPoint,  newValue.attacker.Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            _xz = (_xz - gameObject.transform.position).normalized;
            pEvents.CloseAllPersonalityEffects();
            usedYCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageYAnimationCurve : FightGlobalSetting._knockOffyAnimationCurve;
            usedZCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageZAnimationCurve : FightGlobalSetting._knockOffzAnimationCurve;
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            time_count = 0f;
        }
        float time_counter;
        AnimationCurve usedYCurve;
        AnimationCurve usedZCurve;
        Vector3 effectP, quaV;
        private int flyingStep = 0;// 0 拔地 1 曲线 2 落地以及躺地昏迷
        public override void _State_Update()
        {
            time_counter += Time.deltaTime;
            if (!touchedBoundary)
            {
                if (_BasicPhysicSupport.AtRing)
                {
                    touchedBoundary = true;
                    _xz = Vector3.zero - gameObject.transform.position;
                    _xz.y = 0;
                    _xz = _xz.normalized;
                    effectP = gameObject.transform.position.normalized * BoundaryControllByGod._BattleRingRadius;
                    effectP.y = gameObject.transform.position.y;
                    quaV = Vector3.zero - gameObject.transform.position.normalized;
                    quaV.y = 0;
                    EffectsManager.GenerateEffect("wallCrack", null, effectP, Quaternion.LookRotation(quaV, Vector3.up), null);
                }
            }
            
            switch (flyingStep)
            {
                case 0:
                    gameObject.transform.position +=
                        _xz * (usedZCurve.Evaluate(time_counter + Time.deltaTime) - usedZCurve.Evaluate(time_counter)) +
                        Vector3.up * (usedYCurve.Evaluate(time_counter + Time.deltaTime) - usedYCurve.Evaluate(time_counter));
                    
                    if (!_BasicPhysicSupport.hiddenMethods.Grounded)
                    {
                        flyingStep = 1;
                        FightParamsRef.EnableAllLimbs(true);
                    }
                    break;
                case 1:
                    gameObject.transform.position +=
                        _xz * (usedZCurve.Evaluate(time_counter + Time.deltaTime) - usedZCurve.Evaluate(time_counter)) +
                        Vector3.up * (usedYCurve.Evaluate(time_counter + Time.deltaTime) - usedYCurve.Evaluate(time_counter));
                    if (_BasicPhysicSupport.hiddenMethods.Grounded && time_counter > 0.5f)
                        // time_counter > 0.5f 这个数字是为了确保角色真能飞起来。
                        // 否则很有可能因为动画本身等复杂缘故，刚飞起来就被判断落地
                    {
                        flyingStep = 2;
                    }

                    if (time_counter > 2f)
                    {
                        flyingStep = 2;
                    }
                    break;
                case 2 :
                    if (!dropped)
                    {
                        dropped = true;
                        time_counter = 0;
                        _BasicPhysicSupport.SetUsingGravity(true);
                        effectP = gameObject.transform.position;
                        effectP.y = 0;
                        EffectsManager.GenerateEffect("hit_ground", null, effectP, Quaternion.LookRotation(Vector3.right), null);
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        flyingStep = 3;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}