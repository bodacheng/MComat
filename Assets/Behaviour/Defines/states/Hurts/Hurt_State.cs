using Cysharp.Threading.Tasks;
using UnityEngine;
using HittingDetection;
using UniRx;
using DG.Tweening;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        float used_dizzy_time;
        float TimeCounter { set; get; }
        V_Damage target;
        SingleAssignmentDisposable physicMissionDisposable;
        Tween tween;
        
        void PlayHurtAnim(V_Damage newValue)
        {
            if (_AIStateRunner.GetLastState().StateKey == "KnockOff" && _BasicPhysicSupport.hiddenMethods.Grounded)
            {
                Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("lay"), true, 0.1f);
                return;
            }
            Vector3 point = newValue.DamageEffectPoint;
            point.y = 0;
            if (Vector3.Angle(_DATA_CENTER.WholeT.forward, point - _DATA_CENTER.WholeT.position) > 160)
            {
                Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("back"), true, 0.1f);
                RotateToTarget_Tween(_DATA_CENTER.WholeT.position + (_DATA_CENTER.WholeT.position - newValue.DamageEffectPoint), 0.1f);
            }
            else
            {
                if (newValue.DamageEffectPoint.y > _DATA_CENTER.head_t.position.y)
                {
                    Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("press"), true, 0.1f);
                    RotateToTarget_Tween(newValue.DamageEffectPoint, 0.1f);
                }
                else
                {
                    if (newValue.DamageEffectPoint.y > _DATA_CENTER.geometryCenter.position.y)
                    {
                        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("high"), true, 0.1f);
                        RotateToTarget_Tween(newValue.DamageEffectPoint, 0.1f);
                    }
                    else
                    {
                        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("low"), true, 0.1f);
                        RotateToTarget_Tween(newValue.DamageEffectPoint, 0.1f);
                    }
                }
            }
        }

        public override void AI_State_exit()
        {
            base.AI_State_exit();
            _Rigidbody.mass = 500;
            _BasicPhysicSupport.OpenEnemyTouchingDrag(0);
            FightParamsRef.GettingDamage = false;
            if (tween != null && tween.active && tween.IsPlaying())
                tween.Kill();
            physicMissionDisposable?.Dispose();
            if (_BuffsRunner.Freezing)
            {
                return;
            }
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public override void AI_State_enter(V_Damage newValue)
        {
            target = newValue;
            base.AI_State_enter();
            if (_AIStateRunner.GetLastState().StateKey == "KnockOff")
            {
                var knockOffState = (Knock_Off_State)_AIStateRunner.GetLastState();
                if (knockOffState.FlyingStep == 0 || knockOffState.FlyingStep == 1)
                    _AIStateRunner.ChangeState("KnockOff", target);
                return;
            }
            
            _Animator.applyRootMotion = false;
            PlayHurtAnim(newValue);
            FightParamsRef.GettingDamage = true;
            _Weapon_Animation_Events.ClearMarkerManagers();
            _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
            TimeCounter = 0f;
            pEvents.CloseAllPersonalityEffects();
            
            if (_BuffsRunner.Freezing)
                return;

            if (target.from_weapon.effectSpreadOnBody)
            {
                FightParamsRef.RunShaderChangeProcess(target.from_weapon.element, 0.1f);
            }
            
            FightParamsRef.GetKnockOffCount().PlusGauge(1f);
            FightParamsRef.GetKnockOffCount().PlusTimeCounter(0.2f);
            if (FightParamsRef.GetKnockOffCount().GetGauge() >= FightGlobalSetting.KnockOffExtent
            && target.from_weapon.damage_type != DamageType.stable_damage
            && target.from_weapon.damage_type != DamageType.stable_damage_forward
            && target.from_weapon.damage_type != DamageType.stable_draw)
            {
                FightParamsRef.GetKnockOffCount().SetGauge(0f);
                _AIStateRunner.ChangeState("KnockOff", target);
                return;
            }
            
            switch (target.from_weapon.damage_type)
            {
                case DamageType.slight_damage_forward:
                    used_dizzy_time = FightGlobalSetting.SlightHitLastingTime;
                    NormalStart(target);
                    break;
                case DamageType.light_damage_forward:
                    used_dizzy_time = FightGlobalSetting.LightHitLastingTime;
                    NormalStart(target);
                    break;
                case DamageType.stable_damage:
                    used_dizzy_time = FightGlobalSetting.LightHitLastingTime;
                    NormalStart(target);
                    break;
                case DamageType.stable_damage_forward:
                    used_dizzy_time = FightGlobalSetting.LightHitLastingTime;
                    HeavyStart(target);
                    break;
                case DamageType.heavy_damage_forward:
                    used_dizzy_time = FightGlobalSetting.HeavyHitLastingTime;
                    HeavyStart(target);
                    break;
                case DamageType.supper_damage_forward:
                    used_dizzy_time = FightGlobalSetting.SuperHitLastingTime;
                    HeavyStart(target);
                    EffectsManager.GenerateEffect("electric_s_e", FightGlobalSetting.EffectPathDefine(newValue.from_weapon.element), newValue.DamageEffectPoint, newValue.CutRotation, _DATA_CENTER.geometryCenter).Forget();
                    break;
                case DamageType.draw:
                case DamageType.stable_draw:
                    DrawDamageStart(target);
                    break;
                case DamageType.explosion:
                    ExplosionDamageStart(target);
                    break;
                case DamageType.push_to_mid:
                    PushToMidStart(target, 10f, true);
                    break;
                case DamageType.push_to_mid_slight:
                    PushToMidStart(target, 4f, true);
                    break;
                case DamageType.same_height_to_mid:
                    PushToMidStart(target, 4f, false);
                    break;
                case DamageType.sekka:
                    SekkaStart(target.from_weapon.element);
                    break;
                case DamageType.time_pause:
                    TimePauseStart();
                    return;
                case DamageType.high:
                    // 20201008 修改。high攻击不外乎是直接让对手被击飞，那么击飞状态里确实有相应的一切。
                    _AIStateRunner.ChangeState("KnockOff", target);//HighDamgeStart(target);
                    return;
            }
            
            Animation_Manger.SetTrigger("face_reset");
            Animation_Manger.SetTrigger("hurt");
        }

        public override void _State_FixedUpdate1()
        {
            TimeCounter += Time.fixedDeltaTime;
            switch (target.from_weapon.damage_type)
            {
                case DamageType.high:
                    HighDamageUpdate();
                    break;
                case DamageType.draw:
                case DamageType.stable_draw:
                    DrawDamageUpdate(target);
                    break;
            }
        }

        public override bool Capacity_Exit_Condition()
        {
            return TimeCounter > used_dizzy_time && !_BuffsRunner.Freezing;
        }
    }
}