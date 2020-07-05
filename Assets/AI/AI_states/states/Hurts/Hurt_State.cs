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
    
    public override void AI_State_enter(V_Damage newValue)
	{
        target = newValue;
        base.AI_State_enter();
        _Rigidbody.mass = 80;
        _Animator.applyRootMotion = false;
        _FightAttriCalRef.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        TimeCounter = 0f;
        switch (newValue.from_weapon.damage_type)
        {
            case DamageType.slight_damage_forward:
                SlightDamgeStart(newValue);
            break;
            case DamageType.light_damage_forward:
                LightDamgeStart(newValue);
            break;
            case DamageType.heavy_damage_forward:
                HeavyDamgeStart(newValue);
            break;
            case DamageType.supper_damage_forward:
                SuperDamgeStart(newValue);
            break;
            case DamageType.draw:
                DrawDamgeStart(newValue);
            break;
            case DamageType.explosion:
                ExplosionDamgeStart(newValue);
            break;
            case DamageType.push_to_mid:
                PushToMidStart(newValue);
            break;
            case DamageType.high:
                HighDamgeStart(newValue);
                break;
            default:
            break;
        }
        
        if (newValue.from_weapon.effectSpreadOnBody)
        {
            _FightAttriCalRef.RunShaderChangeProcess(FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), 0.1f);
        }
        
        if (_FightAttriCalRef.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent)
        {
            _FightAttriCalRef.GetKnockOffCount().SetGauge(0f);
            _AIStateRunner.ChangeState("KnockOff", newValue);
            return;
        }
        RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
        pEvents.CloseAllPersonalityEffects();
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

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Rigidbody.mass = 100;
        if (physicMissionDisposable != null && !physicMissionDisposable.IsDisposed)
            physicMissionDisposable.Dispose();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _FightAttriCalRef.SetGettingDamageState(false);
        switch(target.from_weapon.damage_type)
        {
            case DamageType.high:
            break;
        }
        _BasicPhysicSupport.SetUsingGravity(true);
    }
}