using UnityEngine;
using HittingDetection;
using Soul;

//死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
public class Death_State : AI_State
{
    private readonly string clip_name;
    private readonly float stopRunningTime;    
    private readonly GameObject processingBlood;
    private float time_count;
    private string KnockOffSparkPersonalEffectPath;
    Vector3 _xz;    
    bool touchedBoundary;
    bool dropped;
    
    public Death_State(float stopRunningTime, string clip_name)
    {
        this.stopRunningTime = stopRunningTime;
        this.clip_name = clip_name;
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
        _BasicPhysicSupport.SetUsingGravity(false);
        _DATA_CENTER.IsDead.Value = true;
        _DATA_CENTER.DeathInitialize();
        _Rigidbody.velocity = Vector3.zero;
        _Animator.SetFloat("speed", 0f);
        _Animator.applyRootMotion = false;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        Animation_Manger.AnimationTrigger(clip_name);
        KnockOffSparkPersonalEffectPath = newValue.effectPath;
        EffectAndHurtObjectLoading.Instance.GenerateEffect("super_hit", KnockOffSparkPersonalEffectPath,
                                             newValue.damageHappenPoint, gameObject.transform.rotation,
                                             _FightAttriCalReference.transform);
        touchedBoundary = false;
        dropped = false;
        _xz = newValue.AttackerT_foward;
        personality_Events.CloseAllPersonalityEffects();
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        time_count = 0f;
    }

    public override void _State_FixedUpdate1()
    {
        if (!touchedBoundary)
        {
            if (_BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
            {
                touchedBoundary = true;
                _xz = Vector3.zero - gameObject.transform.position;
                _xz.y = 0;
                _xz = _xz.normalized;
            }
        }
        
        if (!dropped)
        {
            if (time_count > 0.1f && _BasicPhysicSupport.hiddenMethods.Grounded)
            {
                dropped = true;
                _FightAttriCalReference.ChangeLayerForAllSelfColliders(0);
                _Rigidbody.velocity = Vector3.zero;
                time_count = 0;//开始针对躺地时间记时
                //Debug.Log(gameObject + "pos:"+ gameObject.transform.position + this._Rigidbody.useGravity);
            }else{
                gameObject.transform.position += 
                _xz * (FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_count + Time.fixedDeltaTime) - FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_count)) +
                Vector3.up * (FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_count + Time.fixedDeltaTime) - FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_count));
            }
        }
        time_count += Time.fixedDeltaTime;
        Sensor.enabled &= time_count <= stopRunningTime;
        _AIStateRunner.enabled &= time_count <= stopRunningTime + 1f;
        time_count += Time.fixedDeltaTime;
    }
}