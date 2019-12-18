using UnityEngine;
using HittingDetection;
using Soul;

//死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
public class Death_State : AI_State
{
    private readonly string clip_name;
    private readonly float stopRunningTime;    
    private readonly bool if_r_rotation;
    private readonly GameObject processingBlood;
    private float time_count;
    private string KnockOffSparkPersonalEffectPath;
    private int landedCal;//1 还在地上 2 已经被打飞起来 3 落地
    
    private Quaternion startquaternion;
    private Matrix4x4 Matrix;

    public Death_State(float stopRunningTime, string clip_name)
    {
        this.stopRunningTime = stopRunningTime;
        this.clip_name = clip_name;
        if_r_rotation = false;
        StateType = stateType.KnockOff;
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override bool Enter_condition_priority2()
    {
        return false;
    }

    public override bool Naturally_exit_condition()
    {
        return false;
    }

    public override bool Force_enter_condition()
    {
        return false;
    }

    public override void AI_State_enter(V_Damage newValue)
    {
        base.AI_State_enter();
        time_count = 0f;
        personality_Events.CloseAllPersonalityEffects();
        _DATA_CENTER.DeathInitialize();

        landedCal = 1;
        _Rigidbody.velocity = Vector3.zero;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        Animation_Manger.PlayLayerAnim(clip_name);
        startquaternion =  Quaternion.LookRotation(newValue.fromWeapon.GetOwnerFightAttriCalReference()._Center.WholeT.forward, Vector3.up);
        KnockOffSparkPersonalEffectPath = newValue.fromWeapon?.GetEffectPath();
        EffectAndHurtObjectLoading.Instance.GenerateEffect("super_hit", KnockOffSparkPersonalEffectPath,
                                             newValue.damageHappenPoint, gameObject.transform.rotation,
                                             _FightAttriCalReference.transform);
        if (if_r_rotation)
            RotateToDirection(gameObject.transform.position - newValue.fromWeapon.transform.position, 10f, true);
        Matrix = Matrix4x4.TRS(gameObject.transform.position, startquaternion, Vector3.one * 1);
        personality_Events.CloseAllPersonalityEffects();
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        time_count = 0f;
        landedCal = 1;
        _Rigidbody.velocity = Vector3.zero;
    }

    public override void _State_FixedUpdate1()
    {
        time_count += Time.fixedDeltaTime;
        Sensor.enabled &= time_count <= stopRunningTime;
        _AIStateRunner.enabled &= time_count <= stopRunningTime + 1f;
        time_count += Time.fixedDeltaTime;
        gameObject.transform.position = Matrix.MultiplyPoint3x4(new Vector3(1,
                                            FightGlobalSetting._knockOffyAnimationCurve.Evaluate( time_count ) * 1f,
                                            FightGlobalSetting._knockOffzAnimationCurve.Evaluate( time_count ) * 1f ));        
        RotateToVelocityNegative(3f, true);
    }
}