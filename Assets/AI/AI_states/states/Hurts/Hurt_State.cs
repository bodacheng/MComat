using UnityEngine;
using HittingDetection;
using Soul;

public partial class Hurt_State : Behavior {

    float used_dizzy_time;
    Vector3 fixDesPos;
    float TimeCounter { set; get; }
    V_Damage target;
    
    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }
    
    public override void AI_State_enter(V_Damage newValue)
	{
        target = newValue;
        base.AI_State_enter();
        _Animator.applyRootMotion = false;
        _FightAttriCalReference.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        _FightAttriCalReference.PlusCriticalGauge(1);
        TimeCounter = 0f;
        _Rigidbody.mass = 500;
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
            _FightAttriCalReference.RunShaderChangeProcess(FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), 0.1f);
        }
        
        if (_FightAttriCalReference.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent)
        {
            _FightAttriCalReference.GetKnockOffCount().SetGauge(0f);
            _AIStateRunner.ChangeState("KnockOff", newValue);
            return;
        }
        RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
        personality_Events.CloseAllPersonalityEffects();
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
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _Rigidbody.mass = 1000;
        _FightAttriCalReference.SetGettingDamageState(false);
        switch(target.from_weapon.damage_type)
        {
            case DamageType.high:
                HighDamageEnd();
            break;
        }
    }
}