using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

//死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
public class Death_State : AI_State
{
    private string clip_name;
    private float stopRunningTime;
    private float time_count;

    private float Upforce;
    private float horizentalForce;
    private bool if_r_rotation;
    private Vector3 used_velcoity;
    private GameObject processingBlood;
    private Vector3 force_direction;
    private string KnockOffSparkPersonalEffectPath;
    private int landedCal;//1 还在地上 2 已经被打飞起来 3 落地

    public Death_State(float stopRunningTime, string clip_name, float Upforce, float horizentalForce)
    {
        this.stopRunningTime = stopRunningTime;

        this.clip_name = clip_name;
        this.Upforce = Upforce;
        this.horizentalForce = horizentalForce;
        if_r_rotation = false;

        StateType = stateType.KnockOff;
    }

    public override void pre_process_before_enter()
    {
		base.pre_process_before_enter ();
    }

    public override bool enter_condition_priority2()
    {
        return false;
    }

    public override bool capacity_exit_condition()
    {
        return false;
    }

    public override bool force_enter_condition()
    {
        return false;
    }

    public override void AI_State_enter()
    {
        time_count = 0f;
        base.AI_State_enter();
        AI_DATA_CENTER.setDeathState(true);
        AI_DATA_CENTER.deActiveObjects();
        AI_DATA_CENTER.deathInitialize();

        landedCal = 1;
        _Rigidbody.velocity = Vector3.zero;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        Animation_Manger.PlayLayerAnim(animator_layer_index.Full_Body, clip_name);

        if (this.BS_Main_Health.returnDamageList(damageType.deathknockoff).Count > 0)
        {
            //if (BS_Main_Health.returnDamageList(damageType.supper_damage)[0].ifExplosion)
            force_direction = this.BS_Main_Health.returnDamageList(damageType.deathknockoff)[0].force_direction;
            if (this.BS_Main_Health.returnDamageList(damageType.deathknockoff)[0].fromWeapon != null)
                KnockOffSparkPersonalEffectPath = this.BS_Main_Health.returnDamageList(damageType.deathknockoff)[0].fromWeapon.personalEffectPath;
            else
                KnockOffSparkPersonalEffectPath = null;

            defaultPools.Instance.GenerateEffect("super_hit", KnockOffSparkPersonalEffectPath,
                                                 this.BS_Main_Health.returnDamageList(damageType.deathknockoff)[0].damageHappenPoint,this.gameObject.transform.rotation,
                                                 this.BS_Main_Health.getHealthBodyCenterTransform());               
            force_direction.y = 0;
            if (if_r_rotation)
                this.RotateToDirection(force_direction, 20f, true);

            used_velcoity = force_direction.normalized * horizentalForce + Vector3.up * Upforce;
            _Rigidbody.velocity = used_velcoity;
            this.BS_Main_Health.eatDamage(damageType.deathknockoff);
        }
        AI_DATA_CENTER.deActiveObjects();
        this.BS_Main_Health.clearDamageLists();
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        time_count = 0f;
        landedCal = 1;
        this._Rigidbody.velocity = Vector3.zero;
    }

    public override void _f_State_Update()
    {
        time_count += Time.deltaTime;

        if (time_count > stopRunningTime)
        {
			Sensor.enabled = false;
            _Rigidbody.velocity = Vector3.zero;
        }
        if (time_count > stopRunningTime + 1f)
        {
            _AIStateRunner.enabled = false;
            //this.gameObject.SetActive(false);
        }

        if (landedCal == 1)
        {
            if (!AI_DATA_CENTER.IsGrounded())
            {
                landedCal = 2;
            }
            else
            {
                _Rigidbody.velocity = used_velcoity;//一定让它飞起来
            }
        }
        if (landedCal == 2)
        {
            if (AI_DATA_CENTER.IsGrounded())
            {
                landedCal = 3;
            }
        }

        if (landedCal == 3)
        {
            this._Rigidbody.velocity = Vector3.zero;
        }

        if (!AI_DATA_CENTER.IsGrounded())
            this.RotateToVelocityNegative(3f, true);
    }
}