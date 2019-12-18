using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Defend_State : AI_State
{
    readonly string defend_clip_name;
    readonly string block_break_name;
    public float time_counter;
    float used_block_least_time;
    int defendHP;
    List<Collider> damagingweaponList;
    List<Collider> nearbyenemymeat;
    Vector3 fixDesPos;
    
    public Defend_State(string defend_clip_name,string block_break_name)
    {
        this.defend_clip_name = defend_clip_name;
        this.block_break_name = block_break_name;
    }

    void DefendHPfade(V_Damage damage)
    {
        switch (damage.damage_type)
        {
            case DamageType.light_block:
                defendHP -= 1;
                break;
            case DamageType.heavy_block:
                defendHP -= 2;
                break;
        }
        if (defendHP <= 0)
        {
            _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.supper_damage, WeaponPosAdjustMode.pushToMidForward, damage.damageHappenPoint, damage.AttackerT_foward,damage.AttackerT_pos, damage.fromWeapon));
            EffectAndHurtObjectLoading.Instance.GenerateEffect("onEnableShieldSpark", null, damage.damageHappenPoint, Quaternion.identity, null);
        }
    }

    public override void Pre_process_before_enter()
    {
        base.Pre_process_before_enter();
    }

    public override bool Capacity_enter_condition()
    {
        return true;
    }

    public override bool Enter_condition_priority1()
    {
        if (_FightAttriCalReference.IFgettingDamage())
            return true;
        damagingweaponList = Sensor.GetNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.GetInnerEnemiesColliders();
        if (nearbyenemymeat.Count == 0)
        {
            if (damagingweaponList.Count > 0)
                return true;
        }else{
            if (damagingweaponList.Count > 0)
            {
                if (Vector3.Distance(nearbyenemymeat[0].transform.position, _DATA_CENTER.geometryCenter.position)
                    >
                    Vector3.Distance(damagingweaponList[0].transform.position, _DATA_CENTER.geometryCenter.position))
                    return true;
            }
        }
        return false;
    }

    public override bool Enter_condition_priority3()
    {
        return (Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null && Sensor.GetInnerEnemiesColliders().Count > 0);
    }

    public override bool Naturally_exit_condition() 
    {
        return time_counter <= 0;
    }

    public override bool Strategic_exit_condition()
    {
        return !Enter_condition_priority1() && this._AIStateRunner.HaveFirstSkillToTrigger();
    }

    public override void AI_State_enter()
    {
        defendHP = FightGlobalSetting._defendHP;
        _Weapon_Animation_Events.ClearMarkerManagers();
        Sensor.ContinuousDetectionStart(5);
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        Animation_Manger.PlayLayerAnim(defend_clip_name);
        _Rigidbody.velocity = Vector3.zero;
        used_block_least_time = FightGlobalSetting._lightBlockLastingTime;
        time_counter = used_block_least_time;
        if (shaderManager != null)
            shaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), 0.7f, 0.05f);      
        _SkillCancelFlag.turn_off_flag();
         //this.AI_DATA_CENTER.turnShield(true);
    }
    
    public override void AI_State_enter(V_Damage newValue)
    {
        defendHP = FightGlobalSetting._defendHP;
        _Weapon_Animation_Events.ClearMarkerManagers();
        Sensor.ContinuousDetectionStart(5);
        base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        if (shaderManager != null)
            shaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), 0.7f, 0.05f);      
        _SkillCancelFlag.turn_off_flag();
         //this.AI_DATA_CENTER.turnShield(true);
         
         fixDesPos = CalFixPosDestination(newValue.damageHappenPoint,
                                    newValue.AttackerT_foward,
                                        newValue.AttackerT_pos,
                                            gameObject.transform.position,
                                                newValue._WeaponPosAdjustMode);
         switch(newValue.damage_type)
         {
            case DamageType.light_block:
                Animation_Manger.PlayLayerAnim(block_break_name);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                used_block_least_time = FightGlobalSetting._lightBlockLastingTime;
                time_counter = used_block_least_time;
                DefendHPfade(newValue);
                _FightAttriCalReference.PlusCriticalGauge(2);
            break;
            case DamageType.heavy_block:
                Animation_Manger.PlayLayerAnim(block_break_name);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                used_block_least_time = FightGlobalSetting._heavyBlockLastingTime;
                time_counter = used_block_least_time;
                DefendHPfade(newValue);
                _FightAttriCalReference.PlusCriticalGauge(2);
            break;
         }
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        //this.Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body,null);
        //注意看changeState环节，上一个状态的exit和下一个状态的enter是同一个帧执行的。
        //从这里我们曾经发现了动画播放模块一个重要问题，就是在特定情况下，
        //比如defend状态的exit里有PlayLayerAnim(_animator_layer_index, null)，防御后接攻击，
        //那么先执行PlayLayerAnim(_animator_layer_index, null) ，同一帧执行PlayLayerAnim(_animator_layer_index, clip_name);
        //就会产生bug：动画器无法正常播放攻击动画，角色会立在那里。这是我们动画模块的一个性质。
        // 我们把defend状态exit中的PlayLayerAnim(_animator_layer_index, null)删除了后就不再产生对应bug。
        // 关于动画模块的“技能动作清空”，我们是把它放在了move状态的开头，从而避免了清空函数与触发动画函数在同一帧执行。
        _ResistanceManager.ResistanceClear();
        //AI_DATA_CENTER.turnShield(false);
    }

    public override void _State_FixedUpdate1()
    {
        _ResistanceManager.Resistance.Value = defendHP > 0 ? 5 : 0;
        damagingweaponList = Sensor.GetNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.GetInnerEnemiesColliders();
        
        if (nearbyenemymeat.Count > 0)
        {
            if (nearbyenemymeat[0] != null)
                RotateToTarget(nearbyenemymeat[0].transform.position, 1f, true);
        }
        else
        {
            if (damagingweaponList.Count > 0)
            {
                if (damagingweaponList[0] != null)
                    RotateToTarget(damagingweaponList[0].transform.position, 1f, true);
            }
        }
        if (time_counter >= 0f)
        {
            time_counter -= Time.fixedDeltaTime;
            if (time_counter < 0f)
                Animation_Manger.PlayLayerAnim(defend_clip_name);
        }
        if (time_counter < used_block_least_time/2)
        {
            _Rigidbody.velocity = Vector3.zero;
        }
    }
}
