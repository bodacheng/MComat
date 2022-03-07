using UnityEngine;
using HittingDetection;
using Skill;

namespace Soul
{
    public class Knock_Off_State : Behavior
    {
        float time_counter;
        Vector3 _xz;
        bool touchedBoundary;
        bool dropped, canWakeUp;
        AnimationCurve usedYCurve;
        AnimationCurve usedZCurve;

        public Knock_Off_State()
        {
            StateType = BehaviorType.KnockOff;
        }
        
        public override void AI_State_enter(V_Damage newValue)
        {
            base.AI_State_enter();
            flyingStep = 0;
            time_counter = 0;
            touchedBoundary = false;
            dropped = false;
            canWakeUp = false;
            _FightAttriCalRef.GettingDamage = true;
            _BasicPhysicSupport.SetUsingGravity(false);
            _Animator.SetFloat("speed", 0f);
            _Animator.applyRootMotion = false;
            _Weapon_Animation_Events.ClearMarkerManagers();
            pEvents.CloseAllPersonalityEffects();
            _Rigidbody.velocity = Vector3.zero;
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(), true, 0.05f);
            //_xz = newValue.attacker._Center.WholeT.forward;
            _xz = _DATA_CENTER.WholeT.position - newValue.DamageHappenPoint;
            _xz = _xz.normalized;
            _xz.y = 0;
            _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
            EffectsManager.GenerateEffect("super_hit", FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), newValue.DamageHappenPoint, newValue.CutRotation, null);
            usedYCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageYAnimationCurve : FightGlobalSetting._knockOffyAnimationCurve;
            usedZCurve = newValue.from_weapon.damage_type == DamageType.high ? FightGlobalSetting._HdamageZAnimationCurve : FightGlobalSetting._knockOffzAnimationCurve;
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _FightAttriCalRef.GettingDamage = false;
            _SkillCancelFlag.turn_off_flag();
            _BasicPhysicSupport.SetUsingGravity(true);
        }
        
        Vector3 effectP, quaV;
        private int flyingStep = 0;// 0 拔地 1 曲线 2 落地以及躺地昏迷
        public override void _State_Update()
        {
            time_counter += Time.deltaTime;
            if (!touchedBoundary)
            {
                if (_BasicPhysicSupport.atRing)
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
                    if (!canWakeUp)
                    {
                        if (dropped && time_counter > FightGlobalSetting._CanGetUpAfterKnockoffToGround)
                        {
                            canWakeUp = true;
                            _SkillCancelFlag.turn_on_flag();
                        }
                    }
                    if (time_counter > FightGlobalSetting._MaxKnockoffLaidGroundTime)
                    {
                        _AIStateRunner.ChangeState("getUp");
                    }
                    break;
            }
        }
    }
}

// 原先的MultiplyPoint3x4击飞曲线计划相关
//if (!touchedBoundary)
//{
//    if (_DATA_CENTER.onBattleGroundBundary)
//    {
//        touchedBoundary = true;
//        temp = gameObject.transform.position;
//        temp.y = 0;
//        startquaternion = Quaternion.LookRotation(Vector3.zero - temp, Vector3.up);
//        temp = gameObject.transform.position;
//        Matrix = Matrix4x4.TRS(temp, startquaternion, Vector3.one * 1);
//        alreadyFinishedZTranslation = FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_counter);
//        alreadyFinishedYTranslation = gameObject.transform.position.y;
//    }
//}else{
//    touchedBoundary = _DATA_CENTER.onBattleGroundBundary;
//}
//gameObject.transform.position = Matrix.MultiplyPoint3x4(new Vector3(0, 
//FightGlobalSetting._knockOffyAnimationCurve.Evaluate( time_counter ) * 1f - alreadyFinishedYTranslation, 
//FightGlobalSetting._knockOffzAnimationCurve.Evaluate( time_counter ) * 1f - alreadyFinishedZTranslation));

// Knock_off_state should not be super_canceled to revive state,here is the reason:
// While in knock off state, the character may also be transited into knock off state again,
// during the transition of knock off animation to self,the turn on flag you put there is triggerd,
// The AIStateRunner will recognise it as a permision to enter the revive state,then what you will see
// is that revive state is somehow triggerd immmediately. 1107 -- haku

    //if (BS_Main_Health.returnEventDamageList() != null)
   //     {
            //if (BS_Main_Health.returnEventDamageList().Count > 0)
    //        {
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Child == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Child = this.gameObject;
    //            }
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Parent == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Parent = this.gameObject;
    //            }
                //BS_Main_Health.returnEventDamageList()[0].getAttackerHealthBody().eventAttackHitApprove(BS_Main_Health.returnEventDamageList()[0]);
                //BS_Main_Health.returnEventDamageList().Clear();
                //BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
        //    }
        //}
        //if (BS_Main_Health.returnApprovedEventAttackAttempts() != null)
   //     {
            //BS_Main_Health.returnApprovedEventAttackAttempts().Clear();
        //}
