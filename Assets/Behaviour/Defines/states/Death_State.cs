using Cysharp.Threading.Tasks;
using UnityEngine;
using HittingDetection;
using Skill;

namespace Soul
{
    //死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
    public class Death_State : Behavior
    {
        float _timeCounter;
        Vector3 _xz;
        bool _touchedBoundary;
        bool _dropped;
        AnimationCurve _usedYCurve;
        AnimationCurve _usedZCurve;
        Decomposition _layBlocker;
        float _temp;
        
        public Death_State()
        {
            StateType = BehaviorType.KnockOff;
        }
        
        public override void AI_State_enter(V_Damage newValue)
        {
            base.AI_State_enter();
            _DATA_CENTER.FightDataRef.IsDead.Value = true;
            FightParamsRef.ChangeLayerForLimbs(14);
            
            _flyingStep = 0;
            _timeCounter = 0;
            _touchedBoundary = false;
            _dropped = false;
            FightParamsRef.GettingDamage = true;
            _BasicPhysicSupport.SetUsingGravity(false);
            _Animator.SetFloat("speed", 0f);
            _Animator.applyRootMotion = false;
            _Weapon_Animation_Events.ClearMarkerManagers();
            pEvents.CloseAllPersonalityEffects();
            _Rigidbody.velocity = Vector3.zero;
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(), true, 0.05f);
            //_xz = newValue.attacker._Center.WholeT.forward;
            _xz = CalFixPushPos(newValue.impactComingPoint,  newValue.attacker.Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
            _xz = (_xz - gameObject.transform.position).normalized;
            _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
            EffectsManager.GenerateEffect("super_hit", FightGlobalSetting.EffectPathDefine(newValue.from_weapon.element), newValue.DamageEffectPoint, newValue.CutRotation, null).Forget();
            _usedYCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageYAnimationCurve : FightGlobalSetting._knockOffyAnimationCurve;
            _usedZCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageZAnimationCurve : FightGlobalSetting._knockOffzAnimationCurve;
            FightParamsRef.EnableAllLimbs(false);
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }
        
        public override bool Force_enter_condition()
        {
            return false;
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            FightParamsRef.EnableAllLimbs(true);
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            FightParamsRef.GettingDamage = false;
            _SkillCancelFlag.turn_off_flag();
            _BasicPhysicSupport.SetUsingGravity(true);

            if (_layBlocker != null)
                _layBlocker.Phase = -1;
        }
        
        Vector3 _effectP, _quaV;
        private int _flyingStep;// 0 拔地 1 曲线 2 落地以及躺地昏迷
        public override void _State_Update()
        {
            _timeCounter += Time.deltaTime;
            if (!_touchedBoundary)
            {
                if (_BasicPhysicSupport.AtRing)
                {
                    _touchedBoundary = true;
                    _xz = Vector3.zero - gameObject.transform.position;
                    _xz.y = 0;
                    _xz = _xz.normalized;
                    _effectP = gameObject.transform.position.normalized * BoundaryControlByGod._BattleRingRadius;
                    _effectP.y = gameObject.transform.position.y;
                    _quaV = Vector3.zero - gameObject.transform.position.normalized;
                    _quaV.y = 0;
                    EffectsManager.GenerateEffect("wallCrack", null, _effectP, Quaternion.LookRotation(_quaV, Vector3.up), null).Forget();
                }
            }
            
            switch (_flyingStep)
            {
                case 0:
                    _temp = _usedYCurve.Evaluate(_timeCounter + Time.deltaTime) - _usedYCurve.Evaluate(_timeCounter);
                    gameObject.transform.position +=
                        _xz * (_usedZCurve.Evaluate(_timeCounter + Time.deltaTime) - _usedZCurve.Evaluate(_timeCounter)) + Vector3.up * _temp;
                    
                    if (!_BasicPhysicSupport.hiddenMethods.Grounded || _temp <= 0) //着地，或都应该下落了的时候还在地上
                    {
                        _flyingStep = 1;
                        FightParamsRef.EnableAllLimbs(true);
                    }
                    break;
                case 1:
                    gameObject.transform.position +=
                        _xz * (_usedZCurve.Evaluate(_timeCounter + Time.deltaTime) - _usedZCurve.Evaluate(_timeCounter)) +
                        Vector3.up * (_usedYCurve.Evaluate(_timeCounter + Time.deltaTime) - _usedYCurve.Evaluate(_timeCounter));
                    if (_BasicPhysicSupport.hiddenMethods.Grounded && _timeCounter > 0.5f)
                        // time_counter > 0.5f 这个数字是为了确保角色真能飞起来。
                        // 否则很有可能因为动画本身等复杂缘故，刚飞起来就被判断落地
                    {
                        _flyingStep = 2;
                    }

                    if (_timeCounter > 2f)
                    {
                        _flyingStep = 2;
                    }
                    break;
                case 2 :
                    if (!_dropped)
                    {
                        _dropped = true;
                        _timeCounter = 0;
                        _BasicPhysicSupport.SetUsingGravity(true);
                        _effectP = gameObject.transform.position;
                        _effectP.y = 0;
                        EffectsManager.GenerateEffect("hit_ground", null, _effectP, Quaternion.LookRotation(Vector3.right), null).Forget();
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        _flyingStep = 3;
                        layBlock(_DATA_CENTER);
                    }
                    break;
                case 3:
                    break;
            }
        }
        
        async void layBlock(Data_Center _DATA_CENTER)
        {
            _layBlocker = await EffectsManager.GenerateEffect("layBlocker", "defaultmagic", _DATA_CENTER.geometryCenter.position, _DATA_CENTER.geometryCenter.rotation, _DATA_CENTER.geometryCenter);
        }
    }
}