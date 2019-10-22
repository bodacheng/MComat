using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Defend_State : AI_State
{
    private string defend_clip_name,block_break_name;
    public float block_time_counter = 0;
    private int defendHP;
    private List<Collider> damagingweaponList;
    private List<Collider> nearbyenemymeat;
        
    public Defend_State(string defend_clip_name,string block_break_name)
    {
        this.defend_clip_name = defend_clip_name;
        this.block_break_name = block_break_name;
    }
    
    void defendHPfade(V_Damage damage)
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
            this._FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.supper_damage, damage.force_direction, damage.damageHappenPoint, damage.toWho,null));
            EffectAndHurtObjectLoading.Instance.GenerateEffect("onEnableShieldSpark", null,
                                                 damage.damageHappenPoint, Quaternion.identity,null);
        }
    }

    public override void pre_process_before_enter()
    {
        base.pre_process_before_enter();
    }

    public override bool Capacity_enter_condition()
    {
        return true;
    }

    public override bool enter_condition_priority1()
    {
        if (this._FightAttriCalReference.IFgettingDamage())
            return true;
        damagingweaponList = Sensor.getNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.getInnerEnemiesColliders();
        if (nearbyenemymeat.Count == 0)
        {
            if (damagingweaponList.Count > 0)
                return true;
        }else{
            if (damagingweaponList.Count > 0)
            {
                if (Vector3.Distance(nearbyenemymeat[0].transform.position, this._DATA_CENTER.geometryCenter.position)
                    >
                    Vector3.Distance(damagingweaponList[0].transform.position, this._DATA_CENTER.geometryCenter.position))
                    return true;
            }
        }
        return false;
    }

    public override bool enter_condition_priority3()
    {
        if (this._ResistanceManager.Resistance.Value > 0)
            return false;
        if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            return false;
        if (Sensor.getInnerEnemiesColliders().Count > 0)
            return true;
        return false;
    }
    
    public override bool capacity_exit_condition() 
    {
        if (block_time_counter > 0)
            return false;
        return true;
    }
    
    public override bool strategic_exit_condition()
    {
        if (!enter_condition_priority1() && !enter_condition_priority3() && this._AIStateRunner.haveFirstSkillToTrigger())
            return true;
        return false;
    }

    public override void AI_State_enter()
    {
        this.defendHP = 20;
        _Weapon_Animation_Events.clearMarkerManagers();
        this.Sensor.continuousDetectionStart(5);
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Animation_Manger.PlayLayerAnim(defend_clip_name);
        _Rigidbody.drag = 20f;
        block_time_counter = 0.3f;
        if (this.shaderManager != null)
            this.shaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), 0.7f, 0.05f);
        else
            Debug.Log(gameObject.name+"变色器没适配？？");
        //this.AI_DATA_CENTER.turnShield(true);
        _SkillCancelFlag.turn_off_flag();
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
        _Rigidbody.drag = 1f;
        _FightAttriCalReference.ReturnDamageList(DamageType.heavy_block).Clear();
        _FightAttriCalReference.ReturnDamageList(DamageType.light_block).Clear();
        _ResistanceManager.resistanceClear();
        //AI_DATA_CENTER.turnShield(false);
    }

    Vector3 force_direction;
    V_Damage analyzingDamage;
    public override void _State_FixedUpdate1()
    {
        if (defendHP > 0)
            _ResistanceManager.Resistance.Value = 5;//数字没别的意思就是希望让防御状态下维持一定抵抗，不下降
        else
            _ResistanceManager.Resistance.Value = 0;

        damagingweaponList = Sensor.getNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.getInnerEnemiesColliders();
        
        if (nearbyenemymeat.Count > 0)
        {
            if (nearbyenemymeat[0] != null)
                this.RotateToTarget(nearbyenemymeat[0].transform.position, 1f, true);
        }
        else
        {
            if (damagingweaponList.Count > 0)
            {
                if (damagingweaponList[0] != null)
                    this.RotateToTarget(damagingweaponList[0].transform.position, 1f, true);
            }
        }

        if (_FightAttriCalReference.ReturnDamageList(DamageType.heavy_block).Count > 0)
        {
            this.Animation_Manger.PlayLayerAnim(block_break_name);
            analyzingDamage = _FightAttriCalReference.ReturnDamageList(DamageType.heavy_block)[0];
            analyzingDamage.damage_type = DamageType.heavy_block;
            force_direction = analyzingDamage.force_direction;
            force_direction.y = 0;

            this._Rigidbody.velocity = force_direction.normalized * 5f;

            //BS_Main_Health._health -=analyzingDamage._damage;
            block_time_counter = 0.5f;
            if (this._FightAttriCalReference.hasPlentyGauge(3))
                _SkillCancelFlag.turn_on_flag();
            defendHPfade(analyzingDamage);
            _FightAttriCalReference.ReturnDamageList(DamageType.heavy_block).RemoveAt(0);
            this._FightAttriCalReference.plusCriticalGauge(2);
        }

        if (_FightAttriCalReference.ReturnDamageList(DamageType.light_block).Count > 0)
        {
            this.Animation_Manger.PlayLayerAnim(block_break_name);
            analyzingDamage = _FightAttriCalReference.ReturnDamageList(DamageType.light_block)[0];
            analyzingDamage.damage_type = DamageType.light_block;
            force_direction = analyzingDamage.force_direction;
            force_direction.y = 0;

            this._Rigidbody.velocity = force_direction.normalized * 2f;
            block_time_counter = 0.3f;
            if (this._FightAttriCalReference.hasPlentyGauge(3))
                _SkillCancelFlag.turn_on_flag();
            
            defendHPfade(analyzingDamage);
            _FightAttriCalReference.ReturnDamageList(DamageType.light_block).RemoveAt(0);
            this._FightAttriCalReference.plusCriticalGauge(2);
        }
        
        if (block_time_counter >= 0f)
        {
            block_time_counter -= Time.fixedDeltaTime;
            if (block_time_counter < 0f)
                this.Animation_Manger.PlayLayerAnim(defend_clip_name);
        }
        else
        {
            _Rigidbody.velocity = Vector3.zero;
        }
    }
}
