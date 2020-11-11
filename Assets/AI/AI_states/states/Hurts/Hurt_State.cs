using UnityEngine;
using HittingDetection;
using Soul;
using UniRx;

public partial class Hurt_State : Behavior {

    float used_dizzy_time;
    Vector3 fixDesPos;
    float TimeCounter { set; get; }
    V_Damage target;
    SingleAssignmentDisposable physicMissionDisposable;

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }

    string HurtPos;
    void PlayHurtAnim(V_Damage newValue)
    {
        if (_AIStateRunner.GetLastState().StateKey == "KnockOff" && _BasicPhysicSupport.hiddenMethods.Grounded)
        {
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("lay"), true, 0.1f);
            return;
        }
        Vector3 point = newValue.damageHappenPoint;
        point.y = 0;
        if (Vector3.Angle(_DATA_CENTER.WholeT.forward, point - _DATA_CENTER.WholeT.position) > 140)
        {
            Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("back"), true, 0.1f);
            RotateToTarget_Tween(_DATA_CENTER.WholeT.position + (_DATA_CENTER.WholeT.position - newValue.damageHappenPoint), 0.1f, true);
        }else{
            if (newValue.damageHappenPoint.y > _DATA_CENTER.head_t.position.y)
            {
                Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("press"), true, 0.1f);
                RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
            }else{
                if (newValue.damageHappenPoint.y > _DATA_CENTER.geometryCenter.position.y)
                {
                    Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("high"), true, 0.1f);
                    RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
                }else{
                    Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim("low"), true, 0.1f);
                    RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
                }
            }
        }
    }
    
    public override void AI_State_exit()
    {
        base.AI_State_exit();
        switch (target.from_weapon.damage_type)
        {
            case DamageType.time_pause:
            case DamageType.sekka:
                Animation_Manger.Speed = 1;
                shaderManager.FlatColor(0, Color.white);
            break;
        }
        _Rigidbody.mass = 100;
        if (physicMissionDisposable != null && !physicMissionDisposable.IsDisposed)
            physicMissionDisposable.Dispose();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _FightAttriCalRef.SetGettingDamageState(false);
        _BasicPhysicSupport.SetUsingGravity(true);
    }
    
    public override void AI_State_enter(V_Damage newValue)
	{
        if (_AIStateRunner.GetLastState().StateType == Skill.BehaviorType.Hit && target.from_weapon.damage_type == DamageType.time_pause)
        {
            TimePauseStart();
            return;
        }
        
        target = newValue;
        base.AI_State_enter();
        _Rigidbody.mass = 80;
        _Animator.applyRootMotion = false;
        _FightAttriCalRef.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        TimeCounter = 0f;
        pEvents.CloseAllPersonalityEffects();
        switch (target.from_weapon.damage_type)
        {
            case DamageType.slight_damage_forward:
                SlightDamgeStart(target);
            break;
            case DamageType.light_damage_forward:
                LightDamgeStart(target);
            break;
            case DamageType.heavy_damage_forward:
                HeavyDamgeStart(target);
            break;
            case DamageType.supper_damage_forward:
                SuperDamgeStart(target);
            break;
            case DamageType.draw:
                DrawDamgeStart(target);
            break;
            case DamageType.explosion:
                ExplosionDamgeStart(target);
            break;
            case DamageType.push_to_mid:
                PushToMidStart(target, 10f, true);
            break;
            case DamageType.high:
                // 20201008 修改。high攻击不外乎是直接让对手被击飞，那么击飞状态里确实有相应的一切。
                _AIStateRunner.ChangeState("KnockOff", target);//HighDamgeStart(target);
                return;
            case DamageType.push_to_mid_slight:
                PushToMidStart(target, 4f, true);
            break;
            case DamageType.same_height_to_mid:
                PushToMidStart(target, 4f, false);
            break;
            case DamageType.sekka:
                SekkaStart(target.from_weapon.zokusei);
            break;
            case DamageType.time_pause:
                TimePauseStart();
            return;
        }
        
        if (target.from_weapon.effectSpreadOnBody)
        {
            _FightAttriCalRef.RunShaderChangeProcess(FightGlobalSetting.EffectPathDefine(target.from_weapon.zokusei), 0.1f);
        }
        
        if (_FightAttriCalRef.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent)
        {
            _FightAttriCalRef.GetKnockOffCount().SetGauge(0f);
            _AIStateRunner.ChangeState("KnockOff", target);
            return;
        }
        
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("hurt");
    }
    
    public override void _State_FixedUpdate1()
    {
        TimeCounter += Time.fixedDeltaTime;
        switch(target.from_weapon.damage_type)
        {
            case DamageType.high:
                HighDamageUpdate();
            break;
        }
    }
    
    public override bool Capacity_Exit_Condition()
    {
        return TimeCounter > used_dizzy_time;
    }
}