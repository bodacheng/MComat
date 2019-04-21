using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend_State : AI_State
{
    private string defend_clip_name,block_break_name;
    private float block_time_counter = 0;
    private int defendHP;
    
    private List<Collider> damagingweaponList;
    private List<Collider> nearbyenemymeat;
    public Defend_State(string defend_clip_name,string block_break_name)
    {
        this.defend_clip_name = defend_clip_name;
        this.block_break_name = block_break_name;
    }
    
    void defendHPfade(v_Damage damage)
    {
        switch (damage.damage_type)
        {
            case damageType.light_block:
                defendHP -= 1;
                break;
            case damageType.heavy_block:
                defendHP -= 2;
                break;
        }

        if (defendHP <= 0)
        {
            this.BS_Main_Health.ApplyDamage(new v_Damage(0, damageType.supper_damage, damage.force_direction, damage.damageHappenPoint, damage.toWho));
            defaultPools.Instance.GenerateEffect("onEnableShieldSpark", null,
                                                 damage.damageHappenPoint, Quaternion.identity,null);
        }

    }

    public override void pre_process_before_enter()
    {
        base.pre_process_before_enter();
    }

    public override bool capacity_enter_condition()
    {
        return true;
    }

    public override bool enter_condition_priority1()
    {
        if (this.BS_Main_Health.Resistance > 0)
            return false;
        damagingweaponList = Sensor.getNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.getInnerEnemiesColliders();
        if (nearbyenemymeat.Count == 0)
        {
            if (damagingweaponList.Count > 0)
                return true;
        }else{
            if (damagingweaponList.Count > 0)
            {
                if (Vector3.Distance(nearbyenemymeat[0].transform.position, this.AI_DATA_CENTER.geometryCenter.position)
                    >
                    Vector3.Distance(damagingweaponList[0].transform.position, this.AI_DATA_CENTER.geometryCenter.position))
                    return true;
            }
        }
        return false;
    }

    public override bool enter_condition_priority3()
    {
        if (this.BS_Main_Health.Resistance > 0)
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
        this.defendHP = 10;
        _Weapon_Animation_Events.clearMarkerManagers();
        this.Sensor.continuousDetectionStart(5);
        base.AI_State_enter();
        this._Animator.SetFloat("speed", 0f);
        this.Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, defend_clip_name);
        _Rigidbody.drag = 20f;
        block_time_counter = 0;
        if (this.shaderManager != null)
            this.shaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), 0.7f, 0.05f);
        else
            Debug.Log(gameObject.name+"变色器没适配？？");
        //this.AI_DATA_CENTER.turnShield(true);
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
        BS_Main_Health.returnDamageList(damageType.heavy_block).Clear();
        BS_Main_Health.returnDamageList(damageType.light_block).Clear();
        block_time_counter = 0;
        BS_Main_Health.resistanceClear();
        //AI_DATA_CENTER.turnShield(false);
    }

    Vector3 force_direction;
    v_Damage analyzingDamage;
    public override void _f_State_Update()
    {
        if (defendHP > 0)
            BS_Main_Health.Resistance = 5;//数字没别的意思就是希望让防御状态下维持一定抵抗，不下降
        else
            BS_Main_Health.Resistance = 0;

        damagingweaponList = Sensor.getNearbyDamagingWeaponColliders();
        nearbyenemymeat = Sensor.getInnerEnemiesColliders();
        
        if (nearbyenemymeat.Count > 0)
        {
            if (nearbyenemymeat[0] != null)
                this.RotateToTarget(nearbyenemymeat[0].transform.position, 0.25f, true);
        }
        else
        {
            if (damagingweaponList.Count > 0)
            {
                if (damagingweaponList[0] != null)
                    this.RotateToTarget(damagingweaponList[0].transform.position, 0.25f, true);
            }
        }

        if (BS_Main_Health.returnDamageList(damageType.heavy_block).Count > 0)
        {
            this.Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, block_break_name);
            analyzingDamage = BS_Main_Health.returnDamageList(damageType.heavy_block)[0];
            analyzingDamage.damage_type = damageType.heavy_block;
            force_direction = analyzingDamage.force_direction;
            force_direction.y = 0;

            this._Rigidbody.velocity = force_direction.normalized * 5f;

            //BS_Main_Health._health -=analyzingDamage._damage;
            block_time_counter = 0.5f;
            defendHPfade(analyzingDamage);
            BS_Main_Health.returnDamageList(damageType.heavy_block).RemoveAt(0);
            this.BS_Main_Health.plusCriticalGauge(5);
        }

        if (BS_Main_Health.returnDamageList(damageType.light_block).Count > 0)
        {
            this.Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, block_break_name);
            analyzingDamage = BS_Main_Health.returnDamageList(damageType.light_block)[0];
            analyzingDamage.damage_type = damageType.light_block;
            force_direction = analyzingDamage.force_direction;
            force_direction.y = 0;

            this._Rigidbody.velocity = force_direction.normalized * 2f;

            //BS_Main_Health._health-=analyzingDamage._damage);
            block_time_counter = 0.3f;
            defendHPfade(analyzingDamage);
            BS_Main_Health.returnDamageList(damageType.light_block).RemoveAt(0);
            this.BS_Main_Health.plusCriticalGauge(5);
        }
        
        if (block_time_counter >= 0f)
        {
            block_time_counter -= Time.deltaTime;
        }
        else
        {
            if (this.Animation_Manger.current_animation_name != defend_clip_name)
                this.Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, defend_clip_name);
            _Rigidbody.velocity = Vector3.zero;
        }
    }
}
